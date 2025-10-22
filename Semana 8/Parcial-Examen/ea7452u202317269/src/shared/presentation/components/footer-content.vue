<script setup>
import {useI18n} from "vue-i18n";

const { t } = useI18n();
</script>

<template>
  <div class="grid bg-blue-500 text-white mt-4 p-2 align-content-start">
    <div  class="col-12 ml-3 mt-1 align-items-center justify-content-center">
      <p>
        {{ t('authoring-phrase.intro') }} 
        {{ t('authoring-phrase.use') }} <a href="https://primevue.org/" target="_blank" class="footer-link">PrimeVue</a>
        {{ t('authoring-phrase.author', {brand: 'ACME'}) }}
      </p>
    </div>
  </div>
</template>

<style scoped>
.footer-link {
  color: #bfdbfe;
  text-decoration: underline;
  transition: color 0.3s ease;
}
</style>