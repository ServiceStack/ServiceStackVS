"use strict";

var COPY_FILES = [
    { from: 'bin/**/*',                  to: 'wwwroot' },
    { from: 'App_Data/**/*',             to: 'wwwroot' },
    { from: 'Global.asax',               to: 'wwwroot' },
    { from: 'wwwroot_build/deploy/**/*', to: 'wwwroot', flatten: true },
    { from: 'Web.config',                to: 'wwwroot',
      transform: (content, path) => toString(content).replace(
          '<compilation debug="true"',
          '<compilation'
      )
    }
];

COPY_FILES.forEach(x => {
    x.from = root(x.from);
    x.to = root(x.to);
});

var ENV = process.env.npm_lifecycle_event; // npm script
var isTestWatch = ENV === 'test-watch';
var isTest = ENV === 'test' || isTestWatch;
var isProd = ENV === 'build-prod';
var isDev = !isTest && !isProd;
var NONE = function () { return {} };

var packageConfig = require("./package.json"),
    path = require('path'),
    webpack = require('webpack'),
    ExtractTextPlugin = require('extract-text-webpack-plugin'),
    HtmlWebpackPlugin = require('html-webpack-plugin'),
    CopyWebpackPlugin = require('copy-webpack-plugin'),
    Clean = require('clean-webpack-plugin');

var postcssLoader = {
    loader: 'postcss-loader',
    options: { plugins: [require('precss'), require('autoprefixer')] }
};

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
        path: isProd ? root('wwwroot/dist') : root('dist'),
        publicPath: '/dist/',
        filename: isProd ? '[name].[chunkhash].bundle.js' : '[name].bundle.js',
        chunkFilename: isProd ? '[name].[chunkhash].js' : '[name].js',
    },

    devServer: {
        port: 3000,
        historyApiFallback: true,
        inline: true,
        proxy: {
            "/": packageConfig["karma"]["globals"]["BaseUrl"]
        }
    },

    devtool: isProd ? "source-map" : "inline-source-map",

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
                test: /\.html$/,
                loader: 'html-loader'
            },
            {
                enforce: "pre",
                test: /\.js$/,
                loader: "source-map-loader"
            },
            {
                test: /\.(jpe?g|gif|png|ico|svg|wav|mp3)$/i,
                loader: 'file-loader' + (isProd ? '?hash=sha512&digest=hex&name=img/[name].[hash].[ext]' : '?name=img/[name].[ext]')
            },
            {
                test: /\.(eot|ttf|woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
                loader: isProd ? 'url-loader?limit=10000&name=img/[name].[hash].[ext]' : 'file-loader?name=img/[name].[ext]'
            },
            {
                test: /\.(css|sass|scss)$/,
                include: root('src', 'modules'),
                use: ['raw-loader', 'sass-loader'],
            },
            {
                test: /\.(css|sass|scss)$/,
                exclude: root('src', 'modules'),
                loader: ExtractTextPlugin.extract({
                    fallback: 'raw-loader',
                    use: ['css-loader' + (isProd ? '?minimize' : ''), postcssLoader, 'sass-loader']
                })
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
                'NODE_ENV': JSON.stringify(isProd ? 'production' : isTest ? 'testing' : 'development')
            }
        }),
        ...when(!isTest, [
            // Workaround needed for angular 2 angular/angular#11580
            new webpack.ContextReplacementPlugin(
                // The (\\|\/) piece accounts for path separators in *nix and Windows
                /angular(\\|\/)core(\\|\/)@angular/,
                root('src') // location of your src
            ),
            new Clean([isProd ? root('wwwroot/*') : root('dist')]),
            new webpack.optimize.CommonsChunkPlugin({
                name: 'vendor',
                filename: isProd ? 'vendor.[chunkhash].bundle.js' : 'vendor.bundle.js'
            }),
            new HtmlWebpackPlugin({
                template: 'index.template.ejs',
                filename: '../index.html',
                inject: true
            }),
        ]),
        ...when(isDev, [
            new ExtractTextPlugin({ filename: '[name].css', allChunks: true }),
        ]),
        ...when(isProd, [
            new ExtractTextPlugin({ filename: '[name].[chunkhash].css', allChunks: true }),
            new webpack.optimize.UglifyJsPlugin({ sourceMap: true }),
            new CopyWebpackPlugin(COPY_FILES)
        ]),
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
    return (path || (path = require("path"))).join.apply(path, [__dirname].concat(args));
}
function toString(buf) {
    return typeof buf !== "string" //strip (UTF-8 BOM) EFBBBF
        ? String.fromCharCode.apply(null, new Uint16Array(
            (buf[0] === 0xEF && buf[1] === 0xBB && buf[2] === 0xBF) ? buf.slice(3) : buf))
        : buf;
}
