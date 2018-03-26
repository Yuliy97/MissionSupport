import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FlashMessagesService } from 'angular2-flash-messages/module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reset',
  templateUrl: './reset.component.html',
  styleUrls: ['./reset.component.css']
})
export class ResetComponent implements OnInit {

  email: String;

  constructor(
    private flash_message: FlashMessagesService,
    private auth_service: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  on_forgot_pass() {

    const user = {
      email: this.email,
    }

    this.auth_service.authenticate_user_email(user).subscribe(data => {
      if (data.success) {
        this.flash_message.show('An email has been sent with your password!', {cssClass: 'alert-success', timeout: 3000});
      } else {
        this.flash_message.show('Hmm, please try a different email.', {cssClass: 'alert-danger', timeout: 3000});
      }
    });
  }

}
