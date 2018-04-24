import { Component, OnInit } from '@angular/core';
import { ValidateService } from '../../services/validate.service';
import { AuthService } from '../../services/auth.service';
import { FlashMessagesService } from 'angular2-flash-messages/module';
import { Router } from '@angular/router';

/*TODO: Create more relevant fields for registration and provide other standards for password and username validation.
* (i.e. password must be at least 8 characters long and contain one numeric character)
*/

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  first_name: String;
  last_name: String;
  username: String;
  email: String;
  organization: String;
  password: String;

  constructor(
    private validate_service: ValidateService,
    private flash_message: FlashMessagesService,
    private auth_service: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  on_register() {
    const user = {
      first_name: this.first_name,
      last_name: this.last_name,
      username: this.username,
      email: this.email,
      organization: this.organization,
      password: this.password
    }

    // Validate fields and email
    if (!this.validate_service.validate_register(user)) {
      this.flash_message.show('Please fill in all the fields!', {cssClass: 'alert-danger', timeout: 3000});
      return false;
    }

    if (!this.validate_service.validate_email(user.email)) {
      this.flash_message.show('Please enter a valid email address!');
      return false;
    }

    // Register user
    this.auth_service.register_user(user).subscribe(data => {
      if (data.success) {
        this.flash_message.show('You have registered successfully and may now log in!', {cssClass: 'alert-success', timeout: 3000});
        // upon success redirect to login
        this.router.navigate(['/login']);
      } else {
        this.flash_message.show('Something went wrong, please try again.', {cssClass: 'alert-danger', timeout: 3000});
        // go back to registration screen
        this.router.navigate(['/registration']);
      }
    });
  }

}
