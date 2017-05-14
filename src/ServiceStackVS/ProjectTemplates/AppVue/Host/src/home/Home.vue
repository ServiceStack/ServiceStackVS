<template>
    <div class="form-group">
        <input class="form-control" type="text" placeholder="Your name" v-model="txtName" />
        <h3 class="result">{{ result }}</h3>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { client } from '../shared';
import { Hello } from '../dtos';

export default Vue.extend({
    props: ['name'],
    data() {
        return { result: '', txtName: this.name }
    },
    created() {
        this.nameChanged(this.txtName)
    },
    watch: {
        txtName(val) { this.nameChanged(val) }
    },
    methods: {
        async nameChanged(name: string) {
            if (name) {
                let request = new Hello();
                request.name = name;
                let r = await client.get(request);
                this.result = r.result;
            } else {
                this.result = '';
            }
        }
    }
});
</script>

<style lang="scss">
@import '../app.scss';

.result {
    margin: 10px;
    color: darken($navbar-background, 10%);
}
</style>