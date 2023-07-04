import Vue from 'vue'
import VueRouter from 'vue-router'
import Swal from 'sweetalert2'
import Home from '@/views/Home.vue'
import About from '@/views/About.vue'
import Login from '@/views/Login.vue'
import Register from '@/views/Register.vue'
import Profile from '@/views/Profile.vue'
import Server from '@/views/Server.vue'
import PasswordReset from "@/views/PasswordReset";
import ConfirmEmail from "@/views/ConfirmEmail";

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/profile',
    name: 'Profile',
    component: Profile
  },
  {
    path: '/server',
    name: 'Server',
    component: Server
  },
  {
    path: '/resetpassword',
    name: 'ResetPassword',
    component: PasswordReset
  },
  {
    path: '/confirmemail',
    name: 'ConfirmEmail',
    component: ConfirmEmail
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    //component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
    component: About
  }
]

const router = new VueRouter({
  //base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
  const publicPages = ['/login', '/register', '/resetpassword', '/confirmemail', '/'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  // trying to access a restricted page + not logged in
  // redirect to login page
  if (authRequired && !loggedIn) {
    Swal.fire({
      title: 'Unauthorized!',
      text: "You are not allowed to visit this page whilst not logged in.",
      icon: 'error',
      timer: 3000,
      confirmButtonText: 'Ok'
    })
    next('/login');
  } else {
    next();
  }
});

export default router
