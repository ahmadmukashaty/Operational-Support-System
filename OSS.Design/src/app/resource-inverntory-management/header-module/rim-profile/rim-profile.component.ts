import { Component, OnInit, Input } from '@angular/core';
import * as $ from "jquery";
import { trigger, state, style, transition, animate, AUTO_STYLE } from '@angular/animations';
import { ModelInfo } from '../../extraClasses/ModelInfo';
import { AppConfiguration } from '../../extraClasses/AppConfiguration';

@Component({
  selector: 'app-rim-profile',
  templateUrl: './rim-profile.component.html',
  styleUrls: ['./rim-profile.component.css'],
  animations: [
    trigger('mobileMenuTop', [
        state('no-block, void',
            style({
                overflow: 'hidden',
                height: '0px',
            })
        ),
        state('yes-block',
            style({
                height: AUTO_STYLE,
            })
        ),
        transition('no-block <=> yes-block', [
            animate('400ms ease-in-out')
        ])
    ])
  ]
})
export class RimProfileComponent implements OnInit {
 
    @Input()
    ModelInfoObj:ModelInfo;
//public config:AppConfiguration=new AppConfiguration();

  

  isCollapsedSideBar = 'no-block';
  
    constructor() { }
  
    ngOnInit() {
        if (this.ModelInfoObj.ModelName=="Transmission"){
            this.ModelInfoObj.SetProfileInfo(AppConfiguration.ModelProfileMainMenu_theme1,AppConfiguration.ModelProfile_theme1,AppConfiguration.ModelProfileMoreDetails_theme1);
            //console.log("ModelInfoObj input: ",this.ModelInfoObj);
        }
        else if(this.ModelInfoObj.ModelName=="RAN"){
            this.ModelInfoObj.SetProfileInfo(AppConfiguration.ModelProfileMainMenu_theme2,AppConfiguration.ModelProfile_theme2,AppConfiguration.ModelProfileMoreDetails_theme2);
            //console.log("ModelInfoObj input: ",this.ModelInfoObj);
        }

 
    }
  
    
    toggleOpenedSidebar() {
      this.isCollapsedSideBar = this.isCollapsedSideBar === 'yes-block' ? 'no-block' : 'yes-block';
      
    }

}
