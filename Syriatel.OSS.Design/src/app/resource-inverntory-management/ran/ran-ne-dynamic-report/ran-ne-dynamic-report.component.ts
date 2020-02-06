import { Component, OnInit } from '@angular/core';
import {TabMenuModule} from 'primeng/primeng';
import {MenuItem} from 'primeng/primeng';

@Component({
  selector: 'app-ran-ne-dynamic-report',
  templateUrl: './ran-ne-dynamic-report.component.html',
  styleUrls: ['./ran-ne-dynamic-report.component.css']
})
export class RanNeDynamicReportComponent implements OnInit {
 items: MenuItem[];

  constructor() { }

  ngOnInit() {
    this.items = [
      {label: 'Category Report', icon: 'fa-bar-chart',routerLink: ['RanCategoryReport']}
       //, queryParams: {'recent': 'true'}
    /*   ,{label: 'Global Report', icon: 'fa-book',routerLink: ['RanGlobalReport']}*/
    ];
  }

}

