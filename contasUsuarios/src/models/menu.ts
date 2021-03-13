export class Menu {

    itensMenu: MenuItem[]
    appBarStyle?: string

    constructor(itens: MenuItem[]) {
        this.itensMenu = itens
    }
}

export class MenuItem {

    title: string
    path: string
    //icon: string
    isFather: boolean
    itens?: MenuItem[]

    constructor(title: string, path: string, isFather: boolean, itens?: MenuItem[]) { //icon: string,

        this.title = title
        this.path = path
        //this.icon = icon
        this.isFather = isFather
        this.itens = itens
    }


}