import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import i18n from "@/i18n.js";
import PrimeVue from 'primevue/config';
import Material from '@primeuix/themes/material'

createApp(App)
    .use(i18n)
    .use(PrimeVue , { theme: { preset: Material}, ripple: true})
    .mount('#app')
