import { Component, OnInit, Input,OnChanges, SimpleChanges, ChangeDetectorRef } from '@angular/core';
import { TabDetails } from '../extraClasses/TabDetails';
import { Router } from '@angular/router';
import { ModelInfo } from '../extraClasses/ModelInfo';
import { AppConfiguration } from '../extraClasses/AppConfiguration';

@Component({
  selector: 'app-header-module',
  templateUrl: './header-module.component.html',
  styleUrls: ['./header-module.component.css']
})
export class HeaderModuleComponent implements OnInit{//,OnChanges 
  @Input() Model1: string;
  public tabs: TabDetails[];
  public Model:ModelInfo;
  
  //public config:AppConfiguration=new AppConfiguration();

  constructor(private _router: Router,private cdr: ChangeDetectorRef) {

  }
  // ngOnChanges(changes: SimpleChanges) {
  //   console.log("navigate to new route");
  //   for (let propName in changes) {  
  //     // let change = changes[propName];
      
  //     // let curVal  = JSON.stringify(change.currentValue);
  //     // let prevVal = JSON.stringify(change.previousValue);
  //     // let changeLog = `${propName}: currentValue = ${curVal}, previousValue = ${prevVal}`;
      
  //     if (propName === 'Model1') {
  //       if (this.Model1==AppConfiguration.ModelNameRAN){
  //     this.Model= null;
  //     this.Model=new ModelInfo(AppConfiguration.ModelBackgroundImgClassName_theme2,AppConfiguration.ModelTabsClassName_theme2,"RAN");
  //     console.log(this.Model);
  //     this.cdr.detectChanges();
  //     //ChangeDetectorRef.detectChanges();
  //   }
  //   else if(this.Model1==AppConfiguration.ModelNameTransmission){
  //     this.Model= null;
  //     this.Model=this.Model= new ModelInfo(AppConfiguration.ModelBackgroundImgClassName_theme1,AppConfiguration.ModelTabsClassName_theme1,"Transmission");
  //     console.log(this.Model);
  //     this.cdr.detectChanges();
  //   }
  //        //this.allMsgChangeLogs.push(changeLog);
  //     // } else if (propName === 'employee') {
  //     //    this.allEmployeeChangeLogs.push(changeLog);
  //     // }
  //         }
  // }}

  ngOnInit() {
    // if (this.Model1==AppConfiguration.ModelNameRAN){
    //   this.Model= new ModelInfo(AppConfiguration.ModelBackgroundImgClassName_theme2,AppConfiguration.ModelTabsClassName_theme2,"RAN");
      
    // }
    // else if(this.Model1==AppConfiguration.ModelNameTransmission){
    //   this.Model= new ModelInfo(AppConfiguration.ModelBackgroundImgClassName_theme1,AppConfiguration.ModelTabsClassName_theme1,"Transmission");
    // }
      if(this._router.url.includes("Transmission")){

        this.Model= new ModelInfo(AppConfiguration.ModelBackgroundImgClassName_theme1,AppConfiguration.ModelTabsClassName_theme1,"Transmission");
        
      }
      else if(this._router.url.includes("RAN")){

        this.Model= new ModelInfo(AppConfiguration.ModelBackgroundImgClassName_theme2,AppConfiguration.ModelTabsClassName_theme2,"RAN");
      }
  }

}
