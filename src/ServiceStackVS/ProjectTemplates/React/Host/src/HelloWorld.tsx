import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { JsonServiceClient } from 'servicestack-client';
import { Hello } from './dtos';

var client = new JsonServiceClient('/');

export default class HelloWorld extends React.Component<any, any> {
    constructor(props, context) {
        super(props, context);
        this.state = { msg: '' };
    }

    update(e: any) {
        var request = new Hello();
        request.name = (e.target as HTMLInputElement).value;
        client.get(request)
            .then(r => {
                this.setState({ msg: r.result });
            });
    }

    render() {
        return (
            <div className="form-group">
                <input type="text" placeholder="Your name" onChange={e => this.update(e)} className="form-control" />
                <h3 style={{ margin: 10 }}>{this.state.msg}</h3>
            </div>);
    }
}