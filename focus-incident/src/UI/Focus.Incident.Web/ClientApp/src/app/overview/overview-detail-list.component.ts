import { Component, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { GridComponent, GridDataResult, DataStateChangeEvent, RowClassArgs } from '@progress/kendo-angular-grid';
import { toODataString, State } from '@progress/kendo-data-query';
import { environment } from '../../environments/environment';
import { PrimaryBusinessLineService } from '../Common/primary-business-line/primary-business-line.service';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';
import { AssignmentGroupService } from '../Common/assignment-group/assignment-group.service';
import { ApplicationService } from '../Common/application/application.service';
import { PersonService } from '../Common/person/person.service';
import { SummaryOverviewService } from '../Common/Summary-Overview/summary-overview.service';

@Component({
  selector: 'app-overview-detail-list',
  templateUrl: './overview-detail-list.component.html',
  providers: [PrimaryBusinessLineService]

})
export class OverviewDetailListComponent {

  public primaryBusinessLines: any;
  public assignmentGroups: any;
  public applications: any;
  public persons: any;
  public summaries: any;

  // ********States*********
  primaryBusinessLinestate: State = {
    skip: 0,
    sort: [{ field: 'BusinessLine', dir: 'asc' }]
  };
  assignmentGroupState: State = {
    skip: 0,
    sort: [{ field: 'AssignmentGroup', dir: 'asc' }]
  };
  applicationState: State = {
    skip: 0,
    sort: [{ field: 'ApplicationName', dir: 'asc' }]
  };
  personState: State = {
    skip: 0,
    sort: [{ field: 'personName', dir: 'asc' }]
  };
  summaryState: State = {
    skip: 0,
  };


  view: Observable<GridDataResult>;
  state: State = {
    skip: 0,
    take: environment.pageSize,
    sort: [{ field: 'BusinessLine', dir: 'asc' }]
  };

  @ViewChild(Notifications) private notifications: Notifications;

  constructor(private primaryBusinessLine: PrimaryBusinessLineService,
    private assignmentGroupService: AssignmentGroupService,
    private applicationService: ApplicationService,
    private personService: PersonService,
    private summaryService: SummaryOverviewService,
    private loadingService: LoadingService) {

    this.getPrimaryBusinessLines();
    this.getAssignmentGroups();
    this.getApplications();
    this.getPersons();
    this.getSummary();
  }


  private getPrimaryBusinessLines() {
    this.primaryBusinessLine.read(this.primaryBusinessLinestate)
      .subscribe(
        (data: any) => {
        this.primaryBusinessLines = data.data;
          console.log(this.primaryBusinessLines);
        },
        () => { this.loadingService.toggleLoadingIndicator(false); });
  }

  private getAssignmentGroups() {
    this.assignmentGroupService.readTop20(this.assignmentGroupState)
      .subscribe(
        (data: any) => {
        this.assignmentGroups = data.data;
          console.log(this.assignmentGroups);
        },
        () => { this.loadingService.toggleLoadingIndicator(false); });
  }

  private getApplications() {
    this.applicationService.readTop20(this.applicationState)
      .subscribe(
        (data: any) => {
        this.applications = data.data;
          console.log(this.applications);
        },
        () => { this.loadingService.toggleLoadingIndicator(false); });
  }

  private getPersons() {
    this.personService.readTop20(this.personState)
      .subscribe(
        (data: any) => {
        this.persons = data.data;
          console.log(this.persons);
        },
        () => { this.loadingService.toggleLoadingIndicator(false); });
  }

  private getSummary() {
    this.summaryService.read(this.summaryState)
      .subscribe(
        (data: any) => {
        this.summaries = data.data;
          console.log(this.summaries);
        },
        () => { this.loadingService.toggleLoadingIndicator(false); });
  }

}
