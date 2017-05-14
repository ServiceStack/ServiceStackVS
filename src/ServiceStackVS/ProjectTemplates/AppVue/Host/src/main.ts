import 'bootstrap/dist/css/bootstrap.css';
import "font-awesome/css/font-awesome.css";
import './app.scss';
import 'es6-shim';

import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import App from './App.vue';
import Home from './home/Home.vue';
import View1 from './view1/View1.vue';
import View2 from './view2/View2.vue';

const routes = [
  { path: '/', component: Home, props: { name: "Vue" } },
  { path: '/view1', component: View1 },
  { path: '/view2', component: View2 }
]

const router = new VueRouter({
    mode: 'history',
    linkActiveClass: 'active',
    routes,
});

Vue.use(VueRouter);
const app = new Vue({
    el: '#app',
    render: h => h(App),
    router,
    mixins: [VueRouter],
});
