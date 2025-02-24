import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../pages/loginPages.vue';
import HomePage from '../pages/projectList.vue';
import ProjectForm from '../pages/projectForm.vue';
import EditProject from '../pages/editProject.vue'; // Perbaiki nama
import ViewProject from '../pages/viewProject.vue';

const routes = [
  { path: '/login', component: LoginPage }, 
  { path: '/', component: HomePage }, 
  { path: '/projects', component: HomePage }, 
  { path: '/projects/new', component: ProjectForm }, 
  { path: '/projects/edit/:id', name: 'EditProject', component: EditProject, props: true },
  { path: '/projects/view/:id', name: 'ViewProject', component: ViewProject, props: true }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

// 🛡️ Navigation Guard: Cek sessionStorage sebelum masuk halaman kecuali login
router.beforeEach((to, from, next) => {
  const userId = sessionStorage.getItem("userId");
  
  if (!userId && to.path !== "/login") {
    next("/login"); // Redirect ke login jika belum login
  } else {
    next(); // Lanjutkan ke halaman yang diminta
  }
});

export default router;