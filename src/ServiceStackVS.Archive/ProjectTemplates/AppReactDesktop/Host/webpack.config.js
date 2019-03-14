"use strict";

module.exports = (env) => {

    const isProd = env && env.prod;
    const isDev = !isProd;
    const npmScript = process.env.npm_lifecycle_event;
    const isDevServer = npmScript === 'dev';

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

        entry: {
            app: [
                './src/app.tsx'
            ]
        },

        output: {
            path: root('wwwroot/dist'),
            publicPath: '/dist/',
            filename: '[name].bundle.js',
            chunkFilename: '[name].js',
        },

        devServer: {
            port: 3000,
            historyApiFallback: true,
            inline: true,
            proxy: {
                "/": packageConfig["jest"]["globals"]["BaseUrl"]
            }
        },

        devtool: isProd ? "source-map" : "inline-source-map",

        resolve: {
            extensions: [
                '.webpack.js',
                '.web.js',
                '.js',
                '.jsx',
                '.ts',
                '.tsx'
            ]
        },

        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    loader: 'awesome-typescript-loader?silent=true'
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
                    loader: 'file-loader?name=img/[name].[ext]'
                },
                {
                    test: /\.(eot|ttf|woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
                    loader: 'file-loader?name=img/[name].[ext]'
                },
                ...when(isDev, [
                    {
                        test: /\.css$/,
                        use: ['style-loader', 'css-loader', postcssLoader]
                    },
                    {
                        test: /\.(sass|scss)$/,
                        use: ['style-loader', 'css-loader', postcssLoader, 'sass-loader']
                    },
                ]),
                ...when(isProd, [
                    {
                        test: /\.css$/,
                        loader: ExtractTextPlugin.extract({
                            fallback: 'style-loader',
                            use: ['css-loader?minimize', postcssLoader],
                        }),
                    },
                    {
                        test: /\.(sass|scss)$/,
                        loader: ExtractTextPlugin.extract({
                            fallback: 'style-loader',
                            use: ['css-loader?minimize', postcssLoader, 'sass-loader'],
                        }),
                    }
                ])
            ]
        },

        plugins: [
            new CheckerPlugin(),
            new webpack.DefinePlugin({ 'process.env.NODE_ENV': isDev ? '"development"' : '"production"' }),
            new webpack.WatchIgnorePlugin([root("wwwroot")]),
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
            new AddAssetHtmlPlugin({ filepath: root('wwwroot/dist/*.dll.css'), typeOfAsset: 'css' }),
            ...when(isProd, [
                new ExtractTextPlugin({ filename: '[name].css', allChunks: true }),
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
