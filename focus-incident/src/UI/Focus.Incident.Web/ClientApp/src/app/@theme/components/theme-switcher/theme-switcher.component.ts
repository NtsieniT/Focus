import { Component, OnInit } from '@angular/core';
import { NbThemeService } from '@nebular/theme';
import { NbJSThemeOptions } from '@nebular/theme/services/js-themes/theme.options';
//import { AnalyticsService } from '../../../@core/utils/analytics.service';
import { StateService } from '../../../@core/services/state.service';

@Component({
  selector: 'ngx-theme-switcher',
  styleUrls: ['./theme-switcher.component.scss'],
  template: `
    <label class="theme-switch">
      <span class="light" title="Light"><i class="nb-sunny"></i></span>      
      <div class="switch">
        <input type="checkbox" [checked]="currentBoolTheme()" (change)="toggleTheme(theme.checked)" #theme>
        <span class="slider"></span>
      </div>
      <span class="cosmic" title="Dark"><i class="nb-rainy"></i></span>
    </label>
  `,
})
export class ThemeSwitcherComponent implements OnInit {
  theme: NbJSThemeOptions;

  constructor(private themeService: NbThemeService/*, private analyticsService: AnalyticsService*/, protected stateService: StateService) {
  }

  ngOnInit() { 
    this.themeService.getJsTheme()
      .subscribe((theme: NbJSThemeOptions) => this.theme = theme); 

    // JASON customization
    this.stateService.onThemeState().subscribe(themeName => {
      this.themeService.changeTheme(themeName); 
    });
  }

  toggleTheme(theme: boolean) {
    const boolTheme = this.boolToTheme(theme);
    this.themeService.changeTheme(boolTheme);
    //this.analyticsService.trackEvent('switchTheme');

    // JASON customization
    this.stateService.setThemeState(boolTheme);
  }

  currentBoolTheme() {
    return this.themeToBool(this.theme);
  }

  private themeToBool(theme: NbJSThemeOptions) {
    return theme.name === 'cosmic';
  }

  private boolToTheme(theme: boolean) {
    return theme ? 'cosmic' : 'default';
  }
}
