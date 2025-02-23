import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../pages/loginPages.vue';
import HomePage from '../pages/projectList.vue';
import ProjectForm from '../pages/projectForm.vue';
import EditProject from '../pages/editProject.vue'; // Perbaiki nama

const routes = [
  { path: '/login', component: LoginPage }, // Halaman login
  { path: '/', component: HomePage }, 
  { path: '/projects', component: HomePage }, // Halaman daftar proyek
  { path: '/projects/new', component: ProjectForm }, // Halaman tambah proyek
  { 
    path: '/projects/edit/:id', 
    name: 'EditProject',
    component: EditProject,
    props: true // Mengizinkan ID dikirim sebagai prop
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
