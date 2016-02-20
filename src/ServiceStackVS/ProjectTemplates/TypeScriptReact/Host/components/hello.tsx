// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path="../typings/browser.d.ts" />

import * as React from 'react';
import * as ReactDOM from 'react-dom';
export default class HelloWorld extends React.Component<any, any> {
    render() {
        return <div>Hello, World!!!</div>;
    }
}