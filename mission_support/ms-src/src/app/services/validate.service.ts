import { Injectable } from '@angular/core';

@Injectable()
export class ValidateService {

  constructor() { }

  validate_register(user) {
    if (user.first_name == undefined || user.last_name == undefined || user.username == undefined || user.email == undefined || user.password == undefined) {
      return false;
    }
    return true;
  }

  validate_email(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

}
