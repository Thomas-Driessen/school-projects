<template>
  <div class="container">
    <div class="welcomeMessage">
      <h3>
        Welcome {{profileInfo.username}}!
      </h3>
    </div>
    <div>
      <b-list-group>
        <b-list-group-item>Username: {{profileInfo.username}}</b-list-group-item>
        <b-list-group-item>Email: {{profileInfo.email}}</b-list-group-item>
        <b-list-group-item>Email confirmed: {{profileInfo.emailConfirmed}}</b-list-group-item>
      </b-list-group>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import APIConnection from "@/globals/APIConnectionURL";
import authHeader from "@/services/auth-header";
import Swal from "sweetalert2";

export default {
name: "ProfileOverviewComponent",
  data() {
    return {
      profileInfo: {
        username: "Loading...",
        email: "Loading...",
        emailConfirmed: "Loading..."
      }
    }
  },
  methods: {
    async getUser() {
      let _this = this;
      await axios.get(APIConnection.APIAuthenticationURL + '/api/main/user/getUser', { headers: authHeader() })
          .then(function (response) {
            // handle success
            _this.profileInfo.username = response.data.name;
            _this.profileInfo.email = response.data.email;
            _this.profileInfo.emailConfirmed = response.data.emailConfirmed;
          })
          .catch(function (error) {
            // handle error
            console.log(error);
            Swal.fire({
              title: 'Error!',
              text: "Failed to fetch data!",
              icon: 'error',
              confirmButtonText: 'Ok'
            })
          });
    }
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    }
  },
  created() {
    if (!this.loggedIn) {
      this.$router.push('/login');
    }
  },
  async mounted() {
    await this.getUser();
  }
}
</script>

<style scoped>

</style>