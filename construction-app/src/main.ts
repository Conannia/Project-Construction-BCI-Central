import { createApp } from 'vue';
import App from './App.vue';
import router from './router/index.js'; // Pastikan path benar

const app = createApp(App);
app.use(router);
app.mount('#app');
