var path = require('path');
var webpack = require('webpack');
var autoprefixer = require('autoprefixer');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var EncodingPlugin = require('webpack-encoding-plugin');

var CommonsChunkPlugin = require("webpack/lib/optimize/CommonsChunkPlugin");

var getPath = function (pathToFile) { return path.resolve(__dirname, pathToFile); }

var ENV = process.env.npm_lifecycle_event;
console.log(ENV);


var isProd = ENV.indexOf('dev') === -1;

var isTest = ENV === 'build-test';

var ppath = isProd ? '/dtm/' : 'http://localhost:8082/';
//if (isTest)
//    ppath = '/dtm.view/';

module.exports = (function makeWebpackConfig() {

    var config = {};

    config.entry = {
        'app': './src/app/index.ts',
        'vendor': './src/app/vendor.ts'
    };

    config.output = {
        path: getPath('./dist'),
        filename: isProd ? '[name].[hash].js' : '[name].bundle.js',
        chunkFilename: isProd ? '[name].[hash].js' : '[name].bundle.js',
        publicPath: ppath
    };

    config.devtool = 'source-map';
    config.resolve = {
        extensions: ['', '.webpack.js', '.web.js', '.ts', '.js']
    };
    config.module = {
        loaders: [
            {
                // TS LOADER
                test: /\.ts$/,
                loader: 'ng-annotate!awesome-typescript-loader',
                include: [
                    getPath("src/app")
                ]
            },
            {
                // JS LOADER
                test: /\.js$/,
                loader: 'babel',
                include: [
                    getPath("src/app")
                ]
            },
            {
                // SCSS LOADER - generates a separate CSS file, and adds the link to <head>
                test: /\.scss$/,
                loader: ExtractTextPlugin.extract('style', 'css?sourceMap=true!postcss!sass?outputStyle=expanded&sourceMap=true&sourceMapContents=true')
            },
            //awesome font
            {
                test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                loader: "url-loader?limit=10000&mimetype=application/font-woff"
            },
            {
                test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                loader: "file-loader"
            },
            /**
             * To keep CSS bundled in with the generated JS, uncomment this section
             */
            // {
            //     test: /\.scss$/,
            //     loader: 'style!css!sass',
            // },
            {
                // ASSET LOADER
                test: /\.(png|jpe?g|gif|svg|woff|woff2|ttf|eot|ico)$/,
                loader: 'file?name=assets/[name].[hash].[ext]'
            },
            {
                // templateCaches LOADER
                test: /\.html$/,
                loader: 'ng-cache-loader?prefix=[dir]/[dir]!raw',
                include: getPath("src/app/astum/templates")
            },
            //{
            //    test: /\.html$/,
            //    loader: 'ngtemplate?relativeTo=' + getPath('') + '/'
            //},
            {
                // HTML LOADER
                test: /\.html$/,
                loader: 'raw'
            },
            {
                include: /\.json$/, loaders: ["json-loader"]
            }



        ]
    };

    config.postcss = [
        autoprefixer({
            browsers: ['last 2 versions']
        })
    ];


    config.plugins = [
        new CommonsChunkPlugin({
            names: ['vendor'],
            minChunks: Infinity
        }),

        new HtmlWebpackPlugin({
            template: getPath('./src/index.html'),
            inject: 'body'
        }),
        new ExtractTextPlugin("[name].css", { allChunks: true }),
        new webpack.ProvidePlugin({
            'window.jQuery': 'jquery'
        }),
        new webpack.DefinePlugin({
            'process.env': {
                "ENV": JSON.stringify(ENV)
            }
        })
    ];

    if (isProd) {
        config.plugins.push(
            // Create separate CSS file
            new ExtractTextPlugin('app.css'),

            // Dedupe modificators in the output
            new webpack.optimize.DedupePlugin(),

            // Minifiy all JS, switch loaders to minimizing mode
            new webpack.optimize.UglifyJsPlugin()
        );
    }


    config.devServer = {
        port: '8082',
        contentBase: './src'
    }

    return config;
})();