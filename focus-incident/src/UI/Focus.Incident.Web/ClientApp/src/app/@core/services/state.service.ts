import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/observable/of';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class StateService {
  constructor(private cookieService: CookieService) {
    // JASON customization
    this.initLayout();
    this.initSidebar();
    this.initTheme();
  }

  protected layouts: any = [
    {
      name: 'One Column',
      icon: 'nb-layout-default',
      id: 'one-column',
      //selected: true,
    },
    {
      name: 'Two Column',
      icon: 'nb-layout-two-column',
      id: 'two-column',
    },
    {
      name: 'Center Column',
      icon: 'nb-layout-centre',
      id: 'center-column',
    },
  ];

  protected sidebars: any = [
    {
      name: 'Left Sidebar',
      icon: 'nb-layout-sidebar-left',
      id: 'left',
      //selected: true
    },
    {
      name: 'Right Sidebar',
      icon: 'nb-layout-sidebar-right',
      id: 'right'
    },
  ];

  protected layoutState$;//= new BehaviorSubject(this.layouts[0]);
  protected sidebarState$;//= new BehaviorSubject(this.sidebars[0]);
  protected themeState$;

  setLayoutState(state: any): any {
    this.layoutState$.next(state);

    // JASON customization
    this.cookieService.set('LayoutState', JSON.stringify(state));
  }

  getLayoutStates(): Observable<any[]> {
    return Observable.of(this.layouts);
  }

  onLayoutState(): Observable<any> {
    return this.layoutState$.asObservable();
  }

  setSidebarState(state: any): any {
    this.sidebarState$.next(state);

     // JASON customization
    this.cookieService.set('SidebarState', JSON.stringify(state));
  }

  getSidebarStates(): Observable<any[]> {
    return Observable.of(this.sidebars);
  }

  onSidebarState(): Observable<any> {
    return this.sidebarState$.asObservable();
  } 
   

  setThemeState(state: any): any {
    this.themeState$.next(state);

    // JASON customization
    this.cookieService.set('ThemeState', JSON.stringify(state));
  }
  onThemeState(): Observable<any> {
    return this.themeState$.asObservable();
  }

  // JASON customization
  initLayout() {
    // load from cookie
    var state;
    var cookieState = this.cookieService.get('LayoutState');
    if (cookieState) state = JSON.parse(cookieState);
    else state = this.layouts[0]; // default to first item

    // update selected item in array (used to indicate the selected options in UI)
    this.layouts = this.layouts.map(x => {
      x.selected = x.id == state.id;
      return x;
    });
     
    // update observable (used to render layout in UI)
    this.layoutState$ = new BehaviorSubject(state); 
  }
  initSidebar() {
    // load from cookie
    var state;
    var cookieState = this.cookieService.get('SidebarState');
    if (cookieState) state = JSON.parse(cookieState);
    else state = this.sidebars[0]; // default to first item

    // update selected item in array (used to indicate the selected options in UI)
    this.sidebars = this.sidebars.map(x => {
      x.selected = x.id == state.id;
      return x;
    });

    // update observable (used to render sidebar in UI)
    this.sidebarState$ = new BehaviorSubject(state);
  } 
  initTheme() {    
    // load from cookie
    var state = '';
    var cookieState = this.cookieService.get('ThemeState');
    if (cookieState) state = JSON.parse(cookieState);
    else state = 'cosmic'; // default to cosmic

    // update observable (used to render theme in UI)
    this.themeState$ = new BehaviorSubject(state);
  }
}
