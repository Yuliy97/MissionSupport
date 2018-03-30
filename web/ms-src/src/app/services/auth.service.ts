import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { tokenNotExpired } from 'angular2-jwt';
import 'rxjs/add/operator/map';

@Injectable()
export class AuthService {
  authToken: any;
  user: any;
  site: any;

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

  //TO DO: HOW DO I GET THIS TO ONLY GIVE TOKEN TO USERS WHO HAVE USER_TYPE: Admin? HELP :,(
  isAdmin() {
      return tokenNotExpired('id_token');
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
    return this.http.get('http://localhost:3000/sites/all_sites', {headers: headers}).map(res => res.json());
  }
}
