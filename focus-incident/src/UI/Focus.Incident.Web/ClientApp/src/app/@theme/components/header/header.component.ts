import { Component, Input, OnInit } from '@angular/core';

import { NbMenuService, NbSidebarService, NbSearchService } from '@nebular/theme';
//import { AnalyticsService } from '../../../@core/utils/analytics.service'; 
import { AuthenticationService } from '../../../@core/services/authentication.service';
import { NbMenuItem } from '@nebular/theme/components/menu/menu.service';
import { User } from 'oidc-client';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'ngx-header',
  styleUrls: ['./header.component.scss'],
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit {
  @Input() position = 'normal';

  user: User;

  userMenu: NbMenuItem[] = [{ title: 'Log out', link: '/logout' }];

  constructor(private sidebarService: NbSidebarService,
    private menuService: NbMenuService,
    public authService: AuthenticationService,
    private searchService: NbSearchService,
    private router: Router
  ) {
    searchService.onSearchSubmit().subscribe((search:any) => { 
      this.router.navigate(['/configitems', search.term]);
    });
  }

  ngOnInit() {
    this.authService.user.subscribe((user: User) => this.user = user);
  }

  toggleSidebar(): boolean {
    this.sidebarService.toggle(true, 'menu-sidebar');
    return false;
  }

  toggleSettings(): boolean {
    this.sidebarService.toggle(false, 'settings-sidebar');
    return false;
  }

  goToPortal() {
    window.location.href = environment.portalUrl;
  }

  goToHome() {
    this.menuService.navigateHome();
  }

  startSearch() {
    //this.analyticsService.trackEvent('startSearch'); 
  } 
}
