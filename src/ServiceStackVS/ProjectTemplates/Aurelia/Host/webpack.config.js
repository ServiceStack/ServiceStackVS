var path = require('path'),
    webpack = require('webpack'),
    AureliaWebPackPlugin = require('aurelia-webpack-plugin');

module.exports = {
    entry: {
        app: [
            './src/main.ts'
        ],
        vendor: [
            'aurelia-animator-css',
            'aurelia-bootstrapper-webpack',
            'aurelia-event-aggregator',
            'aurelia-framework',
            'aurelia-history-browser',
            'aurelia-logging-console',
            'aurelia-pal-browser',
            'aurelia-polyfills',
            'aurelia-router',
            'aurelia-templating-binding',
            'aurelia-templating-router',
            'aurelia-templating-resources',
            'es6-shim',
            'servicestack-client'
        ]
    },

    output: {
        path: path.join(__dirname, 'dist'),
        publicPath: '/dist/',
        filename: '[name].bundle.js'
    },

    devtool: "source-map",

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
                loader: 'awesome-typescript-loader'
            },
            {
                test: /\.html$/,
                loader: 'html-loader'
            },
            {
                enforce: "pre",
                test: /\.js$/, loader: "source-map-loader"
            }
        ]
    },

    plugins: [
        new AureliaWebPackPlugin(),
        new webpack.optimize.CommonsChunkPlugin('vendor')
    ]
};