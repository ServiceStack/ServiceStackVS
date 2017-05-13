import './home.scss';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { client } from '../shared';
import { Hello } from '../dtos';

export default class Home extends React.Component<any, any> {
    constructor(props, context) {
        super(props, context);
        this.state = { msg: '' };
    }

    async nameChanged(name:string) {
        if (name) {
            let request = new Hello();
            request.name = name;
            let r = await client.get(request);
            this.setState({ msg: r.result });
        } else {
            this.setState({ msg: '' });
        }
    }

    render() {
        return (
            <div className="form-group">
                <input className="form-control" type="text" placeholder="Your name" 
                    onChange={e => this.nameChanged((e.target as HTMLInputElement).value)} />
                <h3 className="result">{this.state.msg}</h3>
            </div>);
    }
}