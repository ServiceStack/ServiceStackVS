import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { shallow, mount, render } from 'enzyme';

import Home from './Home';

describe('<Home />', () => {

  it ('Updates heading on setState', done => {
    const el = shallow(<Home />);

    expect(el.find('h3').text()).toBe("");

    el.setState({ msg: 'A' }, () => {
      expect(el.find('h3').text()).toBe("A");
      done();
    });
  });

  it ('Updates heading on update', async () => {
    const el = shallow(<Home />);

    expect(el.find('h3').text()).toBe("");

    await (el.instance() as Home).nameChanged('A');

    expect(el.find('h3').text()).toBe("Hello, A!");
  });

  it ('Updates heading on keyDown', done => {
    const el = shallow(<Home />);

    expect(el.find('h3').text()).toBe("");

    el.find('input').simulate('change', { target: { value: 'A' } });

    setTimeout(() => {
      expect(el.find('h3').text()).toBe("Hello, A!");
      done();
    }, 100);
  });

});
