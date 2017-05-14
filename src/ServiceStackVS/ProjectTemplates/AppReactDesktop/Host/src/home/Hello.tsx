import './hello.scss';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { client } from '../shared';
import { Hello } from '../dtos';

export default class HelloComponent extends React.Component<any, any> {
    constructor(props, context) {
        super(props, context);
        this.state = { result: '' };
        this.nameChanged(this.props.name);
    }

    async nameChanged(name:string) {
        if (name) {
            let request = new Hello();
            request.name = name;
            let r = await client.get(request);
            this.setState({ result: r.result });
        } else {
            this.setState({ result: '' });
        }
    }

    render() {
        return (
            <div className="form-group">
                <input className="form-control" type="text" placeholder="Your name"
                    defaultValue={this.props.name}
                    onChange={e => this.nameChanged((e.target as HTMLInputElement).value)} />
                <h3 className="result">{this.state.result}</h3>
            </div>);
    }
}