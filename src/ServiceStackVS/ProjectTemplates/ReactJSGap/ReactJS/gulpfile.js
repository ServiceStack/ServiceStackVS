﻿/*global require*/
(function () {
    var argv = require('yargs').argv;
    var WEB = 'web';
    var NATIVE = 'native';

    var webBuildDir = argv.serviceStackSettingsDir || './wwwroot_build/';

    var COPY_FILES = [
        { src: './bin/**/*', dest: 'bin/', host: WEB },
        { src: './img/**/*', dest: 'img/' },
        { src: './App_Data/**/*', dest: 'App_Data/', host: WEB },
        { src: './Global.asax', host: WEB },
        { src: './jspm_packages/npm/bootstrap@3.2.0/dist/fonts/*.*', dest: 'lib/fonts/' },
        { src: './platform.*', dest: '/', host: WEB },
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
    var path = require('path');
    // include gulp
    var gulp = require('gulp');
    var gulpUtil = require('gulp-util');
    // include plug-ins
    var del = require('del');
    var uglify = require('gulp-uglify');
    var newer = require('gulp-newer');
    var useref = require('gulp-useref');
    var gulpif = require('gulp-if');
    var minifyCss = require('gulp-minify-css');
    var gulpReplace = require('gulp-replace');
    var htmlBuild = require('gulp-htmlbuild');
    var eventStream = require('event-stream');
    var jspmBuild = require('gulp-jspm');
    var rename = require('gulp-rename');
    var runSequence = require('run-sequence');
    var nugetRestore = require('gulp-nuget-restore');
    var msbuild = require('gulp-msbuild');
    var msdeploy = require('gulp-msdeploy');
    var exec = require('child_process').exec;

    var resourcesRoot = '../$safeprojectname$.Resources/';
    var webRoot = 'wwwroot/';
    var resourcesLib = '../../lib/';
    var configFile = 'config.json';
    var configDir = webBuildDir + 'publish/';
    var configPath = configDir + configFile;
    var appSettingsFile = 'appsettings.txt';
    var appSettingsDir = webBuildDir + 'deploy/';
    var appSettingsPath = appSettingsDir + appSettingsFile;

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

    // Deployment config
    createConfigsIfMissing();
    var config = require(configPath);

    // htmlbuild replace for CDN references
    function pipeTemplate(block, template) {
        eventStream.readArray([
            template
        ].map(function (str) {
            return block.indent + str;
        })).pipe(block);
    }

    // Tasks

    gulp.task('www-clean-dlls', function (done) {
        var binPath = webRoot + '/bin/';
        del(binPath, done);
    });
    gulp.task('www-copy-bin', function () {
        var binDest = webRoot + 'bin/';
        return gulp.src('./bin/**/*')
            .pipe(newer(binDest))
            .pipe(gulp.dest(binDest));
    });
    gulp.task('www-copy-appdata', function () {
        return gulp.src('./App_Data/**/*')
            .pipe(newer(webRoot + 'App_Data/'))
            .pipe(gulp.dest(webRoot + 'App_Data/'));
    });
    gulp.task('www-copy-webconfig', function () {
        return gulp.src('./web.config')
            .pipe(newer(webRoot))
            .pipe(gulpReplace('<compilation debug="true" targetFramework="4.5">', '<compilation targetFramework="4.5">'))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('www-copy-asax', function () {
        return gulp.src('./Global.asax')
            .pipe(newer(webRoot))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('www-clean-client-assets', function (done) {
        del([
            webRoot + '**/*.*',
            '!wwwroot/bin/**/*.*', //Don't delete dlls
            '!wwwroot/App_Data/**/*.*', //Don't delete App_Data
            '!wwwroot/**/*.asax', //Don't delete asax
            '!wwwroot/**/*.config', //Don't delete config
            '!wwwroot/appsettings.txt' //Don't delete deploy settings
        ], done);
    });
    gulp.task('www-copy-fonts', function () {
        return gulp.src('./jspm_packages/npm/bootstrap@3.2.0/fonts/*.*')
            .pipe(gulp.dest(webRoot + 'lib/fonts/'));
    });
    gulp.task('www-copy-files', function(done) {
        var count = 0;
        var length = COPY_FILES.length;
        var result = [];
        var processCopy = function (index) {
            var copy = COPY_FILES[index];
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
                hosts = typeof copy.host == 'string'
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
                count++;
                if (count === length) {
                    done();
                }
            });
            return copyTask;
        }
        for (var i = 0; i < length; i++) {
            result.push(processCopy(i));
        }
    });
    gulp.task('www-copy-images', function () {
        return gulp.src('./img/**/*')
            .pipe(gulp.dest(webRoot + 'img/'));
    });
    gulp.task('www-copy-jspm-config', function () {
        return gulp.src('./config.js')
            .pipe(gulp.dest(webRoot))
            .pipe(gulp.dest(resourcesRoot));
    });
    gulp.task('www-bundle-html', function (done) {
        return gulp.src('./default.html')
            .pipe(gulpif('*.css', minifyCss()))
            .pipe(useref())
            .pipe(htmlBuild({
                bootstrapCss: function(block) {
                    pipeTemplate(block, '<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.2.0/css/bootstrap.min.css" />');
                },
                appbundle: function(block) {
                    pipeTemplate(block, '<script src="app.js"></script>'); // file generated by 'www-jspm-build' task below
                }
            }))
            .pipe(gulp.dest(webRoot))
            .pipe(gulp.dest(resourcesRoot));
    });
    gulp.task('www-jspm-build', function () {
        return gulp.src('./src/app.js')
            .pipe(jspmBuild())
			.pipe(rename('app.js'))
            .pipe(gulp.dest(webRoot))
            .pipe(gulp.dest(resourcesRoot));
    });
    gulp.task('www-copy-resources-lib', function() {
        return gulp.src('../$safeprojectname$.Resources/bin/Release/$safeprojectname$.Resources.dll')
            .pipe(newer(resourcesLib))
            .pipe(gulp.dest(resourcesLib));
    })
    gulp.task('www-copy-deploy-files', function () {
        return gulp.src(webBuildDir + 'deploy/*.*')
            .pipe(newer(webRoot))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('www-jspm-deps', function () {
        return gulp.src('./src/deps.js')
            .pipe(jspmBuild())
            .pipe(rename('deps.lib.js'))
            .pipe(gulp.dest('./'));
    });

    gulp.task('www-msbuild', function() {
        return gulp.src('../../$safeprojectname$.sln')
            .pipe(msbuild({
                targets: ['Clean', 'Rebuild'],
                properties: {
                    Configuration: 'Release'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
    });
    gulp.task('www-msbuild-resources', function () {
        return gulp.src('../$safeprojectname$.Resources/$safeprojectname$.Resources.csproj')
            .pipe(msbuild({
                targets: ['Clean', 'Rebuild'],
                properties: {
                    Configuration: 'Release'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
    });
    gulp.task('www-msbuild-console', function () {
        return gulp.src('../$safeprojectname$.AppConsole/$safeprojectname$.AppConsole.csproj')
            .pipe(msbuild({
                targets: ['Clean', 'Rebuild'],
                properties: {
                    Configuration: 'Release'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
    });
    gulp.task('www-msbuild-winforms', function () {
        return gulp.src('../$safeprojectname$.AppWinforms/$safeprojectname$.AppWinforms.csproj')
            .pipe(msbuild({
                targets: ['Clean', 'Rebuild'],
                properties: {
                    Configuration: 'Release',
                    Platform: 'x86'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
    });

    gulp.task('www-nuget-restore', function() {
        return gulp.src('../../$safeprojectname$.sln')
            .pipe(nugetRestore());
    });

    gulp.task('exec-package-console', function(cb) {
        exec('cmd /c "cd wwwroot_build && package-deploy-console.bat', function(err, stdout, stderr) {
            console.log(stdout);
            console.log(stderr);
            cb(err);
        });
    });

    gulp.task('01-bundle-all', function(callback) {
        runSequence(
            'www-clean-dlls',
            'www-clean-client-assets',
            'www-copy-fonts',
            'www-copy-images',
			'www-copy-jspm-config',
            'www-copy-files',
            'www-jspm-build',
            'www-bundle-html',
            'www-nuget-restore',
            'www-msbuild-resources',
            'www-copy-resources-lib',
            callback
        );
    });

    gulp.task('02-package-console', function(cb) {
        runSequence(
            'www-nuget-restore',
            'www-msbuild-console',
            '01-bundle-all',
            'exec-package-console');
    });

    gulp.task('01-package-server', function (callback) {
        runSequence('www-nuget-restore',
            'www-msbuild', 'www-clean-dlls',
                [
                    'www-copy-bin',
                    'www-copy-appdata',
                    'www-copy-webconfig',
                    'www-copy-asax',
                    'www-copy-deploy-files'
                ],
                callback);
    });

    gulp.task('02-package-client', function (callback) {
        runSequence('www-clean-client-assets',
                [
                    'www-copy-fonts',
                    'www-copy-images',
					'www-copy-jspm-config',
                    'www-bundle-html'
                ],
                'www-jspm-build',
                callback);
    });

    gulp.task('00-update-deps-js', function (callback) {
        runSequence('www-nuget-restore',
            'www-msbuild', 'www-jspm-deps',
                callback);
    });

    gulp.task('www-msdeploy-pack', function () {
        return gulp.src('wwwroot/')
            .pipe(msdeploy({
                verb: 'sync',
                sourceType: 'iisApp',
                dest: {
                    'package': path.resolve('./webdeploy.zip')
                }
            }));
    });

    gulp.task('www-msdeploy-push', function () {
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

    gulp.task('03-deploy-app', function (callback) {
        runSequence('www-msdeploy-pack', 'www-msdeploy-push',
            callback);
    });

    gulp.task('package-and-deploy', function (callback) {
        runSequence('01-package-server', '02-package-client', '03-deploy-app',
            callback);
    });
    gulp.task('default', function (callback) {
        runSequence('01-package-server', '02-package-client',
                callback);
    });
})();