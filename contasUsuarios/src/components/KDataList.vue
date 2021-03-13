<template>
  <div>
    <k-dialog
      :title="titleDialogAction"
      :dialog="dialogDialogAction"
      @fecharDialog="fecharDialogAction"
      width="500"
    >
      <template v-slot:text>
        <div style="text-align: center;">
          <v-btn
            style="margin-top: 5vh;"
            :color="action.color"
            v-for="action in itemItemAction[itemHeaderAction.value]"
            :key="action.text"
            :width="action.width"
            @click="$emit(action.action, itemItemAction)"
          >{{action.text}}</v-btn>
        </div>
      </template>
    </k-dialog>

    <k-data-table v-if="!isMobile" :headers="headers" :itens="itens" :defaultItens="false" :search="search">
      <template v-slot:body="{ itens }">
        <tr v-for="(item,index) in itens" :key="item.name" @click="itemClick(item)">
          <td :style="'text-align: '+header.align" v-for="header in headers.list" :key="header.text">
            <slot :name="header.value" v-bind:item="item" v-bind:index="index">
              <span v-if="header.isIcon">
                <v-icon
                  small
                  v-for="icon in item[header.value]"
                  :key="icon.icon"
                  :color="icon.color"
                  :title="icon.title"
                >{{icon.icon}}</v-icon>
              </span>
              <span v-else-if="header.isAction">
                <v-icon
                  small
                  v-for="icon in item[header.value]"
                  :key="icon.icon"
                  :color="icon.color"
                  @click="$emit(icon.action, item)"
                >{{icon.icon}}</v-icon>
              </span>
              <span v-else>{{item[header.value]}}</span>
            </slot>
          </td>
        </tr>
      </template>
    </k-data-table>

    <div v-else-if="orientation == 'Vertical'">
      <v-card
        @click="mobileActionsShowDialog? actionItem(item): itemClick(item)"
        style="margin-top: 5px;"
        v-for="item in itens"
        :key="item.key"
      >
        <v-card-text>
          <v-row v-for="header in headers.list" :key="header.text">
            <slot :name="header.value" v-bind:item="item">
              <v-col x6 v-if="header.isIcon">
                <b>{{header.text}}:</b>
                <v-icon
                  v-for="icon in item[header.value]"
                  :key="icon.icon"
                  :color="icon.color"
                  :title="icon.title"
                >{{icon.icon}}</v-icon>
              </v-col>
              <v-col x12 v-else-if="header.isAction">
                <v-row v-if="!mobileActionsShowDialog" style="text-align: center;">
                  <v-col v-for="icon in item[header.value]" :key="icon.icon">
                    <v-icon :color="icon.color" @click="$emit(icon.action, item)">{{icon.icon}}</v-icon>
                  </v-col>
                </v-row>
              </v-col>
              <v-col x6 v-else>
                <b>{{header.text}}:</b>
                {{item[header.value]}}
              </v-col>
            </slot>
          </v-row>
        </v-card-text>
      </v-card>
      <v-btn
        v-if="mobileFunctionAdd"
        @click="functionAdd()"
        style="margin-bottom: 10vh;"
        absolute
        fab
        bottom
        fixed
        right
        color="primary"
      >
        <v-icon color="colorPrimaryText">{{mobileIconAdd}}</v-icon>
      </v-btn>
    </div>
    <div v-else>
      <v-row>
        <v-col cols="1">
          <v-icon @click="listaHorizontalLeft()">mdi-arrow-left-bold</v-icon>
        </v-col>
        <v-spacer></v-spacer>
        <v-col cols="2">
          <v-icon @click="listaHorizontalRight()">mdi-arrow-right-bold</v-icon>
        </v-col>
      </v-row>
      <v-card
        @click="mobileActionsShowDialog? actionItem(itemListHorizontal): itemClick(itemListHorizontal)"
        style="margin-top: 5px;"
      >
        <v-card-text>
          <v-row v-for="header in headers.list" :key="header.text
          ">
            <slot :name="header.value" v-bind:item="item">
              <v-col x6 v-if="header.isIcon">
                <b>{{header.text}}:</b>
                <v-icon
                  v-for="icon in itemListHorizontal[header.value]"
                  :key="icon.icon"
                  :color="icon.color"
                  :title="icon.title"
                >{{icon.icon}}</v-icon>
              </v-col>
              <v-col x12 v-else-if="header.isAction">
                <v-row v-if="!mobileActionsShowDialog" style="text-align: center;">
                  <v-col v-for="icon in itemListHorizontal[header.value]" :key="icon.icon">
                    <v-icon
                      :color="icon.color"
                      @click="$emit(icon.action, itemListHorizontal)"
                    >{{icon.icon}}</v-icon>
                  </v-col>
                </v-row>
              </v-col>
              <v-col x6 v-else>
                <b>{{header.text}}:</b>
                {{itemListHorizontal[header.value]}}
              </v-col>
            </slot>
          </v-row>
        </v-card-text>
      </v-card>
      <v-btn
        v-if="mobileFunctionAdd"
        @click="functionAdd()"
        style="margin-bottom: 10vh;"
        absolute
        fab
        bottom
        fixed
        right
        color="primary"
      >
        <v-icon color="colorPrimaryText">{{mobileIconAdd}}</v-icon>
      </v-btn>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import KDataTable from "../components/KDataTable.vue";
