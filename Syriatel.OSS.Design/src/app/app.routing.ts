import {Routes} from '@angular/router';
import { ResourceInverntoryManagementComponent } from './resource-inverntory-management/resource-inverntory-management.component';
import { TransmissionComponent } from './resource-inverntory-management/transmission/transmission.component';
import { RanComponent } from './resource-inverntory-management/ran/ran.component';
import { RanNeChartsComponent } from './resource-inverntory-management/ran/ran-ne-charts/ran-ne-charts.component';
import { RanNeDetailsComponent } from './resource-inverntory-management/ran/ran-ne-details/ran-ne-details.component';
import { RanNeDynamicReportComponent } from './resource-inverntory-management/ran/ran-ne-dynamic-report/ran-ne-dynamic-report.component';
import { RanCategoryReportComponent } from './resource-inverntory-management/ran/ran-ne-dynamic-report/ran-category-report/ran-category-report.component';
import { RanGlobalReportsComponent } from './resource-inverntory-management/ran/ran-ne-dynamic-report/ran-global-reports/ran-global-reports.component';
import { NeDetailsComponent } from './resource-inverntory-management/transmission/ne-details/ne-details.component';
import { NeDynamicReportComponent } from './resource-inverntory-management/transmission/ne-dynamic-report/ne-dynamic-report.component';
import { CategoryReportComponent } from './resource-inverntory-management/transmission/ne-dynamic-report/category-report/category-report.component';
import { GlobalReportsComponent } from './resource-inverntory-management/transmission/ne-dynamic-report/global-reports/global-reports.component';
import { NeChartsComponent } from './resource-inverntory-management/transmission/ne-charts/ne-charts.component';
import { RanNeMapsComponent } from './resource-inverntory-management/ran/ran-ne-maps/ran-ne-maps.component';

export const AppRoutes: Routes = [
  {
    path: '',
    redirectTo: 'RIM',
    pathMatch: 'full'
  },
  {
    path: 'RIM',
    component: ResourceInverntoryManagementComponent,
    children: [
      {
        
        path: 'Transmission',
        component: TransmissionComponent,
        children: [
          {
            path: '',
            redirectTo: 'Reports',
            pathMatch: 'full'
          },
          {
            path: 'Details',
            component: NeDetailsComponent
          },
          {
            path: 'Reports',
            component: NeDynamicReportComponent,
            children:[
              {
                path: '',
                redirectTo: 'CategoryReport',
                pathMatch: 'full'
              },
              {
                path:'CategoryReport',
                component:CategoryReportComponent 
              },
              {
                path:'GlobalReport',
                component:GlobalReportsComponent 
              }
            ]
          },
          {
            path: 'Charts',
            component: NeChartsComponent
          }
          

        ]
      },   
          {
            path:'RAN',
            component:RanComponent,
            children: [
              
              {
                path: 'RanDetails',
                component: RanNeDetailsComponent
              },              {
                path: 'RanMaps',
                component: RanNeMapsComponent
              },
              {
                path: 'RanReports',
                component: RanNeDynamicReportComponent,
                children:[
                  {
                    path: '',
                    redirectTo: 'RanCategoryReport',
                    pathMatch: 'full'
                  },
                  {
                    path:'RanCategoryReport',
                    component:RanCategoryReportComponent 
                  },
                  {
                    path:'RanGlobalReport',
                    component:RanGlobalReportsComponent 
                  }
                ]
              },
              {
                path: '',
                redirectTo: 'RanReports',
                pathMatch: 'full'
              },
              {
                path: 'RanCharts',
                component: RanNeChartsComponent
              }
            ]
         } 
      ]
        
  }
 ];
