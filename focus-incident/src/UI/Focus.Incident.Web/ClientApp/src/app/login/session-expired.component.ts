import { Component, ViewChild, Injectable } from '@angular/core';
import { AuthenticationService } from '../@core/services/authentication.service';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';

@Component({
  templateUrl: './session-expired.component.html',
  styleUrls: ['./session-expired.component.css']
})
@Injectable()
export class SessionExpired {

  constructor() {
  }
}
