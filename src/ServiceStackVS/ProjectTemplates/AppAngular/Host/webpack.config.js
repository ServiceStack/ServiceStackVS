var path = require('path');
var webpack = require('webpack');

// Webpack Plugins
var CommonsChunkPlugin = webpack.optimize.CommonsChunkPlugin;
var autoprefixer = require('autoprefixer');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

var ENV = process.env.npm_lifecycle_event; //npm script
var isTestWatch = ENV === 'test-watch';
var isTest = ENV === 'test' || isTestWatch;
var NONE = function(){return {}};

module.exports = {
  entry: isTest ? NONE : {
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
    path: root('dist'),
    publicPath: '/dist/',
    filename: '[name].bundle.js'
  },

  devtool: isTest ? "inline-source-map" : "source-map",

  resolve: {
    extensions: ['.ts', '.js', '.json', '.css', '.scss', '.html'],
  },

  module: {
    rules: [
      {
        test: /\.ts$/,
        loaders: [
          'awesome-typescript-loader' + (isTest ? "?inlineSourceMap=true&sourceMap=false" : ""), 
          'angular2-template-loader', 
          '@angularclass/hmr-loader'
        ],
        exclude: [isTest ? /\.(e2e)\.ts$/ : /\.(spec|e2e)\.ts$/, /node_modules\/(?!(ng2-.+))/]
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
      },
      ...when(isTest, {
        test: /\.ts$/,
        enforce: 'post',
        include: root('src'),
        loader: 'istanbul-instrumenter-loader',
        exclude: [/\.spec\.ts$/, /\.e2e\.ts$/, /node_modules/]
      })
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
      root('src') // location of your src
    ),
    ...when(!isTest, new CommonsChunkPlugin({
      name: ['vendor', 'polyfills']
    }))
  ]
};

//helpers
function ensureArray(config) {
  return config && (Array.isArray(config) ? config : [config]) || [];
}
function when(condition, config, negativeConfig) {
  return condition ? ensureArray(config) : ensureArray(negativeConfig);
}
function root(args) {
  args = Array.prototype.slice.call(arguments, 0);
  return path.join.apply(path, [__dirname].concat(args));
}
