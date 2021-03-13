<template>
  <v-data-table
    :hide-default-footer="!defaultFooter"
    :items-per-page="10"
    :headers="headers.list"
    :items="itens"
    :search="search"
    class="elevation-1"
  >
    <template v-if="!defaultItens" v-slot:body="{ items }">
      <tbody>
        <slot name="body" v-bind:itens="items"></slot>
      </tbody>
    </template>
  </v-data-table>
</template>

<script lang="ts">
import Vue from "vue";
import { Headers } from "../models/headers";

export default Vue.extend({
  props: {
    headers: {
      type: Headers
    },
    itens: {
      type: Array,
      default: []
    },
    search: String,
    defaultItens: {
      type: Boolean,
      default: true
    },
    defaultHeaders: {
      type: Boolean,
      default: true
    },
    defaultFooter: {
      type: Boolean,
      default: true
    }
  }
});
</script>


<style >
.v-data-table th {
  color: var(--v-colorTableText-base) !important;
  background-color: var(--v-primary-base) !important;
}

.v-data-table .v-data-table-header__icon {
  color: var(--v-colorTableText-base) !important;
}
</style>>