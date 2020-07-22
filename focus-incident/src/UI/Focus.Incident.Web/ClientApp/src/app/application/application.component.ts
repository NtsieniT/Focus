import { Component, ViewChild, Input, Injectable, ViewEncapsulation} from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { toODataString, State } from '@progress/kendo-data-query';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';
import { Application } from '../Common/application/application.model';
import { ApplicationService } from '../Common/application/application.service';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.scss']
})
export class ApplicationComponent {

}
