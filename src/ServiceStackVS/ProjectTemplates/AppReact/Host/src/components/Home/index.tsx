import * as React from 'react';
import { StateContext, signout } from '../../shared';
import { LinkButton } from '@servicestack/react';
import { HelloApi } from './HelloApi';

export const Home: React.FC<any> = (props:any) => {
    const {state, dispatch} = React.useContext(StateContext);

    const handleSignOut = async () => await signout(dispatch);

    return (<div className="row justify-content-between">
        <div className="col col-1">
            <i className="svg-home svg-9x" />
        </div>
        <div className="col col-4 mt-4">
            <HelloApi name={props.name} />
        </div>
        <div className="col-md-auto" />
        <div className="col col-4">
            {state.userSession ?
            (<div className="text-right">
                <p className="pt-3">{`Hi ${state.userSession!.displayName}!`}</p>
                <LinkButton onClick={handleSignOut} sm primary>Sign Out</LinkButton>
            </div>) :
            (<div className="text-right">
                <p className="pt-3">You are not authenticated.</p>
                <LinkButton href="/signin" sm primary>Sign In</LinkButton>
                <LinkButton href="/signup" sm outline-secondary className="ml-2">Register New User</LinkButton>
            </div>)}
        </div>
    </div>);
}