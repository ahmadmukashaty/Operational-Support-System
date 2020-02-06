import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppRoutes } from './app.routing';
import { AppComponent } from './app.component';
import {HashLocationStrategy,LocationStrategy, PathLocationStrategy} from '@angular/common';
import { ResourceInverntoryManagementComponent } from './resource-inverntory-management/resource-inverntory-management.component';
import { SidebarService } from './resource-inverntory-management/services/getSidebarMenu.service';
import { TreeService } from './resource-inverntory-management/services/getTree.service';
import { LoadingModule } from 'ngx-loading';
import { MatDialogModule, MatCardModule, MatButtonModule, MatFormFieldModule, MatCheckboxModule } from '@angular/material';
import {MatAutocompleteModule, MatAutocomplete, MatAutocompleteSelectedEvent , MatAutocompleteTrigger} from '@angular/material/autocomplete';
import { TreeDataService } from './resource-inverntory-management/services/TreeData.service';
import { DataTableModule, DropdownModule, PanelModule,MultiSelectModule,ButtonModule,DataListModule,AccordionModule,
  TooltipModule,BreadcrumbModule, MenuItem, ChartModule,TabMenuModule,AutoCompleteModule,RadioButtonModule,TreeModule,TreeNode,DialogModule} from 'primeng/primeng';
import { TransmissionComponent } from './resource-inverntory-management/transmission/transmission.component';
import { SiteLocationService } from './resource-inverntory-management/services/getSiteInfo.service';
import { AttributeTreeService } from './resource-inverntory-management/services/attributeTree.service';
import { NeTypesService } from './resource-inverntory-management/services/NeTypes.service';
import { SubCategoryTreeService } from './resource-inverntory-management/services/getSubCategory.service';
import { GenerateCategoryReportService } from './resource-inverntory-management/services/generateFirstReport.service';
import { CategoryDropDownService } from './resource-inverntory-management/services/getCategoryDropDown.service';
import { BusynessResultService } from './resource-inverntory-management/services/BusynessResult.service';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { GetNeDetailsService } from './resource-inverntory-management/services/GetNeDetails.service';
import { RimLogoComponent } from './resource-inverntory-management/header-module/rim-logo/rim-logo.component';
import { RimNavigationComponent } from './resource-inverntory-management/header-module/rim-navigation/rim-navigation.component';
import { RimProfileComponent } from './resource-inverntory-management/header-module/rim-profile/rim-profile.component';
import { RimSideBarComponent } from './resource-inverntory-management/rim-side-bar/rim-side-bar.component';
import { HeaderModuleComponent } from './resource-inverntory-management/header-module/header-module.component';
import { RanComponent } from './resource-inverntory-management/ran/ran.component';
import { RIMGetDataService } from './resource-inverntory-management/services/general services/RIMGetDataService.service';
import { RanNeDetailsComponent } from './resource-inverntory-management/ran/ran-ne-details/ran-ne-details.component';
import { RanNeChartsComponent } from './resource-inverntory-management/ran/ran-ne-charts/ran-ne-charts.component';
import { RanNeDynamicReportComponent } from './resource-inverntory-management/ran/ran-ne-dynamic-report/ran-ne-dynamic-report.component';
import { RanCategoryReportComponent } from './resource-inverntory-management/ran/ran-ne-dynamic-report/ran-category-report/ran-category-report.component';
import { RanGlobalReportsComponent } from './resource-inverntory-management/ran/ran-ne-dynamic-report/ran-global-reports/ran-global-reports.component';
import { NeDynamicReportComponent } from './resource-inverntory-management/transmission/ne-dynamic-report/ne-dynamic-report.component';
import { CategoryReportComponent } from './resource-inverntory-management/transmission/ne-dynamic-report/category-report/category-report.component';
import { GlobalReportsComponent } from './resource-inverntory-management/transmission/ne-dynamic-report/global-reports/global-reports.component';
import { NeDetailsComponent } from './resource-inverntory-management/transmission/ne-details/ne-details.component';
import { NeChartsModule } from './resource-inverntory-management/transmission/ne-charts/ne-charts.module';
import { LoginComponentComponent } from './Authentication/login-component/login-component.component';
import { ProfileComponentComponent } from './Authentication/profile-component/profile-component.component';
import { NodeService } from './resource-inverntory-management/services/getNETree.service';
import { RanNeMapsComponent } from './resource-inverntory-management/ran/ran-ne-maps/ran-ne-maps.component';
import { AgmCoreModule } from '@agm/core';
import { MapService } from './resource-inverntory-management/services/Map.service';
import { RAN_ChartsService } from './resource-inverntory-management/services/RAN_Charts.service';
import { Trans_ChartService } from './resource-inverntory-management/services/Trans_Chart.service';

@NgModule({
  declarations: [
    AppComponent,
    ResourceInverntoryManagementComponent,
    TransmissionComponent,
    TransmissionComponent,
    NeDynamicReportComponent,NeDetailsComponent,
    CategoryReportComponent,
    GlobalReportsComponent,
    RimLogoComponent, RimNavigationComponent, RimProfileComponent, RimSideBarComponent, 
    LoginComponentComponent, ProfileComponentComponent, HeaderModuleComponent, RanComponent, RanNeDetailsComponent,
     RanNeChartsComponent, RanNeDynamicReportComponent, RanCategoryReportComponent, RanGlobalReportsComponent, RanNeMapsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(AppRoutes, {useHash: true}),
    FormsModule,
    HttpModule,
    TreeModule,
    LoadingModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatDialogModule,
    MatCheckboxModule,
    PanelModule,
     DataTableModule,
     DropdownModule,
    MultiSelectModule,
     ButtonModule,
     DataListModule,
     AccordionModule,
     TooltipModule,
     BreadcrumbModule,
     ChartModule,
     TabMenuModule,
     AutoCompleteModule,
     ReactiveFormsModule,
     MatAutocompleteModule , DropdownModule, RadioButtonModule, ChartsModule,DialogModule,DataTableModule,NeChartsModule,
     AgmCoreModule.forRoot({
      apiKey: 'AIzaSyC3GVpr4D0pUcusv2qPHB1WzvmIv3h63_M'
    })
  ],
  exports: [],
  providers: [
      { provide: LocationStrategy, useClass: HashLocationStrategy },
      SidebarService,TreeService,TreeDataService,SiteLocationService,AttributeTreeService,
      NeTypesService,SubCategoryTreeService,GenerateCategoryReportService,CategoryDropDownService, BusynessResultService,GetNeDetailsService,
      RIMGetDataService, NodeService, MapService,RAN_ChartsService,Trans_ChartService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
