import { Component, ViewChild } from '@angular/core';

import { MENU_ITEMS } from './app-menu';
import { AuthorizationService } from './@core/services/authorization.service';
import { ToasterConfig } from 'angular2-toaster';
import { NbMenuItem } from '@nebular/theme';
import { Notifications } from './@core/components/notification.component';
import { LoadingService } from './@core/services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  //styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  menu = [];

  @ViewChild(Notifications) notifications: Notifications;

  config: ToasterConfig = new ToasterConfig({
    positionClass: 'toast-top-right',
    timeout: 5000,
    newestOnTop: true,
    tapToDismiss: true,
    preventDuplicates: false,
    animation: 'fade',
    limit: 5,
    mouseoverTimerStop: false
  });;

  constructor(private authorizationService: AuthorizationService, private loadingService: LoadingService) {
    this.menu = MENU_ITEMS;

    //this.loadingService.toggleLoadingIndicator(true);

    // * show/hide menu items based on user roles
    //this.authorizationService.roles.subscribe(roles => {
    //  // recurse and filter the MENU_ITEMS array based on user's Roles specified in the 'data' property
    //  var that = this;
    //  var recurse = function (arr: NbMenuItem[]): NbMenuItem[] {
    //    return arr.filter(menuItem => {
    //      var roles_menuItem = menuItem.data ? <string[]>menuItem.data : [];

    //      if (menuItem.children)
    //        menuItem.children = recurse(menuItem.children);

    //      if (!(menuItem.data) || that.intersection(roles, roles_menuItem).length > 0)
    //        return menuItem;
    //    });
    //  }

    //  // NB: deep copy to new object (so that the passed-in array doesn't get modified)
    //  this.menu = recurse(JSON.parse(JSON.stringify(MENU_ITEMS)));

    //  this.loadingService.toggleLoadingIndicator(false);
    //},
    //  error => {
    //    this.notifications.popupError = 'There was a problem accessing the authoriztion server. Some features may be unavailable : (';
    //    this.loadingService.toggleLoadingIndicator(false);
    //  });
  }

  intersection(array1, array2): Array<any> {
    return array1.filter(function (n) {
      return array2.indexOf(n) !== -1;
    });
  };
}
