// include gulp
var gulp = require('gulp');

// include plug-ins
var jshint = require('gulp-jshint');

// JS hint task
gulp.task('jshint', function () {
    gulp.src('./js/*.js')
      .pipe(jshint())
      .pipe(jshint.reporter('default'));
});