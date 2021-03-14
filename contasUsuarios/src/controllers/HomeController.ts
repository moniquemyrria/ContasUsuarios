import { IHomeView, IHomeController } from "../contracts/HomeContract";

export class HomeController implements IHomeController {
  private View: IHomeView;

  constructor(_view: IHomeView) {
    this.View = _view;
  }

  init(): void {
    this.View.mostraErro("efubh ");
  }
}
