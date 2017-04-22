// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// http://www.typescriptlang.org/docs/handbook/jsx.html

import "es6-shim";
import * as ReactDOM from 'react-dom';
import * as React from 'react';
import HelloWorld from './HelloWorld';

declare var nativeHost;

class App extends React.Component<any,any> { 
    constructor(props,context) {
        super(props, context);
    }

    handleAbout() {
        nativeHost.showAbout();
    }
    handleToggleWindow() {
        nativeHost.toggleFormBorder();
    }
    handleQuit() {
        nativeHost.quit();
    }

    render() {
        return (
            <div>
                <nav className="navbar navbar-toggleable-md navbar-inverse bg-inverse">
                    <div className="container">
                        <a className="navbar-brand" href="/">
                            <i className="fa fa-code" aria-hidden="true"></i>
                            <span style={{ paddingLeft: 5 }}>$safeprojectname$</span>
                        </a>

                        <div className="collapse navbar-collapse" style={{ paddingLeft: 15 }}>
                            <ul className="navbar-nav mr-auto">
                                <li className="nav-item"><a className="nav-link" onClick={this.handleAbout}>About</a></li>
                                <li className="nav-item platform winforms">
                                    <a className="nav-link" onClick={this.handleToggleWindow}>Toggle Window</a>
                                </li>
                                <li className="nav-item platform winforms mac console">
                                    <a className="nav-link" onClick={this.handleQuit}>Close</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
                <div className="container">
                    <div className="row" style={{ margin: "10px 0" }}>
                        <HelloWorld />
                    </div>
                </div>
            </div>);
    }
}

ReactDOM.render(<App />, document.getElementById('body'));
