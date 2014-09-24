'use strict';

module.exports = function (grunt) {
    var path = require('path');

    //Deployment config
    //var config = require('./deploy/config.json');

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
        //,
        //msdeploy: {
        //    pack: {
        //        options: {
        //            verb: "sync",
        //            source: {
        //                "iisApp": path.resolve("./wwwroot")
        //            },
        //            dest: {
        //                'package': path.resolve("./$safeprojectname$.zip")
        //            }
        //        }
        //    },
        //    push: {
        //        options: {
        //            verb: "sync",
        //            allowUntrusted:"true",
        //            source: {
        //                'package': path.resolve("./$safeprojectname$.zip")
        //            },
        //            dest: {
        //                iisApp: "$safeprojectname$/",
        //                wmsvc: config.serverName,
        //                UserName: config.userName,
        //                Password: config.msdeployPassword
        //            }
        //        }
        //    }
        //}
    });

    grunt.loadNpmTasks('grunt-karma');
    grunt.loadNpmTasks('grunt-xdt-config-transformation');
    grunt.loadNpmTasks('grunt-msdeploy');

    grunt.registerTask('run-karma', ['karma']);
    grunt.registerTask('webconfig-release', ['xdt_config_transformation']);

    //grunt.registerTask('package-deploy-release', ['msdeploy:pack', 'msdeploy:push']);
    grunt.registerTask('packages-deploy-release', [], function () {
        console.log("\r\n\r\nSee the example in the msdeploy task configuration above and refer to documentation at https://www.npmjs.org/package/grunt-msdeploy");
    });
};