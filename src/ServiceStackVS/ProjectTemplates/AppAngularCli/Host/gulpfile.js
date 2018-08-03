
(function () {
    var SCRIPTS = {
        'dev': 'npm run dev',
        'start': 'npm run start',
        'dtos': 'npm run dtos',
        'build': 'npm run build',
        'publish': 'npm run publish',
        'lint': 'npm run lint',
        'tests': 'npm run test',
        'tests-e2e': 'npm run e2e'
    };

    var gulp = require('gulp');
    var exec = require('child_process').exec;

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

})();
