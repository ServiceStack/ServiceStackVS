/** @jsx React.DOM */

var React = React || require('react');
var module = module || {};

var HelloWorld = React.createClass({
	handleHello: function(e) {
		e.preventDefault();
		var yourName = this.refs.yourName.getDOMNode().value.trim();
		var self = this;
		$.ajax({
			url: 'hello/' + yourName,
			dataType: 'json',
			type: 'GET',
			success: function(response) {
				self.setState({ yourName: response.Result });
			}
		});
	},
	getInitialState: function () {
		return {yourName: ''};
	},
	render: function() {
		return (
			<div>
				<input type="text" placeholder="Your name" ref="yourName" className="form-control"
						onChange={this.handleHello} />
				<h3>{this.state.yourName}</h3>
			</div>
		);
}
});
module.exports = HelloWorld