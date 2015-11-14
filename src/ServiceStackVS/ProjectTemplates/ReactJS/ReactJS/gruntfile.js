/// <reference path="~/node_modules/grunt/lib/grunt.js" />
/// <reference path="~/node_modules/grunt/lib/util/task.js"/>
/// <reference path="~/node_modules/gulp/index.js"/>
/// <reference path="~/node_modules/requirejs/require.js"/>

/* global module, require */
module.exports = function (grunt) {
    "use strict";

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
    var react = require('gulp-react');

    var webRoot = 'wwwroot/';
    var webBuildDir = grunt.option('serviceStackSettingsDir') || './wwwroot_build/';
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

    // Project configuration.
    grunt.initConfig({
        exec: {
            jest: 'jest'
        },
        msbuild: {
            release: {
                src: ['$safeprojectname$.csproj'],
                options: {
                    projectConfiguration: 'Release',
                    targets: ['Clean', 'Rebuild'],
                    stdout: true,
                    version: 4.0,
                    maxCpuCount: 4,
                    buildParameters: {
                        WarningLevel: 2
                    },
                    verbosity: 'quiet'
                }
            }
        },
        nugetrestore: {
            restore: {
                src: 'packages.config',
                dest: '../../packages/'
            }
        },
        msdeploy: {
            pack: {
                options: {
                    verb: 'sync',
                    source: {
                        iisApp: path.resolve('./wwwroot')
                    },
                    dest: {
                        'package': path.resolve('./webdeploy.zip')
                    }
                }
            },
            push: {
                options: {
                    verb: 'sync',
                    allowUntrusted: 'true',
                    source: {
                        'package': path.resolve('./webdeploy.zip')
                    },
                    dest: {
                        iisApp: config.iisApp,
                        wmsvc: config.serverAddress,
                        UserName: config.userName,
                        Password: config.password
                    }
                }
            }
        },
        gulp: {
            'wwwroot-clean-dlls': function (done) {
                var binPath = webRoot + '/bin/';
                del(binPath, done);
            },
            'wwwroot-copy-bin': function () {
                var binDest = webRoot + 'bin/';
                var dest = gulp.dest(binDest).on('end', function () {
                    grunt.log.ok('wwwroot-copy-bin finished.');
                });
                return gulp.src('./bin/**/*')
                    .pipe(newer(binDest))
                    .pipe(dest);
            },
            'wwwroot-copy-appdata': function () {
                return gulp.src('./App_Data/**/*')
                    .pipe(newer(webRoot + 'App_Data/'))
                    .pipe(gulp.dest(webRoot + 'App_Data/'));
            },
            'wwwroot-copy-webconfig': function () {
                return gulp.src('./web.config')
                    .pipe(newer(webRoot))
                    .pipe(gulpReplace('<compilation debug="true" targetFramework="4.5">', '<compilation targetFramework="4.5">'))
                    .pipe(gulp.dest(webRoot));
            },
            'wwwroot-copy-asax': function () {
                return gulp.src('./Global.asax')
                    .pipe(newer(webRoot))
                    .pipe(gulp.dest(webRoot));
            },
            'wwwroot-clean-client-assets': function (done) {
                del([
                    webRoot + '**/*.*',
                    '!wwwroot/bin/**/*.*', //Don't delete dlls
                    '!wwwroot/App_Data/**/*.*', //Don't delete App_Data
                    '!wwwroot/**/*.asax', //Don't delete asax
                    '!wwwroot/**/*.config', //Don't delete config
                    '!wwwroot/appsettings.txt' //Don't delete deploy settings
                ], done);
            },
            'wwwroot-copy-partials': function () {
                var partialsDest = webRoot + 'partials';
                return gulp.src('partials/**/*.html')
                    .pipe(newer(partialsDest))
                    .pipe(gulp.dest(partialsDest));
            },
            'wwwroot-copy-fonts': function () {
                return gulp.src('./bower_components/bootstrap/dist/fonts/*.*')
                    .pipe(gulp.dest(webRoot + 'lib/fonts/'));
            },
            'wwwroot-copy-images': function () {
                return gulp.src('./img/**/*')
                    .pipe(gulp.dest(webRoot + 'img/'));
            },
            'wwwroot-bundle': function () {
                var assets = useref.assets({ searchPath: './' });
                var checkIfJsx = function (file) {
                    return file.relative.indexOf('.jsx.js') !== -1;
                };
                return gulp.src('./**/*.cshtml')
                    .pipe(assets)
                    .pipe(gulpif('*.jsx.js', react()))
                    .pipe(gulpif(checkIfJsx, uglify()))
                    .pipe(gulpif('*.css', minifyCss()))
                    .pipe(assets.restore())
                    .pipe(useref())
                    .pipe(htmlBuild({
                        jqueryCdn: function (block) {
                            pipeTemplate(block, '<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>');
                        },
                        bootstrapCss: function (block) {
                            pipeTemplate(block, '<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.1/css/bootstrap.min.css" />');
                        },
                        bootstrapJs: function (block) {
                            pipeTemplate(block, '<script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.1/js/bootstrap.min.js"></script>');
                        }
                    }))
                    .pipe(gulp.dest(webRoot));
            },
            'wwwroot-bundle-html': function () {
                var assets = useref.assets({ searchPath: './' });
                var checkIfJsx = function (file) {
                    return file.relative.indexOf('.jsx.js') !== -1;
                };
                return gulp.src('./default.html')
                    .pipe(assets)
                    .pipe(gulpif('*.jsx.js', react()))
                    .pipe(gulpif(checkIfJsx, uglify()))
                    .pipe(gulpif('*.css', minifyCss()))
                    .pipe(assets.restore())
                    .pipe(useref())
                    .pipe(htmlBuild({
                        jqueryCdn: function (block) {
                            pipeTemplate(block, '<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>');
                        },
                        bootstrapCss: function (block) {
                            pipeTemplate(block, '<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.1/css/bootstrap.min.css" />');
                        },
                        bootstrapJs: function (block) {
                            pipeTemplate(block, '<script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.1/js/bootstrap.min.js"></script>');
                        }
                    }))
                    .pipe(gulp.dest(webRoot));
            },
            'wwwroot-copy-deploy-files': function () {
                return gulp.src(webBuildDir + 'deploy/*.*')
                    .pipe(newer(webRoot))
                    .pipe(gulp.dest(webRoot));
            }
        }
    });

    grunt.loadNpmTasks('grunt-exec');
    grunt.loadNpmTasks('ssvs-utils');
    grunt.loadNpmTasks('grunt-gulp');
    grunt.loadNpmTasks('grunt-msbuild');
    grunt.loadNpmTasks('grunt-nuget');

    grunt.registerTask('01-run-tests', ['exec:jest']);
    grunt.registerTask('02-package-server', [
        'nugetrestore',
        'msbuild:release',
        'gulp:wwwroot-clean-dlls',
        'gulp:wwwroot-copy-bin',
        'gulp:wwwroot-copy-appdata',
        'gulp:wwwroot-copy-webconfig',
        'gulp:wwwroot-copy-asax',
        'gulp:wwwroot-copy-deploy-files'
    ]);
    grunt.registerTask('03-package-client', [
        'gulp:wwwroot-clean-client-assets',
        'gulp:wwwroot-copy-partials',
        'gulp:wwwroot-copy-fonts',
        'gulp:wwwroot-copy-images',
        'gulp:wwwroot-bundle',
        'gulp:wwwroot-bundle-html'
    ]);

    grunt.registerTask('build', ['02-package-server', '03-package-client']);

    grunt.registerTask('04-deploy-webapp', ['msdeploy:pack', 'msdeploy:push']);

    grunt.registerTask('package-and-deploy', ['02-package-server', '03-package-client', '04-deploy-app']);

    grunt.registerTask('default', ['build']);
};