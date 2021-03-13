<template>
  <div class="loginFluid">
    <v-app id="inspire">
      <k-loading title="Carregando" :dialog="dialogLoading"></k-loading>
      <k-dialog
        @fecharDialog="fecharDialogMessage"
        :dialog="dialogMessage"
        title="Error"
        :text="textError"
      ></k-dialog>
      <v-snackbar
        v-model="snackbar"
        :color="colorBorder"
        timeout="3000"
        :vertical="vertical"
      >
        <b>{{ text }}</b>
        <template v-slot:action="{ attrs }">
          <v-btn :color="color" text v-bind="attrs" @click="snackbar = false"
            >Ok</v-btn
          >
        </template>
      </v-snackbar>

      <v-container style="margin: auto;  margin-top: 25vh; margin-left: 67%;">
        <div style="text-align:center;">
          <v-row>
            <img src="../assets/Inventus_Power_logo.png" id="logo" />
          </v-row>

          <v-row>
            <v-col cols="12" sm="5" md="3">
              <span id="titulo">SMART</span>
            </v-col>
          </v-row>
          <v-row style="margin-top: -6vh">
            <v-col cols="12" sm="5" md="3">
              <span id="titulo2">POINT</span>
            </v-col>
          </v-row>
          <v-row style="margin-bottom: -30px;">
            <v-col cols="12" sm="5" md="3">
              <v-text-field
                filled
                placeholder="Matrícula"
                @keyup.enter="funcLogin"
                v-model="usuario.usuario"
                type="text"
                autocomplete="username"
                append-icon="mdi-account"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" sm="5" md="3">
              <v-text-field
                filled
                placeholder="Senha"
                @keyup.enter="funcLogin"
                autocomplete="current-password"
                v-model="usuario.senha"
                type="password"
                append-icon=" mdi-lock"
              ></v-text-field>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" sm="5" md="3">
              <v-btn
                type="submit"
                color="primary"
                width="100%"
                style="height:50px;"
                @click.prevent.stop="funcLogin"
                dark
                >Entrar</v-btn
              >
            </v-col>
          </v-row>
        </div>
      </v-container>
      <!-- <v-footer color="#313C42" dark>
        <v-spacer></v-spacer>
        <span id="kodigos">® Desenvolvido por Kodigos Software Engineering</span>
        <v-spacer></v-spacer>
      </v-footer>-->
      <k-footer></k-footer>
    </v-app>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

import KLoading from "../components/KLoading.vue";
import KDialog from "../components/KDialog.vue";
import KFooter from "../components/KFooter.vue";

import Component from "vue-class-component";

import { ILoginView, ILoginController } from "../contracts/LoginContract";
import { LoginController } from "../controllers/LoginController";

@Component({
  components: {
    KLoading,
    KDialog,
    KFooter,
  },
})
export default class Login extends Vue implements ILoginView {
  _controller?: ILoginController;

  dialogLoading = false;
  dialogMessage = false;
  textError = "";

  usuario = {
    usuario: "",
    senha: "",
  };

  senhaAlteracao = {
    senhaAtual: "",
    senhaNova: "",
    senhaNovaConfirma: "",
  };

  //SNACKBAR
  snackbar = false;
  text = "";
  vertical = true;
  color = "";
  colorBorder = "";

  validate() {
    if (this.usuario.usuario == "") {
      this.colorBorder = "error";
      this.color = "white";
      this.text = "Informe a sua Matrícula para acessar a aplicação.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.senha == "") {
      this.colorBorder = "error";
      this.color = "white";
      this.text = "Informe a Senha para acessar a aplicação.";
      this.snackbar = true;
      return true;
    }
  }

  loading(visible: boolean): void {
    this.dialogLoading = visible;
  }

  fecharDialogMessage() {
    this.dialogMessage = false;
  }

  showMsg(msg: string | Error): void {
    this.textError = msg.toString();
    this.dialogMessage = true;
  }

  pushMain(): void {
    this.$router.push("/");
  }

  pushAlteracaoSenha(matricula: string): void {
    //console.log(matricula);
    this.$router.push("/alteracaoSenha/" + matricula);
  }

  funcLogin() {
    if (this.validate()) {
    } else {
      this.usuario.usuario = this.usuario.usuario.toString().toUpperCase();
      this._controller!.login(this.usuario.usuario, this.usuario.senha);
    }
  }

  created() {
    this._controller = new LoginController(this);
  }
}
</script>

<style scoped>
.loginFluid {
  background-image: url("../assets/background-inventus5.png");
  background-size: cover;
}

#inspire {
  background: none;
  height: 100%;
}

#titulo {
  margin-left: 8vh;
  text-align: left;
  color: var(--unnamed-color-f79733);
  text-align: left;
  font-style: normal normal bold Montserrat;
  font-size: 9vh;
  letter-spacing: 0px;
  color: #f79733;
  opacity: 1;
  font-weight: bold;
}

#titulo2 {
  margin-left: 20vh;
  text-align: center;
  font-style: normal normal Geometr415 Blk BT;
  letter-spacing: 6.24px;
  font-size: 5.5vh;
  color: #313c42;
  text-transform: uppercase;
  opacity: 1;
  font-weight: bold;
}

#kodigos {
  margin-left: 0px;
  width: 1280px;
  height: 27px;
  background: #313c42 0% 0% no-repeat padding-box;
  opacity: 1;
}

#logo {
  /* margin-left: 110vh;
  width: 40vh;
  background: transparent 0% 0% no-repeat padding-box;
  opacity: 1; */
  margin-top: -20vh;
  margin-left: -120vh;
  width: 35vh;
  height: 9vh;
}

.Fundo {
  background-image: url(../assets/Inventus_Power_logo.png);
  background-size: cover;
  height: 100%;
}
.center {
  margin-top: 15vh;
}

.hidden-sm-and-down .v-icon {
  color: white !important;
}
</style>
