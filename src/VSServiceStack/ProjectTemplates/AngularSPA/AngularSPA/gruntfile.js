'use strict';

module.exports = function (grunt) {
    var path = require('path');
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
        },
        msdeploy: {
            pack: {
                options: {
                    verb: "sync",
                    source: {
                        "dirPath": path.resolve(".\\wwwroot")
                    },
                    dest: {
                        "package": path.resolve(".\\$safeprojectname$.zip")
                    }
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-karma');
    grunt.loadNpmTasks('grunt-xdt-config-transformation');
    grunt.loadNpmTasks('grunt-msdeploy');

    grunt.registerTask('run-karma', ['karma']);
    grunt.registerTask('webconfig-release', ['xdt_config_transformation']);
    grunt.registerTask('package-release', ['msdeploy']);
};