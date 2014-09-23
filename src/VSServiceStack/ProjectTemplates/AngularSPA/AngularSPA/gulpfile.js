// include gulp
var gulp = require('gulp');
// include plug-ins
var jshint = require('gulp-jshint');
var rimraf = require('gulp-rimraf');
var uglify = require('gulp-uglify');
var runSequence = require('run-sequence');
var useref = require('gulp-useref');
var gulpif = require('gulp-if');
var minifyCss = require('gulp-minify-css');

var webRoot = 'wwwroot/';
//grunt tasks via gulp, eg transform web.Release.config
require('gulp-grunt')(gulp);


// build tasks
gulp.task('build_Release', function () {
    return runSequence(
        '-build_Release_clean',
        '-build_Release_webconfig',
        '-build_Release_bin',
        '-build_Release_asax',
        '-build_Release_copy_partials',
        '-build_Release_html');
});

// Sub tasks are prefixed with '-' to avoid showing up in MS Task Runner Explorer extension
gulp.task('-build_Release_webconfig', function () {
    return gulp.start('grunt-webconfig-release');
});

gulp.task('-build_Release_bin', function () {
    return gulp.src('./bin/**/*')
        .pipe(gulp.dest(webRoot + 'bin/'));
});

gulp.task('-build_Release_asax', function () {
    return gulp.src('./Global.asax')
        .pipe(gulp.dest(webRoot));
});

gulp.task('-build_Release_clean', function () {
    return gulp.src(webRoot, { read: false })
      .pipe(rimraf());
});

gulp.task('-build_Release_copy_partials', function () {
    return gulp.src('partials/**/*.html')
        .pipe(gulp.dest(webRoot + 'partials'));
});

gulp.task('-build_Release_html', function () {
    var assets = useref.assets();

    return gulp.src('index.html')
        .pipe(assets)
        .pipe(gulpif('*.js', uglify()))
        .pipe(gulpif('*.css', minifyCss()))
        .pipe(assets.restore())
        .pipe(useref())
        .pipe(gulp.dest(webRoot));
});