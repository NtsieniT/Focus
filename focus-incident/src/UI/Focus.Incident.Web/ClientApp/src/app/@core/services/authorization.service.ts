// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { ReplaySubject } from 'rxjs/ReplaySubject';

// import { AuthenticationService } from './authentication.service';
// import { environment } from '../../../environments/environment';

// @Injectable()
// export class AuthorizationService {
//   roles = new ReplaySubject<string[]>();
//   private BASE_URL: string = `${environment.authorization.authority}/odata/CurrentUser/GetDistinctRoles`;

//   constructor(private authService: AuthenticationService, private http: HttpClient) {
//     this.authService.user.subscribe(user => {
//       if (!user)
//         this.roles.next([]);
//       else
//         this.http.get<string[]>(`${this.BASE_URL}`).subscribe(
//           roles => this.roles.next(roles),
//           error => {
//             this.roles.error(error)
//           });
//     });
//   }
// }
