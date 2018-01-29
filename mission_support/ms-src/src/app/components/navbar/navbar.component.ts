import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FlashMessagesService } from 'angular2-flash-messages/module';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(
    private auth_service: AuthService,
    private router: Router,
    private flash_message: FlashMessagesService
  ) { }

  ngOnInit() {
  }

  on_logout() {
    this.auth_service.logout();
    this.flash_message.show('You are now logged out.', {cssClass: 'alert-info', timeout: 3000});
    this.router.navigate(['/login']);
    return false;
  }

}
