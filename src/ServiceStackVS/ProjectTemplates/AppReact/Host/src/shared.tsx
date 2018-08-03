import * as React from 'react';
import * as cls from 'classnames';
import { Link, withRouter } from 'react-router-dom';
import { JsonServiceClient } from '@servicestack/client';

declare var global: any; // populated from package.json/jest

export const client = new JsonServiceClient(global.BaseUrl || '/');

class NavItemImpl extends React.Component<any,any> {

  public render () {
    const { to, location, children } = this.props;
    const active = location.pathname === to;

    return (
      <li role="presentation" className={cls('nav-item', { active })}>
        <Link to={to} className="nav-link">{children}</Link>
      </li>
    )
  }
}

export const NavItem = withRouter(NavItemImpl);
