import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { environment } from '../../../environments/environment';

import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { Observable } from 'rxjs/Observable';

export function getClientSettings(): UserManagerSettings {
  return {
    authority: environment.authentication.authority,
    client_id: environment.authentication.client_id,
    redirect_uri: environment.authentication.redirect_uri,
    post_logout_redirect_uri: environment.authentication.post_logout_redirect_uri,
    response_type: "id_token token",
    scope: environment.authentication.scope,
    silent_redirect_uri: environment.authentication.silent_redirect_uri,
    automaticSilentRenew: true,
    accessTokenExpiringNotificationTime: 10, // The number of seconds before an access token is to expire to raise the accessTokenExpiring event
    //silentRequestTimeout: 100, // Number of milliseconds to wait for the silent renew to return before assuming it has failed or timed out
    filterProtocolClaims: true,
    loadUserInfo: true
  };
}

@Injectable()
export class AuthenticationService {
  user = new ReplaySubject<User>();
  private userCache: User;

  private manager = new UserManager(getClientSettings());

  constructor(private router: Router) {
    this.manager.getUser().then(user => {
      this.userCache = user;
      this.user.next(user); 
    });

    this.manager.events.addSilentRenewError((error) => { 
      this.manager.clearStaleState();
      this.manager.removeUser();
      this.manager.revokeAccessToken();
      this.user.next(null);

      // store the url we're trying to access so that we can return to it after loggin in
      localStorage.setItem('signin-redirect-url', router.routerState.snapshot.url);

      router.navigate(['/session-expired']);
    });
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  } 

  signOut(): Promise<void> {
    return this.manager.signoutRedirect();
  }

  getAuthorizationHeader(): Promise<string> {
    return this.manager.getUser().then(user => {
      const token = user.access_token;
      if (token !== '') {
        return 'Bearer ' + token;
      }
      return '';
    });
  } 
}
