import { Component, ViewChild, Input, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';
import { ApplicationService } from '../Common/application/application.service';
import { Summary } from '../Common/Summary-Overview/summary-overview.model';
import { SummaryOverviewService } from '../Common/Summary-Overview/summary-overview.service';

@Component({
  selector: 'app-summary-overview-list',
  templateUrl: './summary-overview-list.component.html',

})
export class SummaryOverviewListComponent {

  private summary: Summary;

  view: Observable<GridDataResult>;
  state: State = {
    skip: 0,
    take: environment.pageSize,
    sort: [{ field: 'Id', dir: 'asc' }]
  };
  pageSizes = environment.pageSizes;
  @ViewChild(Notifications) private notifications: Notifications;
  public dataItem = this.summary;


  constructor(
    private service: SummaryOverviewService,
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
      .subscribe((data: any) => { this.view = data }, error => this.notifications.popupError = error, () => { this.loadingService.toggleLoadingIndicator(false) });
  } 

}
