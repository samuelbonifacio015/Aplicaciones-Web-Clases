import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import i18n from "@/i18n.js";
import PrimeVue from 'primevue/config';
import Material from '@primeuix/themes/material'
import router from "./router.js";
import {Button, Drawer, SelectButton, Toolbar} from "primevue";

createApp(App)
    .use(i18n)
    .use(PrimeVue , { theme: { preset: Material}, ripple: true})
    .component('pv-button', Button)
    .component('pv-toolbar', Toolbar)
    .component('pv-drawer', Drawer)
    .component('pv-select-button', SelectButton)
    .use(router)
    .mount('#app')
