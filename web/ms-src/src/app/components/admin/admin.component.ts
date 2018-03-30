import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  user: Object;

  constructor(
    private auth_service: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
      this.auth_service.get_admin().subscribe(profile => {
        this.user = profile.user.user_type;
      },
      err => {
        console.log(err);
        return false;
      });
  }

}
