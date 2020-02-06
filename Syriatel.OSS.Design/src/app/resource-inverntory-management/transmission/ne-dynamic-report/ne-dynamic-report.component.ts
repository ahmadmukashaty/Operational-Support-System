import { Component, OnInit } from '@angular/core';
import {TabMenuModule} from 'primeng/primeng';
import {MenuItem} from 'primeng/primeng';


@Component({
  selector: 'app-ne-dynamic-report',
  templateUrl: './ne-dynamic-report.component.html',
  styleUrls: ['./ne-dynamic-report.component.css']
})
export class NeDynamicReportComponent implements OnInit {
  
  items: MenuItem[];

  constructor() { }

  ngOnInit() {
    this.items = [
      {label: 'Category Report', icon: 'fa-bar-chart',routerLink: ['CategoryReport']}, //, queryParams: {'recent': 'true'}
      {label: 'Global Report', icon: 'fa-book',routerLink: ['GlobalReport']}
    ];
  }

}
