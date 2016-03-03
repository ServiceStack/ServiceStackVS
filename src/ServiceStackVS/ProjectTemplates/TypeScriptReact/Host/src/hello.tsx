// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='../typings/browser.d.ts'/>

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import 'jquery';
export default class HelloWorld extends React.Component<any, any> {
    constructor(props, context) {
        super(props, context);
        this.state = { msg: '' };
    }

    update(event: any) {
        var yourName = event.target.value;
        $.getJSON(`hello/${yourName}`, (r) => {
		   this.setState({ msg: r.Result });
		});
    }

    render() {
        return (
            <div className="form-group">
                <input type="text" placeholder="Your name" onChange={e => this.update(e)} className="form-control" />
                <h3>{this.state.msg}</h3>
            </div>);
    }
}