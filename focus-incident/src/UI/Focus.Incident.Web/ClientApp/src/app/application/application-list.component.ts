import { Component, ViewChild, Input, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';
import { Application } from '../Common/application/application.model';
import { ApplicationService } from '../Common/application/application.service';

@Component({
  selector: 'app-application-list',
  templateUrl: './application-list.component.html',

})
export class ApplicationListComponent {

  private application: Application;

  view: Observable<GridDataResult>;
  state: State = {
    skip: 0,
    take: environment.pageSize,
    sort: [{ field: 'ApplicationName', dir: 'asc' }]
  };
  pageSizes = environment.pageSizes;
  @ViewChild(Notifications) private notifications: Notifications;
  public dataItem = this.application;


  constructor(
    private service: ApplicationService,
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
