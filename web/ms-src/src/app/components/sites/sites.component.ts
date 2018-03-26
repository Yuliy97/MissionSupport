import { Component, Injectable, OnInit, NgZone } from '@angular/core';
import { ValidateService } from '../../services/validate.service';
import { AuthService } from '../../services/auth.service';
import { FlashMessagesService } from 'angular2-flash-messages/module';
import { IMyDpOptions } from 'mydatepicker';
import { Observable, Observer } from 'rxjs';
import { MapsAPILoader } from 'angular2-google-maps/core';
import { GoogleMapsAPIWrapper } from 'angular2-google-maps/core';
import { GMapsService } from '../../services/google-maps.service'

var markers = [];

@Component({
  selector: 'app-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.css']
})

@Injectable()
export class SitesComponent implements OnInit {

  ms = [ {lat: 24, lng: -84} ];

  site_name: String;
  site_address: String;
  site_date: String;
  mkers: any[];

  myDatePickerOptions: IMyDpOptions = {
    dateFormat: 'mm.dd.yyyy',
    selectionTxtFontSize: '15px'
  };

  constructor(
    private validate_service: ValidateService,
    private flash_message: FlashMessagesService,
    private auth_service: AuthService,
    private gm_service: GMapsService,
    private __zone: NgZone
  ) {
    //this.load_markers();
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
        this.gm_service.getLatLan(addr)
        .subscribe(
            result => {
                this.__zone.run(() => {
                    this.lat = result.lat();
                    this.lng = result.lng();
                    const mylatlng = {lat: this.lat, lng: this.lng};
                    markers.push(mylatlng);
                })
            },
          error => console.log(error),
          () => console.log('Geocoding completed!')
        );
      }
    });
  }

  on_add() {
    //this.load_markers();
    console.log(markers)
    const site = {
      site_name: this.site_name,
      site_address: this.site_address,
      site_date: this.site_date
    }

    if (!this.validate_service.validate_site(site)) {
      alert("Please fill in all the fields");
      return false;
    }

    this.auth_service.create_site(site).subscribe(data => {
      if (data.success) {
        alert("You have successfully created a site");
      } else {
        alert("Something went wrong");
      }
    });
  }
}
