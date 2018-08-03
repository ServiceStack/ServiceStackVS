// https://github.com/facebook/create-react-app/issues/1070#issuecomment-314696847
process.env.NODE_ENV = 'development';

const fs = require('fs-extra');
const paths = require('react-scripts-ts/config/paths');

const path = require('path');
const appDirectory = fs.realpathSync(process.cwd());
const resolveApp = relativePath => path.resolve(appDirectory, relativePath);

const webpack = require('webpack');
const config = require('react-scripts-ts/config/webpack.config.dev.js');

// remove react-dev-utils/webpackHotDevClient.js
config.entry.splice(config.entry.findIndex(
    e => e.indexOf('webpackHotDevClient.js') >= 0), 1);

config.output.path =  resolveApp('wwwroot');

webpack(config).watch({}, (err, stats) => {
  if (err) {
    console.error(err);
  } else {
    //copyPublicFolder();
  }
  console.error(stats.toString({
    chunks: false,
    colors: true
  }));
});

function copyPublicFolder() {
  fs.copySync(paths.appPublic, paths.appBuild, {
    dereference: true,
    filter: file => file !== paths.appHtml
  });
}