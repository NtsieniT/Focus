import { Component, ViewChild, Input, Injectable, ViewEncapsulation } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { toODataString, State } from '@progress/kendo-data-query';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';
import { AssignmentGroup } from '../Common/assignment-group/assignment-group.model';
import { AssignmentGroupService } from '../Common/assignment-group/assignment-group.service';

@Component({
  selector: 'app-assignment-group-list',
  templateUrl: './assignment-group-list.component.html',
})
export class AssignmentGroupListComponent {

  private assignmentGroup: AssignmentGroup;

  view: Observable<GridDataResult>;
  state: State = {
    skip: 0,
    take: environment.pageSize,
    sort: [{ field: 'AssignmentGroup', dir: 'asc' }]
  };
  pageSizes = environment.pageSizes;
  @ViewChild(Notifications) private notifications: Notifications;
  public dataItem = this.assignmentGroup;


  constructor(
    private service: AssignmentGroupService,
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
