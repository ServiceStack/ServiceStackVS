(function () {
    var MSBUILD_TOOLS_VERSION = getMSBuildToolsVersion();
    var SCRIPTS = {
        '00-webpack-dev': 'npm run dev',
        '00-webpack-watch': 'npm run watch',
        'webpack-build': 'npm run build',
        'webpack-build-prod': 'npm run build-prod',
        'tests-run': 'npm run test',
        'tests-watch': 'npm run test-watch',
        'tests-coverage': 'npm run test-coverage',
        'update-dtos': 'npm run typescript-ref'
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
        runSequence(
            'webpack-build',
            done);
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