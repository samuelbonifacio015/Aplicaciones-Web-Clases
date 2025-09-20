import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import i18n from "@/i18n.js";
import PrimeVue from "primevue/config";
import Material from '@primeuix/themes/material'
import {Menubar} from "primevue";

createApp(App)
    .use(i18n)
    .use(PrimeVue, { ripple:ture, theme: {preset: Material }} )
    .component('pv-menubar', Menubar)
    .component('pv-button', Button)
    .mount('#app')