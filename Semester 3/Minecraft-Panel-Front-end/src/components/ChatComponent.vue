<template>
  <div class="container overflow-auto">
    <b-button class="getChatMessagesButton" @click="getChatMessagesManually">Get messages</b-button>
    <b-table class="chatMessagesTable mt-3" :items="chatMessages" :fields="form.fields" :current-page="form.currentPage" :per-page="0"
             responsive="sm" :busy="form.isBusy" outlined>

      <template #table-busy>
        <div class="text-center text-danger my-2">
          <b-spinner class="align-middle"></b-spinner>
          <strong>Loading...</strong>
        </div>
      </template>
    </b-table>
    <b-pagination v-model="form.currentPage" :total-rows="form.rows" :per-page="form.perPage"></b-pagination>
  </div>
</template>

<script>
import axios from "axios";
import APIConnection from "@/globals/APIConnectionURL";
import authHeader from "@/services/auth-header";

export default {
  name: "ChatComponent.vue",
  data() {
    return {
      chatMessages: [],
      form: {
        rows: 0,
        currentPage: 1,
        perPage: 10,
        isBusy: false,
        fields: ['senderName', 'content', 'timeStamp', 'actions']
      },
      playerDataForTest: null,
    }
  },
  methods: {
    async getChatMessagesWithPagination() {
      let _this = this;
      //_this.form.isBusy = !_this.form.isBusy;
      // Make a request for a user with a given ID
      await axios.post(APIConnection.APIConnectionURL + '/gateway/mc/chat/getChatMessagesWithAmount', {
        Skip: (_this.form.currentPage - 1) * _this.form.perPage,
        Take: _this.form.perPage
      }, {
        headers: authHeader()
      })
          .then(function (response) {
            // handle success
            _this.chatMessages = [];
            _this.chatMessages = response.data;
            //_this.form.isBusy = !_this.form.isBusy;
          })
          .catch(function (error) {
            // handle error
            console.log(error.response.data);
          });
    },
    getTotalMessageCount() {
      let _this = this;
      axios.get(APIConnection.APIConnectionURL + '/gateway/mc/chat/getTotalMessageCount', {
        headers: authHeader()
      })
          .then(function (response) {
            _this.form.rows = response.data;
          })
          .catch(function (error) {
            // handle error
            console.log(error.response.data);
          });
    },
    async getChatMessagesManually() {
      this.form.rows = this.getTotalMessageCount();
      await this.getChatMessagesWithPagination();
    }
  },
  async mounted() {
    this.form.rows = this.getTotalMessageCount();
    await this.getChatMessagesWithPagination();
  },
  watch: {
    'form.currentPage': {
      handler: function () {
        this.getChatMessagesWithPagination();
      }
    }
  }
}
</script>

<style scoped>

</style>