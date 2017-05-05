import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { client } from './shared';
import { Hello } from './dtos';

export default class HelloWorld extends React.Component<any, any> {
    constructor(props, context) {
        super(props, context);
        this.state = { msg: '' };
    }

    async update(name:string) {
        let request = new Hello();
        request.name = name;
        let r = await client.get(request);
        this.setState({ msg: r.result });
    }

    render() {
        return (
            <div className="form-group">
                <input className="form-control" type="text" placeholder="Your name" 
                    onChange={e => this.update((e.target as HTMLInputElement).value)} />
                <h3 style={{ margin: 10 }}>{this.state.msg}</h3>
            </div>);
    }
}