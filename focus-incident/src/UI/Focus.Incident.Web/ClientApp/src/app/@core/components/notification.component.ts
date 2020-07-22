import { Component, Input } from '@angular/core';
import { LoadingService } from '../services/loading.service';
import { ToasterService, Toast, BodyOutputType } from 'angular2-toaster';

@Component({
  selector: 'notifications',
  templateUrl: './notification.component.html'
})
export class Notifications { 
  messageInfo = '';
  messageError = '';
  messageUnauthorized = '';
  messageSuccess = '';

  constructor(private loadingService: LoadingService, private toasterService: ToasterService) {
  }

  @Input() public set info(model: string) {
    this.loadingService.toggleLoadingIndicator(false);

    // show static message
    this.clear();
    this.messageInfo = model;
  }
  @Input() public set popupInfo(model: string) {
    this.loadingService.toggleLoadingIndicator(false);

    // show popup
    this.showToast('info', 'Success!', model);
  }

  @Input() public set error(model: any) {
    this.loadingService.toggleLoadingIndicator(false);

    this.clear();

    if (model && typeof (model) == 'string') {
      this.messageError = model;
      console.log(model);
    }
    else if (model && typeof (model) == 'object' && model.status > 0) {
      if (model.status == 401 || model.status == 403)
        this.messageUnauthorized = 'Access Denied.';
      else
        this.messageError = model.statusText;

      console.log(model);
    }
    else
      this.messageError = 'There was an unexpected error. Please contact your system administrator :(';
  }
  @Input() public set popupError(model: any) {
    this.loadingService.toggleLoadingIndicator(false);

    if (model && typeof (model) == 'string') {
      this.showToast('error', 'Error', model);
      console.log(model);
    }
    else if (model && typeof (model) == 'object' && model.status > 0) {
      if (model.status == 401 || model.status == 403)
        this.showToast('warning', 'Access Denied', model.statusText);
      else
        this.showToast('error', 'Error', model.statusText);

      console.log(model);
    }
    else
      this.showToast('error', 'Error', 'There was an unexpected error. Please contact your system administrator :(');
  }

  @Input() public set success(model: string) {
    this.loadingService.toggleLoadingIndicator(false);

    // show static message
    this.clear();
    this.messageSuccess = model;
  }
  @Input() public set popupSuccess(model: string) {
    this.loadingService.toggleLoadingIndicator(false);

    // show popup
    this.showToast('success', 'Success!', model);
  }

  public clear() {
    this.messageInfo = '';
    this.messageError = '';
    this.messageUnauthorized = '';
    this.messageSuccess = '';
  }

  private showToast(type: string, title: string, body: string) {
    var timeout = 5000;
    if (type == 'error') timeout = 0; // make error popups sticky

    const toast: Toast = {
      type: type,
      title: title,
      body: body,
      showCloseButton: true,
      bodyOutputType: BodyOutputType.TrustedHtml,
      timeout: timeout
    };
    this.toasterService.popAsync(toast);
  }
}
