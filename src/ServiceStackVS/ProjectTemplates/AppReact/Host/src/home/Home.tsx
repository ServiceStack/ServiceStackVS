import * as React from 'react';
import Hello from './Hello';

export default class Home extends React.Component<any, any> {
    constructor(props: any, context: any) {
        super(props, context);
    }

    public render() {
        return (<Hello name={this.props.name} />);
    }
}