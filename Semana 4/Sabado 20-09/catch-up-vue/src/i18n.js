import en from './locals/en.json';
import es from './locals/es.json';
import {createI18n} from 'vue-i18n';

const i18n = createI18n({
    legacy: false,
    locale: 'en',
    fallbackLocale: 'en',
    messages: { en, es }
});

export default i18n;