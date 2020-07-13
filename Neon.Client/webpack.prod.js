const merge = require('webpack-merge');
const commonBuild = require('./webpack.common.build.js');

module.exports = merge(commonBuild, {
    mode: 'production'
});