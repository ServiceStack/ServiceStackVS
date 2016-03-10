var App = React.createClass({
    handleAbout: function () {
        nativeHost.showAbout();
    },
    handleToggleWindow: function () {
        nativeHost.toggleFormBorder();
    },
    handleQuit: function () {
        nativeHost.quit();
    },
    render: function () {
        return (
            <div>
                <div className="navbar navbar-inverse" role="navigation">
                    <div className="container">
                        <div className="navbar-header">
                            <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                                <span className="sr-only">Toggle navigation</span>
                                <span className="icon-bar"></span>
                                <span className="icon-bar"></span>
                                <span className="icon-bar"></span>
                            </button>
                            <a className="navbar-brand" href="/">
                                <img src="/img/react-logo.png" />
                                $safeprojectname$
                            </a>
                        </div>
                        <div className="navbar-collapse collapse">
                            <ul className="nav navbar-nav pull-right">
                                <li><a onClick={this.handleAbout}>About</a></li>
                                <li className="platform winforms">
                                    <a onClick={this.handleToggleWindow}>Toggle Window</a>
                                </li>
                                <li className="platform winforms mac">
                                    <a onClick={this.handleQuit}>Close</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="container">
                    <HelloWorld />
                </div>
            </div>
        );
    }
});

React.render(<App />, document.body);