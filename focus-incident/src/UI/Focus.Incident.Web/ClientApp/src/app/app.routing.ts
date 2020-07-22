import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { Login } from './login/login.component';
import { Logout } from './login/logout.component';
import { SessionExpired } from './login/session-expired.component';
import { BusinessLineListComponent } from './business-line/business-line-list.component';
import { ApplicationListComponent } from './application/application-list.component';
import { AssignmentGroupListComponent } from './assignment-group/assignment-group-list.component';
import { PersonListComponent } from './person/person-list.component';
import { SummaryOverviewListComponent } from './summary-overview/summary-overview-list.component';
import { OverviewDetailListComponent } from './overview/overview-detail-list.component';
import { LoggedInGuard } from './@core/guards/logged-in.guard';

const appRoutes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: Login },
  { path: 'logout', component: Logout },
  { path: 'session-expired', component: SessionExpired },
  { path: 'businessLine', component: BusinessLineListComponent, canActivate: [LoggedInGuard] },
  { path: 'application', component: ApplicationListComponent, canActivate: [LoggedInGuard] },
  { path: 'applicationGroup', component: AssignmentGroupListComponent, canActivate: [LoggedInGuard] },
  { path: 'person', component: PersonListComponent, canActivate: [LoggedInGuard] },
  { path: 'SummaryOverview', component: SummaryOverviewListComponent, canActivate: [LoggedInGuard] },
  { path: 'Overview', component: OverviewDetailListComponent, canActivate: [LoggedInGuard] },
  { path: '**', component: HomeComponent }
];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(appRoutes);
