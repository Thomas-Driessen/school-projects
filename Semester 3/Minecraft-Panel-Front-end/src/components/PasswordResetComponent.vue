<template>
  <div class="container">
    <div class="justify-content-center">
      <b-form class="loginForm" @submit="onSubmit" @reset="onReset" v-if="show">
        <b-form-group
            id="input-group-1"
            label="New password:"
            label-for="input-1">
          <b-form-input
              id="input-1"
              v-model="form.password"
              required
              type="password"
              placeholder="Enter password"
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2" label="Confirm new password:" label-for="input-2">
          <b-form-input
              id="input-2"
              v-model="form.confirmPassword"
              required
              type="password"
              placeholder="Confirm password"
          ></b-form-input>
        </b-form-group>

        <b-button type="submit" variant="primary">Submit</b-button>
        <b-button type="reset" variant="danger">Reset</b-button>

        <div class="text-center text-danger my-2" v-if="loading">
          <b-spinner class="align-middle"></b-spinner>
          <strong>Loading...</strong>
        </div>
      </b-form>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import APIConnectionURL from "@/globals/APIConnectionURL";
import authHeader from "@/services/auth-header";

export default {
  name: "PasswordResetComponent",
  data() {
    return {
      form: {
        password: '',
        confirmPassword: ''
      },
      show: true,
      loading: false
    }
  },
  methods: {
    onSubmit(evt) {
      let _this = this;
      evt.preventDefault()
      _this.loading = true;
      axios.post(APIConnectionURL.APIAuthenticationURL + '/api/main/user/resetPassword', {
        NewPassword: _this.form.password,
        NewPasswordConfirmation: _this.form.confirmPassword
      }, {
        headers: authHeader()
      }).then(function (response) {
        _this.loading = !_this.loading;
        _this.makeToast('Success', response.data, 'success');
      }).catch(function (error) {
        _this.loading = !_this.loading;
        console.log(error.response.data)
        _this.makeToast('Error', error.response.data, 'danger');
      });
    },
    onReset(evt) {
      evt.preventDefault()
      this.form.confirmPassword = ''
      this.form.password = ''
      // Trick to reset/clear native browser form validation state
      this.show = false
      this.$nextTick(() => {
        this.show = true
      })
    },
    makeToast(title, content, variant = null) {
      this.$bvToast.toast(content, {
        title: title,
        variant: variant,
        solid: true
      })
    }
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    }
  }
}
</script>

<style scoped>

</style>