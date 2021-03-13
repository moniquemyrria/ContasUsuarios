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
      title="Remover Usuário?"
      text="O Usuário selecionado será removido. Deseja Continuar?"
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
        <v-col cols="10">
          <h3>
            <b>Usuários</b>
          </h3>
          <span>
            <b>Cadatro simplificado de Usuários .</b>
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
              label="Pesquisa de Usuários"
              class="mx-4"
              prepend-icon="mdi-magnify"
            ></v-text-field>
          </v-col>
        </v-col>
      </v-row>
      <v-row style="margin-top: -30px" align="center" justify="center">
        <v-col cols="12">
          <v-data-table
            orientation="Horizontal"
            mobileIconAdd="mdi-plus-circle"
            :headers="headers"
            :search="search"
            :items="itens"
          >
            <template v-slot:item.acao="{ item }">
              <v-row>
                <v-col  sm="4">
                  <v-icon
                    title="Editar dados"
                    
                    color="black"
                    @click="editarItem(item)"
                    dark
                    >mdi-pencil</v-icon
                  >
                </v-col>
                <v-col   sm="4">
                  <v-icon
                    title="Deletar regstro"
                    
                    color="red"
                    @click="deleteItem(item)"
                    dark
                    >mdi-delete</v-icon
                  >
                </v-col>
                <v-col   sm="4">
                  <v-icon
                    title="Visualizar dados do usuário e endereços"
                    
                    color="green"
                    @click="visualizarItem(item)"
                    dark
                    >mdi-eye</v-icon
                  >
                </v-col>
              </v-row>
            </template>
          </v-data-table>
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
    this.$router.push("/Usuario/" + item.id + "/1");
  }

  visualizarItem(item: any) {
    this.$router.push("/Usuario/" + item.id + "/2");
  }

  erpUsuariosCarregar() {
    this.loading(true);
    this.itens = [];
    this.isMobile = Display.isMobile();
    this._controller = new UsuarioListaController(this);

    //this._controller.carregarItens();
  }

  isMobile = false;

  headers = [
    {
      text: "Id",
      align: "start",
      sortable: false,
      value: "id",
      width: 50,
    },

    {
      text: "Nome do usuário",
      align: "start",
      sortable: false,
      value: "nome",
      width: 100,
    },

    {
      text: "E-mail",
      align: "start",
      sortable: false,
      value: "email",
      width: 140,
    },

    {
      text: "Ação",
      align: "center",
      sortable: false,
      value: "acao",
      width: 20,
    },
  ];

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