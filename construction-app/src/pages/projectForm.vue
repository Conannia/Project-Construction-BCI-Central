<template>
  <div class="container mx-auto mt-10">
    <h1 class="text-2xl font-bold">{{ isEdit ? 'Edit Project' : 'Create Project' }}</h1>
    <form @submit.prevent="saveProject">
      <input v-model="project.name" type="text" placeholder="Project Name" class="input-field" required />
      <select v-model="project.stage" class="input-field" required>
        <option value="Concept">Concept</option>
        <option value="Design & Documentation">Design & Documentation</option>
        <option value="Pre-Construction">Pre-Construction</option>
        <option value="Construction">Construction</option>
      </select>
      <button type="submit" class="btn-primary">Save</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const project = ref({ name: '', stage: '' });
const isEdit = false; 

const saveProject = () => {
  alert("Project saved!");
  router.push('/projects');
};
</script>
