"use strict";

module.exports = (env) => {

    const isProd = env && env.prod;
    const isDev = !isProd;
    const npmScript = process.env.npm_lifecycle_event;
    const isDevServer = npmScript === 'dev';
    const isTest = npmScript === 'test' || npmScript == 'test-watch';
    const NONE = function () { return {} };

    const packageConfig = require("./package.json"),
          path = require('path'),
          webpack = require('webpack'),
          ExtractTextPlugin = require('extract-text-webpack-plugin'),
          HtmlWebpackPlugin = require('html-webpack-plugin'),
          AddAssetHtmlPlugin = require('add-asset-html-webpack-plugin'),
          CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin,
          Clean = require('clean-webpack-plugin');

    const postcssLoader = {
        loader: 'postcss-loader',
        options: { plugins: [require('precss'), require('autoprefixer')] }
    };
    
    return [{
        entry: isTest ? NONE : {
            'app': [
                './src/main.ts'
            ]
        },
    
        output: {
            path: root('wwwroot/dist'),
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
            extensions: ['.webpack.js', '.web.js', '.ts', '.js', '.json', '.css', '.scss', '.html'],
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
            new webpack.WatchIgnorePlugin([root("wwwroot")]),
            ...when(!isTest, [
                // Workaround needed for angular 2 angular/angular#11580
                new webpack.ContextReplacementPlugin(
                    // The (\\|\/) piece accounts for path separators in *nix and Windows
                    /angular(\\|\/)core(\\|\/)@angular/,
                    root('src') // location of your src
                ),
                new webpack.DllReferencePlugin({
                    context: __dirname,
                    manifest: require('./wwwroot/dist/vendor-manifest.json')
                }),
                new HtmlWebpackPlugin({
                    template: 'index.template.ejs',
                    filename: '../index.html',
                    inject: true
                }),
                new AddAssetHtmlPlugin({ filepath: root('wwwroot/dist/*.dll.js') }),
                new AddAssetHtmlPlugin({ filepath: root('wwwroot/dist/*.dll.css'), typeOfAsset: 'css' })
            ]),
            ...when(isDev, [
                new ExtractTextPlugin({ filename: '[name].css', allChunks: true }),
            ]),
            ...when(isProd, [
                new ExtractTextPlugin({ filename: '[name].[chunkhash].css', allChunks: true }),
                new webpack.optimize.UglifyJsPlugin({ sourceMap: true })
            ]),
        ]
    }];

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
};
