<template>
  <div class="container">
    <div class="server row">
      <div class="col-lg-6">
        <PlayerComponent />
      </div>
      <div class="col-lg-6">
        <PluginComponent />
      </div>
      <div class="col-lg-8">
        <ChatComponent />
      </div>
    </div>
  </div>
</template>

<script>
import PlayerComponent from "@/components/PlayerComponent";
import PluginComponent from "@/components/PluginComponent";
import ChatComponent from "@/components/ChatComponent";

export default {
  name: "Server",
  components: {
    PlayerComponent,
    PluginComponent,
    ChatComponent
  }
}
</script>

<style scoped>

</style>