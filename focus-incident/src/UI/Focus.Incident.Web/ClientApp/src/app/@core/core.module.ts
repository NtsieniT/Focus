import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CookieService } from 'ngx-cookie-service'; 
import { RippleModule } from '@progress/kendo-angular-ripple';

import { AuthenticationService } from './services/authentication.service';
import { AuthorizationService } from './services/authorization.service';
import { LoadingService } from './services/loading.service';
import { StateService } from './services/state.service'; 
import { Notifications } from './components/notification.component'; 

import { LoggedInGuard } from './guards/logged-in.guard';
//import { AdminGuard } from '../admin/admin.guard';
//import { ConfigItemGuard } from '../config-item/config-item.guard';
//import { ReportGuard } from '../report/report.guard';

const SERVICES = [
  CookieService,
  AuthenticationService,
  AuthorizationService,
  LoadingService,
  StateService,

  LoggedInGuard,
  //AdminGuard,
  //ConfigItemGuard,
  //ReportGuard
];

@NgModule({
  imports: [
    CommonModule, RippleModule
  ],
  declarations: [  
    Notifications
  ],
  exports: [  
    Notifications
  ],
  providers: [StateService]
})
export class CoreModule {
  static forRoot(): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: CoreModule,
      providers: [
        ...SERVICES,
      ],
    };
  }
}
