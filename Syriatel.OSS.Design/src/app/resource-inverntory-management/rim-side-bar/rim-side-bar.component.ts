import { Component, OnInit, Input, Output ,EventEmitter} from '@angular/core';
import * as $ from "jquery";
import { AppConfiguration } from '../extraClasses/AppConfiguration';
import { Router, ActivatedRoute } from '@angular/router';
//import { EventEmitter } from 'events';

@Component({
  selector: 'app-rim-side-bar',
  templateUrl: './rim-side-bar.component.html',
  styleUrls: ['./rim-side-bar.component.css']
})
export class RimSideBarComponent implements OnInit {
  @Input() Model: string;
  //@Output() NavigateModel:EventEmitter<string> =   new EventEmitter();
  
  constructor(private _router: Router,private route: ActivatedRoute) { }

  ngOnInit() {
    console.log("from side bar, here is the model ",this.Model);
    if(this.Model==AppConfiguration.ModelNameTransmission){
      $("#elem1 span").addClass('sidebarActive');
    }
    else if(this.Model==AppConfiguration.ModelNameRAN){
      $("#elem2 span").addClass('sidebarActive');
    }
  }

  SidebarActive(event)
  {
    let model_to:string = $(event.target.parentElement).find("span").text();
    //console.log("model tooooooo ",$(event.target.parentElement).val($($(this).children("span")[0]).text()));//children("span"));
    if($(event.target.parentElement).children("span").hasClass("sidebarActive"))
    {
      $("#snav ul li a span").removeClass('sidebarActive');
    }
    else
    {
      $("#snav ul li a span").removeClass('sidebarActive');
      $(event.target.parentElement).children("span").addClass("sidebarActive");
      //debugger;
      if(model_to==AppConfiguration.ModelNameTransmission){
        //this.NavigateModel.emit(model_to);
        this._router.navigate(['../Transmission'], { relativeTo: this.route });
      }
      else if(model_to==AppConfiguration.ModelNameRAN){
        //this.NavigateModel.emit(model_to);
        this._router.navigate(['../RAN'], { relativeTo: this.route });
      }
    }
  }

 
}
