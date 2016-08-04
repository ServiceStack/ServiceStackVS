// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='../typings/index.d.ts'/>

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import {JsonServiceClient} from 'servicestack-client';
import {Hello} from './dtos';
export default class HelloWorld extends React.Component<any, any> {
    client: JsonServiceClient;
    constructor(props, context) {
        super(props, context);
        this.state = { msg: '' };
        this.client = new JsonServiceClient('/');
    }

    update(event: any) {
        var request = new Hello();
        request.Name = event.target.value;
        this.client.get(request).then((hellResponse) => {
            this.setState({
                msg: hellResponse.Result
            });
        });
    }

    render() {
        return (
            <div className="form-group">
                <input type="text" placeholder="Your name" onChange={e => this.update(e) } className="form-control" />
                <h3>{this.state.msg}</h3>
            </div>);
    }
}