Error.stackTraceLimit = Infinity;
import Vue from 'vue';

Vue.config.productionTip = false

// require all test files (files that ends with .spec.ts)
var testsContext = require.context('./src', true, /\.spec\.ts/);
testsContext.keys().forEach(testsContext);

// require all src files except main.ts for coverage.
// you can also change this to match only the subset of files that
// you want coverage for.
var srcContext = require.context('./src', true, /^\.\/(?!main(\.ts)?$)/);
srcContext.keys().forEach(srcContext);
