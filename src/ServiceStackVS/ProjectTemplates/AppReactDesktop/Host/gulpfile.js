(function () {
    var MSBUILD_TOOLS_VERSION = getMSBuildToolsVersion();
    var SCRIPTS = {
        'dev': 'npm run dev',
        'webpack-build': 'npm run build',
        'webpack-build-prod': 'npm run build-prod',
        'webpack-build-vendor': 'npm run build-vendor',
        'tests-run': 'npm run test',
        'tests-watch': 'npm run test-watch',
        'tests-coverage': 'npm run test-coverage',
        'dtos-update': 'npm run dtos-update'
    };

    var fs = require('fs');
    var path = require('path');
    var gulp = require('gulp');
    var gulpUtil = require('gulp-util');
    var exec = require('child_process').exec;
    var runSequence = require('run-sequence');
    var msbuild = require('gulp-msbuild');
    
    var del = require('del');
    var newer = require('gulp-newer');
    var nugetpack = require('gulp-nuget-pack');

    var webRoot = 'wwwroot/';
    var webBuildDir = './wwwroot_build/';
    var winformsInstallerDir = webBuildDir + 'winforms-installer/';

    var resourcesLib = '../../lib/';
    var resourcesDir = '../$safeprojectname$.Resources/';
    var winFormsAssemblyInfoPath = '../$safeprojectname$.AppWinForms/Properties/AssemblyInfo.cs';
    var consoleDir = '../$safeprojectname$.AppConsole/bin/Release/';
    var consoleExeName = '$safeprojectname$-console.exe';

    function getMSBuildToolsVersion() {
        fs = fs || require("fs");
        return fs.existsSync(process.env["PROGRAMFILES(X86)"] + "/MSBuild/15.0") ?
            15
            : fs.existsSync(process.env["PROGRAMFILES(X86)"] + "/MSBuild/14.0") ?
                14 :
                null;
    }

    function resolvePackagesDir() {
        var check = ['../packages', '../../packages', '../../../packages'];
        for (var i = 0; i < check.length; i++) {
            if (fs.existsSync(check[i]))
                return check[i];
        }
        throw new Error("Could not find NuGet packages folder");
    }

    function runScript(script, done) {
        process.env.FORCE_COLOR = 1;
        var proc = exec(script + (script.startsWith("npm") ? " --silent" : ""));
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', err => done(err));
    }

    // Tasks
    Object.keys(SCRIPTS).forEach(name => {
        gulp.task(name, done => runScript(SCRIPTS[name], done));
    });
    gulp.task('www-postinstall', done => {
        runSequence('www-clean-resources',
            'webpack-build',
            'webpack-build-prod',
            'www-copy-resources',
            done);
    });
    gulp.task('www-clean-resources', () => {
        return del([ resourcesDir + '/dist/*' ], { force: true });
    });
    gulp.task('www-copy-resources-lib', () => {
        return gulp.src('../$safeprojectname$.Resources/bin/Release/$safeprojectname$.Resources.dll')
            .pipe(newer(resourcesLib))
            .pipe(gulp.dest(resourcesLib));
    });
    gulp.task('www-copy-resources', () => {
        return gulp.src(['./wwwroot/**/*',
            '!./wwwroot/platform.*'])
            .pipe(gulp.dest('../$safeprojectname$.Resources/'));
    });
    gulp.task('www-copy-deploy-files', () => {
        return gulp.src(webBuildDir + 'deploy/*.*')
            .pipe(newer(webRoot))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('www-msbuild-web', () => {
        return gulp.src('./$safeprojectname$.csproj')
            .pipe(msbuild({
                toolsVersion: MSBUILD_TOOLS_VERSION,                
                targets: ['Clean', 'Rebuild'],
                properties: {
                    Configuration: 'Release'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
    });
    gulp.task('www-msbuild-resources', () => {
        return gulp.src('../$safeprojectname$.Resources/$safeprojectname$.Resources.csproj')
            .pipe(msbuild({
                toolsVersion: MSBUILD_TOOLS_VERSION,                
                targets: ['Clean', 'Rebuild'],
                properties: {
                    Configuration: 'Release'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
    });
    gulp.task('www-msbuild-console', () => {
        return gulp.src('../$safeprojectname$.AppConsole/$safeprojectname$.AppConsole.csproj')
            .pipe(msbuild({
                toolsVersion: MSBUILD_TOOLS_VERSION,                
                targets: ['Clean', 'Rebuild'],
                properties: {
                    Configuration: 'Release'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
    });
    gulp.task('www-msbuild-winforms', () => {
        return gulp.src('../$safeprojectname$.AppWinforms/$safeprojectname$.AppWinforms.csproj')
            .pipe(msbuild({
                toolsVersion: MSBUILD_TOOLS_VERSION,                
                targets: ['Clean', 'Rebuild'],
                properties: {
                    Configuration: 'Release'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
    });

    function extractAssemblyAttribute(filePath, attrName) {
        var assemblyInfoContents = fs.readFileSync(filePath, { encoding: 'utf8' });
        var lines = assemblyInfoContents.split('\n');
        var result = null;
        for (var i = 0; i < lines.length; i++) {
            if(lines[i].startsWith('//'))
                continue;
            var line = lines[i].trim();
            if (line.startsWith('[assembly: ') && line.indexOf(attrName) > 0) {
                var startIndex = line.indexOf('(') + 1;
                var endIndex = line.indexOf(')');
                if (line.indexOf('("') > -1) {
                    //String value
                    startIndex = line.indexOf('("') + 2;
                    endIndex = line.indexOf('")');
                }
                result = line.substr(startIndex, endIndex - startIndex);
                break;
            }
        }
        return result;
    }

    function mkdirp(dirPath) {
        var sep = '/';
        var initDir = path.isAbsolute(dirPath) ? sep : '';
        dirPath.split(sep).reduce(function (parentDir, childDir) {
            var curDir = path.resolve(parentDir, childDir);
            if (!fs.existsSync(curDir)) {
                fs.mkdirSync(curDir);
            }
            return curDir;
        }, initDir);
    };

    gulp.task('www-nuget-pack-winforms', function (callback) {
        var globule = require('globule');
        mkdirp(winformsInstallerDir);
        var version = extractAssemblyAttribute(winFormsAssemblyInfoPath, 'AssemblyVersion');
        var title = extractAssemblyAttribute(winFormsAssemblyInfoPath, 'AssemblyTitle');
        var description = extractAssemblyAttribute(winFormsAssemblyInfoPath, 'AssemblyDescription') || 'Test';
        var authors = extractAssemblyAttribute(winFormsAssemblyInfoPath, 'AssemblyCompany');
        var excludes = globule.find([
            '../$safeprojectname$.AppWinForms/bin/x86/Release/*.pdb',
            '../$safeprojectname$.AppWinForms/bin/x86/Release/*.vshost.*'
        ]);
        var includes = [
        { src: '../$safeprojectname$.AppWinForms/bin/x86/Release/*.*', dest: '/lib/net45/' },
        { src: '../$safeprojectname$.AppWinForms/bin/x86/Release/locales/*.*', dest: '/lib/net45/locales/' }];
        return nugetpack({
            id: title,
            title: title,
                version: version,
                authors: authors,
                description: description,
                excludes: excludes,
                iconUrl: 'https://raw.githubusercontent.com/ServiceStack/Assets/master/img/artwork/logo-100sq.png',
                outputDir: winformsInstallerDir
            },
            includes,
            callback);
    });

    gulp.task('www-exec-package-winforms', done => {
        mkdirp(winformsInstallerDir);
        var squirrelExe = path.resolve(resolvePackagesDir() + '/squirrel.windows.1.7.8/tools/Squirrel.exe');
        var appName = extractAssemblyAttribute(winFormsAssemblyInfoPath, 'AssemblyTitle');
        var version = extractAssemblyAttribute(winFormsAssemblyInfoPath, 'AssemblyVersion');
        var nugetPkg = winformsInstallerDir + appName + '.' + version + '.nupkg';
        var releaseDir = winformsInstallerDir + 'Releases';
        gulpUtil.log(gulpUtil.colors.green('Packaging using Squirrel: ') + gulpUtil.colors.white(nugetPkg));

        runScript(squirrelExe + ' --releasify ' + nugetPkg + ' --releaseDir ' + releaseDir + ' --no-msi', err => {
            if (!err) {
                gulpUtil.log(gulpUtil.colors.green('Package created/updated at: ') + gulpUtil.colors.white(path.resolve(releaseDir)));
            }
            done(err);
        });
    });

    gulp.task('www-exec-package-console', done => {
        mkdirp(webBuildDir);
        var ilmergeExe = path.resolve(resolvePackagesDir() + '/ILRepack.2.0.13/tools/ILRepack.exe');
        var files = fs.readdirSync(consoleDir);
        var exeName = null;
        var dlls = [];
        files.forEach(file => {
            if (file.endsWith('.exe'))
                exeName = file;
            else if (file.endsWith('.dll'))
                dlls.push(file);
        });

        var outputPath = webBuildDir + consoleExeName;
        del(outputPath);
        dlls.unshift(exeName);
        var inputs = dlls.map(dll => path.resolve(consoleDir + dll)).join(' ');

        var cmd = `${ilmergeExe} /target:exe /targetplatform:v4,"C:\\Program Files (x86)\\Reference Assemblies\\Microsoft\\Framework\\.NETFramework\\v4.5" /lib:${path.resolve(consoleDir)} /out:"${path.resolve(outputPath)}" /ndebug ${inputs}`;
        runScript(cmd, err => {
            if (!err) {
                gulpUtil.log(gulpUtil.colors.green('Package created/updated at: ') + gulpUtil.colors.white(path.resolve(webBuildDir)));
            }
            done(err);
        });
    });

    gulp.task('www-npm-publish', done => runScript('npm run publish', done));

    gulp.task('publish', done => {
        runSequence('www-clean-resources', 'www-msbuild-web', 'www-npm-publish', 'www-copy-resources', 'www-msbuild-resources', 'www-copy-resources-lib', done);
    });

    gulp.task('publish-console', done => {
        runSequence(
            'www-msbuild-web',
            'www-msbuild-console',
            'www-exec-package-console',
            done);
    });
    
    gulp.task('publish-winforms', done => {
        runSequence(
            'www-msbuild-web',
            'www-msbuild-winforms',
            'www-nuget-pack-winforms',
            'www-exec-package-winforms',
            done);
    });

})();
