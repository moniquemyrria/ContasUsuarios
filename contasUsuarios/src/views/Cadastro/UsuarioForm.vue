<template>
  <v-content style="margin: 10px">
    <v-snackbar
      v-model="snackbar"
      :color="colorBorder"
      shaped
      timeout="3000"
      :vertical="vertical"
    >
      <b>{{ text }}</b>
      <template v-slot:action="{ attrs }">
        <v-btn v-bind="attrs" @click="snackbar = false" icon dark small>
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </template>
    </v-snackbar>

    <k-loading title="Carregando" :dialog="dialogLoading"></k-loading>

    <k-dialog
      @fecharDialog="fecharDialogMessage"
      :dialog="dialogMessage"
      title="Mensagem"
      :text="textError"
    ></k-dialog>

    <k-dialog
      :dialog="dialogDeleteUsuario"
      title="Remover Endereço?"
      text="O endereço selecionado será removido. Deseja Continuar?"
    >
      <template v-slot:buttoes>
        <v-btn
          @click="cancelaRemoverEndereco()"
          width="100"
          text
          dark
          color="red darken-2"
          >Cancelar</v-btn
        >
        <v-btn
          @click="removerEndereco()"
          width="100"
          text
          dark
          color="green darken-2"
          >Sim</v-btn
        >
      </template>
    </k-dialog>

    <!-- ADD ENDERECO-->
    <v-dialog v-model="dialogAddEndereco" scrollable width="1200">
      <v-card>
        <v-card-title style="background-color: #1867c0">
          <span style="color: white" class="headline">
            Endereço do Usuário
          </span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12" sm="12">
                <span>
                  <b>Dados do Endereço</b>
                </span>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" sm="3">
                <v-row>
                  <v-col cols="12" sm="11">
                    <v-text-field
                      outlined
                      v-model="endereco.cep"
                      label="CEP *"
                      placeholder="Consulta Automatica de CEP"
                      maxl
                      dense
                      counter="9"
                      maxlength="9"
                      v-mask="maskCep"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="1">
                    <v-btn
                      v-if="!isMobile"
                      color="primary"
                      fab
                      dark
                      small
                      title="Consulta de CEP (Correios Brasil)"
                      @click="consultaCEP()"
                    >
                      <v-icon dark>mdi-map-marker</v-icon>
                    </v-btn>
                  </v-col>
                </v-row>
              </v-col>
            </v-row>
            <v-row id="row">
              <v-col cols="12" sm="9">
                <v-text-field
                  outlined
                  v-model="endereco.logradouro"
                  label="Endereço *"
                  placeholder="Nome da Rua / Avenida / Logradouro"
                  maxl
                  dense
                  counter="80"
                  maxlength="80"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-select
                  outlined
                  :items="itensTipoEndereco"
                  v-model="endereco.tipo"
                  label="Tipo de Endereço *"
                  placeholder="Selecione o Tipo"
                  dense
                ></v-select>
              </v-col>
            </v-row>
            <v-row id="row">
              <v-col cols="12" sm="10">
                <v-text-field
                  outlined
                  v-model="endereco.bairro"
                  label="Bairro *"
                  placeholder="Bairro"
                  maxl
                  dense
                  counter="80"
                  maxlength="80"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="2">
                <v-text-field
                  outlined
                  v-model="endereco.numero"
                  label="Número (ou S/N)*"
                  placeholder="Número"
                  maxl
                  dense
                  counter="5"
                  maxlength="5"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row id="row">
              <v-col cols="12" sm="7">
                <v-text-field
                  outlined
                  v-model="endereco.complemento"
                  label="Complemento "
                  placeholder="Complemento do Endereço"
                  maxl
                  dense
                  counter="50"
                  maxlength="50"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  outlined
                  v-model="endereco.localidade"
                  label="Cidade *"
                  placeholder="Cidade"
                  maxl
                  dense
                  counter="50"
                  maxlength="50"
                ></v-text-field>
              </v-col>

              <v-col cols="12" sm="2">
                <v-text-field
                  outlined
                  v-model="endereco.uf"
                  label="UF *"
                  placeholder="UF"
                  maxl
                  dense
                  counter="2"
                  maxlength="2"
                ></v-text-field>
              </v-col>
            </v-row>

            <v-row id="row">
              <v-spacer></v-spacer>
              <div>
                <b style="font-size: 12px"
                  >Campos obrigatórios identificados com um asterísco *</b
                >
              </div>
            </v-row>
          </v-container>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="#7BC6FF"
            width="150"
            @click="cancelaAddEndereco"
            dark
            large
            >Cancelar</v-btn
          >
          <v-btn color="primary" width="150" dark large @click="addEndereco()"
            >Adicionar</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-container>
      <v-row ref="form">
        <v-col cols="12" sm="12" v-if="!idUsuario">
          <h3>
            <b>Novo Usuário</b>
          </h3>
          <span>
            <b>Criando um novo usuário no sistema</b>
          </span>
        </v-col>

        <v-col cols="12" v-if="idUsuario">
          <h3>
            <b>Editando Usuário</b>
          </h3>
          <span>
            <b>Alterando dados do Usuário no sistema</b>
          </span>
        </v-col>

        <!-- DADOS DO USUARIO -->
        <v-row>
          <v-col cols="12" sm="12">
            <v-row>
              <v-col cols="12" sm="12">
                <div>Dados do Usuário</div>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" sm="12">
                <v-text-field
                  outlined
                  v-model="usuario.nome"
                  label="Nome *"
                  placeholder="Informe o nome"
                  maxl
                  dense
                  counter="100"
                  maxlength="100"
                  :disabled="disabledNome"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row id="row">
              <v-col cols="12" sm="2">
                <v-menu
                  ref="menu1"
                  v-model="menu1"
                  :close-on-content-click="false"
                  transition="scale-transition"
                  offset-y
                  max-width="290px"
                  min-width="290px"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-text-field
                      v-model="dateFormatted"
                      label="Data de Nascimento "
                      placeholder="Data de Nascimento"
                      persistent-hint
                      v-bind="attrs"
                      @blur="date = parseDate(dateFormatted)"
                      v-on="on"
                      outlined
                      dense
                      v-mask="maskDatas"
                      :disabled="disabledcampos"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="date"
                    no-title
                    @input="menu1 = false"
                  ></v-date-picker>
                </v-menu>
              </v-col>
              <v-col cols="12" sm="4">
                <v-text-field
                  outlined
                  v-model="usuario.cpf"
                  label="CPF "
                  placeholder="Informe o CPF"
                  dense
                  v-mask="maskCPF"
                  counter="14"
                  required
                  maxlength="14"
                  :disabled="disabledcampos"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row id="row">
              <v-col cols="12" sm="6">
                <v-text-field
                  outlined
                  v-model="usuario.email"
                  label="E-mail *"
                  placeholder="Informe o e-mail"
                  counter="50"
                  dense
                  required
                  maxlength="50"
                  :disabled="disabledcampos"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  outlined
                  v-model="usuario.confirmeEmail"
                  label="Confirmação de e-mail *"
                  placeholder="Redigite o e-mail"
                  counter="50"
                  dense
                  required
                  maxlength="50"
                  :disabled="disabledcampos"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row id="row">
              <v-col cols="12" sm="6">
                <v-text-field
                  outlined
                  v-model="usuario.senha"
                  label="Senha de Acesso *"
                  placeholder="Informe uma senha de acesso"
                  counter="50"
                  dense
                  required
                  maxlength="50"
                  type="password"
                  :disabled="disabledcampos"
                ></v-text-field>
              </v-col>

              <v-col cols="12" sm="6">
                <v-text-field
                  outlined
                  v-model="usuario.confirmaSenha"
                  label="Confirme a Senha *"
                  placeholder="Redigite a senha"
                  counter="50"
                  dense
                  required
                  maxlength="50"
                  type="password"
                  :disabled="disabledcampos"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-col cols="12" sm="12">
              <v-row id="row">
                <v-spacer></v-spacer>
                <div>
                  <b style="font-size: 12px"
                    >Campos obrigatórios identificados com um asterísco *</b
                  >
                </div>
              </v-row>
            </v-col>
            <v-divider></v-divider>
          </v-col>
          <v-col cols="12" sm="12">
            <!-- DADOS DO ENDEREÇO -->
            <v-row id="row2">
              <v-col cols="12" sm="9">
                <div>Endereços</div>
              </v-col>

              <v-col cols="12" sm="3">
                <v-btn
                  v-if="!isMobile"
                  color="#7BC6FF"
                  large
                  dark
                  @click="modalAddEndereco()"
                  style="float: right"
                  :disabled="disabledcampos"
                >
                  <v-icon dark>mdi-plus</v-icon>Novo Endereço
                </v-btn>
              </v-col>
            </v-row>
          </v-col>

          <v-col cols="12" sm="12">
            <k-data-list
              orientation="Horizontal"
              mobileIconAdd="mdi-plus-circle"
              mobileFunctionAdd="addSolicitacao"
              :headers="headers"
              :itens="usuario.endereco"
            >
              <template v-slot:dados="{ item }">
                {{
                  "Endereço: " +
                  item.logradouro +
                  ", Bairro: " +
                  item.bairro +
                  ", Nº: " +
                  item.numero
                }}
              </template>
              <template v-slot:acao="{ item }">
                <v-icon title="Remover endereço selecionado" color="red" :disabled="disabledcampos" @click="deleteItem(item)" dark
                  >mdi-delete</v-icon
                >
              </template>
            </k-data-list>
          </v-col>

          <v-col cols="12" sm="12">
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="#7BC6FF"
                @click="retornaListaUsuarios()"
                large
                dark
                width="150"
                >{{this.$route.params.tipo == '2' ? 'Voltar' : 'Cancelar'}}</v-btn
              >

              <v-btn
                v-if="!isMobile && idUsuario && this.$route.params.tipo == '1'"
                color="primary"
                large
                dark
                width="150"
                @click="updateUsuario()"
                >Salvar</v-btn
              >

              <v-btn
                v-if="!isMobile && !idUsuario"
                color="primary"
                large
                dark
                width="150"
                @click="saveUsuario()"
                >Salvar</v-btn
              >

              <v-btn
                v-else-if="isMobile && !idUsuario"
                color="primary"
                large
                dark
                width="150"
                @click="saveUsuario()"
                >Salvar</v-btn
              >
            </v-card-actions>
          </v-col>
        </v-row>
      </v-row>
    </v-container>
  </v-content>
