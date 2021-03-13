import { ILoginView, ILoginController } from "../contracts/LoginContract";
import axios from "../plugins/axios";
import { Storage } from "../configuration/storage";
export class LoginController implements ILoginController {
  _view: ILoginView;

  constructor(view: ILoginView) {
    this._view = view;
    Storage.salvar("Logado", "false");
    Storage.salvar("Session", "");
    Storage.salvar("User", "");
    Storage.salvar("Colaborador", JSON.stringify({ codigo: "" }));
  }

  async login(matricula: string, senha: string) {
    this._view.loading(true);

    //Processamento
    let login = new Login(matricula, senha);

  //  await axios.post('Login', login).then(async (response: any) => {
  //     this._view.loading(false)
  //     if (response.data.sucesso == true) {
  //       Storage.salvar("Logado", "true")
  //       Storage.salvar("Session", response.data.message)
  //       Storage.salvar("User", response.data.tRetorno.matricula)
  //       Storage.salvar("Colaborador", JSON.stringify(response.data.tRetorno))
  //       await this.carregaUsuarioPermissoesAcessos(matricula);
  //       axios.defaults.headers.PortalComprasSession =
  //         response.data.message;

  //         if (response.data.message == 'Primeiro acesso.'){
  //           this._view.pushAlteracaoSenha(matricula);

  //         }else{
  //           this._view.pushMain();
  //         }
          
          

  //     } else {
  //       this._view.showMsg(response.data.message)
  //     }
  //   }).catch((e: any) => {
  //     this._view.loading(false)
  //     this._view.showMsg(e)
  //   })

    if (login.matricula == "MONIQUE" && login.senha == "123") {
      Storage.salvar("Logado", "true");
      Storage.salvar("Session", "123");
      Storage.salvar("User", "123");
      Storage.salvar(
        "Colaborador",
        JSON.stringify({ isSuper: true, nome: "Monique" })
      );
      axios.defaults.headers.PortalComprasSession = "123";
      this._view.pushMain();
    }

    this._view.loading(false);
  }

  async carregaUsuarioPermissoesAcessos(matricula: string){

    this._view.loading(true);
    //CARREGA EDITAR
    await axios
      .get("Usuario/usuarioPermissaoAcessos/" + matricula)
      .then((response: any) => {
        
        Storage.salvar("Acessos", JSON.stringify(response.data))
        
      })
      .catch((e: any) => {
        console.log(e);
          this._view.showMsg(e);
      });

  }
}

class Login {
  matricula: string;
  senha: string;
  constructor(matricula: string, senha: string) {
    this.matricula = matricula;
    this.senha = senha;
  }
}
