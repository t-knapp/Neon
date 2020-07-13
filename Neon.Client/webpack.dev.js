const merge = require('webpack-merge');
const commonBuild = require('./webpack.common.build.js');

module.exports = merge(commonBuild, {
    mode: 'development',
    devtool: 'eval-source-map'
});