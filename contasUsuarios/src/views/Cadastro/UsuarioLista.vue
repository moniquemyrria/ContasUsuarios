<template>
  <v-content style="margin: 10px">
    <k-loading title="Carregando" :dialog="dialogLoading"></k-loading>
    <k-dialog
      @fecharDialog="fecharDialogMessage"
      :dialog="dialogMessage"
      title="Mensagem"
      :text="textError"
    ></k-dialog>
    <k-dialog
      :dialog="dialogDelete"
      title="Remover Usuario?"
      text="O Usuario selecionado será removido. Deseja Continuar?"
    >
      <template v-slot:buttoes>
        <v-btn
          @click="dialogDelete = false"
          width="100"
          text
          dark
          color="red darken-2"
          >Cancelar</v-btn
        >
        <v-btn
          @click="deleteItemConfirmado"
          width="100"
          text
          dark
          color="green darken-2"
          >Sim</v-btn
        >
      </template>
    </k-dialog>
    <v-container>
      <v-row>
        <v-breadcrumbs style="margin-left: -10px" :items="caminho">
          <template v-slot:divider>
            <!-- <v-icon>mdi-home</v-icon> -->
          </template>
          <template v-slot:item="{ item }">
            <router-link :to="item.href">
              {{ item.text }}
            </router-link>
          </template>
        </v-breadcrumbs>
      </v-row>

      <v-row>
        <v-col cols="10">
          <h3>
            <b>Usuarios</b>
          </h3>
          <span>
            <b>Usuarios dos Departamentos da Empresa.</b>
          </span>
        </v-col>
        <v-spacer></v-spacer>

        <v-col cols="1">
          <v-btn
            v-if="!isMobile"
            color="primary"
            fab
            dark
            @click="addSolicitacao"
          >
            <v-icon dark>mdi-plus</v-icon>
          </v-btn>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" sm="12">
          <v-col cols="12" sm="5" style="float: right">
            <v-text-field
              v-model="search"
              label="Pesquisa Usuarios"
              class="mx-4"
              prepend-icon="mdi-magnify"
            ></v-text-field>
          </v-col>
        </v-col>
      </v-row>
      <v-row style="margin-top: -30px" align="center" justify="center">
        <v-col cols="12">
          <k-data-list
            orientation="Horizontal"
            mobileIconAdd="mdi-plus-circle"
            mobileFunctionAdd="addSolicitacao"
            @editarItem="editarItem"
            @deleteItem="deleteItem"
            @addSolicitacao="addSolicitacao"
            :headers="headers"
            :search="search"
            :itens="itens"
          >
            <!-- <template v-slot:dtAdmissao="{ item }">
            <b>{{ 
              item.dtAdmissao
            
            }}</b>
            </template>-->
          </k-data-list>
        </v-col>
      </v-row>
    </v-container>
  </v-content>
</template>

<script lang="ts">
import Vue from "vue";
import KLoading from "../../components/KLoading.vue";
import KDialog from "../../components/KDialog.vue";
import KDataList from "../../components/KDataList.vue";

import { Display } from "../../configuration/display";

import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

import {
  IUsuarioListaView,
  IUsuarioListaController,
} from "../../contracts/Cadastro/UsuarioListaContract";

import { UsuarioListaController } from "../../controllers/Cadastro/UsuarioListaController";

import Component from "vue-class-component";

@Component({
  components: {
    KLoading,
    KDialog,
    KDataList,
  },
})
export default class UsuarioLista extends Vue implements IUsuarioListaView {
  _controller?: IUsuarioListaController;

  dialogLoading = false;
  dialogMessage = false;
  textError = "";

  search = "";

  dialogDelete = false;
  itemUsuarioDelete: any;

  caminho = [
    {
      text: "Home",
      disabled: false,
      href: "/",
    },
    {
      text: "Usuarios",
      disabled: false,
      href: "/Usuarios",
    },
    {
      text: "Novo Usuario",
      disabled: false,
      href: "/Usuario",
    },
  ];
  loading(visible: boolean): void {
    this.dialogLoading = visible;
  }
  showMsg(msg: string): void {
    this.textError = msg;
    this.dialogMessage = true;
  }
  showItens(list: any): void {
    this.itens = list;
  }
  fecharDialogMessage() {
    this.dialogMessage = false;
  }

  deleteItem(item: any) {
    this.itemUsuarioDelete = item;
    this.dialogDelete = true;
  }

  deleteItemConfirmado() {
    this.dialogDelete = false;
    this._controller!.deleteitem(this.itemUsuarioDelete.id);
  }

  editarItem(item: any) {
    this.$router.push("/Usuario/" + item.id);
  }

  erpUsuariosCarregar() {
    this.loading(true);
    this.itens = [];
    this.isMobile = Display.isMobile();
    this._controller = new UsuarioListaController(this);

    //this._controller.carregarItens();
  }

  isMobile = false;

  headers = new Headers([
    new Header("Sigla", "sigla", false, false, "center", true, "100"),
    new Header(
      "Descrição do Usuario",
      "descricao",
      false,
      false,
      "left",
      true,
      "200"
    ),
    new Header("Ações", "acoes", true, false, "center", false, "80"),
  ]);

  itens = [];

  addSolicitacao() {
    this.$router.push("/Usuario");
  }

  created() {
    this.isMobile = Display.isMobile();
    this._controller = new UsuarioListaController(this);

    this._controller.carregarItens();
  }
}
</script>