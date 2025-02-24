<template>
  <div style="background: beige;">
    <h1>Edit Project</h1>
    <p>Editing Project ID: {{ projectId }}</p>

    <form @submit.prevent="updateProject">
      <label>Project Name:</label>
      <input type="text" v-model="project.name" required />

      <label>Project Location:</label>
      <input type="text" v-model="project.location" required />

      <label>Project Stage:</label>
      <select v-model="project.stage">
        <option>Concept</option>
        <option>Design & Documentation</option>
        <option>Pre-Construction</option>
        <option>Construction</option>
      </select>

      <label>Project Category:</label>
      <select v-model="project.category">
        <option>Education</option>
        <option>Health</option>
        <option>Office</option>
        <option value="Others">Others</option>
      </select>
      <input
        v-if="project.category === 'Others'"
        type="text"
        v-model="project.otherCategory"
        placeholder="Enter other category"
      />

      <label>Start Date:</label>
      <input type="text" v-model="formattedStartDate" required />

      <label>Project Details:</label>
      <textarea v-model="project.details" rows="4"></textarea>

      <button type="submit">Update Project</button>
      <button type="button" @click="goBack">Cancel</button>
    </form>
  </div>
</template>

<script>
import { ref, computed, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    const projectId = ref(route.params.id);
    const project = ref({
      name: "",
      location: "",
      stage: "",
      category: "",
      otherCategory: "",
      startDate: "",
      details: "",
    });

    // Fetch project data from API
    const fetchProject = async () => {
      try {
        const response = await fetch(`http://localhost:5030/projects/${projectId.value}`);
        if (!response.ok) throw new Error("Project not found");
        project.value = await response.json();
      } catch (error) {
        console.error(error);
        alert("Failed to fetch project");
      }
    };

    // Update project data
    const updateProject = async () => {
      try {
        await fetch(`http://localhost:5030/projects/${projectId.value}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(project.value),
        });
        alert("Project updated successfully!");
        router.push("/");
      } catch (error) {
        console.error(error);
        alert("Failed to update project");
      }
    };

    const goBack = () => router.push("/");


    const formattedStartDate = computed(() => {
      if (!project.value.startDate) return "";
      const date = new Date(project.value.startDate);
      const day = date.getDate().toString().padStart(2, "0"); // 01-31
      const monthAbbr = date.toLocaleString("en-US", { month: "short" }); // Jan, Feb, Mar, ...
      const year = date.getFullYear(); // 2025
      return `${day} ${monthAbbr} ${year}`; // Contoh output: 28Feb2025
    });

    onMounted(fetchProject);

    return { projectId, formattedStartDate, project, updateProject, goBack };
  },
};

</script>
<style scoped>
/* Container Styling */
div {
  max-width: 600px;
  margin: 40px auto;
  background: #ffffff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

/* Form Title */
h1 {
  text-align: center;
  color: #333;
  font-size: 24px;
}

/* Label Styling */
label {
  display: block;
  font-weight: bold;
  margin: 10px 0 5px;
  color: #555;
}

/* Input & Select Styling */
input,
select,
textarea {
  width: 100%;
  padding: 10px;
  margin-bottom: 15px;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 14px;
}

/* Focus Effect */
input:focus,
select:focus,
textarea:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.3);
}

/* Button Styling */
button {
  width: 48%;
  padding: 10px;
  margin-top: 10px;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  cursor: pointer;
  transition: 0.3s;
}

/* Update Button */
button[type="submit"] {
  background: #28a745;
  color: white;
}

/* Cancel Button */
button[type="button"] {
  background: #dc3545;
  color: white;
}

/* Button Hover Effects */
button:hover {
  opacity: 0.8;
}

/* Button Container */
button + button {
  margin-left: 4%;
}

/* Responsive Design */
@media (max-width: 600px) {
  div {
    max-width: 90%;
  }
  button {
    width: 100%;
    margin: 5px 0;
  }
}
</style>

