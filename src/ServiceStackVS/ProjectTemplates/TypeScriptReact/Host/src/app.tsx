// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// http://www.typescriptlang.org/docs/handbook/jsx.html

import "es6-shim";
import * as ReactDOM from 'react-dom';
import * as React from 'react';
import HelloWorld from './HelloWorld';

ReactDOM.render(<HelloWorld />, document.getElementById("content"));
