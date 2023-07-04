<template>
  <div class="container">
    <b-button class="getPluginsButton" @click="GetPlayers">Get players</b-button>
    <b-table class="usersTable mt-3" :items="players" :fields="form.fields" responsive="sm" :busy="form.isBusy" outlined>

      <template #table-busy>
        <div class="text-center text-danger my-2">
          <b-spinner class="align-middle"></b-spinner>
          <strong>Loading...</strong>
        </div>
      </template>

      <template #row-details="row">
        <b-card>
          <b-row class="mb-2">
            <b-col sm="3" class="text-sm-right"><b>Health:</b></b-col>
            <b-col>{{ row.item.Health }}</b-col>
            <b-col sm="3" class="text-sm-right"><b>Hunger:</b></b-col>
            <b-col>{{ row.item.foodLevel }}</b-col>
            <b-col sm="3" class="text-sm-right"><b>Level:</b></b-col>
            <b-col>{{ row.item.level }}</b-col>
            <b-col sm="3" class="text-sm-right"><b>GameMode:</b></b-col>
            <b-col>{{ row.item.gamemode }}</b-col>
          </b-row>

          <b-row class="mb-2">
            <b-col sm="3" class="text-sm-right"><b>Position:</b></b-col>
            <b-col>X: {{ row.item.playerLocation.positionX }}</b-col>
            <b-col>Y: {{ row.item.playerLocation.positionY }}</b-col>
            <b-col>Z: {{ row.item.playerLocation.positionZ }}</b-col>
          </b-row>
        </b-card>
      </template>

      <template #cell(actions)="row">
        <b-button size="sm" @click="row.toggleDetails" class="mr-2">
          {{ row.detailsShowing ? 'Hide' : 'Show'}} Details
        </b-button>
      </template>
    </b-table>
  </div>
</template>

<script>
import axios from 'axios';
import connection from "@/globals/SignalRConnection";
import APIConnection from "@/globals/APIConnectionURL";
import authHeader from "@/services/auth-header";
import Vue from 'vue';

export default {
  name: "PlayerComponent",

  data() {
    return {
      toastCount: 0,
      playersTableFields: ['ID', 'username', 'actions'],
      players: [],
      player: {
        id: null,
        username: null
      },
      form: {
        isBusy: false,
        fields: ['ID', 'userName', 'actions'],
      },
      playerDataForTest: null,
      Vue: Vue
      //  userName: null,
      //  error: false
      //}
    }
  },
  methods: {
    GetPlayers(){
      let _this = this;
      _this.form.isBusy = !_this.form.isBusy;
      // Make a request for a user with a given ID
      axios.get(APIConnection.APIConnectionURL + '/gateway/mc/player', {
        headers: authHeader()
      })
          .then(function (response) {
            // handle success
            _this.players = [];
            _this.players = response.data;
            _this.form.isBusy = !_this.form.isBusy;
          })
          .catch(function (error) {
            // handle error
            console.log(error.response.data);
          });
    },
    GetPlayerData: function() {
      let _this = this;
      axios.post(APIConnection.APIConnectionURL + '/gateway/mc/player/getPlayerData', {
        UserName: "Test name"
      }, {
        headers: authHeader()
      }).then(function (response) {
        _this.playerDataForTest = response.data.userName;
          })
          .catch(function (error) {
            _this.playerDataForTest = error;
          });
    },
    makeToast(append, user) {
      this.toastCount++
      this.$bvToast.toast(`User: ${user} triggered an event`, {
        title: 'Notice',
        autoHideDelay: 5000,
        appendToast: append
      })
    }
  },
  async mounted() {
    //await this.StartConnection();
    connection.on("PlayerJoinEvent", (user) => {
      this.makeToast(true, user);
      this.GetPlayers();
    });
    connection.on("PlayerQuitEvent", (user) => {
      this.makeToast(false, user);
      this.GetPlayers();
    });
    connection.on("ChatMessageEvent", (user/*, message*/) => {
      this.makeToast(false, user);
    });

    this.GetPlayers();
  }
}
</script>

<style scoped>

</style>