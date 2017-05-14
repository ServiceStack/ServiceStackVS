Error.stackTraceLimit = Infinity;

require('core-js/client/shim');
require('reflect-metadata');

require('ts-helpers');

require('zone.js/dist/zone');
require('zone.js/dist/long-stack-trace-zone');
require('zone.js/dist/proxy');
require('zone.js/dist/sync-test');
require('zone.js/dist/jasmine-patch');
require('zone.js/dist/async-test');
require('zone.js/dist/fake-async-test');

var config = require('./package.json');

for (var k in config.karma.globals) {
    global[k] = config.karma.globals[k];
}

// require all test files (files that ends with .spec.ts)
var testsContext = require.context('./src', true, /\.spec\.ts/);
testsContext.keys().forEach(testsContext);

// Select BrowserDomAdapter.
// see https://github.com/AngularClass/angular2-webpack-starter/issues/124
// Somewhere in the test setup
var testing = require('@angular/core/testing');
var browser = require('@angular/platform-browser-dynamic/testing');

testing.TestBed.initTestEnvironment(browser.BrowserDynamicTestingModule, browser.platformBrowserDynamicTesting());
