import { expect } from 'chai';
import Vue from 'vue';

import Home from './Home.vue';

const ctor = (propsData=null) => new Home({ propsData }).$mount() as Home;

describe('Home.vue', () => {

    it ('should have correct data', function () {
        const vm = ctor();
        expect(vm.result).to.be.undefined;
    })

    it ('should render correct contents', done => {
        const vm = ctor({ initResult: 'Hello Vue' });
        expect(vm.$el.querySelector('input').type).eq('text');
        expect(vm.$el.querySelector('h3.result').textContent).eq('Hello Vue');

        vm.result = "Bye Vue";

        Vue.nextTick(() => {
            expect(vm.$el.querySelector('h3.result').textContent).eq('Bye Vue');
            done();
        });
    })

})
