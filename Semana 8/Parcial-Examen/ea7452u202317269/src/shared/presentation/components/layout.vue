<script setup>

import LanguageSwitcher from "./language-switcher.vue";
import {ref} from "vue";
import {useI18n} from "vue-i18n";
import FooterContent from "./footer-content.vue";
const { t } = useI18n();

const drawer = ref(false);
const toggleDrawer = () => {
  drawer.value = !drawer.value;
}
const items = [
  {label: 'option.home', to: '/home'},
  {label: 'option.new-issues', to: '/new-issue'},
];
</script>

<template>
  <pv-toast/>
  <pv-confirm-dialog/>
  <div class="app-container">
    <div class="header">
      <pv-toolbar class="bg-blue-600 text-white">
        <template #start>
          <img src="/aldi_sued_logo.png"
          alt="Aldi Logo"
               style="height: 40px; margin-right: 10px;"
          />
          <h3>Aldi Operations Continuity Platform</h3>
        </template>
        <template #center>

        </template>
        <template #end>
          <div class="flex-column mr-3">
            <pv-button v-for="item in items" :key="item.label" as-child v-slot="slotProps">
              <router-link :to="item.to" :class="slotProps['class']">{{ t(item.label) }}</router-link>
            </pv-button>
          </div>
          <language-switcher/>
        </template>
      </pv-toolbar>
      <pv-drawer v-model:visible="drawer"/>
    </div>
    <div class="main-content">
      <router-view/>
    </div>
    <div class="footer">
      <footer-content/>
    </div>
  </div>
</template>

<style scoped>
.app-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.header {
  flex-shrink: 0;
}

.main-content {
  flex: 1;
  padding-top: 60px;
  padding-bottom: 20px;
}

.footer {
  flex-shrink: 0;
  width: 100%;
  padding: 10px;
}
</style>