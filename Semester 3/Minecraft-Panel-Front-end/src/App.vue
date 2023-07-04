<template>
  <div id="app">
    <div id="nav">
      <b-button class="toggleSideBarMenu" v-b-toggle.sidebar-backdrop><b-icon-list /></b-button>
      <div class="currentPage">
        <h3> {{this.$router.currentRoute.name}}</h3>
      </div>
    </div>

    <b-sidebar id="sidebar-backdrop" title="Navigation" :backdrop-variant="variant" backdrop shadow>
      <div class="px-3 py-2">
        <nav class="mb-3">
          <b-nav vertical>
            <b-nav-item>
              <router-link class="homeRedirect" to="/">Home</router-link>
            </b-nav-item>
            <template v-if="!currentUser">
              <b-nav-item>
                <router-link class="loginRedirect" to="/login">Login</router-link>
              </b-nav-item>
              <b-nav-item>
                <router-link class="registerRedirect" to="/register">Account Registration</router-link>
              </b-nav-item>
            </template>
            <b-nav-item v-if="currentUser">
              <router-link class="profileRedirect" to="/profile">Profile</router-link>
            </b-nav-item>
            <b-nav-item v-if="currentUser">
              <router-link class="serverRedirect" to="/server">Server</router-link>
            </b-nav-item>
            <b-nav-item v-if="currentUser">
              <router-link class="aboutRedirect" to="/about">About</router-link>
            </b-nav-item>
          </b-nav>
        </nav>
      </div>
      <template #footer="{ hide }" v-if="currentUser">
        <div class="d-flex bg-dark text-light align-items-center px-3 py-2">
          <strong class="mr-auto">Account actions</strong>
          <LogoutComponent :hideProp="hide"/>
        </div>
      </template>
    </b-sidebar>

    <transition name="fade" mode="out-in">
      <!-- VIEW -->
      <router-view />
      <!-- VIEW -->
    </transition>
  </div>
</template>

<script>
import LogoutComponent from "@/components/LogoutComponent";

export default {
  components: {LogoutComponent},
  data() {
    return {
      variant: 'secondary',
      components: {
        LogoutComponent
      }
    }
  },
  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
  text-align: left;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

.currentPage {
  text-align: center;
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
