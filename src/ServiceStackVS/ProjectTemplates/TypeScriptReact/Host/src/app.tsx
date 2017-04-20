import "es6-shim";
import * as ReactDOM from 'react-dom';
import * as React from 'react';
import HelloWorld from './HelloWorld';

var LINKS = ["Link 1", "Link 2", "Link 3"];

class App extends React.Component<any, any> {
    constructor(props) {
        super(props);
        this.state = { activeLink: "Link 1" };
    }
    render() {
        return (
            <div>
                <nav className="navbar navbar-toggleable-md navbar-inverse bg-inverse">
                    <div className="container">
                        <a className="navbar-brand" href="#">
                            <i className="fa fa-code" aria-hidden="true"></i>
                            <span style={{ paddingLeft: 5 }}>$safeprojectname$</span>
                        </a>
                        <div className="collapse navbar-collapse">
                            <ul className="navbar-nav mr-auto">
                                {LINKS.map(x =>
                                    (<li key={x} className={"nav-item " + (this.state.activeLink == x ? "active" : "")}
                                        onClick={e => this.setState({ activeLink: x })}>
                                        <a className="nav-link" href="#">{x}</a>
                                    </li>)
                                )}
                            </ul>
                        </div>
                    </div>
                </nav>

                <div className="container">
                    <div className="row" style={{ margin: "10px 0" }}>
                        <div id="content">
                            <HelloWorld />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("body"));
