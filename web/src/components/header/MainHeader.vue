<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  props: {
    title: {
      type: String,
      required: true
    }
  }
})
</script>

<template>
  <header class="header">
    <h1 class="header-title">{{ title }}</h1>
  </header>
</template>

<style scoped>
@import './MainHeader.css';
</style>
