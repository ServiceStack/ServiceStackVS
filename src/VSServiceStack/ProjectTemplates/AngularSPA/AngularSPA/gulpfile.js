// include gulp
var gulp = require('gulp');
// include plug-ins
var rimraf = require('gulp-rimraf');
var uglify = require('gulp-uglify');
var runSequence = require('run-sequence');
var newer = require('gulp-newer');
var useref = require('gulp-useref');
var gulpif = require('gulp-if');
var minifyCss = require('gulp-minify-css');

var webRoot = 'wwwroot/';
//grunt tasks via gulp, eg transform web.Release.config
require('gulp-grunt')(gulp);


// build tasks
gulp.task('build_Release', function () {
    return runSequence(
        'build_Release_clean', //Clean wwwroot output directory before new build. Completes before dependent tasks are streamed async
        [
            //grunt-* tasks are accessible due to 'gulp-grunt'
            'grunt-xdt_config_transformation', //web.config Release transformation
            'build_Release_bin', //copy bin folder to wwwroot output
            'build_Release_asax', //copy Global.asax to wwwroot output
            'build_Release_copy_partials', //copy partials
            'build_Release_html' //Crawl html for assets and minify/replace references
        ]);
});

gulp.task('build_Release_bin', function () {
    var binDest = webRoot + 'bin/';
    return gulp.src('./bin/**/*')
        .pipe(newer(binDest))
        .pipe(gulp.dest(binDest));
});

gulp.task('build_Release_asax', function () {
    return gulp.src('./Global.asax')
        .pipe(newer(webRoot))
        .pipe(gulp.dest(webRoot));
});

gulp.task('build_Release_clean', function () {
    return gulp.src(webRoot, { read: false })
      .pipe(rimraf());
});

gulp.task('build_Release_copy_partials', function () {
    var partialsDest = webRoot + 'partials';
    return gulp.src('partials/**/*.html')
        .pipe(newer(partialsDest))
        .pipe(gulp.dest(partialsDest));
});

gulp.task('build_Release_copy_fonts', function () {
    return gulp.src('./bower_components/bootstrap/dist/fonts/*.*')
        .pipe(gulp.dest(webRoot + 'lib/fonts/'));
});

gulp.task('build_Release_html', function () {
    var assets = useref.assets();

    return gulp.src('index.html')
        .pipe(assets)
        .pipe(gulpif('*.js', uglify()))
        .pipe(gulpif('*.css', minifyCss()))
        .pipe(assets.restore())
        .pipe(useref())
        .pipe(gulp.dest(webRoot));
});

//Build task alias
gulp.task('build-app', ['build_Release']);