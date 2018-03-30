import { Component, Injectable, OnInit, NgZone } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

var sites = [];
var users = [];

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
    this.load_site_info();
    this.load_user_info();
  }

  containsObject(obj, list) {
    var i;
    for (i = 0; i < list.length; i++) {
        if (list[i] === obj) {
            return true;
        }
    }
    return false;
  }

  load_site_info() {
    this.auth_service.get_all_sites().subscribe(data => {
      console.log(data);
      for (var i = 0; i < data.length; i++) {
        var info = {name: data[i].site_name, address: data[i].site_address, date: data[i].created_on};
        if (!this.containsObject(info, sites)) {
          sites.push(info);
        }
      }
    });
  }

  load_user_info() {
    this.auth_service.get_all_users().subscribe(data => {
      console.log(data);
      for (var i = 0; i < data.length; i++) {
        var info = {username: data[i].username, email: data[i].email, organization: data[i].organization, first_name: data[i].first_name, last_name: data[i].last_name};
        if (!this.containsObject(info, users)) {
          users.push(info);
        }
      }
    });
  }

  site_info = sites;
  user_info = users;
}
