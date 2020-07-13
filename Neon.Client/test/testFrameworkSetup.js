/////////////////////////////////////////////////////////////////////////////////
// This script runs immediately after the jest-testing-framework is initialized.
/////////////////////////////////////////////////////////////////////////////////

// set global variable "lang"
// import lang from '../src/js/i18n/lang/lang.de.js';
// global.lang = lang;

global.console.internal = global.console.log;

global.fetch = require('jest-fetch-mock');
