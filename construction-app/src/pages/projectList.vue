<template>
  <div>
    <h1>Construction Projects</h1>
    <button @click="$router.push('/projects/new')">Create Project</button>
    
    <table border="1" style="width: 100%; margin-top: 10px; border-collapse: collapse;">
      <thead>
        <tr>
          <th>Project ID</th>
          <th>Project Name</th>
          <th>Stage</th>
          <th>Category</th>
          <th>Start Date</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="project in projects" :key="project.id">
          <td>{{ project.id }}</td>
          <td>{{ project.name }}</td>
          <td>{{ project.stage }}</td>
          <td>{{ project.category }}</td>
          <td>{{ project.startDate }}</td>
          <td>
            <button @click="editProject(project.id)">Edit</button>
            <button @click="deleteProject(project.id)" style="margin-left: 5px;">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { ref, onMounted } from "vue";

export default {
  setup() {
    const projects = ref([]);

    // Fetch data dari API JSON Server
    
    const fetchProjects = async () => {
    try {
        const response = await fetch("http://localhost:5030/projects"); // Sesuaikan dengan port backend
        if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
        }
        projects.value = await response.json();
    } catch (error) {
        console.error("Error fetching projects:", error);
    }
    };

    // Aksi ketika klik Edit
    const editProject = (id) => {
      console.log(`Edit project ${id}`);
      //alert(`Edit project ${id}`); // Gantilah dengan routing atau modal edit
      window.location.href = `/projects/edit/${id}`;
    };

    // Aksi ketika klik Delete
    const deleteProject = async (id) => {
      if (!confirm(`Are you sure you want to delete project ${id}?`)) return;

      try {
        await fetch(`http://localhost:5030/projects/${id}`, {
          method: "DELETE",
        });
        projects.value = projects.value.filter((p) => p.id !== id);
      } catch (error) {
        console.error("Error deleting project:", error);
      }
    };

    onMounted(fetchProjects);

    return { projects, editProject, deleteProject };
  },
};
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
}
th, td {
  border: 1px solid black;
  padding: 8px;
  text-align: left;
}
th {
  background-color: #f4f4f4;
}
button {
  padding: 5px 10px;
  border: none;
  cursor: pointer;
}
</style>
