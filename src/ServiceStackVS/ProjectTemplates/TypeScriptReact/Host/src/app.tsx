// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='../typings/browser.d.ts'/>

import * as ReactDOM from 'react-dom';
import * as React from 'react';
import HelloWorld from './hello';

ReactDOM.render(<HelloWorld/>, document.getElementById("content"));