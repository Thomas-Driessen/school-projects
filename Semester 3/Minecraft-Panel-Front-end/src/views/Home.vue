<template>
  <div class="home">
    <h2>Welcome to the minecraft panel frontend!</h2>
    <p v-if="currentUser">Visit the <router-link class="loginRedirect" to="/server">server page</router-link> to explore the functionality!</p>
    <p v-else>Please <router-link class="loginRedirect" to="/login">login</router-link> to use the panel in full.</p>
  </div>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'Home',
  components: {

  },
  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    }
  }
}
</script>
