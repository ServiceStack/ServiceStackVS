import './view.css';

import * as React from 'react';

const View = (props: any) => (
    <div id="view1">
        <h3>{props.message}</h3>
    </div>
);

export default () => <View message="This is View 1" />;
