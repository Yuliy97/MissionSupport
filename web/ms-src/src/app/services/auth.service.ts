import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { tokenNotExpired } from 'angular2-jwt';
import { Location } from '@angular/common';
import 'rxjs/add/operator/map';

@Injectable()
export class AuthService {
  authToken: any;
  user: any;
  site: any;
  administrator: boolean;

  constructor(private http: Http) { }

  //User auth
  register_user(user) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.post('http://localhost:3000/users/register', user, {headers: headers}).map(res => res.json());
  }

  authenticate_user(user) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.post('http://localhost:3000/users/auth', user, {headers: headers}).map(res => res.json());
  }

  authenticate_user_email(user) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.post('http://localhost:3000/users/reset', user, {headers: headers}).map(res => res.json());
  }

  loggedIn() {
      return tokenNotExpired('id_token');
  }

  isAdmin() {
    if(localStorage.getItem('admin') == 'true') {
        return tokenNotExpired('id_token');
    }
  }

  accessedSite() {
      if(localStorage.getItem('sites_accessed') == 'true'){
          return true;
      }else{
          return false;
      }
  }

  store_Admin(token, user) {
      localStorage.setItem('id_token', token);
      localStorage.setItem('user', JSON.stringify(user));
      this.authToken = token;
      this.user = user;
      this.administrator = true;
      localStorage.setItem('admin', 'true');
  }

  get_profile() {
    let headers = new Headers();
    this.load_token();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.get('http://localhost:3000/users/profile', {headers: headers}).map(res => res.json());
  }

  store_data(token, user) {
    localStorage.setItem('id_token', token);
    localStorage.setItem('user', JSON.stringify(user));
    this.authToken = token;
    this.user = user;
  }

  load_token() {
    const token = localStorage.getItem('id_token');
    console.log(token);
    this.authToken = token;
  }

  logout() {
    this.authToken = null;
    this.user = null;
    localStorage.clear();
    window.location.reload();
  }

  get_all_users() {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.get('http://localhost:3000/users/all_users', {headers: headers}).map(res => res.json());
  }

  //Site auth
  create_site(site) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.post('http://localhost:3000/sites/create', site, {headers: headers}).map(res => res.json());
  }

  get_all_sites() {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    localStorage.setItem('sites_accessed', 'true');
    return this.http.get('http://localhost:3000/sites/all_sites', {headers: headers}).map(res => res.json());
  }

  last_accesed_site(site) {
      this.site = site;
  }

  get_last_accesed_site() {
      return this.site;
  }

  update_site(site) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.post('http://localhost:3000/sites/update', site, {headers: headers}).map(res => res.json());
  }

}
