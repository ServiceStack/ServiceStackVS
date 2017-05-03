/*global require*/
(function () {
    var argv = require('yargs').argv;
    var webBuildDir = argv.serviceStackSettingsDir || './wwwroot_build/';

    var MSBUILD_TOOLS_VERSION = getMSBuildToolsVersion();
    var COPY_SERVER_FILES = [
        { src: './bin/**/*', dest: 'bin/' },
        { src: './App_Data/**/*', dest: 'App_Data/' },
        { src: './Global.asax' },
        { src: webBuildDir + 'deploy/*.*' },
        {
            src: './web.config',
            afterReplace: [{
                from: '<compilation debug="true" targetFramework="4.5"',
                to: '<compilation targetFramework="4.5"'
            }]
        }
    ];

    var COPY_CLIENT_FILES = [
        { src: './img/**/*', dest: 'img/' },
        { src: './node_modules/font-awesome/fonts/*', dest: 'fonts/' }
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

    var webRoot = 'wwwroot/';
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

        copyTask = copyTask
            .pipe(newer(webRoot + dest))
            .pipe(gulp.dest(webRoot + dest));

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
        runSequence(
            'webpack-build',
            done);
    });
    gulp.task('www-copy-server', done => {
        var completed = 0;
        var COPY_FILES = COPY_SERVER_FILES;

        for (var i = 0; i < COPY_FILES.length; i++) {
            (function (index) {
                copyFilesTask(COPY_FILES[index], function () {
                    if (++completed === COPY_FILES.length)
                        done();
                });
            })(i);
        }
    });
    gulp.task('www-copy-client', done => {
        var completed = 0;
        var COPY_FILES = COPY_CLIENT_FILES;

        for (var i = 0; i < COPY_FILES.length; i++) {
            (function (index) {
                copyFilesTask(COPY_FILES[index], function () {
                    if (++completed === COPY_FILES.length)
                        done();
                });
            })(i);
        }
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
    gulp.task('www-bundle-html', () => {
        return gulp.src('./default.html')
            .pipe(useref())
            .pipe(gulpif('*.js', uglify()))
            .pipe(gulpif('*.css', minifyCss()))
            .pipe(gulp.dest(webRoot));
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
        var proc = exec('npm test -- --watch');
        proc.stdout.pipe(process.stdout);
        proc.stderr.pipe(process.stderr);
        proc.on('exit', done);
    });

    gulp.task('default', done => {
        runSequence('01-package-server', '02-package-client',
            done);
    });

    gulp.task('01-package-server', done => {
        runSequence('www-msbuild', 'www-clean-server',
            [
                'www-copy-server'
            ],
            done);
    });

    gulp.task('02-package-client', done => {
        runSequence('www-clean-client',
            [
                'www-copy-client',
                'www-bundle-html'
            ],
            'webpack-build-prod',
            done);
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