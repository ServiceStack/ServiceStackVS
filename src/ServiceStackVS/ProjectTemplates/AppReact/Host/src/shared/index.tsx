import * as React from 'react';
import { createContext, useReducer } from 'react';
import { JsonServiceClient, GetNavItemsResponse, UserAttributes, IAuthSession } from '@servicestack/client';
import { History } from 'history';

declare let global: any; // populated from package.json/jest

if (typeof global === 'undefined') {
  (window as any).global = window;
}

export let client = new JsonServiceClient(global.BaseUrl || '/');

export {
  errorResponse, errorResponseExcept,
  splitOnFirst, toPascalCase,
  queryString,
  classNames,
} from '@servicestack/client';

export {
  ResponseStatus, ResponseError,
  Authenticate, AuthenticateResponse,
  Register,
  Hello, HelloResponse
} from './dtos';

import {
  Authenticate, AuthenticateResponse
} from './dtos';

export enum Routes {
  Home = '/',
  About = '/about',
  SignIn = '/signin',
  SignUp = '/signup',
  Profile = '/profile',
  Admin = '/admin',
  Forbidden = '/forbidden',
}

export enum Roles {
  Admin = 'Admin',
}

export const redirect = (history: History, path: string) => {
  setTimeout(() => {
    const externalUrl = path.indexOf('://') >= 0;
    if (!externalUrl) {
      history.push(path);
    } else {
      location.href = path;
    }
  }, 0);
}

// Shared state between all Components
interface State {
  nav: GetNavItemsResponse;
  userSession: IAuthSession | null;
  userAttributes?: string[];
  roles?: string[];
  permissions?: string[];
}
interface Action {
  type: 'signout' | 'signin'
  data?: AuthenticateResponse | any
}

const initialState: State = {
  nav: global.NAV_ITEMS as GetNavItemsResponse,
  userSession: global.AUTH as AuthenticateResponse,
  userAttributes: UserAttributes.fromSession(global.AUTH),
};

const reducer = (state: State, action: Action) => {
  switch (action.type) {
    case 'signin':
    const userSession = action.data as IAuthSession;
    return { ...state, userSession, roles: userSession.roles || [], permissions: userSession.permissions || [],
                userAttributes: UserAttributes.fromSession(userSession) } as State;
    case 'signout':
      return { nav:state.nav, userSession:null } as State;
    default:
      throw new Error();
  }
}

interface Context {
  state: State,
  dispatch: React.Dispatch<Action>
}

export const StateContext = createContext({} as Context);

export const StateProvider = (props: any) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  return (<StateContext.Provider value={{ state, dispatch }}>{props.children}</StateContext.Provider>);
}

type Dispatch = React.Dispatch<Action>;

export const checkAuth = async (dispatch: Dispatch) => {
  try {
    dispatch({ type: 'signin', data: await client.post(new Authenticate()) });
  } catch (e) {
    dispatch({ type: 'signout' });
  }
};

export const signout = async (dispatch: Dispatch) => {
  dispatch({ type: 'signout' });
  await client.post(new Authenticate({ provider: 'logout' }));
};