</template>

<script lang="ts">
import Vue from "vue";

import KLoading from "../../components/KLoading.vue";
import KDialog from "../../components/KDialog.vue";
import KDataList from "../../components/KDataList.vue";

import Component from "vue-class-component";
import { Watch } from "vue-property-decorator";

import { Display } from "../../configuration/display";
import { Storage } from "../../configuration/storage";

import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

import {
  IUsuarioFormView,
  IUsuarioFormController,
} from "../../contracts/Cadastro/UsuarioFormContract";
import { UsuarioFormController } from "../../controllers/Cadastro/UsuarioFormController";

@Component({
  components: {
    KLoading,
    KDialog,
    KDataList,
  },
})
export default class UsuarioForm extends Vue implements IUsuarioFormView {
  _controller?: IUsuarioFormController;

  //validate form
  valid = true;
  lazy = false;

  isMobile = false;

  isSucesso = false;

  dialogLoading = false;
  dialogDeleteUsuario = false;
  dialogMessage = false;
  textError = "";

  disabledcampos = false;
   disabledNome = false;

  maskDatas = "##/##/####";
  maskCPF = "###.###.###-##";
  maskTel = "(##)#####-####";
  maskCep = "#####-###";
  date = new Date().toISOString().substr(0, 10);
  dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10));
  menu1 = false;

  idUsuario = "";

  dialogAddEndereco = false;

  endereco: any = {
    bairro: "",
    cep: "",
    complemento: "",
    ddd: "",
    gia: "",
    ibge: "",
    localidade: "",
    logradouro: "",
    siafi: "",
    uf: "",
    tipo: "",
    numero: "",
  };

  itensEndereco: any = [];
  usuario = {
    nome: "",
    email: "",
    confirmeEmail: "",
    senha: "",
    confirmaSenha: "",
    dataNascimento: "",
    cpf: "",
    deletado: "N",
    dataCadastro: new Date(),
    endereco: this.itensEndereco,
  };

  //SNACKBAR
  snackbar = false;
  text = "";
  vertical = true;
  color = "";
  colorBorder = "";

  itensTipoEndereco = ["RESIDENCIAL", "COMERCIAL", "CORRESPONDÊNCIAS"];

  headers = new Headers([
    new Header("Tipo de Endereço", "tipo", false, false, "center", true, "100"),
    new Header("CEP", "cep", false, false, "center", true, "100"),
    new Header("Dados do Endereço", "dados", false, false, "left", true, "200"),
    new Header("Ações", "acao", true, false, "center", false, "80"),
  ]);

  limparCamposEndereco() {
    this.endereco = {
      bairro: "",
      cep: "",
      complemento: "",
      ddd: "",
      gia: "",
      ibge: "",
      localidade: "",
      logradouro: "",
      siafi: "",
      uf: "",
      tipo: "",
    };
  }

  itemEnderecoDelete: any = [];

  deleteItem(item: any) {
    this.itemEnderecoDelete = item;
    this.dialogDeleteUsuario = true;
  }

  cancelaRemoverEndereco() {
    this.itemEnderecoDelete = [];
    this.dialogDeleteUsuario = false;
  }

  removerEndereco() {
    this.usuario.endereco = this.usuario.endereco.filter((t: any) => {
      return t != this.itemEnderecoDelete;
    });
    this.itemEnderecoDelete = [];
    this.dialogDeleteUsuario = false;
  }

  consultaCEP() {
    if (this.validateCep()) {
    } else {
      let cep = this.endereco.cep;
      cep = cep.replace(/[^\d]+/g, "");
      this.endereco.cep = cep;
      this._controller!.cosultarEndereco(this.endereco.cep);
    }
  }

  carregaEndereco(endereco: any): void {
    this.endereco = endereco;

    if (endereco.logradouro == null) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O CEP consultado é inválido.";
      this.snackbar = true;
      return;
    }
  }

  addEndereco() {
    if (this.validateEndereco()) {
    } else {
      this.usuario.endereco.push(this.endereco);
      this.dialogAddEndereco = false;
    }

    console.log(this.usuario.endereco);
  }

  modalAddEndereco() {
    this.limparCamposEndereco();
    this.dialogAddEndereco = true;
  }

  cancelaAddEndereco() {
    this.dialogAddEndereco = false;
    this.endereco = [];
  }

  formatDate(date: string) {
    if (!date) return null;

    const [year, month, day] = date.split("-");
    return `${day}/${month}/${year}`;
  }

  parseDate(date: string) {
    if (!date) return null;
    const [day, month, year] = date.split("/");
    return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
  }

  validaCPF(cpf: any): boolean {
    let numeros, digitos, soma, i, resultado, digitosIguais;
    digitosIguais = 1;
    if (cpf.length < 11) return false;
    for (i = 0; i < cpf.length - 1; i++)
      if (cpf.charAt(i) != cpf.charAt(i + 1)) {
        digitosIguais = 0;
        break;
      }
    if (!digitosIguais) {
      numeros = cpf.substring(0, 9);
      digitos = cpf.substring(9);
      soma = 0;
      for (i = 10; i > 1; i--) soma += numeros.charAt(10 - i) * i;
      resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
      if (resultado != digitos.charAt(0)) return false;
      numeros = cpf.substring(0, 10);
      soma = 0;
      for (i = 11; i > 1; i--) soma += numeros.charAt(11 - i) * i;
      resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
      if (resultado != digitos.charAt(1)) return false;
      return true;
    } else return false;
  }

  loading(visible: boolean): void {
    this.dialogLoading = visible;
  }

  fecharDialogMessage() {
    this.dialogMessage = false;
    if (this.isSucesso) {
      this.$router.push("/Usuarios");
    }
  }

  retornaListaUsuarios() {
    this.$router.push("/Usuarios");
  }

  showMsg(msg: string, modo: number): void {
    this.textError = msg;
    if (modo == 1) {
      this.isSucesso = true;
    }
    this.dialogMessage = true;
  }

  validateEndereco() {
    if (this.endereco.cep == "" || this.endereco.cep.length < 0) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "Informe o CEP para realziar a consulta do endereço.";
      this.snackbar = true;
      return true;
    }

    if (this.endereco.logradouro == "" || this.endereco.logradouro.length < 0) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O Endereço está vazio ou inválido.";
      this.snackbar = true;
      return true;
    }

    if (this.endereco.tipo == "" || this.endereco.tipo == null) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "Selecione o Tipo de Enderço.";
      this.snackbar = true;
      return true;
    }

    if (this.endereco.bairro == "" || this.endereco.bairro == null) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O campo Bairro está vazio ou inválido.";
      this.snackbar = true;
      return true;
    }

    if (this.endereco.numero == "" || this.endereco.numero == null) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O campo Número está vazio ou inválido.";
      this.snackbar = true;
      return true;
    }

    if (this.endereco.localidade == "" || this.endereco.localidade == null) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O campo Cidade está vazio ou inválido.";
      this.snackbar = true;
      return true;
    }

    if (this.endereco.uf == "" || this.endereco.uf == null) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O campo UF está vazio ou inválido.";
      this.snackbar = true;
      return true;
    }

    return false;
  }

  validate() {
    if (this.usuario.nome == "" || this.usuario.nome.length > 100) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O campo Nome está vazio ou inválido.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.email == "" || this.usuario.email.length > 50) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O campo e-mail do Usuario está vazio ou inválido.";
      this.snackbar = true;
      return true;
    } else if (this.usuario.email !== this.usuario.confirmeEmail) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "Os e-mails informados não correspondem.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.senha == "" || this.usuario.senha.length > 50) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O campo Senha do Usuario está vazio ou inválido.";
      this.snackbar = true;
      return true;
    } else if (this.usuario.senha !== this.usuario.confirmaSenha) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "As senhas informadas não correspondem.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.cpf != "" && this.usuario.cpf.length > 0) {
      let cpf = this.usuario.cpf;
      cpf = cpf.replace(/[^\d]+/g, "");

      if (!this.validaCPF(cpf)) {
        this.colorBorder = "error";
        this.color = "error";
        this.text = "O CPF informador é inválido. Digite um CPF válido.";
        this.snackbar = true;
        return true;
      }
    }

    if (this.usuario.endereco.length == 0) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "É necessário informar ao menos um endereço para o usuário.";
      this.snackbar = true;
      return true;
    }

    return false;
  }

  validateCep() {
    if (this.endereco.cep == "" || this.endereco.cep.length < 9) {
      this.colorBorder = "error";
      this.color = "error";
      this.text = "O campo CEP está vazio ou inválido.";
      this.snackbar = true;
      return true;
    }

    return false;
  }

  saveUsuario() {
    if (this.validate()) {
    } else {
      let cpf = this.usuario.cpf;
      cpf = cpf.replace(/[^\d]+/g, "");
      this.usuario.cpf = cpf;

      this.usuario.dataNascimento = this.date;
      this._controller!.salvar(this.usuario);
    }
  }

  updateUsuario() {
    if (this.validate()) {
    } else {
      let cpf = this.usuario.cpf;
      cpf = cpf.replace(/[^\d]+/g, "");
      this.usuario.cpf = cpf;
      this._controller!.update(this.idUsuario, this.usuario);
    }
  }

  carregarDtos(): void {
    this.idUsuario = this.$route.params.id;
    if (this.$route.params.tipo == "1") {
      this.disabledcampos = false;
      
    } 
    
    if (this.$route.params.tipo == "2"){
      this.disabledcampos = true;
     
    }

    if (this.$route.params.tipo == undefined){
      this.disabledcampos = false;
      
    }

  }

  showUsuarioForEditar(item: any) {
    this.disabledNome = true;
    this.usuario = item;
    this.usuario.confirmeEmail = item.email;
    this.usuario.confirmaSenha = item.senha;

    if (item.cpf != null || item.cpf !== "") {
      this.usuario.cpf =
        item.cpf.substring(0, 3) +
        "." +
        item.cpf.substring(3, 6) +
        "." +
        item.cpf.substring(6, 9) +
        "-" +
        item.cpf.substring(9, 13);
    }
  }

  created() {
    this._controller = new UsuarioFormController(this);
    this._controller.carregarDtos(this.$route.params.id);
    this.carregarDtos();
    this.isMobile = Display.isMobile();
  }

  @Watch("date")
  onDatasChanged(val: string) {
    this.dateFormatted = this.formatDate(this.date);
  }
}
</script>

<style>
#row {
  margin-top: -15px;
}

#row2 {
  margin-top: -20px;
}
</style>


