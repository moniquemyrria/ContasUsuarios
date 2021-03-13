export interface ILoginView {

    loading(visible: boolean): void
    showMsg(msg: string): void
    pushMain(): void
    pushAlteracaoSenha(matricula: string): void
}

export interface ILoginController {

    login(user: string, pass: string): void

}