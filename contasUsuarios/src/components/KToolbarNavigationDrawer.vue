<template>
  <div id="app" v-if="menu">
    <v-app-bar
      :style="menu.appBarStyle"
      color="secudary"
      app
      left
      id="card-toolbar"
    >
      <v-app-bar-nav-icon id="card-toolbar-icons" @click="drawer = !drawer" />
      <v-toolbar-title>
        <img
          style="width: 80px; margin-top: 14px; margin-left: -20px"
          class="img-fluid"
          width="25%"
          src="../assets/undraw_personal_information_962o.svg"
        />
      </v-toolbar-title>

      <v-spacer></v-spacer>
      <v-menu offset-y transition="slide-y-transition">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            title="Informação"
            icon
            class="ma-2"
            v-bind="attrs"
            v-on="on"
            id="card-toolbar-icons"
            @click="expand = !expand"
          >
            <v-badge overlap color="#093768" right>
              <template v-slot:badge>
                <span>:)</span>
              </template>
              <v-icon large dark>mdi-information</v-icon>
            </v-badge>
          </v-btn>
        </template>
        <v-spacer></v-spacer>
        <v-card class="mx-auto" max-width="344">
          <v-img
            src="../assets/undraw_design_components_9vy6.svg"
            height="200px"
          ></v-img>

          <v-card-title> Monique Rocha </v-card-title>

          <v-card-subtitle>
            Esta aplicação foi desenvolvida por
          </v-card-subtitle>
        </v-card>
      </v-menu>
      <label class="colorPrimaryText--text">
        <span>Olá, bem-vindo(a) </span>
      </label>
    </v-app-bar>

    <v-navigation-drawer
      v-if="menu"
      id="navigation-drawer"
      v-model="drawer"
      app
      color="#093768"
    >
      <v-list dense>
        <v-list-item link @click="drawer = !drawer">
          <v-list-item-content class="title" id="tituloG">
            <v-list-item-title>
              <span id="titulo">Contas de</span>
            </v-list-item-title>
            <v-list-item-subtitle>
              <span id="sub-titulo">Usuários</span>
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <div v-for="itemMenu in menu.itensMenu" :key="itemMenu.title">
          <v-list-item v-if="!itemMenu.isFather" link>
            <v-list-item-title>
              <router-link style="text-decoration: none" :to="itemMenu.path">
                <span class="white--text">{{ itemMenu.title }}</span>
              </router-link>
            </v-list-item-title>
          </v-list-item>
          <v-list-group v-else no-action color="#093768">
            <v-list-item link style="margin-left: -15px" slot="activator">
              <v-list-item-title>
                <span class="white--text">{{ itemMenu.title }}</span>
              </v-list-item-title>
            </v-list-item>

            <v-list-item
              link
              v-for="itemSubMenu in itemMenu.itens"
              :key="itemSubMenu.path"
            >
              <v-list-item-content style="margin-left: -40px">
                <v-list-item-title>
                  <router-link :to="itemSubMenu.path">
                    <v-row>
                      <v-list-item-icon>
                        <v-icon style="margin-left: 10px" color="white">mdi-account</v-icon>
                      </v-list-item-icon>
                      <v-list-item-content>
                        <v-list-item-title class="white--text">{{
                          itemSubMenu.title
                        }}</v-list-item-title>
                      </v-list-item-content>
                    </v-row>
                    <!-- <span class="white--text">{{ itemSubMenu.title }}</span> -->
                  </router-link>
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </div>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Menu, MenuItem } from "../models/menu";

import { Display } from "../configuration/display";

export default Vue.extend({
  props: {
    menu: Menu,
  },
  data: () => ({
    isMobile: Display.isMobile(),

    expand: false,
    /////
    drawer: null,
  }),
  methods: {},
});
</script>


<style >
</style>

<style scoped>
#titulo {
  margin-left: 55px;
  width: 111px;
  height: 30px;
  text-align: center;
  font: Bold 30px/36px Montserrat;
  letter-spacing: 0px;
  color: #ffffff;
  opacity: 1;
}

#sub-titulo {
  margin-left: 115px;
  width: 68px;
  height: 22px;
  text-align: center;
  font: Black 19px/21px Geometr415 Blk BT;
  letter-spacing: 2.28px;
  color: #aeddff;
  text-transform: uppercase;
  opacity: 1;
}
</style>
