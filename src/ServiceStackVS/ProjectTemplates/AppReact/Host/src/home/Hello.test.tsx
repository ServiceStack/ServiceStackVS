import * as React from 'react';
import { shallow } from 'enzyme';

import Hello from './Hello';

describe('<Hello />', () => {

  it ('Updates heading on setState', done => {
    const el = shallow(<Hello />);

    expect(el.find('h3').text()).toBe('');

    el.setState({ result: 'A' }, () => {
        expect(el.update().find('h3.result').text()).toBe('A');
        done();
    });
  });

  it ('Updates heading on update', async () => {
    const el = shallow(<Hello />);

    expect(el.find('h3').text()).toBe('');

    await (el.instance() as Hello).nameChanged('A');

    expect(el.update().find('h3').text()).toBe('Hello, A!');
  });

  it ('Updates heading on keyDown', done => {
    const el = shallow(<Hello />);

    expect(el.find('h3').text()).toBe('');

    el.find('input').simulate('change', { target: { value: 'A' } });

    setTimeout(() => {
      expect(el.update().find('h3').text()).toBe('Hello, A!');
      done();
    }, 100);
  });

});
