<template>
  <div style="width: 350px; margin: auto; padding: 20px; text-align: center; border-radius: 8px; background: #f4f4f4;">
    <h2>Welcome Back!</h2>
    <input v-model="email" type="email" placeholder="Enter Email Address..." style="width: 90%; padding: 10px; margin: 10px 0;">
    <input v-model="password" type="password" placeholder="Password" style="width: 90%; padding: 10px; margin: 10px 0;">
    <button @click="login" style="width: 100%; padding: 10px; background: blue; color: white; border: none; border-radius: 5px;">Login</button>
    <p v-if="errorMessage" style="color: red;">{{ errorMessage }}</p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      email: '',
      password: '',
      errorMessage: ''
    };
  },
  methods: {
    login() {
      fetch(`http://localhost:3000/users?email=${this.email}&password=${this.password}`)
        .then(response => response.json())
        .then(data => {
          if (data.length > 0) {
            // Login sukses, redirect ke /projects/new
            this.$router.push('/projects');
          } else {
            this.errorMessage = "Email atau password salah!";
          }
        })
        .catch(error => console.error("Error:", error));
    }
  }
};
</script>
