import * as React from 'react';
import Hello from './Hello';

export default class Home extends React.Component<any, any> {
    constructor(props, context) {
        super(props, context);
    }

    render() {
        return (<Hello name={this.props.name} />);
    }
}