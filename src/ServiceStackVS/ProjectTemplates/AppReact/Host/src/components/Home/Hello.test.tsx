import * as React from 'react';
import { mount } from 'enzyme';

import { HelloApi } from './HelloApi';
import { Input } from '@servicestack/react';

const hold = console.error;

describe('<Hello />', () => {

  // remove when fixed https://github.com/facebook/react/issues/14769#issuecomment-482694782
  beforeAll(() => {
    console.error = jest.fn();
  });

  afterAll(() => {
    console.error = hold;
  });

  it ('Updates heading on input change', done => {

    const el = mount(<HelloApi />);

    expect(el.find('h3').text()).toBe('');
    (el.find(Input).prop('onChange') as any)('A');

    setTimeout(() => {
      expect(el.find('h3').text()).toBe('Hello, A!');
      done();
    }, 100);
  });

});
