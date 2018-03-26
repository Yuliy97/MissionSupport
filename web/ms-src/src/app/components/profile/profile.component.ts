import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

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
