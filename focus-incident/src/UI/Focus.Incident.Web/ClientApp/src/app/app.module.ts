import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // kendo needs this
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { LayoutModule } from '@progress/kendo-angular-layout';

import { CoreModule } from './@core/core.module';
import { ThemeModule } from './@theme/theme.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToasterModule, ToasterService, ToasterConfig } from 'angular2-toaster';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { Login } from './login/login.component';
import { Logout } from './login/logout.component';
import { SessionExpired } from './login/session-expired.component';

// todo: move to other modules
import { GridModule } from '@progress/kendo-angular-grid';
import { DropDownListModule } from '@progress/kendo-angular-dropdowns';
import { ChartsModule } from '@progress/kendo-angular-charts';
import 'hammerjs'; // todo
import { TooltipModule } from '@progress/kendo-angular-tooltip';

// import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BusinessLineComponent } from './business-line/business-line.component';
import { AssignmentGroupComponent } from './assignment-group/assignment-group.component';
import { PersonComponent } from './person/person.component';
import { ApplicationComponent } from './application/application.component';
import { BusinessLineListComponent } from './business-line/business-line-list.component';
import { ApplicationListComponent } from './application/application-list.component';
import { AssignmentGroupListComponent } from './assignment-group/assignment-group-list.component';
import { PersonListComponent } from './person/person-list.component';
import { SummaryOverviewComponent } from './summary-overview/summary-overview.component';
import { SummaryOverviewListComponent } from './summary-overview/summary-overview-list.component';
import { OverviewComponent } from './overview/overview.component';
import { OverviewDetailListComponent } from './overview/overview-detail-list.component';

import { PrimaryBusinessLineService } from './Common/primary-business-line/primary-business-line.service';
import { ApplicationService } from './Common/application/application.service';

import { AppRouting } from './app.routing';

// http
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './@core/token.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    Login,
    Logout,
    SessionExpired,

    // NavMenuComponent,
    BusinessLineComponent,
    AssignmentGroupComponent,
    PersonComponent,
    ApplicationComponent,
    BusinessLineListComponent,
    ApplicationListComponent,
    AssignmentGroupListComponent,
    PersonListComponent,
    SummaryOverviewComponent,
    SummaryOverviewListComponent,
    OverviewComponent,
    OverviewDetailListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    BrowserAnimationsModule,
    LayoutModule,
    AppRouting,

    NgbModule.forRoot(),
    ThemeModule.forRoot(),
    CoreModule.forRoot(),
    ToasterModule.forRoot(),

    GridModule,
    DropDownListModule,
    ChartsModule,
    TooltipModule,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }, ToasterService, PrimaryBusinessLineService, ApplicationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
