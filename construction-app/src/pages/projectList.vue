<template>
  <div style="background: beige; display: flex; flex-direction: column; align-items: center; justify-content: center; text-align: center;">
    <h1 style="font-size: 28px; font-weight: bold; margin-bottom: 20px;">Construction Projects</h1>
    <button @click="$router.push('/projects/new')" 
      style="background: #87CEEB; color: white; padding: 12px 20px; border: none; border-radius: 5px; font-size: 16px; font-weight: bold; cursor: pointer; transition: background 0.3s;">
      Create Project
    </button>
    
    <table border="1" style="width: 100%; margin-top: 10px; border-collapse: collapse; box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);">
    <thead >
      <tr style="background: #4CAF50; color: black;">
        <th style="padding: 10px; text-align: left;">Project ID</th>
        <th style="padding: 10px; text-align: left;">Project Name</th>
        <th style="padding: 10px; text-align: left;">Stage</th>
        <th style="padding: 10px; text-align: left;">Category</th>
        <th style="padding: 10px; text-align: left;">Start Date</th>
        <th style="padding: 10px; text-align: center;">Actions</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="project in projects" :key="project.id" style="background: #f9f9f9; border-bottom: 1px solid #ddd;">
        <td style="padding: 8px;">{{ project.id }}</td>
        <td style="padding: 8px;">{{ project.name }}</td>
        <td style="padding: 8px;">{{ project.stage }}</td>
        <td style="padding: 8px;">{{ project.category }}</td>
        <td style="padding: 8px;">{{ formatDate(project.startDate) }}</td>
        <td style="padding: 8px; text-align: center;">
          <button @click="viewProject(project.id)" style="background: #2196F3; color: white; border: none; padding: 6px 10px; margin: 3px; border-radius: 4px; cursor: pointer;">View</button>
          <button @click="editProject(project.id)" style="background: #FFC107; color: white; border: none; padding: 6px 10px; margin: 3px; border-radius: 4px; cursor: pointer;">Edit</button>
          <button @click="deleteProject(project.id)" style="background: #F44336; color: white; border: none; padding: 6px 10px; margin: 3px; border-radius: 4px; cursor: pointer;">Delete</button>
        </td>
      </tr>
    </tbody>
  </table>
  </div>
  <div style="background: beige; height: 10vh"></div>
</template>

<script>
import { ref, computed, onMounted } from "vue";

export default {
  setup() {
    const projects = ref([]);


    
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

    const viewProject = (id) => {
      console.log(`Edit project ${id}`);
      //alert(`Edit project ${id}`); // Gantilah dengan routing atau modal edit
      window.location.href = `/projects/view/${id}`;
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

    const formatDate = (dateString) => {
      if (!dateString) return "";
      const date = new Date(dateString);
      const day = date.getDate().toString().padStart(2, "0"); // 01-31
      const monthAbbr = date.toLocaleString("en-US", { month: "short" }); // Jan, Feb, Mar, ...
      const year = date.getFullYear(); // 2025
      return `${day} ${monthAbbr} ${year}`; // Contoh output: 28 Feb 2025
    };

    onMounted(fetchProjects);

    return { projects, formatDate, viewProject, editProject, deleteProject };
  },
};
</script>

<style scoped>
h1{
display: center;
}
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
