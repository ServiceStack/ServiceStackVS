// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='../typings/index.d.ts'/>

import "es6-shim";
import * as ReactDOM from 'react-dom';
import * as React from 'react';
import HelloWorld from './hello';

class App extends React.Component<any,any> { 
    constructor(props,context) {
        super(props, context);
    }

    handleAbout() {
        window.nativeHost.showAbout();
    }
    handleToggleWindow() {
        window.nativeHost.toggleFormBorder();
    }
    handleQuit() {
        window.nativeHost.quit();
    }

    render() {
        return (
            <div>
                <div className="navbar navbar-inverse" role="navigation">
                    <div className="container">
                        <div className="navbar-header">
                            <a className="navbar-brand" href="/">
                                <img src="/img/react-logo.png" />
                                $safeprojectname$
                            </a>
                        </div>
                        <ul className="nav navbar-nav pull-right">
                            <li><a onClick={this.handleAbout}>About</a></li>
                            <li className="platform winforms">
                                <a onClick={this.handleToggleWindow}>Toggle Window</a>
                            </li>
                            <li className="platform winforms mac console">
                                <a onClick={this.handleQuit}>Close</a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className="container">
                    <HelloWorld />
                </div>
            </div>);
    }
}

ReactDOM.render(<App />, document.getElementById('content'));
