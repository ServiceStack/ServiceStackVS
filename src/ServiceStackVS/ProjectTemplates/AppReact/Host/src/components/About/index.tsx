import * as React from 'react';

export const About: React.FC<any> = (props:any) => (
    <div id="about">
        <div className="svg-users svg-8x ml-2"/>
        <h3>{props.message}</h3>
    </div>
);
