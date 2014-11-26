/** @jsx React.DOM */
var HelloWorld = React.createClass({
	handleHello: function(e) {
		e.preventDefault();
		var yourName = this.refs.yourName.getDOMNode().value.trim();
		$.ajax({
			url: 'hello/' + yourName,
			dataType: 'json',
			type: 'GET',
			success: function (response) {
				this.setState({yourName: response.Result});
			}
		})
	},
	getInitialState: function () {
		return {yourName: ''};
	},
	render: function() {
		var text = this.state.yourName;
		return (
			<div>
				<input type="text" placeholder="Your name" ref="yourName"
						onChange={this.handleHello} />
				<h3>{text}</h3>
			</div>
		);
	}
});
React.renderComponent(
		<HelloWorld />,
		document.getElementById('demo')
);