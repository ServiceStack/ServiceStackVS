/* global module, require */

module.exports = function (grunt) {
    "use strict";

    var path = require('path');
    // include gulp
    var gulp = require('gulp');
    // include plug-ins
    var del = require('del');
    var header = require('gulp-header');
    var uglify = require('gulp-uglify');
    var newer = require('gulp-newer');
    var useref = require('gulp-useref');
    var gulpif = require('gulp-if');
    var minifyCss = require('gulp-minify-css');
    var gulpReplace = require('gulp-replace');
    var react = require('gulp-react');
    var resourcesRoot = '../$safeprojectname$.Resources/';
    var webRoot = 'wwwroot/';
    var resourcesLib = '../../lib/';

    var COPY_FILES = [
        { src: './bin/**/*', dest: 'bin/', host: 'web' },
        { src: './img/**/*', dest: 'img/' },
        { src: './App_Data/**/*', dest: 'App_Data/', host: 'web' },
        { src: './Global.asax', host: 'web' },
        { src: './bower_components/bootstrap/dist/fonts/*.*', dest: 'lib/fonts/' },

        { src: './js/web.js', dest: 'js/', host: 'web' },
        { src: './wwwroot_build/deploy/*.*', host: 'web' },
        {
            src: './web.config',
            host: 'web',
            after: function () {
                return gulpReplace('<compilation debug="true" targetFramework="4.5">', '<compilation targetFramework="4.5">');
            }
        }
    ];

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
            },
            'release-resources': {
                src: ['..\\$safeprojectname$.Resources\\$safeprojectname$.Resources.csproj'],
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
            },
            'restore-resources': {
                src: '../$safeprojectname$.Resources/packages.config',
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
            'copy-files': function (done) {
                var count = 0;
                var length = COPY_FILES.length;
                var result = [];
                var processCopy = function (index) {
                    var copy = COPY_FILES[index];
                    var dest = copy.dest || '';
                    var src = copy.src;

                    var copyWeb = function () {
                        if (copy.host == null) return true;
                        return copy.host.toLowerCase().indexOf('web') > -1;
                    }
                    var copyNative = function () {
                        if (copy.host == null) return true;
                        return copy.host.toLowerCase().indexOf('native') > -1;
                    }

                    if (copy.after) {
                        var copyTaskWithAfter = gulp.src(src)
                            .pipe(copy.after())
                            .pipe(gulpif(copyWeb, newer(webRoot + dest)))
                            .pipe(gulpif(copyWeb, gulp.dest(webRoot + dest)))
                            .pipe(gulpif(copyNative, newer(resourcesRoot + dest)))
                            .pipe(gulpif(copyNative, gulp.dest(resourcesRoot + dest)));
                        copyTaskWithAfter.on('finish', function () {
                            count++;
                            grunt.log.ok('Copied ' + copy.src);
                            if (count === length) {
                                done();
                            }
                        });
                        return copyTaskWithAfter;
                    } else {
                        var copyTask = gulp.src(src)
                            .pipe(gulpif(copyWeb, newer(webRoot + dest)))
                            .pipe(gulpif(copyWeb, gulp.dest(webRoot + dest)))
                            .pipe(gulpif(copyNative, newer(resourcesRoot + dest)))
                            .pipe(gulpif(copyNative, gulp.dest(resourcesRoot + dest)));
                        copyTask.on('finish', function () {
                            grunt.log.ok('Copied ' + copy.src);
                            count++;
                            if (count === length) {
                                done();
                            }
                        });
                        return copyTask;
                    }
                }
                for (var i = 0; i < length; i++) {
                    result.push(processCopy(i));
                }
            },
            'wwwroot-clean-dlls': function (done) {
                var binPath = webRoot + '/bin/';
                del(binPath, done);
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
                    .pipe(gulpif('*.cshtml', header("@*Auto generated file on " + (new Date().toLocaleTimeString()) + ".*@\r\n")))
                    .pipe(gulp.dest(resourcesRoot))
                    .pipe(gulp.dest(webRoot));

            },
            'copy-resources-lib': function () {
                return gulp.src('../$safeprojectname$.Resources/bin/Release/$safeprojectname$.Resources.dll')
                    .pipe(newer(resourcesLib))
                    .pipe(gulp.dest(resourcesLib));
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
        'gulp:copy-files',
        'gulp:wwwroot-bundle',
        'nugetrestore:restore-resources',
        'msbuild:release-resources',
        'gulp:copy-resources-lib'
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