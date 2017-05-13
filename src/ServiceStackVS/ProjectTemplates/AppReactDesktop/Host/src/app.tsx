import 'bootstrap/dist/css/bootstrap.css';
import "font-awesome/css/font-awesome.css";
import './app.scss';

import "es6-shim";
import * as ReactDOM from 'react-dom';
import * as React from 'react';

import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import { NavItem } from './shared';

import Home from './home/Home';
import View1 from './view1/View';
import View2 from './view2/View';

declare var nativeHost;

class App extends React.Component<any, any> {
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
            <Router>
            <div>
                <nav className="navbar navbar-toggleable-md navbar-inverse bg-inverse">
                    <div className="container">
                        <Link to="/" className="navbar-brand"> 
                            <i className="fa fa-code" aria-hidden="true"></i>
                            <span style={{ paddingLeft: 5 }}>$safeprojectname$</span>
                        </Link>
                        <div className="collapse navbar-collapse">
                            <ul className="nav navbar-nav mr-auto">
                                <NavItem to="/">Home</NavItem>
                                <NavItem to="/view1">View1</NavItem>
                                <NavItem to="/view2">View2</NavItem>
                            </ul>
                            <ul className="nav navbar-nav">
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
                        <div id="content">
                            <Route exact path="/" component={Home} activeClassName="active" />
                            <Route path="/view1" component={View1} activeClassName="active" />
                            <Route path="/view2" component={View2} activeClassName="active" />
                        </div>
                    </div>
                </div>

                <h4 style={{position: 'absolute', bottom: 20, width: '100%', textAlign: 'center'}}>
                    <img src={require('./assets/img/logo.png')} />
                    <a href="https://servicestack.net/vs-templates/ReactDesktopApps">Learn about this React Desktop Apps VS.NET template</a>
                </h4>
            </div>
            </Router>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("body"));
