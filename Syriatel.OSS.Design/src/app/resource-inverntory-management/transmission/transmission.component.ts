import { Component, OnInit ,ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import * as $ from "jquery";
import { PlatformLocation } from '@angular/common';
import { AppConfiguration } from '../extraClasses/AppConfiguration';



@Component({
  selector: 'app-transmission',
  templateUrl: './transmission.component.html'


})



export class TransmissionComponent implements OnInit {
  
  model:string;
  constructor(location: PlatformLocation,private _router: Router) 
  { 
     window.scrollTo(0, 0);
  }

  
  ngOnInit() {
    //debugger;
    if(this._router.url.includes(AppConfiguration.ModelNameTransmission)){
      this.model=AppConfiguration.ModelNameTransmission;
    }
    else if(this._router.url.includes(AppConfiguration.ModelNameRAN)){
      this.model=AppConfiguration.ModelNameRAN;
    }
  }


}

