import { Component, ViewChild, Input, Injectable, ViewEncapsulation} from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { toODataString, State } from '@progress/kendo-data-query';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';
import { Person } from '../Common/person/person.model';
import { PersonService } from '../Common/person/person.service';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss']
})
export class PersonComponent {
  }
