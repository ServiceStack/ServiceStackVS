import { View1 } from '../../src/views/view1';

describe('View 1', () => {
  it ('says hello', () => {
    expect(new View1().message).toBe('This is View 1');
  });
});
