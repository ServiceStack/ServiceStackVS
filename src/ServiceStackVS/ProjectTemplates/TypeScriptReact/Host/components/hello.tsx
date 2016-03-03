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
        this.state = { yourName: '' };
    }

    update(event: any) {
        event.preventDefault();
        var self = this;
        var yourName = event.target.value;
        if (yourName == null || yourName.length === 0) {
            self.setState({ yourName: '' });
        } else {
            $.ajax({
                url: 'hello/' + yourName,
                dataType: 'json',
                type: 'GET',
                success(response) {
                    self.setState({ yourName: response.Result });
                }
            });
        }
    }

    handleChange(event:any) {
        this.setState({ yourName: event.target.value });
    }

    render() {
        return <div className="form-group">
                <input type="text" placeholder="Your name" value={this.state.value} onChange={this.update.bind(this)} className="form-control" />
                <h3>{this.state.yourName}</h3>
            </div>;
    }
}