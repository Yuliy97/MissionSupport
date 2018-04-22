import { Component, Injectable, OnInit, NgZone } from '@angular/core';
import { ValidateService } from '../../services/validate.service';
import { AuthService } from '../../services/auth.service';
import { FlashMessagesService } from 'angular2-flash-messages/module';
import { IMyDpOptions } from 'mydatepicker';
import { Observable, Observer } from 'rxjs';
import { MapsAPILoader } from 'angular2-google-maps/core';
import { GoogleMapsAPIWrapper } from 'angular2-google-maps/core';
import { GMapsService } from '../../services/google-maps.service';
import { Location } from '@angular/common';

var markers = [];
var names = [];

@Component({
  selector: 'app-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.css']
})

@Injectable()
export class SitesComponent implements OnInit {

  markers = [];

  site_name: String;
  site_address: String;
  site_organization: String;
  site_information: String;
  created_on: Date;

  myDatePickerOptions: IMyDpOptions = {
    dateFormat: 'mm.dd.yyyy',
    selectionTxtFontSize: '15px'
  };

  constructor(
    private validate_service: ValidateService,
    private flash_message: FlashMessagesService,
    private auth_service: AuthService,
    private gm_service: GMapsService,
    private __zone: NgZone,
    private location: Location
  ) {
  }
  lat: number = 33.7490;
  lng: number = -84.3880;
  ngOnInit() {
    this.load_markers();
  }

  load_markers() {
    this.auth_service.get_all_sites().subscribe(data => {
      console.log(data);
      for (var i = 0; i < data.length; i++) {
        var addr = data[i].site_address;
        try {
          this.gm_service.getLatLan(addr);
          names.push({name: data[i].site_name, organization: data[i].site_organization, information: data[i].site_information, date: data[i].created_on, address: data[i].site_address});
        } catch (err) {
          //
        }
        this.gm_service.getLatLan(addr)
        .subscribe(
            result => {
                this.__zone.run(() => {
                    this.lat = result.lat();
                    this.lng = result.lng();
                    const mylatlng = {lat: this.lat, lng: this.lng};
                    markers.push(mylatlng)
                })
            },
          error => console.log(error),
          () => console.log('Geocoding completed!')
        );
      }
    });
  }

  on_add() {
    var addr_verification = false;

    const site = {
      site_name: this.site_name,
      site_address: this.site_address,
      site_organization: this.site_organization,
      site_information: this.site_information,
      created_on: this.created_on
    }

    console.log(this.site_name);
    console.log(this.site_address);
    console.log(this.site_organization);
    console.log(this.site_information);
    console.log(this.created_on);

    if (!this.validate_service.validate_site(site)) {
      alert("Please fill in all the fields");
      return false;
    }

    this.gm_service.getLatLan(this.site_address as string)
    .subscribe(
        result => {
            this.__zone.run(() => {
                this.lat = result.lat();
                this.lng = result.lng();
                const mylatlng = {lat: this.lat, lng: this.lng};
                addr_verification = true;
            })
        },
      error => console.log(error),
      () => console.log('Geocoding completed!')
    );

    if (addr_verification == false) {
      alert("Invalid address!");
      return false;
    }

    this.auth_service.create_site(site).subscribe(data => {
      if (data.success) {
        alert("You have successfully created a site");
        window.location.reload();
      } else {
        alert("Something went wrong");
      }
    });
  }

 on_edit(site) {
     this.auth_service.last_accesed_site(site);
 }

 on_save_edits(){
    var item = this.auth_service.get_last_accesed_site();

    item.information = this.site_information;

    console.log(item.site_name);
    console.log(item.site_address);
    console.log(item.site_organization);
    console.log(item.site_information);
    console.log(item.created_on);

    this.auth_service.update_site(item).subscribe(data => {
      if (data.success) {
        alert("You have successfully updated a site");
        window.location.reload();
      } else {
        alert("Something went wrong");
      }
    });
 }

  updated_markers = markers;
  site_info = names;
}
