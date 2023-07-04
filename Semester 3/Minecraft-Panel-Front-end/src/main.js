import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store';
import { BootstrapVue, BootstrapVueIcons, LayoutPlugin, DropdownPlugin, TablePlugin, ModalPlugin, CardPlugin, VBScrollspyPlugin, ListGroupPlugin, ToastPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import "./style/main.css"

Vue.use(BootstrapVue)
Vue.use(BootstrapVueIcons)
Vue.use(LayoutPlugin)
Vue.use(ModalPlugin)
Vue.use(CardPlugin)
Vue.use(VBScrollspyPlugin)
Vue.use(DropdownPlugin)
Vue.use(TablePlugin)
Vue.use(ListGroupPlugin)
Vue.use(ToastPlugin)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
