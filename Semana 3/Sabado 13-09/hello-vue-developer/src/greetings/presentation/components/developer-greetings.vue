<script setup lang="ts">

/**
 * Developer Greetings Component
 * This component displays a greeting message based on the provided first and last name props.
 * If only the first name is provided, it shows a generic welcome message.
 * If both names are provided, it creates a Developer instance and shows a personalized congratulatory message.
 */

import { Developer } from '@/greetings/domain/model/developer.entity';
import {computed} from "vue";

const props = defineProps(['firstName', 'lastName']);

/**
 * Computes a greeting message based on the provided first and last name props.
 * If only the first name is provided, it returns a generic welcome message.
 * If both names are provided, it creates a Developer instance and returns a personalized congratulatory message.
 * @type {import('vue').ComputedRef<string>}
 */

const greeting = computed( () => {
  if (props.firstName?.trim() && !props.lastName?.trim()) {
  return "Welcome Anonymous Developer!";
  }
  const developer = new Developer(props.firstName, props.lastName);
  return `Congrats ${developer.fullName}! Now you are a Vue Developer!`
});

</script>

<template>
<p>{{ greeting }}</p>
</template>

<style scoped>

</style>