import * as React from 'react';
import { StateContext, signout } from '../shared';
import { LinkButton } from '@servicestack/react';

export const Profile: React.FC<any> = () => {

    const { state, dispatch } = React.useContext(StateContext);
    const user = state.userSession!;
    const roles = user && user.roles;
    const permissions = user && user.permissions;

    const handleSignOut = async () => await signout(dispatch);

    return (
        <div id="profile" className="text-center">

            <img src={user.profileUrl} className="svg-8x" />

            <p className="my-2">
                {user.displayName} {user.userId ? <span>#{user.userId}</span> : null}
            </p>
            <p>
                {user.userName}
            </p>
            <p className="roles">
                {roles && roles.map(x => <mark key={x}>{x}</mark>)}
            </p>
            <p className="permissions">
                {permissions && permissions.map(x => <mark key={x}>{x}</mark>)}
            </p>
            <p>
                <LinkButton onClick={handleSignOut} primary>Sign Out</LinkButton>
            </p>
        </div>
    );
}
