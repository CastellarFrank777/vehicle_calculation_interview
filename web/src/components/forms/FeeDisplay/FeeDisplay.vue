<script lang="ts">
export default {
  name: 'FeeDisplay',
  props: {
    label: {
      type: String,
      required: true
    },
    value: {
      type: Number,
      required: true
    }
  }
}
</script>

<template>
  <div class="fee-display">
    <span class="span-lbl">{{ label }}:</span>
    <span class="span-value">{{ value.toFixed(2) }}</span>
  </div>
</template>

<style scoped>
@import './FeeDisplay.css';
</style>
