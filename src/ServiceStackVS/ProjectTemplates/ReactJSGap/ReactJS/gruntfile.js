/* global module, require */

module.exports = function (grunt) {
    "use strict";

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
    var react = require('gulp-react');
    var resourcesRoot = '../$safeprojectname$.Resources/';
    var webRoot = 'wwwroot/';

    // Deployment config
    var config = require('./wwwroot_build/publish/config.json');

    // Project configuration.
    grunt.initConfig({
        exec: {
            'package-console': {
                command: 'cmd /c "cd wwwroot_build && package-deploy-console.bat"',
                exitCodes: [0, 1]
            },
            'package-winforms': {
                command: 'cmd /c "cd wwwroot_build && package-deploy-winforms.bat"',
                exitCodes: [0, 1]
            }
        },
        msbuild: {
            'release-console': {
                src: ['../$safeprojectname$.AppConsole/$safeprojectname$.AppConsole.csproj'],
                options: {
                    projectConfiguration: 'Release',
                    targets: ['Clean', 'Rebuild'],
                    stdout: true,
                    version: 4.0,
                    maxCpuCount: 4,
                    buildParameters: {
                        WarningLevel: 2,
                        SolutionDir: '..\\..'
                    },
                    verbosity: 'quiet'
                }
            },
            'release-winforms': {
                src: ['../$safeprojectname$.AppWinForms/$safeprojectname$.AppWinForms.csproj'],
                options: {
                    projectConfiguration: 'Release',
                    targets: ['Clean', 'Rebuild'],
                    stdout: true,
                    version: 4.0,
                    maxCpuCount: 4,
                    buildParameters: {
                        WarningLevel: 2,
                        SolutionDir: '..\\..'
                    },
                    verbosity: 'quiet'
                }
            },
            'release-webapp': {
                src: ['.\\$safeprojectname$.csproj'],
                options: {
                    projectConfiguration: 'Release',
                    targets: ['Clean', 'Rebuild'],
                    stdout: true,
                    version: 4.0,
                    maxCpuCount: 4,
                    buildParameters: {
                        WarningLevel: 2,
                        SolutionDir: '..\\..'
                    },
                    verbosity: 'quiet'
                }
            }
        },
        nugetrestore: {
            'restore-console': {
                src: '../$safeprojectname$.AppConsole/packages.config',
                dest: '../../packages/'
            },
            'restore-winforms': {
                src: '../$safeprojectname$.AppWinForms/packages.config',
                dest: '../../packages/'
            },
            'restore-webapp': {
                src: './packages.config',
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
                var partialsResourceDest = resourcesRoot + 'partials';
                var partialsWebRootDest = webRoot + 'partials';
                return gulp.src('partials/**/*.html')
                    .pipe(newer(partialsResourceDest))
                    .pipe(gulp.dest(partialsResourceDest))
                    .pipe(gulp.dest(partialsWebRootDest));
            },
            'wwwroot-copy-fonts': function () {
                return gulp.src('./bower_components/bootstrap/dist/fonts/*.*')
                    .pipe(gulp.dest(resourcesRoot + 'lib/fonts/'))
                    .pipe(gulp.dest(webRoot + 'lib/fonts/'));
            },
            'wwwroot-copy-images': function () {
                return gulp.src('./img/**/*')
                    .pipe(gulp.dest(resourcesRoot + 'img/'))
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
                    .pipe(gulp.dest(resourcesRoot))
                    .pipe(gulp.dest(webRoot));

            },
            'wwwroot-copy-deploy-files': function () {
                return gulp.src('./wwwroot_build/deploy/*.*')
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

    grunt.registerTask('01-bundle-all', [
        'gulp:wwwroot-clean-dlls',
        'gulp:wwwroot-clean-client-assets',
        'gulp:wwwroot-copy-bin',
        'gulp:wwwroot-copy-appdata',
        'gulp:wwwroot-copy-webconfig',
        'gulp:wwwroot-copy-asax',
        'gulp:wwwroot-copy-deploy-files',
        'gulp:wwwroot-copy-partials',
        'gulp:wwwroot-copy-fonts',
        'gulp:wwwroot-copy-images',
        'gulp:wwwroot-bundle'
    ]);

    grunt.registerTask('02-package-console', [
        'nugetrestore:restore-console',
        'msbuild:release-console',
        '01-bundle-all',
        'exec:package-console'
    ]);
    grunt.registerTask('03-package-winforms', [
        'nugetrestore:restore-winforms',
        'msbuild:release-winforms',
        '01-bundle-all',
        'exec:package-winforms'
    ]);
    grunt.registerTask('04-deploy-webapp', [
        'nugetrestore:restore-webapp',
        'msbuild:release-webapp',
        '01-bundle-all',
        'msdeploy:pack',
        'msdeploy:push'
    ]);

    grunt.registerTask('default', ['02-package-console', '03-package-winforms']);
};