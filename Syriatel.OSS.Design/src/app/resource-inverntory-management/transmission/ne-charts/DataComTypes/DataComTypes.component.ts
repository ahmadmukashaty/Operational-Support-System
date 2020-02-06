import { Component, OnInit } from '@angular/core';
import { BusynessResultService } from '../../../services/BusynessResult.service';
import { DataComTypes } from '../../../extraClasses/DataComTypes';

@Component({
  selector: 'app-DataComTypes',
  templateUrl: './DataComTypes.component.html',
  styleUrls: ['./DataComTypes.component.css']
})
export class DataComTypesComponent implements OnInit {
  DataComTypesList:DataComTypes[] = new Array();
  ModelList: string[] = new Array();
  CountList: number[]=new Array();
  bChartData: any;
  bChartLegend: boolean;
  bChartType: string;
  bChartLabels: string[];
  // tslint:disable-next-line:max-line-length
  bChartOptions: any;

  constructor(private GetService: BusynessResultService) { }

  ngOnInit() {
    this.getdate();
  }
getdate(){
  this.GetService.getDataComTypes('test','test').subscribe( res => {
    this.DataComTypesList = res;
    console.log("datacomtypes is: ", this.DataComTypesList)
    this.DataComTypesList.forEach(row => {
      this.ModelList.push(row.Model);
      this.CountList.push(row.Count);
    });
  });


  this.bChartOptions = {
    optionsVariable : { scales : { xAxes: [{ ticks: { beginAtZero: true, stepValue : 10, max : 50, } }] } },
    scaleShowVerticalLines: false,
    responsive: false
  };
  this.bChartLabels = this.ModelList;
this.bChartType = 'bar';
 this.bChartLegend = true;

this.bChartData = [
  {data: this.CountList, label: 'NE Count'}
];
}

}
