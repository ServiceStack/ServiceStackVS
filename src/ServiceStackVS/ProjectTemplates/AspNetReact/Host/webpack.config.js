"use strict";

var path = require('path'),
    webpack = require('webpack');

module.exports = {
    entry: {
        app: [
            './src/app.tsx'
        ],
        vendor: [
            'es6-shim',
            'react',
            'react-dom',
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
        new webpack.optimize.CommonsChunkPlugin('vendor')
    ]
};