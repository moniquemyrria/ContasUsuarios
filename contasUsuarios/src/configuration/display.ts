export class Display {
    private static mobile: boolean = false

    static isMobile(): boolean {
        Display.checkDisplay()
        //console.log(Display.mobile)
        return Display.mobile
    }

    private static checkDisplay(): any {
        if (navigator.userAgent.match(/Android/i)
            || navigator.userAgent.match(/webOS/i)
            || navigator.userAgent.match(/iPhone/i)
            || navigator.userAgent.match(/iPad/i)
            || navigator.userAgent.match(/iPod/i)
            || navigator.userAgent.match(/BlackBerry/i)
            || navigator.userAgent.match(/Windows Phone/i)
        ) {
            Display.mobile = true
        }
        else {
            Display.mobile = false
        }
    }
}