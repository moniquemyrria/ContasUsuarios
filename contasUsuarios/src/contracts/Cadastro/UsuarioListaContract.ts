//UsuarioFormContract

export interface IUsuarioListaView {

    loading(visible: boolean): void
    showMsg(msg: string): void
    showItens(list: []): void

}

export interface IUsuarioListaController {
    carregarItens(): void
    //atualizaUsuarioFuncionariosERP(): void
    deleteitem(id: number): void

}