import KDialog from "../components/KDialog.vue";
import { Display } from "../configuration/display";
import { Headers } from "../models/headers";
export default Vue.extend({
  components: {
    KDataTable,
    KDialog
  },
  props: {
    headers: {
      type: Headers
    },
    itens: {
      type: [],
      default: []
    },
    itemClickAction: {
      type: String,
      default: ""
    },
    search: {
      type: String,
      default: ""
    },
   
    orientation: {
      type: String,
      default: "Vertical"
    },
    mobileIconAdd: {
      type: String,
      default: "mdi-plus"
    },
    mobileFunctionAdd: {
      type: String,
      default: ""
    },
    mobileActionsShowDialog: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      isMobile: false,
      dialogDialogAction: false,
      itemHeaderAction: {},
      itemItemAction: {},
      itemListHorizontal: {},
      itemListHorizontalPosition: 0,
      titleDialogAction: "Ações"
    };
  },
  methods: {
    itemClick(item: any) {
      if (this.itemClickAction) {
        this.$emit(this.itemClickAction, item);
      }
    },
    listaHorizontalLeft() {
      if (this.itemListHorizontalPosition == 0) {
        this.itemListHorizontalPosition = this.itens.length - 1;
      } else {
        this.itemListHorizontalPosition--;
      }
      this.itemListHorizontal = this.itens[this.itemListHorizontalPosition];
    },

    listaHorizontalRight() {
      if (this.itemListHorizontalPosition == this.itens.length - 1) {
        this.itemListHorizontalPosition = 0;
      } else {
        this.itemListHorizontalPosition++;
      }
      this.itemListHorizontal = this.itens[this.itemListHorizontalPosition];
    },

    functionAdd() {
      this.$emit(this.mobileFunctionAdd);
    },
    actionItem(item: any) {
      let existAction = false;
      this.itemItemAction = item;
      this.headers.list.forEach(element => {
        if (element.isAction) {
          existAction = true;
          this.itemHeaderAction = element;
        }
      });
      if (existAction) {
        this.dialogDialogAction = true;
      }
    },
    fecharDialogAction() {
      this.dialogDialogAction = false;
    }
  },

  created() {
    this.isMobile = Display.isMobile();
    if (this.itens.length > 0) {
      this.itemListHorizontal = this.itens[0];
      this.itemListHorizontalPosition = 0;
    }
  }
});
</script>
