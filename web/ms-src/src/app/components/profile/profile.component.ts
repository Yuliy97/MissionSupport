import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

/* TODO: Add relevant profile information as well as means to upload picture and save to the DB.
* Also, the profile tab should not be accesible by typing the url into the address bar.
* -- Refer to the auth.guard.ts file under /guards folder to find a similar method of implementing this.
*/

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user: Object;

  constructor(
    private auth_service: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.auth_service.get_profile().subscribe(profile => {
      this.user = profile.user;
    },
    err => {
      console.log(err);
      return false;
    });
  }
}
