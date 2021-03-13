<template >
  <v-app>
    <router-view></router-view>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import { Storage } from "./configuration/storage";
import { Display } from "./configuration/display";
export default Vue.extend({
  name: "App",
  data: () => ({}),
  methods: {
    resize(): any {
      console.error("werijf nkm");
    }
  },
  created() {
    // window.onresize = resize;

    // function resize() {
    //   window.location.reload();
    // }
    // this.$router.push("/Login");
    //if (!Storage.get("Logado")) {
      //Storage.salvar("Logado", "false");
     // Storage.salvar("Session", "");
     // Storage.salvar("User", "");
      this.$router.push("/usuarios");
    //}
  }
});
</script>
