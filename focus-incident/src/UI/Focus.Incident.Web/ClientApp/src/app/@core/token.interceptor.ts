import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from './services/authentication.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromPromise'
import 'rxjs/add/operator/mergeMap';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthenticationService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //* use cookie
    //request = request.clone({ 
    //  withCredentials: true
    //}); 
    //return next.handle(request);

    //* use bearer token 
    var headers = new HttpHeaders();

    // don't set the content-type to json when uploading files
    if (request.method == 'POST' && request.body.toString() != "[object FormData]") {
      headers = headers.set('Content-Type', 'application/json');
      headers = headers.set('Accept', 'application/json');
    }
    return Observable.fromPromise(this.auth.getAuthorizationHeader()).mergeMap(headerValue => {
      if (headerValue) headers = headers.set('Authorization', headerValue);

      request = request.clone({
        headers: headers
      });
      return next.handle(request);
    });
  }
}
