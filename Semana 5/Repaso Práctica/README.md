## Repositorio de Github con el proyecto de práctica desplegado:

[repo-github](https://github.com/samuelbonifacio015/caso-tvshow)

---

# Orden de pasos para el setup del proyecto para práctica

## Paso 1:

Package.json -> instalación de dependencias

Lista de dependencias:

    "axios": "^1.12.2",
    "vue": "^3.5.18",
    "vue-i18n": "^11.1.12"

## Paso 2:

### Setup de ENV.

- **.env.development**: 

```
# Environment: Development
# Description: This file contains environment variables for the development environment.
# Note: In real scenarios, this files is not committed to the repository for security reasons.

# VITE_NEWS_API_KEY is the API KEY for the News API.
VITE_NEWS_API_KEY="7dd4442ad84642768511a09c74f5ff5b"

# VITE_NEWS_API_URL is the base URL for the News API.
VITE_NEWS_API_URL="https://newsapi.org/v2"

# VITE_LOGO_API_URL is the base URL for the Logo API.
VITE_LOGO_API_URL="https://logo.clearbit.com"

# VITE_SOURCES_ENDPOINT_PATH is the endpoint path for fetching news sources.
VITE_SOURCES_ENDPOINT_PATH="/top-headlines/sources"

# VITE_TOP_HEADLINES_ENDPOINT_PATH is the endpoint path for fetching top headlines.
VITE_TOP_HEADLINES_ENDPOINT_PATH="/top-headlines"
```

- **.env.production**

```
# Environment: Production
# Description: This file contains environment variables for the production environment.
# Note: In real scenarios, this files is not committed to the repository for security reasons.

# VITE_NEWS_API_KEY is the API KEY for the News API.
VITE_NEWS_API_KEY="7dd4442ad84642768511a09c74f5ff5b"

# VITE_NEWS_API_URL is the base URL for the News API.
VITE_NEWS_API_URL="https://newsapi.org/v2"

# VITE_LOGO_API_URL is the base URL for the Logo API.
VITE_LOGO_API_URL="https://logo.clearbit.com"

# VITE_SOURCES_ENDPOINT_PATH is the endpoint path for fetching news sources.
VITE_SOURCES_ENDPOINT_PATH="/top-headlines/sources"

# VITE_TOP_HEADLINES_ENDPOINT_PATH is the endpoint path for fetching top headlines.
VITE_TOP_HEADLINES_ENDPOINT_PATH="/top-headlines"
```

Las variables env. sirven para poner las claves de las APIS, cada uno es uno en específico, parecen idénticos pero se utilizan según contexto


## Paso 3:

Eliminar assets & components (donde Vue almacena sus templates de cada proyecto)

## Paso 4:

### Traducción.

Crear carpeta **"Locales"**

```json
en.json //lenguaje ingles
{
  "read-more": "Read more",
  "unavailable-news": "News service is unavailable now",
  "authoring-phrase": {
    "intro": "Made with",
    "use": "using",
    "author": "by {brand} Developer Team"
  },
  "article": {
    "share": "Share",
    "copy-ti-clipboard": "Copy to clipboard"
  },
  "footer": {
    "powered-by": "Powered by",
    "and": "and"
  }
}

es.json //lenguaje español
{
  {
  "read-more": "Ver más",
  "unavailable-news": "Servicio de noticias no disponible en este momento.",
  "authoring-phrase": {
    "intro": "Hecho con",
    "use": "utilizando",
    "author": "por el Equipo de Desarrollo de {brand}"
  },
  "article": {
    "share": "Compartir",
    "copy-ti-clipboard": "Copiar al portapapeles"
  },
  "footer": {
    "powered-by": "Contenido proporcionado por",
    "and": "y"
  }
}
```

## Paso 5:

Crear carpeta **News** *(Según contexto)*

## Paso 6:

### Organización de carpetas.

```
|📂 src/
│
├── 📂 news/ # Carpeta principal 
│ ├── 📂 application/ # Aplicacion
│  └── news.store.js/ # JavaScript para funcionalidad de la app (LoadSources) 
│
│ ├── 📂 domain/ # Bounded Context Domain
│  └── 📂 model/ # Modelos o Entidades
│     └── article.entity.js # Entidad para los detalles de la API (ejemplo: año, mes, dia)
│     └── source.entity.js/ # Entidad para los detalles específicos (ejemplo: nombre, url, descripcion)
│ 
│ ├── 📂 infraestructure/ # Assembler
│     └── article.assembler.js/ # Transforma datos recibidos por la API a modelos/DTO que usa la app
│     └── news-api.js/ # Cliente que hace requests HTTP (fetch/axios) a endpoints de noticias. 
│     └── source.assembler.js/ # Transforma respuestas relacionadas a sources (fuentes) en objetos 
│
│ ├── 📂 presentation/ # Frontend
│  └── 📂 components/ # Componentes de Frontend (UI)
│     └── source-item.vue  # Muestra los sources desde la API (usa article.entity.js)
│     └── source-list.vue  # Muestra una lista desde la api (usa source.entity.js)
│
├── 📂 shared/ # Recurso común a todo el proyecto (esto va fuera de news).
│ ├── 📂 infraestructure/ # Assembler
│     └── logo-api.js/ 
│
│ ├── 📂 presentation/ # Frontend
│  └── 📂 components/ # Componentes de Frontend (UI)
│     └── footer-content.vue 
│     └── language-switcher.vue 
│     └── layout.vue # Layout principal del boton
│
├── App.vue # Principal app
├── i18n.js # Traduccion (crear primero para empezar con el proyecto)
├── main.js # Monta la app

```

#### La creación de este archivo corresponde al 22/09/25 (12:25PM) sujeto a futuras actualizaciónes
**Actualizacion 23/09 (17:51PM) : especificar carpetas src y shared**

## Paso 7:

### Crear i18n.js

Archivo importantísimo para el cambio de idiomas

```js
import en from './locales/en.json';
import es from './locales/es.json';
import {createI18n} from "vue-i18n";

const i18n = createI18n({
    legacy: false,
    locale: 'en',
    fallbackLocale: 'en',
    messages: { en, es }
});

export default i18n;
```

## Paso 8:

### Crear entidades en domain/model

Crear las entidades como se muestra:
```
│ ├── 📂 domain/ # Bounded Context Domain
│  └── 📂 model/ # Modelos o Entidades
│     └── article.entity.js # Entidad para los detalles de la API (ejemplo: año, mes, dia)
│     └── source.entity.js/ # Entidad para los detalles específicos (ejemplo: nombre, url, descripcion)
```

**Ejemplo**:
```js
export class Source {
    constructor( { id, name, country, officialSite } ) {
        this.id = id;
        this.name = name;
        this.officialSite = officialSite;
        this.country = country
            ? {
            name: country.name,
            code: country.code,
            timezone: country.timezone,
        }
        :null;
    }
}
```

```js
import {Source} from '@/shows/domain/model/source.entity.js';

export class Show {
    constructor({
      id,
      url,
    }) {
      this.id = id;
      this.url = url;
    }
}
```

***Importante adaptar lo necesario a los datos de la API***

## Paso 9:

### Crear context-api.js

En este paso crearemos la instancia principal para acceder a la API usando axios como backend para Vue.

**Ejemplo**:

```js
import axios from "axios";

const showApiUrl = import.meta.env.VITE_SHOWS_API_URL;
const showsEndpoint = import.meta.env.VITE_SHOWS_ENDPOINT_PATH;
const showsHeadlinesEndpoint = import.meta.env.VITE_SHOWS_HEADLINES_ENDPOINT_PATH;

const http = axios.create({
    baseURL: showsEndpoint
})

export class ShowsApi {
    getSources() {
        return http.get(`${showsEndpoint}`);
    }

    getShowsForSourceId(sourceId) {
        return http.get(showsHeadlinesEndpoint, {params: {sources: sourceId}});
    }
}
```

### > La creación de este archivo corresponde al 22/09/25 (12:25PM) sujeto a futuras actualizaciónes
**Actualizacion 23/09 (22:45PM) : pasos 8 y 9 (avance)**
