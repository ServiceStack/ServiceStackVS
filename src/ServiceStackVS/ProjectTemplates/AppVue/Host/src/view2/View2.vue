<template>
    <div>
        <h3>{{ message }}</h3>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
export default Vue.extend({
  data() {
    return { message: "This is View 2" }
  }
});
</script>
