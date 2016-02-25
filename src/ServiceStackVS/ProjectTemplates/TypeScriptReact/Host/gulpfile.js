/*global require*/
(function () {
    var fs = require('fs');
    var path = require('path');
    // include gulp
    var gulp = require('gulp');
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
    var argv = require('yargs').argv;
    var nugetRestore = require('gulp-nuget-restore');
    var msbuild = require("gulp-msbuild");

    var webRoot = 'wwwroot/';
    var webBuildDir = argv.serviceStackSettingsDir || './wwwroot_build/';
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

    gulp.task('wwwroot-clean-dlls', function (done) {
        var binPath = webRoot + '/bin/';
        del(binPath, done);
    });
    gulp.task('wwwroot-copy-bin', function () {
        var binDest = webRoot + 'bin/';
        return gulp.src('./bin/**/*')
            .pipe(newer(binDest))
            .pipe(gulp.dest(binDest));
    });
    gulp.task('wwwroot-copy-appdata', function () {
        return gulp.src('./App_Data/**/*')
            .pipe(newer(webRoot + 'App_Data/'))
            .pipe(gulp.dest(webRoot + 'App_Data/'));
    });
    gulp.task('wwwroot-copy-webconfig', function () {
        return gulp.src('./web.config')
            .pipe(newer(webRoot))
            .pipe(gulpReplace('<compilation debug="true" targetFramework="4.5">', '<compilation targetFramework="4.5">'))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('wwwroot-copy-asax', function () {
        return gulp.src('./Global.asax')
            .pipe(newer(webRoot))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('wwwroot-clean-client-assets', function (done) {
        del([
            webRoot + '**/*.*',
            '!wwwroot/bin/**/*.*', //Don't delete dlls
            '!wwwroot/App_Data/**/*.*', //Don't delete App_Data
            '!wwwroot/**/*.asax', //Don't delete asax
            '!wwwroot/**/*.config', //Don't delete config
            '!wwwroot/appsettings.txt' //Don't delete deploy settings
        ], done);
    });
    gulp.task('wwwroot-copy-fonts', function () {
        return gulp.src('./jspm_packages/npm/bootstrap@3.2.0/fonts/*.*')
            .pipe(gulp.dest(webRoot + 'lib/fonts/'));
    });
    gulp.task('wwwroot-copy-images', function () {
        return gulp.src('./img/**/*')
            .pipe(gulp.dest(webRoot + 'img/'));
    });
    gulp.task('wwwroot-bundle-html', function () {
        var assets = useref.assets({ searchPath: './' });

        return gulp.src('./default.html')
            .pipe(assets)
            .pipe(gulpif('*.js', uglify()))
            .pipe(gulpif('*.css', minifyCss()))
            .pipe(assets.restore())
            .pipe(useref())
            .pipe(htmlBuild({
                bootstrapCss: function (block) {
                    pipeTemplate(block, '<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.2.0/css/bootstrap.min.css" />');
                },
                appbundle: function (block) {
                    pipeTemplate(block, '<script src="/build.js"></script>'); // file comes from 'wwwroot-jspm-build' task below
                }
            }))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('wwwroot-jspm-build', function () {
        return gulp.src('./app.js')
            .pipe(jspmBuild())
            .pipe(rename('build.js'))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('wwwroot-copy-appjs', function () {
        return gulp.src('./app.js')
        .pipe(uglify())
        .pipe(gulp.dest(webRoot));
    });
    gulp.task('wwwroot-copy-components', function () {
        return gulp.src('./components/**/*.js')
        .pipe(uglify())
        .pipe(gulp.dest(webRoot + 'components'));
    });
    gulp.task('wwwroot-copy-deploy-files', function () {
        return gulp.src(webBuildDir + 'deploy/*.*')
            .pipe(newer(webRoot))
            .pipe(gulp.dest(webRoot));
    });
    gulp.task('jspm-deps', function () {
        return gulp.src('./deps.js')
            .pipe(jspmBuild())
            .pipe(rename('deps.lib.js'))
            .pipe(gulp.dest('./'));
    });
    gulp.task('msbuild', function () {
        return gulp.src('../../$safeprojectname$.sln')
            .pipe(nugetRestore())
            .pipe(msbuild({
                targets: ['Clean', 'Build'],
                stdout: true,
                verbosity: 'quiet'
            }
            ));
    });

    gulp.task('01-package-server', function (callback) {
        runSequence('msbuild', 'wwwroot-clean-dlls',
                [
                    'wwwroot-copy-bin',
                    'wwwroot-copy-appdata',
                    'wwwroot-copy-webconfig',
                    'wwwroot-copy-asax',
                    'wwwroot-copy-deploy-files'
                ],
                callback);
    });

    gulp.task('02-package-client', function (callback) {
        runSequence('wwwroot-clean-client-assets',
                [
                    'wwwroot-copy-fonts',
                    'wwwroot-copy-images',
                    'wwwroot-bundle-html'
                ],
                'wwwroot-jspm-build',
                callback);
    });
    gulp.task('build', function (callback) {
        runSequence('01-package-server', '02-package-client',
                callback);
    });

    gulp.task('update-depsjs', function (callback) {
        runSequence('msbuild', 'jspm-deps',
                callback);
    });

    gulp.task('msdeploy-pack', function () {
        return gulp.src('wwwroot/')
            .pipe(msdeploy({
                verb: 'sync',
                sourceType: 'iisApp',
                dest: {
                    'package': path.resolve('./webdeploy.zip')
                }
            }));
    });

    gulp.task('msdeploy-push', function () {
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
        runSequence('msdeploy-pack', 'msdeploy-push',
            callback);
    });

    gulp.task('package-and-deploy', function (callback) {
        runSequence('01-package-server', '02-package-client', '03-deploy-app',
            callback);
    });
    gulp.task('default', function (callback) {
        runSequence('build', callback);
    });
})();