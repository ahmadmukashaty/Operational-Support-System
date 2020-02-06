import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NeChartsComponent } from './ne-charts.component';
import { DataTableModule, SharedModule } from 'primeng/primeng';
//import { LoadingModule } from 'ngx-loading';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '../../../../../node_modules/@angular/platform-browser/animations';
import {
  DropdownModule, PanelModule, MultiSelectModule, ButtonModule, DataListModule, AccordionModule, TooltipModule, BreadcrumbModule, MenuItem, ChartModule, TabMenuModule, AutoCompleteModule, RadioButtonModule
  , TreeModule, TreeNode, DialogModule
} from 'primeng/primeng';
import { FormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import { NgxLoadingModule  } from 'ngx-loadingV2';
import { ChartsModule } from 'ng2-charts';


@NgModule({
  imports: [
    CommonModule, DataTableModule, BrowserModule,
    BrowserAnimationsModule, DropdownModule, PanelModule, MultiSelectModule, ButtonModule, DataListModule, AccordionModule, TooltipModule, BreadcrumbModule, ChartModule, TabMenuModule, AutoCompleteModule, RadioButtonModule
    , TreeModule, DialogModule, ChartsModule, SharedModule, FormsModule,
    AgmCoreModule.forRoot({
      // please get your own API key here:
      // https://developers.google.com/maps/documentation/javascript/get-api-key?hl=en
      apiKey: 'AIzaSyCQeY11JIl0yC5kHqpHNMOzkQprYHrUVzY'
    }),
    NgxLoadingModule.forRoot({}),
  ],
  declarations: [NeChartsComponent]
})
export class NeChartsModule { }
