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
    var htmlBuild = require('gulp-htmlbuild');
    var eventStream = require('event-stream');
    var react = require('gulp-react');
    var webRoot = '../$safeprojectname$.Resources/';

    // Project configuration.
    grunt.initConfig({
        exec: {
            jest: 'jest'
        },
        gulp: {
            'wwwroot-copy-webconfig': function () {
                return gulp.src('./web.config')
                    .pipe(newer(webRoot))
                    .pipe(gulpReplace('<compilation debug="true" targetFramework="4.5">', '<compilation targetFramework="4.5">'))
                    .pipe(gulp.dest(webRoot));
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
                    .pipe(gulp.dest(webRoot));
            },
            //Static index.html bundling
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
    grunt.loadNpmTasks('grunt-gulp');

    grunt.registerTask('01-run-tests', ['exec:jest']);
    grunt.registerTask('02-package-winforms', [
        'gulp:wwwroot-copy-partials',
        'gulp:wwwroot-copy-fonts',
        'gulp:wwwroot-copy-images',
        'gulp:wwwroot-bundle',
        'gulp:wwwroot-bundle-html'
    ]);

    grunt.registerTask('build', ['02-package-winforms']);

    grunt.registerTask('default', ['build']);
};