<script setup>

import {ref} from "vue";

/**
 * The first name input for the developer registration form.
 * @type {import('vue').Ref<string>}
 */
const firstName = ref("");

/**
 * The last name input for the developer registration form.
 * @type {import('vue').Ref<string>}
 */
const lastName = ref("");

/**
 * The error message to display if the registration fails.
 * @type {import('vue').Ref<string>}
 */
const errorMessage = ref("");

/**
 * Emits events for a registration actions.
 * The 'developer-registered' event is emitted when a developer successfully registers.
 * The 'registration-deferred' event is emitted when a developer chooses to defer registration.
 * @type {(event: 'developer-registered' | 'registration-deferred',
 * payload: { firstName: string, lastName: string})  => void }
 */
const emit = defineEmits(['developer-registered', 'registration-deferred']);

/**
 * Handles the defer registration action.
 * Emits a 'developer-registered' event with the first and last name if both are provided.
 * If either field is missing, sets an error message.
 */
function submitRegistrationRequest() {
  let submittedFirstName = firstName.value.toString().trim();
  let submittedLastName = lastName.value.toString().trim();
  if (submittedFirstName || submittedLastName) {
    console.log("Registering developer: ", submittedFirstName, submittedLastName);
    emit('developer-registered', {firstName: submittedFirstName, lastName: submittedLastName});
    clearFields();
    errorMessage.value = "";
  } else {
    console.log("Cannot register developer: First and last name are required.");
    errorMessage.value = "First and last name are required.";
  }
}

/**
 * Clears the input fields and error message.
 * @returns {void}
 */
function clearFields() {
  firstName.value = "";
  lastName.value = "";
  errorMessage.value = "";
}

function deferRegistration() {
  console.log("Deferring developer registration.");
  emit('registration-deferred', {firstName: "", lastName: ""});
  clearFields();
  errorMessage.value = "";
}

</script>

<template>
  <div>
    <h2>New Developer</h2>
    <div>
      <form v-on:submit.prevent="submitRegistrationRequest">
        <div class = "field">
          <label for="firstName">FirstName</label><input id="firstName" v-model="firstName" type="text"/>
        </div>
        <div class = "field">
          <label for="lastName">LastName</label><input id="lastName" v-model="lastName" type="text"/>
        </div>
        <div class = "actions">
          <button type="submit">Register</button>
          <button type="button" v-on:click="deferRegistration">Maybe later</button>
          <button type="button" v-on:click="submitRegistrationRequest">Cancel</button>
        </div>
      </form>
    </div>
  </div>
  <p v-if="errorMessage" class = "error" role="alert">{{ errorMessage }}</p>
</template>

<style scoped>
button {
  margin-right: 10px;
  padding: 8px 16px;
  cursor: pointer;
}
.error{
  color:red;
  margin-top: 10px;
  font-size: 14px;
}
.field{
  margin-bottom: 10px;
}
.actions{
  margin-top: 10px;
}
label{
  margin-right: 5px;
}
</style>