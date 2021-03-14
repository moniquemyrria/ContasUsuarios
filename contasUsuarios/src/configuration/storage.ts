export class Storage {

    static valKey = "monique_rocha_contas_usuarios_";

    static salvar(key: string, data: string) {
        sessionStorage.setItem(this.valKey + key, data);
       
    }

    static get(key: string): string | null {
        return sessionStorage.getItem(this.valKey + key)
    }

    static selecionaLinha(key: string, data: string) {
        sessionStorage.setItem(this.valKey + key, data);
       
    }

    static removeLinhaSelecionada(key: string) {
        sessionStorage.removeItem(this.valKey + key);
       
    }

    static limpar() {
        sessionStorage.clear();
       
    }

    
}