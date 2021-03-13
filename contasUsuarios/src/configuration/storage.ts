export class Storage {

    static valKey = "kodigos_smart_point_";

    static salvar(key: string, data: string) {
        sessionStorage.setItem(this.valKey + key, data);
        //sessionStorage.setItem("kodigos_compras_sap_login", "true");
    }

    static get(key: string): string | null {
        return sessionStorage.getItem(this.valKey + key)
    }

    static selecionaLinha(key: string, data: string) {
        sessionStorage.setItem(this.valKey + key, data);
        //sessionStorage.setItem("kodigos_compras_sap_login", "true");
    }

    static removeLinhaSelecionada(key: string) {
        sessionStorage.removeItem(this.valKey + key);
        //sessionStorage.setItem("kodigos_compras_sap_login", "true");
    }

    static limpar() {
        sessionStorage.clear();
        //sessionStorage.setItem("kodigos_compras_sap_login", "true");
    }

    
}