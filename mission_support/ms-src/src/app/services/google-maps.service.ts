import { Injectable, NgZone } from '@angular/core';
import { GoogleMapsAPIWrapper } from 'angular2-google-maps/core';
import { MapsAPILoader } from 'angular2-google-maps/core';
import { Observable, Observer } from 'rxjs';

declare var google: any;

@Injectable()
export class GMapsService extends GoogleMapsAPIWrapper {
    constructor(private __loader: MapsAPILoader, private __zone: NgZone) {
        super(__loader, __zone);
    }

    getLatLan(address: string) {
        return Observable.create(observer => {
            try {
                //at this point the variable google may be still undefined (google maps scripts still loading)
                //so load all the scripts, then...
                this.__loader.load().then(() => {
                    let geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ address }, (results, status) => {

                        if (status === google.maps.GeocoderStatus.OK) {
                            const place = results[0].geometry.location;
                            observer.next(place);
                            observer.complete();
                        } else {
                            console.error('Error - ', results, ' & Status - ', status);
                            if (status === google.maps.GeocoderStatus.ZERO_RESULTS) {
                                observer.error('Address not found!');
                            }else {
                                observer.error(status);
                            }

                            observer.complete();
                        }
                    });
                });
            } catch (error) {
                observer.error('error getGeocoding' + error);
                observer.complete();
            }

        });
    }
}
