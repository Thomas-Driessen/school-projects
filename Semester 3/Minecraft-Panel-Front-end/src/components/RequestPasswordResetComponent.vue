<template>
  <div class="container">
    <div class="row justify-content-center">
      <b-form class="loginForm" @submit="onSubmit" @reset="onReset" v-if="show">
        <b-form-group
            id="input-group-1"
            label="Email address:"
            label-for="input-1">
          <b-form-input
              id="input-1"
              v-model="form.email"
              type="email"
              required
              placeholder="Enter email"
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

export default {
  name: "RequestPasswordResetComponent",
  data() {
    return {
      form: {
        email: ''
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
      axios.post(APIConnectionURL.APIAuthenticationURL + '/api/main/user/sendPasswordResetEmail', null, {params: {
        email: _this.form.email
      }}).then(function (response) {
        _this.loading = !_this.loading;
        _this.makeToast('Success', response.data, 'success');
      })
      .catch(function (error) {
        console.log(error)
        _this.makeToast('Error', error.data, 'error');
      });
    },
    onReset(evt) {
      evt.preventDefault()
      this.form.email = ''
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
  },
  created() {
    if (this.loggedIn) {
      this.$router.push('/profile');
    }
  }
}
</script>

<style scoped>

</style>