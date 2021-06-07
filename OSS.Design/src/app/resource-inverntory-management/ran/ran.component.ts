import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppConfiguration } from '../extraClasses/AppConfiguration';

@Component({
  selector: 'app-ran',
  templateUrl: './ran.component.html'
})
export class RanComponent implements OnInit {
  model:string;
  constructor(private _router: Router)
   {
     window.scrollTo(0, 0); 
  }

  ngOnInit() {
    if(this._router.url.includes(AppConfiguration.ModelNameTransmission)){
      this.model=AppConfiguration.ModelNameTransmission;
    }
    else if(this._router.url.includes(AppConfiguration.ModelNameRAN)){
      this.model=AppConfiguration.ModelNameRAN;
    }
  }

}
