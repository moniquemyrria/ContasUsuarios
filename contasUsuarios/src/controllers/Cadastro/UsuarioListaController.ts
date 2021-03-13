import { IUsuarioListaView, IUsuarioListaController } from "../../contracts/Cadastro/UsuarioListaContract";
import axios from "../../plugins/axios"

import { Storage } from "../../configuration/storage"
import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

export class UsuarioListaController implements IUsuarioListaController {

    _view: IUsuarioListaView

    constructor(view: IUsuarioListaView) {
        this._view = view
    }

    carregarItens(): void {
        this._view.loading(true)

        axios
            .get('Usuario')
            .then((response: any) => {

                response.data.forEach((element: { acoes: HeaderAction[]; }) => {
                    element.acoes = [
                        new HeaderAction("mdi-pencil", "primary", "Teste", "editarItem", "200px"),
                        new HeaderAction("mdi-delete", "primary", "Teste", "deleteItem", "200px")
                    ]
                });

                 this._view.loading(false)
                this._view.showItens(response.data)
            }).catch((e: any) => {
                this._view.loading(false)
                this._view.showMsg(e)
            })
    }


    deleteitem(id: number): void {
        this._view.loading(true)

        axios.delete('Usuario/' + id).then((response: any) => {
            this._view.loading(false)
            if (response.data.sucesso) {
                // this._view.showMsg("Salvo com sucesso.")
                this._view.showMsg(response.data.message)
                this.carregarItens()
            } else {
                this._view.showMsg(response.data.message)
            }
        }).catch((e: any) => {
            this._view.loading(false)
            this._view.showMsg(e)
        })
    }


}