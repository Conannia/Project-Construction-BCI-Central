<template>
  <div style="background: beige; margin: 1% 10%;">
    <h1 style="text-align: center;">Construction Project Details – Create Page</h1>
    <form @submit.prevent="submitProject" style="margin: 1% 10%; justify-self: anchor-center; width: 50vw;">
      <label>Project ID:</label>
      <input type="text" v-model="project.id" disabled />

      <label>Project Name:</label>
      <input type="text" v-model="project.name" maxlength="200" required />

      <label>Project Location:</label>
      <textarea v-model="project.location" maxlength="500" required></textarea>

      <label>Project Stage:</label>
      <select v-model="project.stage" required>
        <option value="Concept">Concept</option>
        <option value="Design & Documentation">Design & Documentation</option>
        <option value="Pre-Construction">Pre-Construction</option>
        <option value="Construction">Construction</option>
      </select>

      <label>Project Category:</label>
      <select v-model="project.category" @change="checkOtherCategory" required>
        <option value="Education">Education</option>
        <option value="Health">Health</option>
        <option value="Office">Office</option>
        <option value="Others">Others</option>
      </select>
      <input
        v-if="project.category === 'Others'"
        type="text"
        v-model="project.otherCategory"
        placeholder="Enter other category"
      />

      <label>Project Construction Start Date:</label>
      <input type="date" v-model="project.startDate" required />

      <label>Project Details/Description:</label>
      <textarea v-model="project.details" maxlength="2000"></textarea>

      <label>Project Creator ID (UserID):</label>
      <input type="text" v-model="project.creatorId" readonly />

      <button type="submit">Create Project</button>
      <button type="button" @click="$router.push('/')">Cancel</button>
    </form>
  </div>
</template>

<script>
import { ref, onMounted } from "vue";

export default {
  setup() {
    const project = ref({
      id: generateProjectID(),
      name: "",
      location: "",
      stage: "Concept",
      category: "Education",
      otherCategory: "",
      startDate: "",
      details: "",
      creatorId: "",
    });

    function generateProjectID() {
      return Math.floor(100000 + Math.random() * 900000).toString();
    }

    function checkOtherCategory() {
      if (project.value.category !== "Others") {
        project.value.otherCategory = "";
      }
    }

    async function submitProject() {
      const newProject = {
        id: project.value.id,
        name: project.value.name,
        location: project.value.location,
        stage: project.value.stage,
        category:
          project.value.category === "Others"
            ? project.value.otherCategory
            : project.value.category,
        startDate: project.value.startDate,
        details: project.value.details,
        creatorId: project.value.creatorId,
      };

      try {
        await fetch("http://localhost:5030/projects", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(newProject),
        });
        alert("Project Created Successfully!");
        window.location.href = "/projects";
      } catch (error) {
        console.error("Error creating project:", error);
      }
    }


    onMounted(() => {
      // Ambil userId dari sessionStorage dan set ke project.creatorId
      project.value.creatorId = sessionStorage.getItem("userId") || "Unknown";
    });

    return { project, checkOtherCategory, submitProject };
  },
};
</script>

<style scoped>
form {
  display: flex;
  flex-direction: column;
  max-width: 500px;
}
label {
  margin-top: 10px;
  font-weight: bold;
}
input,
textarea,
select {
  padding: 8px;
  margin-top: 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
}
button {
  margin-top: 15px;
  padding: 10px;
  border: none;
  background-color: #007bff;
  color: white;
  cursor: pointer;
}
button:hover {
  background-color: #0056b3;
}
</style>
