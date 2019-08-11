import 'es6-shim';
import * as React from 'react';
import * as ReactDOM from 'react-dom';

import { BrowserRouter as Router, Route, Link, Switch, Redirect } from 'react-router-dom';
import { Routes, Roles, StateContext } from './shared';
import { Navbar, Fallback, Forbidden } from '@servicestack/react';

import { Home } from './components/Home';
import { About } from './components/About';
import { SignIn } from './components/SignIn';
import { SignUp } from './components/SignUp';
import { Profile } from './components/Profile';
import { Admin } from './components/Admin';

export const App: React.FC<any> = () => {
    const { state, dispatch } = React.useContext(StateContext);

    const renderHome = () => <Home name="React" />;
    const renderAbout = () => <About message="About page" />;

    const requiresAuth = (Component:any, path?:string) => {
        if (!state.userSession) {
            return () => <Redirect to={{ pathname: Routes.SignIn, search: '?redirect=' + path }}/>;
        }
        return () => <Component/>;
    };
    const requiresRole = (role:string, Component:React.FC<any>, path?:string) => {
        if (!state.userSession) {
            return () => <Redirect to={{ pathname: Routes.SignIn, search: '?redirect=' + path }}/>;
        }
        if (!state.userSession.roles || state.userSession.roles.indexOf(role) < 0) {
            return () => <Forbidden path={path} role={role} />;
        }
        return () => <Component/>;
    };

    return (<Router>
        <div>
            <nav className="navbar navbar-expand-lg navbar-dark">
                <div className="container">
                    <Link to="/" className="navbar-brand">
                        <i className="svg-logo svg-lg mr-1" />
                        <span className="align-middle">$safeprojectname$</span>
                    </Link>
                    <Navbar items={state.nav.results} attributes={state.userAttributes} />
                </div>
            </nav>

            <div id="content" className="container mt-4">
                <Switch>
                    <Route exact path={Routes.Home} render={renderHome} activeClassName="active" />
                    <Route path={Routes.About} render={renderAbout} activeClassName="active" />
                    <Route path={Routes.SignIn} component={SignIn} activeClassName="active" />
                    <Route path={Routes.SignUp} component={SignUp} activeClassName="active" />
                    <Route path={Routes.Profile} render={requiresAuth(Profile,Routes.Profile)} activeClassName="active" />
                    <Route path={Routes.Admin} render={requiresRole(Roles.Admin,Admin,Routes.Admin)} activeClassName="active" />
                    <Route path={Routes.Forbidden} component={Forbidden} />
                    <Route component={Fallback} />
                </Switch>
            </div>
        </div>
    </Router>);
}
