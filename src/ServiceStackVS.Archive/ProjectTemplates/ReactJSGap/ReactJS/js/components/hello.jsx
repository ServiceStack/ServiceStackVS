var HelloWorld = React.createClass({
    getInitialState: function () {
        return { greeting: '' };
    },
    handleNameChange: function (e) {
		e.preventDefault();
		var $this = this;
		var yourName = e.target.value.trim();
        $.getJSON('hello/' + yourName)
            .then(function(response) {
                $this.setState({ greeting: response.Result });
            });
	},
	render: function() {
		return (
			<div>
				<input type="text" className="form-control" placeholder="Your name"
					   onChange={this.handleNameChange} />
				<h3>{this.state.greeting}</h3>
			</div>
		);
	}
});
