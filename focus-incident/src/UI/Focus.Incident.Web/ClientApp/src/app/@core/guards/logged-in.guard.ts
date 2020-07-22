import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { User } from 'oidc-client';

@Injectable()
export class LoggedInGuard implements CanActivate {

  constructor(private authService: AuthenticationService, private router: Router) {
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.authService.user.map(user => {
      if (user) return true;

      // store the url we're trying to access so that we can return to it after loggin in
      localStorage.setItem('signin-redirect-url', state.url);

      // Navigate to the login page
      this.router.navigate(['/login']);

      return false;
    });
  }
}
