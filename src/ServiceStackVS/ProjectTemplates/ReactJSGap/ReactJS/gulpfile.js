/*global require*/
(function () {
    var WEB = 'web';
    var NATIVE = 'native';

    var argv = require('yargs').argv;
    var webBuildDir = argv.serviceStackSettingsDir || './wwwroot_build/';

    var MSBUILD_TOOLS_VERSION = getMSBuildToolsVersion();
    var COPY_FILES = [
        { src: './bin/**/*', dest: 'bin/', host: WEB },
        { src: './img/**/*', dest: 'img/' },
        { src: './node_modules/font-awesome/fonts/*', dest: 'fonts/' },
        { src: './App_Data/**/*', dest: 'App_Data/', host: WEB },
        { src: './Global.asax', host: WEB },
        { src: ['./platform.js', './platform.css'], host: WEB },
        { src: webBuildDir + 'deploy/*.*', host: WEB },
        {
            src: './web.config',
            host: [WEB],
            afterReplace: [{
                from: '<compilation debug="true" targetFramework="4.5"',
                to: '<compilation targetFramework="4.5"'
            }]
        }
    ];

    var fs = require('fs');
    var del = require('del');
    var path = require('path');
    var gulp = require('gulp');
    var gulpUtil = require('gulp-util');
    var uglify = require('gulp-uglify');
    var newer = require('gulp-newer');
    var useref = require('gulp-useref');
    var gulpif = require('gulp-if');
    var minifyCss = require('gulp-clean-css');
    var gulpReplace = require('gulp-replace');
    var htmlBuild = require('gulp-htmlbuild');
    var eventStream = require('event-stream');
    var exec = require('child_process').exec;
    var rename = require('gulp-rename');
    var runSequence = require('run-sequence');
    var nugetRestore = require('gulp-nuget-restore');
    var msbuild = require('gulp-msbuild');
    var msdeploy = require('gulp-msdeploy');
    var webpack = require('webpack');

    var nugetpack = require('gulp-nuget-pack');

    var webRoot = 'wwwroot/';
    var resourcesLib = '../../lib/';
    var configFile = 'config.json';
    var configDir = webBuildDir + 'publish/';
    var configPath = configDir + configFile;
    var appSettingsFile = 'appsettings.txt';
    var appSettingsDir = webBuildDir + 'deploy/';
    var appSettingsPath = appSettingsDir + appSettingsFile;

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
    var config = require(configPath); // Deployment config

    var webpackConfig = require('./webpack.config.js');

    // htmlbuild replace for CDN references
    function pipeTemplate(block, template) {
        eventStream.readArray([
            template
        ].map(function (str) {
            return block.indent + str;
        })).pipe(block);
    }

    function copyFilesTask(copy, cb) {
        var dest = copy.dest || '';
        var src = copy.src;
        var copyTask = gulp.src(src);
        if (copy.afterReplace) {
            for (var i = 0; i < copy.afterReplace.length; i++) {
                var replace = copy.afterReplace[i];
                copyTask = copyTask.pipe(gulpReplace(replace.from, replace.to));
            }
        }
        if (copy.after) {
            copyTask = copyTask.pipe(copy.after());
        }

        var hosts = [WEB, NATIVE];
        if (copy.host) {
            hosts = typeof copy.host === 'string'
                ? [copy.host]
                : copy.host;
        }

        if (hosts.indexOf(WEB) >= 0) {
            copyTask = copyTask
                .pipe(newer(webRoot + dest))
                .pipe(gulp.dest(webRoot + dest));
        }
        if (hosts.indexOf(NATIVE) >= 0) {
            copyTask = copyTask
                .pipe(newer(resourcesRoot + dest))
                .pipe(gulp.dest(resourcesRoot + dest));
        }

        copyTask.on('finish', function () {
            gulpUtil.log(gulpUtil.colors.green('Copied ' + copy.src));
            cb();
        });
        return copyTask;
    }

    function getMSBuildToolsVersion() {
        fs = fs || require("fs");
        return fs.existsSync(process.env["PROGRAMFILES(X86)"] + "/MSBuild/15.0") ?
            15
            : fs.existsSync(process.env["PROGRAMFILES(X86)"] + "/MSBuild/14.0") ?
                14 :
                null;
    }

    // Tasks
    gulp.task('www-postinstall', done => {
        runSequence('www-copy-files',
            'www-bundle-html',
            'webpack-build',
            'webpack-build-prod',
            done);
    });
    gulp.task('www-clean-server', () => {
        var binPath = webRoot + '/bin/';
        return del(binPath);
    });
    gulp.task('www-clean-client', () => {
        return del([
            webRoot + '**/*.*',
            '!wwwroot/bin/**/*.*', //Don't delete dlls
            '!wwwroot/App_Data/**/*.*', //Don't delete App_Data
            '!wwwroot/**/*.asax', //Don't delete asax
            '!wwwroot/**/*.config', //Don't delete config
            '!wwwroot/appsettings.txt' //Don't delete deploy settings
        ]);
    });
    gulp.task('www-copy-files', done => {
        var completed = 0;

        for (var i = 0; i < COPY_FILES.length; i++) {
            (function (index) {
                copyFilesTask(COPY_FILES[index], function () {
                    if (++completed === COPY_FILES.length)
                        done();
                });
            })(i);
        }
    });
    gulp.task('www-bundle-html', () => {
        return gulp.src('./default.html')
            .pipe(useref())
            .pipe(gulpif('*.js', uglify()))
            .pipe(gulpif('*.css', minifyCss()))
            .pipe(gulp.dest(webRoot))
            .pipe(gulp.dest(resourcesRoot));
    });
    gulp.task('webpack-build-prod', done => {
        var config = Object.create(webpackConfig);
        config.output.path = __dirname + "/wwwroot/dist";
        config.plugins = (config.plugins || []).concat(
            new webpack.DefinePlugin({
                'process.env': {
                    'NODE_ENV': JSON.stringify('production')
                }
            }),
            new webpack.optimize.DedupePlugin(),
            new webpack.optimize.UglifyJsPlugin()
        );
        webpack(config).run((err, stats) => {
            if (err) {
                gulpUtil.log('Error', err);
                done();
            } else {
                Object.keys(stats.compilation.assets).forEach(key => {
                    gulpUtil.log('Webpack: output ', gulpUtil.colors.green(key));
                });
                gulpUtil.log('Webpack: ', gulpUtil.colors.blue('finished ', stats.compilation.name));

                gulp.src(__dirname + "/wwwroot/dist/" + config.output.filename)
                    .pipe(gulp.dest(resourcesRoot + "dist/"))
                    .on('end', done);
            }
        });
    });
    gulp.task('www-copy-resources-lib', () => {
        return gulp.src('../$safeprojectname$.Resources/bin/Release/$safeprojectname$.Resources.dll')
            .pipe(newer(resourcesLib))
            .pipe(gulp.dest(resourcesLib));
    })
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

    gulp.task('www-exec-package-console', done => {
        var proc = exec('cmd /c "cd wwwroot_build && package-deploy-console.bat"');
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', done);
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

    gulp.task('webpack-build', done => {
        process.env.FORCE_COLOR = 1;
        var proc = exec('npm run build');
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', done);
    });

    gulp.task('00-webpack-watch', done => {
        process.env.FORCE_COLOR = 1;
        var proc = exec('npm run watch');
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', done);
    });

    gulp.task('tests-run', done => {
        process.env.FORCE_COLOR = 1;
        var proc = exec('npm test');
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', done);
    });

    gulp.task('tests-watch', done => {
        process.env.FORCE_COLOR = 1;
        var proc = exec('npm test -- --watch');
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', done);
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
            'www-clean-server',
            'www-clean-client',
            'www-nuget-restore',
            'www-msbuild-web',
            'www-copy-files',
            'webpack-build-prod',
            'www-bundle-html',
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
        runSequence('www-nuget-restore',
            'www-msbuild-web', 'www-clean-server',
            [
                'www-copy-files',
                'www-copy-deploy-files'
            ],
            done);
    });

    gulp.task('02-package-client', done => {
        runSequence('www-clean-client',
            [
                'www-copy-files',
                'www-bundle-html'
            ],
            'webpack-build-prod',
            done);
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

    gulp.task('03-deploy-app', done => {
        runSequence('www-msdeploy-pack', 'www-msdeploy-push',
            done);
    });

    gulp.task('package-and-deploy', done => {
        runSequence('01-package-server', '02-package-client', '03-deploy-app',
            done);
    });
})();