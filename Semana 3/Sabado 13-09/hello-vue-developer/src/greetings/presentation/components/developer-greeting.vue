<script setup lang="ts">

/**
 * DeveloperGreeting component
 * @component developer-greeting
 * @description
 * Greets a developer by their first and last name. If no name is provided, it greets them as "Anonymous Developer".
 * @prop {string} firstName - The first name of the developer.
 * @prop {string} lastName - The last name of the developer.
 */
import { Developer } from "@/greetings/domain/model/developer.entity";
import {computed} from "vue";

const props = defineProps(['firstName', 'lastName']);

/**
 * Computes a greeting message based on the provided first and last name props.
 * @type {import('vue').ComputedRef<string>}
 */
const greeting = computed(() => {
  if (!props.firstName?.trim() && !props.lastName?.trim()) {
    return "Welcome Anonymous Developer!";
  }
  const developer = new Developer(props.firstName, props.lastName);
  return `Congrats ${developer.fullName}! Now ypu are a Vue Developer!`
});
</script>

<template>
<p>{{ greeting }}</p>
</template>

<style scoped>

</style>