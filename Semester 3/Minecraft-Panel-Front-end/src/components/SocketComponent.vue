<template>
  <div class="socket">
    <h2>Players: </h2>
    <div class="playerList">
      <div class="player row" v-for="player in onlinePlayers" :key="player.Name">
        <div class="col-6">
          {{ player.Name }}
        </div>
      </div>
    </div>
    <ul id="example-2">
      <li v-for="message in socketMessages" :key="message.message">
        {{ message.message }}
      </li>
    </ul>
    <input type="text" v-model="inputMsg">
    <button @click="SendMessage">Send</button>
  </div>
</template>

<script>
import connection from "@/globals/SignalRConnection";

export default {
  name: "SocketComponent",
  data() {
    return {
      xd: connection,
      onlinePlayers: [],
      socketMessages: [],
      inputMsg: ""
    }
  },
  methods: {
    async StartConnection() {
    },
    async PushMessage(message) {
      this.socketMessages.push(message);
    },
    async PlayerJoined() {

    },
    async PlayerQuit() {

    },
    async SendMessage() {
      await connection.invoke("ReceiveMessage", "JSCLIENT", this.inputMsg);
    }
  },
  async mounted() {
    //await this.StartConnection();
    connection.on("PlayerJoinEvent", (user) => {
      let newPlayer = { "Name": user }
      this.onlinePlayers.push(newPlayer);
    });
    connection.on("PlayerQuitEvent", (user) => {
      let _this = this;
      this.onlinePlayers.forEach(async function(player) {
        if (player.Name === user)
        {
          const playerToRemove = _this.onlinePlayers.indexOf(player);
          _this.onlinePlayers.splice(playerToRemove, 1);
        }
      })
    });
    connection.on("ReceiveMessage", (user, message) => {
      const encodedMsg = `${user} says ${message}`;
    });
  }
}
</script>

<style scoped>

</style>