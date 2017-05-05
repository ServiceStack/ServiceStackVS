import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { shallow, mount, render } from 'enzyme';

import HelloWorld from './HelloWorld';

describe('<HelloWorld />', () => {

  it ('Updates heading on setState', done => {
    const el = shallow(<HelloWorld />);

    expect(el.find('h3').text()).toBe("");

    el.setState({ msg: 'A' }, () => {
      expect(el.find('h3').text()).toBe("A");
      done();
    });
  });

  it ('Updates heading on update', async () => {
    const el = shallow(<HelloWorld />);

    expect(el.find('h3').text()).toBe("");

    await (el.instance() as HelloWorld).update('A');

    expect(el.find('h3').text()).toBe("Hello, A!");
  });

  it ('Updates heading on INPUT change', done => {
    const el = shallow(<HelloWorld />);

    expect(el.find('h3').text()).toBe("");

    el.find('input').simulate('change', { target: { value: 'A' } });

    setTimeout(() => {
      expect(el.find('h3').text()).toBe("Hello, A!");
      done();
    }, 100);
  });

});
