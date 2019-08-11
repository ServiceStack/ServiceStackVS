import './hello.css';

import * as React from 'react';
import { Input } from '@servicestack/react';
import { client, Hello } from '../../shared';

export interface HelloApiProps {
    name: string;
}

export const HelloApi: React.FC<any> = (props:HelloApiProps) => {
    const [name, setName] = React.useState(props.name);
    const [result, setResult] = React.useState('');

    React.useEffect(() => {
        (async () => {
            setResult(!name ? '' : (await client.get(new Hello({ name }) )).result)
        })();
    }, [name]); // fires when name changes

    return (<div>
        <div className="form-group">
            <Input value={name} onChange={setName} placeholder="Your name" />
            <h3 className="result pt-2">{ result }</h3>
        </div>
    </div>);
}
