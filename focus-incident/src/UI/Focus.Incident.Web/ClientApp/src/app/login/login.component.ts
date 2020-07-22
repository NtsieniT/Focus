import { Component, ViewChild, Injectable, AfterViewInit } from '@angular/core';
import { AuthenticationService } from '../@core/services/authentication.service';
import { Notifications } from '../@core/components/notification.component';
import { LoadingService } from '../@core/services/loading.service';

@Component({ 
   template: ''
})
@Injectable()
export class Login implements AfterViewInit { 
  @ViewChild(Notifications) notifications: Notifications;

  constructor(public authService: AuthenticationService, private loadingService: LoadingService) { 
  }

  ngAfterViewInit() {
    this.loadingService.toggleLoadingIndicator(true);
    this.authService.startAuthentication().catch(e => {
        this.loadingService.toggleLoadingIndicator(false);
        this.notifications.error = 'There was a problem logging in. Please try again later :(';
    });
  } 
}
