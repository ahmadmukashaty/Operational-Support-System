import { Component, OnInit, Input } from '@angular/core';
import { ModelInfo } from '../../extraClasses/ModelInfo';
import { AppConfiguration } from '../../extraClasses/AppConfiguration';

@Component({
  selector: 'app-rim-navigation',
  templateUrl: './rim-navigation.component.html',
  styleUrls: ['./rim-navigation.component.css']
})
export class RimNavigationComponent implements OnInit {
  @Input()
  ModelInfoObj2:ModelInfo;
  //public config:AppConfiguration=new AppConfiguration();
  
  constructor() { }

  ngOnInit() {
    if (this.ModelInfoObj2.ModelName=="Transmission"){
      this.ModelInfoObj2.SetnavigationInfo(AppConfiguration.ModelNavigationEvenBlock_theme1,AppConfiguration.ModelNavigationOddBlock_theme1);
    }
    else if(this.ModelInfoObj2.ModelName=="RAN"){
      this.ModelInfoObj2.SetnavigationInfo(AppConfiguration.ModelNavigationEvenBlock_theme2,AppConfiguration.ModelNavigationOddBlock_theme2);
    }
  }

}
