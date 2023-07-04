<template>
  <div class="container">
      <div class="row justify-content-center">
        <b-form @submit="onSubmit" @reset="onReset" v-if="show">
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

        <b-form-group id="input-group-2" label="Your password:" label-for="input-2">
            <b-form-input
            id="input-2"
            v-model="form.password"
            required
            placeholder="Enter password"
            ></b-form-input>
        </b-form-group>

        <b-button type="submit" variant="primary">Submit</b-button>
        <b-button type="reset" variant="danger">Reset</b-button>
      </b-form>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import APIConnectionURL from "@/globals/APIConnectionURL";

export default {
  name: "AccountRegistrationComponent",
  data() {
    return {
      form: {
        email: '',
        name: '',
        password: ''
      },
      show: true
    }
  },
  methods: {
    onSubmit(evt) {
      //let _this = this;
      evt.preventDefault()
      axios.post(APIConnectionURL.APIConnectionURL + '/gateway/main/user/registerAccount', {
        Email: this.form.email,
        Name: this.form.name,
        Password: this.from.password
      }).then(function (response) {
        console.log(response)
        //_this.playerDataForTest = response.data.userName;
      })
      .catch(function (error) {
        console.log(error)
        //_this.playerDataForTest = error;
      });
    },
    onReset(evt) {
      evt.preventDefault()
      this.form.email = ''
      this.form.name = ''
      this.form.password = ''
      // Trick to reset/clear native browser form validation state
      this.show = false
      this.$nextTick(() => {
        this.show = true
      })
    }
  }
}
</script>

<style scoped>

</style>