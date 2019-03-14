import * as React from 'react';
import * as cls from 'classnames';
import { Route, Link, withRouter } from 'react-router-dom';
import { JsonServiceClient } from '@servicestack/client';

declare var global; //populated from package.json/jest

export var client = new JsonServiceClient(global.BaseUrl || '/');

@withRouter
export class NavItem extends React.Component<any,any> {

  render () {
    const { to, location, children, ...props } = this.props;
    const active = location.pathname === to;

    return (
      <li role="presentation" className={cls('nav-item', { active })}>
        <Link to={to} className="nav-link">{children}</Link>
      </li>
    )
  }
}
