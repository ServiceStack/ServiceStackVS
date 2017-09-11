<template>
<div>
    <nav class="navbar navbar-expand-lg navbar-dark">
        <div class="container">
            <router-link class="navbar-brand" to="/" exact="">
                <i class="fa fa-code" aria-hidden="true"></i>
                $safeprojectname$
            </router-link>
          
            <div class="navbar-collapse">
                <ul class="navbar-nav mr-auto">
                  <router-link tag="li" class="nav-item" to="/" exact>
                    <a class="nav-link">Home</a>
                  </router-link>
                  <router-link tag="li" class="nav-item" to="/view1">
                    <a class="nav-link">View 1</a>
                  </router-link>
                  <router-link tag="li" class="nav-item" to="/view2">
                    <a class="nav-link">View 2</a>
                  </router-link>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container">
        <div class="row" style="margin:10px 0">
            <div id="content">
                <router-view></router-view>
            </div>
        </div>
    </div>

    <h4 style="position: absolute; bottom: 20px; width: 100%; text-align: center">
        <img src="./assets/img/logo.png" />
        <a :href="url">Learn about this Vue VS.NET template</a>
    </h4>
</div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

@Component
export default class AppComponent extends Vue {
    @Prop({ default: "https://servicestack.net/vs-templates/VueApp" }) url: string;
}
</script>
