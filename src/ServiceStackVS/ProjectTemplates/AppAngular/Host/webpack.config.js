// Helper: root() is defined at the bottom
var path = require('path');
var webpack = require('webpack');

// Webpack Plugins
var CommonsChunkPlugin = webpack.optimize.CommonsChunkPlugin;
var autoprefixer = require('autoprefixer');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

/**
 * Env
 * Get npm lifecycle event to identify the environment
 */
var ENV = process.env.npm_lifecycle_event;
var isTestWatch = ENV === 'test-watch';
var isTest = ENV === 'test' || isTestWatch;
var isProd = ENV === 'build';

module.exports = {
  entry: {
    'app': [
        './src/main.ts'
    ],
    'vendor': [
        '@angular/platform-browser',
        '@angular/platform-browser-dynamic',
        '@angular/core',
        '@angular/common',
        '@angular/http',
        '@angular/router',
        'rxjs',
        '@angularclass/hmr',
        'servicestack-client'
    ],
    'polyfills': [
        'core-js/client/shim',
        'reflect-metadata',
        'ts-helpers',
        'zone.js'
    ]
  },
  
  output: {
    path: path.join(__dirname, 'dist'),
    publicPath: '/dist/',
    filename: '[name].bundle.js'
  },

  devtool: "source-map",

  resolve: {
    extensions: ['.ts', '.js', '.json', '.css', '.scss', '.html'],
  },

  module: {
    rules: [
      {
        test: /\.ts$/,
        loaders: ['awesome-typescript-loader', 'angular2-template-loader', '@angularclass/hmr-loader'],
        exclude: [/\.(spec|e2e)\.ts$/, /node_modules\/(?!(ng2-.+))/]
      },
      {
        test: /\.json$/, 
        loader: 'json-loader'
      },
      {
        test: /\.css$/,
        include: root('src'),
        loader: 'raw-loader'
      },
      {
        test: /\.scss$/,
        include: root('src'),
        loaders: ['raw-loader', 'sass-loader']
      },
      {
        test: /\.html$/, 
        loader: 'raw-loader'
      }
    ]
  },

  plugins: [
    new webpack.DefinePlugin({
      'process.env': {
        ENV: JSON.stringify(ENV)
      }
    }),
    // Workaround needed for angular 2 angular/angular#11580
    new webpack.ContextReplacementPlugin(
      // The (\\|\/) piece accounts for path separators in *nix and Windows
      /angular(\\|\/)core(\\|\/)@angular/,
      root('./src') // location of your src
    ),
    new CommonsChunkPlugin({
      name: ['vendor', 'polyfills']
    }),
  ]
};

function root(args) {
  args = Array.prototype.slice.call(arguments, 0);
  return path.join.apply(path, [__dirname].concat(args));
}
