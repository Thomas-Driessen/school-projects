<template>
  <div class="register">
    <RegisterComponent />
  </div>
</template>

<script>
// @ is an alias to /src
//import HelloWorld from '@/components/HelloWorld.vue'
import RegisterComponent from '@/components/RegisterComponent.vue'

export default {
  name: 'Register',
  components: {
    RegisterComponent
  }
}
</script>
