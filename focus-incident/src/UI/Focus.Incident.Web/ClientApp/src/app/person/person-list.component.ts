import { Component, ViewChild, Input, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';
import { GridDataResult, DataStateChangeEvent, GridComponent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';
import { Person } from '../Common/person/person.model';
import { PersonService } from '../Common/person/person.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
})
export class PersonListComponent {

  private person: Person;

  view: Observable<GridDataResult>;
  state: State = {
    skip: 0,
    take: environment.pageSize,
    sort: [{ field: 'personName', dir: 'asc' }]
  };
  pageSizes = environment.pageSizes;
  @ViewChild(Notifications) private notifications: Notifications;
  public dataItem = this.person;

  constructor(
    private service: PersonService,
    private loadingService: LoadingService) {
  }
  ngOnInit() {
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
