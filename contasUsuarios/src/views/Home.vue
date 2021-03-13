<template>
  <div>
    <k-loading title="Carregando" :dialog="dialogLoading"></k-loading>
    <k-menu
      @sair="sair"
      @changeMenu="createMenu"
      @changePass="changePass"
      :menu="menu"
    ></k-menu>

    <router-view></router-view>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import KLoading from "../components/KLoading.vue";
import KToolbarNavigationDrawer from "../components/KToolbarNavigationDrawer.vue";
import Component from "vue-class-component";
import { Menu, MenuItem } from "../models/menu";

import { IHomeView, IHomeController } from "../contracts/HomeContract";

import { Storage } from "../configuration/storage";

@Component({
  components: {
    KLoading,
    "k-menu": KToolbarNavigationDrawer,
  },
})
export default class Home extends Vue implements IHomeView {
  itensMenu: MenuItem[] = [];
  dialogLoading = false;

  menu?: Menu;

  usuario: any;
  usuarioAcessos: any;

  mostraErro(msg: string) {}

  sair() {
    Storage.salvar("Logado", "false");
    Storage.salvar("Session", "");
    Storage.salvar("User", "");
    Storage.salvar("Acessos", "");
    Storage.limpar();
    this.$router.push("/Home");
  }

  changePass() {
    this.$router.push("/alteracaoSenha");
  }

  async createMenu() {
    this.menu = undefined;
    this.itensMenu = [];


     // if (modulo == "RH") {
        //CADASTRO
        this.itensMenu.push(
          new MenuItem("Cadastro", "", true, [

            new MenuItem("Usuários", "/usuarios", false),

          ])
        );

        
      //}

    let menu: Menu | undefined = undefined;

    menu = new Menu(this.itensMenu);
    menu.appBarStyle = "background: white";

    //console.log(menu);

    this.menu = menu;
    this.$forceUpdate();
  }

  async usuarioPermissaoAcessos() {
    let acessos: any = [];
    acessos = JSON.parse(Storage.get("Acessos")!!);
    this.usuarioAcessos = acessos;

    //console.log(this.usuarioAcessos);
  }

  async created() {
    // var logado = Storage.get("Logado");
    // if (logado == "true") {
    //   this.usuario = JSON.parse(Storage.get("Colaborador")!!);
    //   await this.usuarioPermissaoAcessos();
      await this.createMenu();
    // } else {
    //   this.sair();
    // }
  }
}
</script>