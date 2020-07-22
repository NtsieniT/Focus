import { Component, ViewChild, Input, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { environment } from '../../environments/environment';
import { LoadingService } from '../@core/services/loading.service';
import { Notifications } from '../@core/components/notification.component';
import { PrimaryBusinessLine } from '../Common/primary-business-line/primary-business-line.model';
import { PrimaryBusinessLineService } from '../Common/primary-business-line/primary-business-line.service';

@Component({
  selector: 'app-business-line-list',
  templateUrl: './business-line-list.component.html',

})
export class BusinessLineListComponent {

  private businessLine: PrimaryBusinessLine;

  view: Observable<GridDataResult>;
  state: State = {
    skip: 0,
    take: environment.pageSize,
    sort: [{ field: 'BusinessLine', dir: 'asc' }]
  };
  pageSizes = environment.pageSizes;
  @ViewChild(Notifications) private notifications: Notifications;
  public dataItem = this.businessLine;

  constructor(
    private service: PrimaryBusinessLineService,
    private loadingService: LoadingService) {

    this.refreshGrid();
  }

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.refreshGrid();
  }

  private refreshGrid() {
    this.loadingService.toggleLoadingIndicator(true);
    this.service.read(this.state)
      .subscribe((data: any) => { this.view = data; },
        error => this.notifications.popupError = error, () => { this.loadingService.toggleLoadingIndicator(false); });
  }
}
