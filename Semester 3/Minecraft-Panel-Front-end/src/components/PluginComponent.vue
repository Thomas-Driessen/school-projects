<template>
  <div class="container">
    <b-button class="getPluginsButton" @click="GetPlugins">Get plugins</b-button>
    <b-table class="pluginTable mt-3" :items="plugins" :fields="form.fields" responsive="sm" :busy="form.isBusy" outlined>
      <template #table-busy>
        <div class="text-center text-danger my-2">
          <b-spinner class="align-middle"></b-spinner>
          <strong>Loading...</strong>
        </div>
      </template>

      <template #cell(actions)="row">
        <b-button size="sm" @click="EnablePlugin(row.item, row.index, $event.target)" class="mr-1">
          Enable
        </b-button>
        <b-button size="sm" @click="DisablePlugin(row.item, row.index, $event.target)" class="mr-1">
          Disable
        </b-button>
      </template>
    </b-table>
  </div>
</template>

<script>
import axios from "axios";
import APIConnection from "@/globals/APIConnectionURL";
import authHeader from "@/services/auth-header";
import Swal from 'sweetalert2'

export default {
  name: "PluginComponent.vue",
  data() {
    return {
      plugins: [],
      player: {
        id: null,
        username: null
      },
      form: {
        isBusy: false,
        fields: ['pluginName', 'isEnabled', 'actions'],
      }
    }
  },
  methods: {
    GetPlugins() {
      let _this = this;
      _this.form.isBusy = !_this.form.isBusy;
      // Make a request for a user with a given ID
      axios.get(APIConnection.APIConnectionURL + '/gateway/mc/server/getPlugins', { headers: authHeader() })
          .then(function (response) {
            // handle success
            _this.plugins = response.data.Plugins;
            _this.form.isBusy = !_this.form.isBusy;
          })
          .catch(function (error) {
            // handle error
            console.log(error);
            Swal.fire({
              title: 'Error!',
              text: "Failed to fetch data!",
              icon: 'error',
              confirmButtonText: 'Ok'
            })
          });
    },
    DisablePlugin(item) {
      //let _this = this;
      axios.post(APIConnection.APIConnectionURL + '/gateway/mc/server/disablePlugin', {
        headers: authHeader(),
        PluginName: item.pluginName
      })
          .then(function (response) {
            Swal.fire({
              title: 'Success!',
              text: response.data,
              icon: 'success',
              confirmButtonText: 'Ok'
            })
          })
          .catch(function (error) {
            console.log(error.response.data);
            Swal.fire({
              title: 'Error!',
              text: error.response.data,
              icon: 'error',
              confirmButtonText: 'Ok'
            })
          });
    },
    EnablePlugin(item) {
      //let _this = this;
      axios.post(APIConnection.APIConnectionURL + '/gateway/mc/server/enablePlugin', {
        headers: authHeader(),
        PluginName: item.pluginName
      })
          .then(function (response) {
            Swal.fire({
              title: 'Success!',
              text: response.data,
              icon: 'success',
              confirmButtonText: 'Ok'
            })
          })
          .catch(function (error) {
            console.log(error.response.data);
            Swal.fire({
              title: 'Error!',
              text: error.response.data,
              icon: 'error',
              confirmButtonText: 'Ok'
            })
          });
    },
  },
  mounted: function() {
    this.GetPlugins();
  }
}
</script>

<style scoped>

</style>