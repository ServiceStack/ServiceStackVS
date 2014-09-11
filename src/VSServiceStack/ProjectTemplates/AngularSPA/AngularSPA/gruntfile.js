'use strict';

module.exports = function (grunt) {
    // Project configuration.
    grunt.initConfig({
        karma: {
            unit: {
                configFile: 'tests/karma.conf.js',
                singleRun: true,
                browsers: ['PhantomJS'],
                logLevel: 'ERROR'
            }
        },
        xdt_config_transformation: {
            release: {
                options: {
                    source: 'web.config',
                    transform: 'web.Release.config',
                    destination: 'wwwroot/web.config'
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-karma');
    grunt.loadNpmTasks('grunt-xdt-config-transformation');

    grunt.registerTask('run-karma', ['karma']);
    grunt.registerTask('webconfig-release', ['xdt_config_transformation']);
};