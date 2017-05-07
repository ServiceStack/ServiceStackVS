(function () {
    var MSBUILD_TOOLS_VERSION = getMSBuildToolsVersion();

    var argv = require('yargs').argv;
    var fs = require('fs');
    var path = require('path');
    var gulp = require('gulp');
    var gulpUtil = require('gulp-util');
    var exec = require('child_process').exec;
    var runSequence = require('run-sequence');
    var nugetRestore = require('gulp-nuget-restore');
    var msbuild = require('gulp-msbuild');
    var msdeploy = require('gulp-msdeploy');
    var webpack = require('webpack');

    var webRoot = 'wwwroot/';
    var webBuildDir = './wwwroot_build/';
    var configDir = webBuildDir + 'publish/';
    var configPath = configDir + 'config.json';
    var appSettingsDir = webBuildDir + 'deploy/';
    var appSettingsPath = appSettingsDir + 'appsettings.txt';

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

    // Tasks
    gulp.task('www-postinstall', done => {
        runSequence(
            'webpack-build',
            done);
    });
    gulp.task('webpack-build-prod', done => {
        process.env.npm_lifecycle_event = 'build:prod';
        var config = require('./webpack.config.js');
        webpack(config).run((err, stats) => {
            if (err) {
                gulpUtil.log('Error', err);
            } else {
                Object.keys(stats.compilation.assets).forEach(key => {
                    gulpUtil.log('Webpack: output ', gulpUtil.colors.green(key));
                });
                gulpUtil.log('Webpack: ', gulpUtil.colors.blue('finished ', stats.compilation.name));
            }
            done();
        });
    });
    gulp.task('www-msbuild', () => {
        return gulp.src('../../$safeprojectname$.sln')
            .pipe(nugetRestore())
            .pipe(msbuild({
                toolsVersion: MSBUILD_TOOLS_VERSION,
                targets: ['Clean', 'Build'],
                properties: {
                    Configuration: 'Release'
                },
                stdout: true,
                verbosity: 'quiet'
            }));
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
        var proc = exec('npm run test-watch');
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', done);
    });

    gulp.task('default', done => {
        runSequence('01-package-server', '02-package-client', done);
    });

    gulp.task('01-package-server', done => {
        runSequence('www-msbuild', done);
    });

    gulp.task('02-package-client', done => {
        runSequence('webpack-build-prod', done);
    });

    gulp.task('03-deploy-app', done => {
        runSequence('www-msdeploy-pack', 'www-msdeploy-push', done);
    });

    gulp.task('package-and-deploy', done => {
        runSequence('01-package-server', '02-package-client', '03-deploy-app', done);
    });

})();