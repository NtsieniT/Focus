import { Component } from '@angular/core';
import { AuthenticationService } from '../@core/services/authentication.service';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent {
  isLoggedIn = false;

  constructor(private authService: AuthenticationService) {
    authService.user.subscribe(user => {
      this.isLoggedIn = user != null;
    });
  }
}
