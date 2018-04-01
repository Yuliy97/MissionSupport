import { Component, Injectable, OnInit, NgZone } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { FlashMessagesService } from 'angular2-flash-messages/module';

var sites = [];
var users = [];
var initialOpen = true;
var somethingChanged = false;

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent implements OnInit {

  user: Object;

  constructor(
    private auth_service: AuthService,
    private router: Router,
    private location: Location,
    private flash_message: FlashMessagesService
  ) { }

  ngOnInit() {
    if (this.load()) {
        this.load_site_info();
        this.load_user_info();
    }
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

  deleteUser(user) {
      if(confirm("Are you sure to delete the user:  "+ JSON.stringify(user.username) + "?")) {
          console.log("Implement delete user functionality here from DB (NOT DONE)");
          var index = users.indexOf(user, 0);
          if (index > -1) {
              users.splice(index, 1);
          }
          this.flash_message.show('Deleted User!', {cssClass: 'alert-success', timeout: 3000});
      }
  }

  verifyUser(user) {
      if(confirm("Are you sure to verify the user:  "+ JSON.stringify(user.username) + "?")) {
          console.log("Implement verify user functionality here from DB (NOT DONE)");
           var index = users.indexOf(user, 0);
          this.flash_message.show('Verified User!', {cssClass: 'alert-success', timeout: 3000});
      }
  }

  deleteSite(site) {
      if(confirm("Are you sure to delete the site: "+ JSON.stringify(site.name) + "?")) {
          console.log("Implement delete site functionality here from DB (NOT DONE)");
          var index = sites.indexOf(site, 0);
          if (index > -1) {
              sites.splice(index, 1);
          }
          this.flash_message.show('Deleted Site!', {cssClass: 'alert-success', timeout: 3000});
      }
  }

  load() {
    if (!this.auth_service.accessedSite()) {
        initialOpen = false;
        return true;
    } else if (initialOpen) {
        initialOpen = false;
        return true;
    } else if (somethingChanged) {
        initialOpen = false;
        somethingChanged = false;
        return true;
    }
  }

  site_info = sites;
  user_info = users;
}
