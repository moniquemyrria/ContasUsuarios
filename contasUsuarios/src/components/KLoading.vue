<template>
  <v-dialog v-model="dialog" persistent width="350">
    <v-card color="#1867C0" style="text-align:center;" dark>
      <v-card-text>
        <br />
        {{title}}
        <br />
        <br />
        <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: {
    title: String,
    dialog: Boolean
  }
});
</script>