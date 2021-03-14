//UsuarioFormContract

export interface IUsuarioFormView {

    loading(visible: boolean): void
    showMsg(msg: string, modo: number): void
    carregarDtos(): void
    showUsuarioForEditar(item: any): void
    carregaEndereco(endereco: any): void
}

export interface IUsuarioFormController {

    carregarDtos(id: string): void
    salvar(peril: any): void
    update(id: string, perfil: any): void
    cosultarEndereco(cep: string): void
}