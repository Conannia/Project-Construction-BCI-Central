import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../pages/loginPages.vue'; // Perbaiki path sesuai struktur
import HomePage from '../pages/projectList.vue';
import projectForm from '../pages/projectForm.vue'; // Misalnya ini halaman utama

const routes = [
  { path: '/projects', component: HomePage }, // Halaman utama
  { path: '/', component: LoginPage },
  {path: '/projects/new', component: projectForm} // Halaman login
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
