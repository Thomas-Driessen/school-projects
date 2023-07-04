<template>
  <div>

  </div>
</template>

<script>
import axios from "axios";
import APIConnectionURL from "@/globals/APIConnectionURL";
import authHeader from "@/services/auth-header";
import Swal from "sweetalert2";

export default {
  name: "EmailConfirmComponent",
  data() {
    return {
      emailToken: null,
      show: true,
      loading: false
    }
  },
  methods: {
    confirmEmail() {
      let _this = this;
      _this.loading = true;
      axios.post(APIConnectionURL.APIAuthenticationURL + '/api/main/user/confirmEmail', null, {headers: authHeader(), params: {
          emailConfirmToken: _this.emailToken
        }}).then(function (response) {
        _this.loading = !_this.loading;
        Swal.fire({
          title: 'Success!',
          text: response.data,
          icon: 'success',
          confirmButtonText: 'Ok'
        })
        _this.$router.push("/profile");
      })
          .catch(function (error) {
            console.log(error)
            Swal.fire({
              title: 'Error!',
              text: "Something went wrong!",
              icon: 'error',
              confirmButtonText: 'Ok'
            })
          });
    },
    makeToast(title, content, variant = null) {
      this.$bvToast.toast(content, {
        title: title,
        variant: variant,
        solid: true
      })
    }
  },
  mounted() {
    if (this.$route.query.token !== undefined) {
      this.emailToken = this.$route.query.token;
      this.confirmEmail();
    }
    else {
      console.log("empty");
    }
  }
}
</script>

<style scoped>

</style>