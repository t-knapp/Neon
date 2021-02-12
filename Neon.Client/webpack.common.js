const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TSLintPlugin = require('tslint-webpack-plugin');

module.exports = {
    mode: 'production',
    entry: {
        'app': './src/Index.tsx',
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, 'dist')
    },    
    plugins: [
        new CleanWebpackPlugin(),
        new MiniCssExtractPlugin({
            filename: '[name].css',
            chunkFilename: '[id].css'
        }),
        new HtmlWebpackPlugin({
            template: './src/index.html'
        }),
        new TSLintPlugin({
            files: ['./src/**/*.ts', './src/**/*.tsx'],
            exclude: ['./src/**/*.test.ts', './src/**/*.test.tsx']
        })
    ],
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: [{
                    loader: 'ts-loader',
                    options: {
                        configFile: 'tsconfig.webpack.json',
                        compiler: 'ttypescript'
                    }
                }],
                exclude: /(node_modules|__test__)/
            },
            {
                test: /\.js$/,
                exclude: /(node_modules|bower_components)/,
                use:[{
                    loader: 'babel-loader',
                    options: {
                        babelrc: true
                    }
                }]
            },
            {
                test: /\.js$/,
                enforce: 'pre',
                exclude: [/node_modules/, /dist/],
                use: [
                    {
                        loader: 'eslint-loader'
                    }
                ]
            },
            {
                test: /\.less$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader
                    },
                    {
                        loader: 'css-loader'
                    },
                    {
                        loader: 'less-loader',
                        options: {
                            lessOptions: {
                                strictMath: true,
                                noIeCompat: true
                            }
                        }
                    }
                ]
            },
            {
                test: /\.css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader'
                ]
            },
            {
                test: /\.(jpe?g|png|gif|svg|ico)$/i,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: 'img/'
                        }
                    }
                ]
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: 'fonts/'
                        }
                    }
                ]
            }
        ]
    },
    resolve: {
        extensions: [ '.tsx', '.ts', '.js' ],
    }
};
