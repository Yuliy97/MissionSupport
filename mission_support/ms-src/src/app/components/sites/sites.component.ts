import { Component, OnInit } from '@angular/core';
import { ValidateService } from '../../services/validate.service';
import { AuthService } from '../../services/auth.service';
import { FlashMessagesService } from 'angular2-flash-messages/module';
import { IMyDpOptions } from 'mydatepicker';


@Component({
  selector: 'app-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.css']
})
export class SitesComponent implements OnInit {

  site_name: String;
  site_address: String;
  site_date: String;

  myDatePickerOptions: IMyDpOptions = {
    dateFormat: 'mm.dd.yyyy',
    selectionTxtFontSize: '15px'
  };

  private placeholder: string = 'Select a date';

  constructor(
    private validate_service: ValidateService,
    private flash_message: FlashMessagesService,
    private auth_service: AuthService
  ) { }
  lat: number = 33.7490;
  lng: number = -84.3880;
  ngOnInit() {
  }

  on_add() {
    const site = {
      site_name: this.site_name,
      site_address: this.site_address,
      site_date: this.site_date
    }

    if (!this.validate_service.validate_site(site)) {
      alert("Please fill in all the fields");
      return false;
    }
  }

}
