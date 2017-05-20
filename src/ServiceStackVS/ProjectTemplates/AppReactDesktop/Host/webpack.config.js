"use strict";

var COPY_FILES = [
    { from: 'bin/**/*',                  to: 'wwwroot' },
    { from: 'App_Data/**/*',             to: 'wwwroot' },
    { from: 'Global.asax',               to: 'wwwroot' },
    { from: 'wwwroot_build/deploy/**/*', to: 'wwwroot', flatten: true },
    { from: 'platform.*',                to: 'wwwroot' },
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
var isProd = ENV === 'build-prod';
var isDev = !isProd;

var packageConfig = require("./package.json"),
    path = require('path'),
    webpack = require('webpack'),
    ExtractTextPlugin = require('extract-text-webpack-plugin'),
    HtmlWebpackPlugin = require('html-webpack-plugin'),
    CopyWebpackPlugin = require('copy-webpack-plugin'),
    Clean = require('clean-webpack-plugin');

var postcssLoader = { 
    loader: 'postcss-loader',
    options: { plugins: [ require('precss'), require('autoprefixer') ] }
};

module.exports = {
    entry: {
        app: [
            './src/app.tsx'
        ],
        vendor: [
            'es6-shim',
            'classnames',
            'react',
            'react-dom',
            'react-router-dom',
            'servicestack-client'
        ]
    },

    output: {
        path: isProd ? root('wwwroot/dist') : root('dist'),
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
                loader: 'awesome-typescript-loader'
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
                    use: [ 'style-loader', 'css-loader', postcssLoader ]
                },
                {
                    test: /\.(sass|scss)$/,
                    use: [ 'style-loader', 'css-loader', postcssLoader, 'sass-loader' ] 
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

    externals: {
        "window": "window"
    },

    plugins: [
        new webpack.DefinePlugin({
            'process.env': {
                'NODE_ENV': JSON.stringify(isProd ? 'production' : 'development')
            }
        }),
        new Clean([isProd ? root('wwwroot/*') : root('dist')]),
        new webpack.optimize.CommonsChunkPlugin({
            name: 'vendor',
            filename: 'vendor.bundle.js'
        }),
        new HtmlWebpackPlugin({
            template: 'index.template.ejs',
            filename: '../index.html',
            inject: true
        }),
        ...when(isProd, [
            new ExtractTextPlugin({ filename: '[name].css', allChunks: true }),            
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
