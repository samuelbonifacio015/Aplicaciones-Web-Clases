<script setup>

import DeveloperRegistration from "@/greetings/presentation/components/developer-registration.vue";
import DeveloperGreeting from "@/greetings/presentation/components/developer-greeting.vue";
import DeveloperCountShow from "@/greetings/presentation/components/developer-count-show.vue";
import {ref} from "vue";
import {Developer} from "@/greetings/domain/model/developer.entity.js";

const developerCount = ref(0);
const firstName = ref("");
const lastName = ref("");
const hasRegistered = ref(false);

function updateRegisteredDeveloperInfo(developer) {
  firstName.value = developer.firstName;
  lastName.value = developer.lastName;
  hasRegistered.value = true;
  console.log("Developer Registered: ", developer);
  updateDeveloperCount(developer);
}

function updateDeveloperCount(developer) {
  const dev = new Developer(developer.firstName, developer.lastName);
  if (dev.fullName !== "Unknown") {
    developerCount.value++;
  }
}

function resetRegisteredDeveloperInfo() {
  firstName.value = "";
  lastName.value = "";
  hasRegistered.value = false;
  console.log("Registration deferred by the user.");
}
</script>

<template>
  <h1>Hello Vue Developer Application </h1>
  <developer-registration
      @developer-registered="updateRegisteredDeveloperInfo"
      @registration-deferred="resetRegisteredDeveloperInfo"
  />
  <developer-greeting
      v-if="hasRegistered"
      :first-name="firstName"
      :last-name="lastName"
  />
  <developer-count-show :developer-count="developerCount"></developer-count-show>
</template>

<style scoped>

</style>
