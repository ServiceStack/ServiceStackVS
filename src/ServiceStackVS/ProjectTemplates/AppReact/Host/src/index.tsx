import 'bootstrap/dist/css/bootstrap.css';
import './app.css';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { StateProvider } from './shared';

import { App } from './App';
import * as serviceWorker from './serviceWorker';

ReactDOM.render(
  <StateProvider><App /></StateProvider>,
  document.getElementById('root') as HTMLElement
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
