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

            <b-form-group id="input-group-2" label="Your password:" label-for="input-2">
                <b-form-input
                id="input-2"
                v-model="form.password"
                required
                type="password"
                placeholder="Enter password"
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
export default {
  name: "AccountRegistration",
  data() {
    return {
      form: {
        email: '',
        password: ''
      },
      show: true,
      loading: false
    }
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault()
      let _this = this;
      _this.loading = true;
      /*_this.$validator.validateAll().then(isValid => {
        if (!isValid) {
          this.loading = false;
          return;
        }*/

      if (_this.form.email && _this.form.password) {
        _this.$store.dispatch('auth/login', _this.form).then(
            () => {
              _this.loading = false;
              _this.$router.push('/profile');
            },
            error => {
              _this.loading = false;
              _this.makeToast('Error', (error.response && error.response.data) ||
                  error.message ||
                  error.toString(), 'danger');
            }
        );
      }
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