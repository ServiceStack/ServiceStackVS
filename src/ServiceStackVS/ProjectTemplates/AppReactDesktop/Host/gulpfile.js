(function () {
    var MSBUILD_TOOLS_VERSION = getMSBuildToolsVersion();
    var SCRIPTS = {
        '00-webpack-watch': 'npm run watch',
        'webpack-build': 'npm run build',
        'webpack-build-prod': 'npm run build-prod',
        'tests-run': 'npm run test',
        'tests-watch': 'npm run test-watch',
        'www-exec-package-console': 'cmd /c "cd wwwroot_build && package-deploy-console.bat"'
    };

    var fs = require('fs');
    var path = require('path');
    var gulp = require('gulp');
    var gulpUtil = require('gulp-util');
    var exec = require('child_process').exec;
    var runSequence = require('run-sequence');
    var nugetRestore = require('gulp-nuget-restore');
    var msbuild = require('gulp-msbuild');
    var msdeploy = require('gulp-msdeploy');

    var del = require('del');
    var newer = require('gulp-newer');
    var nugetpack = require('gulp-nuget-pack');

    var webRoot = 'wwwroot/';
    var webBuildDir = './wwwroot_build/';
    var configDir = webBuildDir + 'publish/';
    var configPath = configDir + 'config.json';
    var appSettingsDir = webBuildDir + 'deploy/';
    var appSettingsPath = appSettingsDir + 'appsettings.txt';

    var resourcesLib = '../../lib/';
    var resourcesRoot = '../$safeprojectname$.Resources/';
    var winFormsAssemblyInfoPath = '../$safeprojectname$.AppWinForms/Properties/AssemblyInfo.cs';

    function createConfigsIfMissing() {
        if (!fs.existsSync(configPath)) {
            if (!fs.existsSync(configDir)) {
                fs.mkdirSync(configDir);
            }
            fs.writeFileSync(configPath, JSON.stringify({
                "iisApp": "$safeprojectname$",
                "serverAddress": "deploy-server.example.com",
                "userName": "{WebDeployUserName}",
                "password": "{WebDeployPassword}"
            }, null, 4));
        }
        if (!fs.existsSync(appSettingsPath)) {
            if (!fs.existsSync(appSettingsDir)) {
                fs.mkdirSync(appSettingsDir);
            }
            fs.writeFileSync(appSettingsPath,
                '# Release App Settings\r\nDebugMode false');
        }
    }

    createConfigsIfMissing();

    function getMSBuildToolsVersion() {
        fs = fs || require("fs");
        return fs.existsSync(process.env["PROGRAMFILES(X86)"] + "/MSBuild/15.0") ?
            15
            : fs.existsSync(process.env["PROGRAMFILES(X86)"] + "/MSBuild/14.0") ?
                14 :
                null;
    }

    function runScript(script, done) {
        process.env.FORCE_COLOR = 1;
        var proc = exec(script + (script.startsWith("npm") ? " --silent" : ""));
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', () => done());
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
        return del([ resourcesRoot + '/dist/*' ], { force: true });
    });
    gulp.task('www-copy-resources-lib', () => {
        return gulp.src('../$safeprojectname$.Resources/bin/Release/$safeprojectname$.Resources.dll')
            .pipe(newer(resourcesLib))
            .pipe(gulp.dest(resourcesLib));
    });
    gulp.task('www-copy-resources', () => {
        return gulp.src(['./wwwroot/**/*',
            '!./wwwroot/*.config',
            '!./wwwroot/*.txt',
            '!./wwwroot/*.asax',
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

    gulp.task('www-nuget-restore', () => {
        return gulp.src('../../$safeprojectname$.sln')
            .pipe(nugetRestore());
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

    function initWinformsReleaseDirectory() {
        var appsDir = webBuildDir + 'apps/';
        var winFormsInstall = appsDir + 'winforms-installer/';
        if (!fs.existsSync(appsDir)) {
            fs.mkdirSync(appsDir);
        }
        if (!fs.existsSync(winFormsInstall)) {
            fs.mkdirSync(winFormsInstall);
        }
    }

    gulp.task('www-nuget-pack-winforms', function (callback) {
        var globule = require('globule');
        initWinformsReleaseDirectory();
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
                outputDir: 'wwwroot_build/apps/winforms-installer/'
            },
            includes,
            callback);
    });

    gulp.task('www-exec-package-winforms', done => {
        initWinformsReleaseDirectory();
        var squirrelPath = path.resolve('../../packages/squirrel.windows.1.3.0/tools/');
        var appName = extractAssemblyAttribute(winFormsAssemblyInfoPath, 'AssemblyTitle');
        var version = extractAssemblyAttribute(winFormsAssemblyInfoPath, 'AssemblyVersion');
        var rootDir = 'wwwroot_build\\apps\\winforms-installer\\';
        var nugetPkg = rootDir + appName + '.' + version + '.nupkg';
        var releaseDir = rootDir + 'Releases';
        gulpUtil.log(gulpUtil.colors.green('Packaging using Squirrel: ') + gulpUtil.colors.white(nugetPkg));
        exec('Squirrel.exe --releasify ' + nugetPkg + ' --releaseDir ' + releaseDir + ' --no-msi', { env: { 'PATH': squirrelPath + ';' } }, function (err, stdout, stderr) {
            console.log(stdout);
            console.log(stderr);
            if (!err) {
                gulpUtil.log(gulpUtil.colors.green('Package created/updated at: ') + gulpUtil.colors.white(releaseDir));
            }
            done(err);
        });
    });
    
    gulp.task('www-msdeploy-pack', () => {
        return gulp.src('wwwroot/')
            .pipe(msdeploy({
                verb: 'sync',
                sourceType: 'iisApp',
                dest: {
                    'package': path.resolve('./webdeploy.zip')
                }
            }));
    });

    gulp.task('www-msdeploy-push', () => {
        var config = require(configPath);
        return gulp.src('./webdeploy.zip')
            .pipe(msdeploy({
                verb: 'sync',
                allowUntrusted: 'true',
                sourceType: 'package',
                dest: {
                    iisApp: config.iisApp,
                    wmsvc: config.serverAddress,
                    UserName: config.userName,
                    Password: config.password
                }
            }));
    });

    gulp.task('default', done => {
        runSequence('01-package-server',
            '02-package-client',
            '02-package-console',
            '02-package-winforms',
            done);
    });

    gulp.task('01-bundle-all', done => {
        runSequence(
            'www-nuget-restore',
            'www-msbuild-web',
            'webpack-build-prod',
            'www-msbuild-resources',
            'www-copy-resources-lib',
            done
        );
    });

    gulp.task('02-package-console', done => {
        runSequence(
            'www-nuget-restore',
            '01-bundle-all',
            'www-msbuild-console',
            'www-exec-package-console',
            done);
    });
    
    gulp.task('02-package-winforms', done => {
        runSequence(
            'www-nuget-restore',
            '01-bundle-all',
            'www-msbuild-winforms',
            'www-nuget-pack-winforms',
            'www-exec-package-winforms',
            done);
    });

    gulp.task('01-package-server', done => {
        runSequence('www-nuget-restore', 'www-msbuild-web', done);
    });

    gulp.task('02-package-client', done => {
        runSequence('www-clean-resources', 'webpack-build-prod', 'www-copy-resources', done);
    });

    gulp.task('03-deploy-app', done => {
        runSequence('www-msdeploy-pack', 'www-msdeploy-push', done);
    });

    gulp.task('package-and-deploy', done => {
        runSequence('01-package-server', '02-package-client', '03-deploy-app', done);
    });

})();