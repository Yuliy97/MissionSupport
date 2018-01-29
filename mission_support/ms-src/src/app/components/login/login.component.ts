import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FlashMessagesService } from 'angular2-flash-messages/module';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: String;
  password: String;

  constructor(
    private auth_service: AuthService,
    private router: Router,
    private flash_message: FlashMessagesService
  ) { }

  ngOnInit() {
  }

  on_login() {
    const user = {
      username: this.username,
      password: this.password
    }

    console.log(this.username);

    this.auth_service.authenticate_user(user).subscribe(data => {
      if (data.success) {
        this.auth_service.store_data(data.token, data.user);
        this.flash_message.show('You are now logged in!', {cssClass: 'alert-success', timeout: 3000});
        this.router.navigate(['/']);
      } else {
        this.flash_message.show('Incorrect credentials, please try again.', {cssClass: 'alert-danger', timeout: 3000});
        this.router.navigate(['/login']);
      }
    });
  }
}
