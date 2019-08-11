import * as React from 'react';
import { useState, useContext } from 'react';
import { StateContext, client, checkAuth, Register, Routes, toPascalCase, splitOnFirst, classNames } from '../shared';
import { ErrorSummary, Input, CheckBox, Button, LinkButton } from '@servicestack/react';
import { withRouter } from 'react-router-dom';

export const SignUp = withRouter(({ history }) => {
    const {state, dispatch} = useContext(StateContext);

    const [loading, setLoading] = useState(false);
    const [responseStatus, setResponseStatus] = useState(null);

    const [displayName, setDisplayName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [autoLogin, setAutoLogin] = useState(true);

    const newUser = (s:string) => {
        const names = s.split('@');
        setDisplayName(toPascalCase(names[0]) + ' ' + toPascalCase(splitOnFirst(names[1],'.')[0]));
        setEmail(s);
        setPassword('p@55wOrd');
        setConfirmPassword('p@55wOrd');
    }

    const submit = async () => {
        try {
            setLoading(true);
            setResponseStatus(null);

            const response = await client.post(new Register({
                displayName,
                email,
                password,
                confirmPassword,
                autoLogin,
            }));

            await checkAuth(dispatch);
            setLoading(false);

            history.push(Routes.Home);
        } catch (e) {
            setResponseStatus(e.responseStatus || e);
            setLoading(false);
        }
    };

    const handleSubmit = async (e:React.FormEvent<HTMLFormElement>) => { e.preventDefault(); await submit(); };
    const handleNewUser = () => newUser('new@user.com');

    return (<div className="row">
        <div className="col-4">
            <h3>Register New User</h3>

            <form className={classNames({error:responseStatus, loading})} onSubmit={handleSubmit}>
                <div className="form-group">
                    <ErrorSummary except={'displayName,email,password,confirmPassword'} responseStatus={responseStatus} />
                </div>
                <div className="form-group">
                    <Input type="text" id="displayName" value={displayName} onChange={setDisplayName} responseStatus={responseStatus}
                        placeholder="Display Name" label="Name" help="Your first and last name" />
                </div>
                <div className="form-group">
                    <Input type="text" id="email" value={email} onChange={setEmail} responseStatus={responseStatus}
                        placeholder="Email" label="Email" />
                </div>
                <div className="form-group">
                    <Input type="password" id="password" value={password} onChange={setPassword} responseStatus={responseStatus}
                        placeholder="Password" label="Password" />
                </div>
                <div className="form-group">
                    <Input type="password" id="confirmPassword" value={confirmPassword} onChange={setConfirmPassword} responseStatus={responseStatus}
                        placeholder="Confirm" label="Confirm Password" />
                </div>
                <div className="form-group">
                    <CheckBox id="autoLogin" value={autoLogin} onChange={setAutoLogin} responseStatus={responseStatus}>
                        Auto Login
                    </CheckBox>
                </div>
                <div className="form-group">
                    <Button type="submit" lg primary>Register</Button>
                    <LinkButton href="/signin" navItemClass="btn">Sign In</LinkButton>
                </div>

                <div className="pt-3">
                    <h5>Quick Populate:</h5>
                    <p className="pt-1">
                        <LinkButton outline-info sm onClick={handleNewUser}>new@user.com</LinkButton>
                    </p>
                </div>
            </form>
        </div>
    </div>);
});