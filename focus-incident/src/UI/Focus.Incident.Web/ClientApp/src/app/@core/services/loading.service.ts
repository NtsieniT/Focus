import { Injectable } from '@angular/core';
import { NbSpinnerService } from '@nebular/theme/services/spinner.service';

@Injectable()
export class LoadingService {

  constructor(public spinnerService: NbSpinnerService) {
  }

  toggleLoadingIndicator(value) {
    if (value) {
      this.spinnerService.registerLoader(new Promise(null));
      this.spinnerService.load();
    }
    else {
      this.spinnerService.clear();
      this.spinnerService.load();
    }
  }
}
