import {
  IUsuarioFormView,
  IUsuarioFormController,
} from "../../contracts/Cadastro/UsuarioFormContract";
import axios from "../../plugins/axios";

import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

export class UsuarioFormController implements IUsuarioFormController {
  _view: IUsuarioFormView;

  _lista = [
    {
      numero: "",
      produto: "",
      descricao: "",
      quantidade: "",
      dataNecessidade: "",
      um: "",
      justificativa: "",
      acao: [new HeaderAction("", "", "", "", "")],
    },
  ];

  constructor(view: IUsuarioFormView) {
    this._view = view;
  }

  carregarDtos(id: string): void {

    this._view.loading(true);


    if (id != undefined) {
      //CARREGA EDITAR
      axios
        .get("Usuario/" + id)
        .then((response: any) => {
          this._view.loading(false);
          if (response.data) {
            this._view.showUsuarioForEditar(response.data);
          }
        })
        .catch((e: any) => {
          console.log(e);
          this._view.loading(false);
          this._view.showMsg(e, 0);
        });
    }else{
      this._view.loading(false);
    }
  }


  salvar(funcionario: any): void {

    this._view.loading(true);

    axios
      .post("Usuario", funcionario)
      .then((response: any) => {
        this._view.loading(false);
        if (response.data.sucesso) {
          this._view.showMsg(response.data.message, 1);

        } else {
          this._view.showMsg("Não foi possível salvar os dados do Usuario", 0);
        }
      })
      .catch((e: any) => {
        this._view.loading(false);
        this._view.showMsg(e, 0);
      });

  }

  update(idFuncionario: string, funcionario: any): void {

    this._view.loading(true);

    axios
      .put("Usuario/" + idFuncionario, funcionario)
      .then((response: any) => {
        this._view.loading(false);
        if (response.data.sucesso) {
          this._view.showMsg(response.data.message, 1);
        } else {
          this._view.showMsg(response.data.message, 0);
        }
      })
      .catch((e: any) => {
        this._view.loading(false);
        this._view.showMsg(e, 0);
      });

  }


}
