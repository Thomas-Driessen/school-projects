<template>
  <b-button size="sm" @click="Logout(); hideProp">Logout</b-button>
</template>

<script>
export default {
name: "LogoutComponent.vue",
  data() {
    return {
      hideProp: null,
      show: true,
      loading: false
    }
  },
  methods: {
    Logout() {
      this.$store.dispatch('auth/logout');
      this.$router.push('/login');
    }
  }
}
</script>

<style scoped>

</style>