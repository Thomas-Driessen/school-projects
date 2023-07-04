<template>
  <div>
    <transition name="fade" mode="out-in">
      <LoginComponent v-if="showLoginForm"/>
      <RequestPasswordResetComponent v-if="showResetForm" />
    </transition>
    <div class="row justify-content-center resetExplanation">
      <p v-if="showLoginForm">Forgot your password? Click <a class="changeActiveForm" @click="changeActiveForm">here</a> to reset it.</p>
      <p v-if="showResetForm"><a class="changeActiveForm" @click="changeActiveForm">Click</a> to show the login form!</p>
    </div>
  </div>
</template>

<script>
import LoginComponent from '@/components/LoginComponent.vue'
import RequestPasswordResetComponent from "@/components/RequestPasswordResetComponent";

export default {
  name: "Login",
  components: {
    LoginComponent,
    RequestPasswordResetComponent
  },
  data() {
    return {
      showLoginForm: true,
      showResetForm: false
    }
  },
  methods: {
    changeActiveForm() {
      this.showLoginForm = !this.showLoginForm;
      this.showResetForm = !this.showResetForm;
    }
  }
}
</script>

<style scoped>
.changeActiveForm {
  color: dodgerblue;
}
.resetExplanation {
  margin-top: 2%;
}
.fade-enter-active,
.fade-leave-active {
  transition-duration: 0.3s;
  transition-property: opacity;
  transition-timing-function: ease;
}

.fade-enter,
.fade-leave-active {
  opacity: 0
}
</style>