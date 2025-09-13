<script setup lang="ts">
import {toRefs} from "vue";

/**
 * The number of registered developers to display.
 * developerCount is a required prop of type Number.
 */
const props = defineProps({
  developerCount: {
    type: Number,
    required: true
  },
});

/**
 * Extracts the developerCount prop for use in the template.
 */
const {developerCount} = toRefs(props);
</script>

<template>
  <div>
    <p>Developers registered: {{ developerCount }}</p>
    <p><b>Note:</b> Unknow developers are not considered.</p>
  </div>

</template>

<style scoped>

</style>