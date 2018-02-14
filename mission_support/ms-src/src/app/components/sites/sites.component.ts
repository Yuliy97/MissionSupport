import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.css']
})
export class SitesComponent implements OnInit {

  constructor() { }
  lat: number = 33.7490;
  lng: number = -84.3880;
  ngOnInit() {
  }

}
