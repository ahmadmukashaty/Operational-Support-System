import { Component, OnInit } from '@angular/core';
import { ngxLoadingAnimationTypes } from "ngx-loadingV2";
import { BaseChartDirective } from '../../../../../node_modules/ng2-charts';
import { SiteLocationService } from '../../services/getSiteInfo.service';
import { LocationSiteData } from '../../extraClasses/LocationSiteData';
import { SelectItem } from '../ran-ne-dynamic-report/ran-category-report/SelectItem';
import { trigger, state, style, transition, animate, AUTO_STYLE } from '@angular/animations';
import { Area } from '../../extraClasses/LocationSiteHierarchy/Area';
import { Zone } from '../../extraClasses/LocationSiteHierarchy/Zone';
import { SubArea } from '../../extraClasses/LocationSiteHierarchy/SubArea';
import { Site } from '../../extraClasses/Site';
import { FilterModel } from '../../extraClasses/LocationSiteHierarchy/FilterModel';
import { NodeOfTree } from '../../extraClasses/TreeNode';
import { RAN_ChartsService } from '../../services/RAN_Charts.service';
import { LevelTypes } from '../../extraClasses/RanCharts/LevelTypes';
import { LevelAttributes } from '../../extraClasses/RanCharts/LevelAttributes';
import { CountTypesInput } from '../../extraClasses/RanCharts/CountTypesInput';
import { DBSourceAttributes } from '../../extraClasses/RanCharts/DBSourceAttributes';
import { CountLevelAttributes } from '../../extraClasses/RanCharts/CountLevelAttributes';
import { LocationAttributes } from '../../extraClasses/RanCharts/LocationAttributes';
import { NeTypes } from '../../extraClasses/RanCharts/NeTypes';
import { DataTypesChart } from '../../extraClasses/RanCharts/DataTypesChart';
import { LocTypesListCount } from '../../extraClasses/RanCharts/LocTypesListCount';
import { TypesCount } from '../../extraClasses/RanCharts/TypesCount';
import { BusynessInput } from '../../extraClasses/RanCharts/BusynessInput';
import { BusynessDataObj } from '../../extraClasses/RanCharts/BusynessDataObj';
import { BusynessReturnedData } from '../../extraClasses/RanCharts/BusynessReturnedData';
import { ChartsData } from '../../extraClasses/RanCharts/ChartsData';
import { CellsTypesObj } from '../../extraClasses/RanCharts/CellsTypesObj';
import { ReportList } from '../../extraClasses/ReportList';
import { ReportTypeCount } from '../../extraClasses/RanCharts/ReportTypeCount';
import { HostVersionInput } from '../../extraClasses/RanCharts/HostVersionInput';
import { HostVersionChart } from '../../extraClasses/RanCharts/HostVersionChart';
import { HostVersionTypes } from '../../extraClasses/RanCharts/HostVersionTypes';
import { CellsTypesCount } from '../../extraClasses/RanCharts/CellsTypesCount';
import { async } from 'q';
declare var jquery: any;
declare var $: any;
const PrimaryWhite = '#ffffff';
const SecondaryGrey = '#ccc';

@Component({
  selector: 'app-ran-ne-charts',
  templateUrl: './ran-ne-charts.component.html',
  styleUrls: ['./ran-ne-charts.component.css'],
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
        animate('800ms ease-in-out')
      ])
    ])
  ]
})
export class RanNeChartsComponent implements OnInit {
  Categories: any[] = [{ "label": "Radio", "value": "Radio" }, { "label": "Controller", "value": "Controller" }];
  ShowReport: Boolean = false;
  show: Boolean;
  show2: Boolean;
  SiteInfo: LocationSiteData;
  Region: SelectItem[]
  Area: SelectItem[]
  AllArea: SelectItem[]
  Zones: SelectItem[]
  AllZone: SelectItem[];
  SubAreas: SelectItem[]
  AllSubAreas: SelectItem[]
  SitesCodes: SelectItem[]
  AllSiteCodes: SelectItem[]
  SitesNames: SelectItem[]
  AllSiteNames: SelectItem[]
  loading: boolean;
  selectedRegion: string = '';
  selectedArea: string = '';
  selectedZone = '';
  selectedSubArea = '';
  selectedSiteCode = '';
  selectedSiteName = '';
  RegionTempSiteCode: SelectItem[];
  RegionTempSiteName: SelectItem[];
  AreaTempSiteCode: SelectItem[];
  AreaTempSiteName: SelectItem[];
  ZoneTempSiteCode: SelectItem[];
  ZoneTempSiteName: SelectItem[];

  // filter location site datamember
  tempRegions: Area[];
  tempAreas: Zone[];
  tempZones: SubArea[];
  tempSubAreas: Site[];

  model: FilterModel;

  private NodesOfLevels: NodeOfTree[] = null;

  SelectedLevel: string;
  num: string[];
  charbarlist: Array<String> = [];
  BusyList: Array<number> = [];
  CapacityList: Array<number> = [];
  FreeList: Array<number> = [];
  barChartLabels: Array<String>;
  barChartType: string;
  barChartLegend: boolean;
  /*[{ data: Array<number>, label: string }, { data: Array<number>, label: string }, { data: Array<number>, label: string }];*/

  barChartOptions: any; // { scaleShowVerticalLines: boolean; responsive: boolean; };
  selectedDes: string;
  columnDefs: { headerName: string; field: string; }[];
  rowData: { make: string; model: string; price: number; }[];
  counter: number;
  i: number;
  baseChart: BaseChartDirective;
  canvas4: any;
  barChartLabels_temp: Array<String>;
  bChartLabels_temp: Array<String>;
  eChartLabels_temp: Array<String>;
  ModelList_dim: number;
  LevelSource: CountLevelAttributes;
  LevelDes: CountLevelAttributes;
  Busyness_data_Obj: BusynessInput;


  public primaryColour = PrimaryWhite;
  public secondaryColour = SecondaryGrey;
  public config1 = { animationType: ngxLoadingAnimationTypes.circleSwish, primaryColour: this.primaryColour, secondaryColour: this.secondaryColour, tertiaryColour: this.primaryColour, backdropBorderRadius: '30px' };
  DisableArea: boolean;
  DisableSubArea: boolean;
  DisableZone: boolean;
  selectedCategory: string;
  public NeLevel: LevelTypes;
  public NeLevel_Data: LevelAttributes[];
  To_GET_DBSource: DBSourceAttributes;
  public Count_Type_Obj: CountTypesInput;
  public Host_Version_Obj: HostVersionInput;
  NeTypes: NeTypes;
  HostVersion: HostVersionChart;
  bChartType: string;
  eChartType: string;


  //barChartData: [{ data: number[], label: string }] = null;
  barChartData: string;
  charbarlist_temp: string[]
  barChartData_list: { data: number[]; label: string; }[] = [];
  busynessResult: BusynessReturnedData;
  barChartLabel: string;
  ChartData: ChartsData;
  barChartData_final: any;
  data_test: { "data": number[]; "label": string; }[];
  labels: string[];
  busynessChartLabel: Array<string>;
  busynessChartData: { data: number[]; label: string; }[];
  Cells_Types_Obj: CellsTypesObj;
  ShowRepBusyness: ReportList[];
  ShowRepTypeCount: ReportTypeCount[];
  ShowHostTypeCount: ReportTypeCount[];
  cols: { field: string; header: string; }[];
  colsTypeCount: { field: string; header: string; }[];
  colsHostVersion: { field: string; header: string; }[];
  ShowBusynessReport: boolean;
  ShowTypeCountReport: boolean;
  hostverbarlist_temp: string[];
  hostbarlist: string[];
  HostbarChartData_list: { data: number[]; label: string; }[]; CellTypes: CellsTypesCount;
  charbarlistCell: string[];
  barChartData_listCell: { data: number[]; label: string; }[];
  ChartDataCell: ChartsData;
  charbarlist_tempCell: string[];
  ShowRepTypeCountCell: ReportTypeCount[];
  colsCellTypes: { field: string; header: string; }[];
  showCat: boolean = false;
  showGen: boolean = false;
  ;

  constructor(private siteLocation: SiteLocationService, private ChartService: RAN_ChartsService) { }

  ngOnInit() {
    this.selectedCategory = "Radio"
    this.SelectedLevel = "NE Type"
    this.loading = true;
    this.Count_Type_Obj = new CountTypesInput();
    this.Count_Type_Obj.Level = new CountLevelAttributes();
    this.Count_Type_Obj.LoctionValues = new LocationAttributes();
    this.Count_Type_Obj.LoctionValues.Area = null;
    this.Count_Type_Obj.LoctionValues.Region = null;
    this.Count_Type_Obj.LoctionValues.SubArea = null;
    this.Count_Type_Obj.LoctionValues.Zone = null;


    this.Host_Version_Obj = new HostVersionInput();
    this.Host_Version_Obj.LoctionValues = new LocationAttributes();
    this.Host_Version_Obj.LoctionValues.Area = null;
    this.Host_Version_Obj.LoctionValues.Region = null;
    this.Host_Version_Obj.LoctionValues.SubArea = null;
    this.Host_Version_Obj.LoctionValues.Zone = null;


    this.Busyness_data_Obj = new BusynessInput();
    this.Busyness_data_Obj.LevelSource = new CountLevelAttributes();
    this.Busyness_data_Obj.LevelDistination = new CountLevelAttributes();
    this.Busyness_data_Obj.LoctionValues = new LocationAttributes();
    this.Busyness_data_Obj.LevelSource.ColumnName = "ID",
      this.Busyness_data_Obj.LevelSource.ColumnType = "number"
    this.Busyness_data_Obj.LevelDistination.ColumnName = "ID",
      this.Busyness_data_Obj.LevelDistination.ColumnType = "number"
    this.Busyness_data_Obj.LoctionValues.Area = null;
    this.Busyness_data_Obj.LoctionValues.Region = null;
    this.Busyness_data_Obj.LoctionValues.SubArea = null;
    this.Busyness_data_Obj.LoctionValues.Zone = null;


    this.Cells_Types_Obj = new CellsTypesObj();
    this.Cells_Types_Obj.LoctionValues = new LocationAttributes();
    this.Cells_Types_Obj.LoctionValues.Area = null;
    this.Cells_Types_Obj.LoctionValues.Region = null;
    this.Cells_Types_Obj.LoctionValues.SubArea = null;
    this.Cells_Types_Obj.LoctionValues.Zone = null;
    //this.Cells_Types_Obj.Model = "Ran";

    this.cols = [
      { field: 'NeSiteAtt', header: 'Location' },
      { field: 'BusyLi', header: 'Busy' },
      { field: 'CapcityLi', header: 'Capcity' },
      { field: 'FreeLi', header: 'Free' }
    ];

    this.colsTypeCount = [
      { field: 'NeSiteAtt', header: 'Location' },
      { field: 'Type', header: 'Type' },
      { field: 'Count', header: 'Count' }
    ];

    this.colsHostVersion = [
      { field: 'NeSiteAtt', header: 'Location' },
      { field: 'Type', header: 'Host Version' },
      { field: 'Count', header: 'Count' }
    ];

    this.colsCellTypes = [
      { field: 'NeSiteAtt', header: 'Location' },
      { field: 'Type', header: 'Cell Type' },
      { field: 'Count', header: 'Count' }
    ];



    this.siteLocation.GetSiteLocationData("").subscribe(values => {
      this.SiteInfo = (<LocationSiteData>values.data);
      this.Region = this.siteLocation.GetSiteLocationDropdownParsing(this.SiteInfo.Region);
      this.Area = this.siteLocation.GetSiteLocationDropdownParsing(this.SiteInfo.Area);
      this.AllArea = this.Area;
      this.Zones = this.siteLocation.GetSiteLocationDropdownParsing(this.SiteInfo.Zone);
      this.AllZone = this.Zones;
      this.SubAreas = this.siteLocation.GetSiteLocationDropdownParsing(this.SiteInfo.SubArea);
      this.AllSubAreas = this.SubAreas;
      this.SitesCodes = this.siteLocation.GetSitecode(this.SiteInfo.Site);
      this.AllSiteCodes = this.SitesCodes;
      this.SitesNames = this.siteLocation.GetSiteName(this.SiteInfo.Site);
      this.AllSiteNames = this.SitesNames;

      this.barChartOptions = {
        optionsVariable: { scales: { xAxes: [{ ticks: { beginAtZero: true, stepValue: 10, max: 50, } }], yAxes: [{ ticks: { beginAtZero: true } }], } },
        responsive: true
      };



      console.log("regions: ", this.Region)

    });




    this.ChartService.get_DBSource().subscribe(res => {
      this.To_GET_DBSource = res.data.DB_Source;

      this.Count_Type_Obj.dbsource = this.To_GET_DBSource;
      this.Host_Version_Obj.dbsource = this.To_GET_DBSource;
      this.Busyness_data_Obj.dbsource = this.To_GET_DBSource;
      this.Cells_Types_Obj.dbsource = this.To_GET_DBSource;
    });


    this.Count_Type_Obj = { "Level": { "TableName": "RADIO_NETYPE", "ColumnName": "TYPE", "ColumnType": "VARCHAR2" }, "LoctionValues": { "Area": null, "Region": null, "SubArea": null, "Zone": null }, "dbsource": { "RegionSource": { "TableName": "REGION", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "AreaSource": { "TableName": "AREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "ZoneSource": { "TableName": "ZONE", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SubAreaSource": { "TableName": "SUBAREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SiteSource": { "TableName": "SITE_CANDIDATE", "SiteCodeColumnName": "SITE_CODE", "SiteCodeColumnType": "VARCHAR2", "EnglishNameColumnName": "ENGLISH_NAME", "EnglishNameColumnType": "VARCHAR2" } }, "ClassificationName": "Radio" }
    this.Busyness_data_Obj = { "LevelSource": { "ColumnName": "ID", "ColumnType": "number", "TableName": "Radio_slot" }, "LevelDistination": { "ColumnName": "ID", "ColumnType": "number", "TableName": "Radio_board" }, "LoctionValues": { "Area": null, "Region": null, "SubArea": null, "Zone": null }, "dbsource": { "RegionSource": { "TableName": "REGION", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "AreaSource": { "TableName": "AREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "ZoneSource": { "TableName": "ZONE", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SubAreaSource": { "TableName": "SUBAREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SiteSource": { "TableName": "SITE_CANDIDATE", "SiteCodeColumnName": "SITE_CODE", "SiteCodeColumnType": "VARCHAR2", "EnglishNameColumnName": "ENGLISH_NAME", "EnglishNameColumnType": "VARCHAR2" } }, "ClassificationName": "Radio" }
    this.Host_Version_Obj = { "LoctionValues": { "Area": null, "Region": null, "SubArea": null, "Zone": null }, "dbsource": { "RegionSource": { "TableName": "REGION", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "AreaSource": { "TableName": "AREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "ZoneSource": { "TableName": "ZONE", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SubAreaSource": { "TableName": "SUBAREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SiteSource": { "TableName": "SITE_CANDIDATE", "SiteCodeColumnName": "SITE_CODE", "SiteCodeColumnType": "VARCHAR2", "EnglishNameColumnName": "ENGLISH_NAME", "EnglishNameColumnType": "VARCHAR2" } }, "ClassificationName": "Radio" }
    this.Cells_Types_Obj = { "LoctionValues": { "Area": null, "Region": null, "SubArea": null, "Zone": null }, "dbsource": { "RegionSource": { "TableName": "REGION", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "AreaSource": { "TableName": "AREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "ZoneSource": { "TableName": "ZONE", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SubAreaSource": { "TableName": "SUBAREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SiteSource": { "TableName": "SITE_CANDIDATE", "SiteCodeColumnName": "SITE_CODE", "SiteCodeColumnType": "VARCHAR2", "EnglishNameColumnName": "ENGLISH_NAME", "EnglishNameColumnType": "VARCHAR2" } }, "ClassificationName": "Radio" }

    //#region Intialize NeTypes objects and barchart
    this.NeTypes = new NeTypes();
    this.NeTypes.data = new DataTypesChart();
    this.NeTypes.data.LocationNames = new Array<string>();
    this.NeTypes.data.LocContValues = new Array<TypesCount>();
    this.NeTypes.data.Types = new Array<string>();
    this.charbarlist = new Array<string>();
    this.barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.charbarlist_temp = new Array<string>();
    this.ShowRepTypeCount = new Array<ReportTypeCount>()
    //#endregion


    //#region Intialize cells types count objects and barchart
    this.CellTypes = new CellsTypesCount();
    this.CellTypes.LocationNames = new Array<string>();
    this.CellTypes.LocCountValues = new Array<TypesCount>();
    this.CellTypes.Bands = new Array<string>();
    this.charbarlistCell = new Array<string>();
    this.barChartData_listCell = new Array<{ data: number[], label: string }>();
    this.ChartDataCell = new ChartsData();
    this.charbarlist_tempCell = new Array<string>();
    this.ShowRepTypeCountCell = new Array<ReportTypeCount>()
    //#endregion

    //#region Intialize Busyness objects
    this.busynessResult = new BusynessReturnedData();
    this.busynessResult.data = new BusynessDataObj();
    this.busynessResult.data.LocationNames = new Array<string>();
    this.busynessResult.data.Capacity = new Array<number>();
    this.busynessResult.data.Free = new Array<number>();
    this.busynessResult.data.Busy = new Array<number>();
    this.busynessChartLabel = new Array<string>();

    this.NeTypes.data.LocationNames = new Array<string>();
    //#endregion


    //#region  Intialize host version
    this.HostVersion = new HostVersionChart();
    this.HostVersion.LocationNames = new Array<string>();
    this.HostVersion.HostVersions = new Array<string>();
    this.HostVersion.LocCountValues = new Array<TypesCount>();
    this.ShowHostTypeCount = new Array<ReportTypeCount>()
    this.HostbarChartData_list = new Array<{ data: number[], label: string }>();
    //#endregion


    this.ChartService.GetCountTypes(this.Count_Type_Obj).subscribe(res => {
      this.NeTypes.success = res.success;
      this.NeTypes.errorMessage = res.errorMessage;
      this.NeTypes.data = res.data

      console.log("Ne Types finally", this.NeTypes.data)

    });
    console.log("celllllllllllllllllllllllll", this.Cells_Types_Obj);
    this.ChartService.GetCellsTypes(this.Cells_Types_Obj).subscribe(res => {
      this.CellTypes = res.data
      console.log("Ne cellTypes finally", this.CellTypes)
    })

    this.busynessResult = new BusynessReturnedData();
    this.ChartService.GetBusyness(this.Busyness_data_Obj).subscribe(res => {
      this.busynessResult.data = res.data;
      this.busynessResult.data.LocationNames = res.data.LocationNames;
      this.busynessResult.errorMessage = res.errorMessage;
      this.busynessResult.success = res.success;
      console.log("busyness results", this.busynessResult.data)
    })


    this.ChartService.Get_Host_Version(this.Host_Version_Obj).subscribe(res => {
      this.HostVersion = res.data;
      console.log("host version types", this.HostVersion)
    });


    this.charbarlist = new Array<string>();
    this.barChartData_list = [];
    this.barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.charbarlist_temp = new Array<string>();
    this.ShowRepBusyness = new Array<ReportList>();

    this.hostverbarlist_temp = new Array<string>();
    setTimeout(() => {
      for (let i = 0; i < this.NeTypes.data.LocationNames.length; i++) {
        this.charbarlist_temp.push(this.NeTypes.data.LocationNames[i]);
        for (let j = 0; j < this.NeTypes.data.LocContValues.length; j++) {
          this.ShowRepTypeCount.push({ NeSiteAtt: this.NeTypes.data.LocationNames[i], Type: this.NeTypes.data.Types[j], Count: this.NeTypes.data.LocContValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.CellTypes.LocationNames.length; i++) {
        this.charbarlist_tempCell.push(this.CellTypes.LocationNames[i]);
        for (let j = 0; j < this.CellTypes.LocCountValues.length; j++) {
          this.ShowRepTypeCountCell.push({ NeSiteAtt: this.CellTypes.LocationNames[i], Type: this.CellTypes.Bands[j], Count: this.CellTypes.LocCountValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.HostVersion.LocationNames.length; i++) {
        this.hostverbarlist_temp.push(this.HostVersion.LocationNames[i]);
        for (let j = 0; j < this.HostVersion.LocCountValues.length; j++) {
          this.ShowHostTypeCount.push({ NeSiteAtt: this.HostVersion.LocationNames[i], Type: this.HostVersion.HostVersions[j], Count: this.HostVersion.LocCountValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.NeTypes.data.Types.length; i++) {
        this.barChartData_list.push({ data: this.NeTypes.data.LocContValues[i].Values, label: this.NeTypes.data.Types[i] })

      }

      for (let i = 0; i < this.CellTypes.Bands.length; i++) {
        this.barChartData_listCell.push({ data: this.CellTypes.LocCountValues[i].Values, label: this.CellTypes.Bands[i] })

      }

      for (let i = 0; i < this.HostVersion.HostVersions.length; i++) {
        this.HostbarChartData_list.push({ data: this.HostVersion.LocCountValues[i].Values, label: this.HostVersion.HostVersions[i] })

      }
      this.charbarlist = this.charbarlist_temp;
      this.charbarlistCell = this.charbarlist_tempCell;
      this.hostbarlist = this.hostverbarlist_temp;


      for (let i = 0; i < this.busynessResult.data.LocationNames.length; i++) {
        this.busynessChartLabel.push(this.busynessResult.data.LocationNames[i]);
        this.ShowRepBusyness.push({
          NeSiteAtt: this.busynessResult.data.LocationNames[i], CapcityLi: this.busynessResult.data.Capacity[i],
          FreeLi: this.busynessResult.data.Free[i], BusyLi: this.busynessResult.data.Busy[i]
        });
      }
      this.busynessChartData = [
        { data: this.busynessResult.data.Capacity, label: 'Capacity' },
        { data: this.busynessResult.data.Busy, label: 'Busy' },
        { data: this.busynessResult.data.Free, label: 'Free' }
      ]

      this.show = true;
      this.show2 = true;
      this.loading = false;
      this.ShowBusynessReport = true;
      this.ShowTypeCountReport = true;
      this.SelectedLevel = null;
      this.ShowCharts(event)
    }, 10000);


  }
  isCollapsedSideBar = 'yes-block';
  toggleOpenedSidebar() {
    this.isCollapsedSideBar = this.isCollapsedSideBar === 'yes-block' ? 'no-block' : 'yes-block';
  }

  isCollapsedSideBar2 = 'yes-block';
  toggleOpenedSidebar2() {
    this.isCollapsedSideBar2 = this.isCollapsedSideBar2 === 'yes-block' ? 'no-block' : 'yes-block';


  }

  chart1Height = '350px';
  //chart 1
  chart1HeightClosed = '55px';
  chart1HeightOpened = '425px';
  chart1IsOpened = true;
  toggleOpenedChart1() {
    if (this.chart1IsOpened) {
      $('.shadow10').animate({
        height: this.chart1HeightClosed
      }, 500);
      $('.canvas1').css('display', 'none');

      $('.shadow10_icon').removeClass('fa-angle-double-up');
      $('.shadow10_icon').addClass('fa-angle-double-down');
      this.chart1IsOpened = false;
    }
    else {
      $('.shadow10').animate({
        height: this.chart1HeightOpened
      }, 500);
      $('.canvas1').css('display', 'block');
      this.chart1IsOpened = true;
      $('.shadow10_icon').removeClass('fa-angle-double-down');
      $('.shadow10_icon').addClass('fa-angle-double-up');
    }
  }

  //chart 2
  chart2HeightClosed = '55px';
  chart2HeightOpened = '425px';
  chart2IsOpened = true;
  toggleOpenedChart2() {
    if (this.chart2IsOpened) {
      $('.shadow11').animate({
        height: this.chart2HeightClosed
      }, 500);
      $('.canvas2').css('height', '0px');
      $('.shadow11_icon').removeClass('fa-angle-double-up');
      $('.shadow11_icon').addClass('fa-angle-double-down');
      this.chart2IsOpened = false;
    }
    else {
      $('.shadow11').animate({
        height: this.chart2HeightOpened
      }, 500);
      $('.canvas2').css('height', this.chart1Height);
      this.chart2IsOpened = true;
      $('.shadow11_icon').removeClass('fa-angle-double-down');
      $('.shadow11_icon').addClass('fa-angle-double-up');
    }
  }

  //chart 3
  chart3HeightClosed = '55px';
  chart3HeightOpened = '425px';
  chart3IsOpened = true;
  toggleOpenedChart3() {
    if (this.chart3IsOpened) {
      $('.shadow12').animate({
        height: this.chart3HeightClosed
      }, 500);
      $('.canvas3').css('height', '0px');
      $('.shadow12_icon').removeClass('fa-angle-double-up');
      $('.shadow12_icon').addClass('fa-angle-double-down');
      this.chart3IsOpened = false;
    }
    else {
      $('.shadow12').animate({
        height: this.chart3HeightOpened
      }, 500);
      $('.canvas3').css('height', this.chart1Height);
      this.chart3IsOpened = true;
      $('.shadow12_icon').removeClass('fa-angle-double-down');
      $('.shadow12_icon').addClass('fa-angle-double-up');
    }
  }


  //chart 4
  chart4HeightClosed = '55px';
  chart4HeightOpened = '425px';
  chart4IsOpened = true;
  toggleOpenedChart4() {
    if (this.chart4IsOpened) {
      $('.shadow13').animate({
        height: this.chart4HeightClosed
      }, 500);
      $('.canvas4').css('height', '0px');
      $('.shadow13_icon').removeClass('fa-angle-double-up');
      $('.shadow13_icon').addClass('fa-angle-double-down');
      this.chart4IsOpened = false;
    }
    else {
      $('.shadow13').animate({
        height: this.chart4HeightOpened
      }, 500);
      $('.canvas4').css('height', this.chart1Height);
      this.chart4IsOpened = true;
      $('.shadow13_icon').removeClass('fa-angle-double-down');
      $('.shadow13_icon').addClass('fa-angle-double-up');
    }
  }


  ShowCharts(event) {
     
    $(".ChartsCriteria").fadeOut(function () {
      $(".ChartsVeiw").fadeIn();
    });
    
    
    $(event.target.parentNode).addClass("elegantshadow");
    $(event.target.parentNode.nextElementSibling).removeClass("elegantshadow");
    //this.ShowReport = true;
  }

  ShowCharts2() {
     
    $(".ChartsCriteria").fadeOut(function () {
      $(".ChartsVeiw").fadeIn();
    });
    // var event = $(".elegantshadow");
    // console.log(event);
    $(".RAN_chart_lebel").addClass("elegantshadow");
    $(".RAN_criteria_lebel").removeClass("elegantshadow");
    

    //$(event.target.parentNode).addClass("elegantshadow");
    //$(event.target.parentNode.nextElementSibling).removeClass("elegantshadow");
    //this.ShowReport = true;
  }

  ShowCriteria(event) {

    

    $(".ChartsVeiw").fadeOut(function () {
      $(".ChartsCriteria").fadeIn();
    });

    $(event.target.parentNode).addClass("elegantshadow");
    $(event.target.parentNode.previousElementSibling).removeClass("elegantshadow");

  }



  RegionSelect(event) {
    this.showCat = true;
    this.selectedCategory = null;

      this.Count_Type_Obj.LoctionValues.Area = null
      this.Count_Type_Obj.LoctionValues.Zone = null
      this.Count_Type_Obj.LoctionValues.SubArea = null

      this.Host_Version_Obj.LoctionValues.Area = null
      this.Host_Version_Obj.LoctionValues.Zone = null
      this.Host_Version_Obj.LoctionValues.SubArea = null

      this.Busyness_data_Obj.LoctionValues.Area = null
      this.Busyness_data_Obj.LoctionValues.Zone = null
      this.Busyness_data_Obj.LoctionValues.SubArea = null

      
      this.Cells_Types_Obj.LoctionValues.Area = null
      this.Cells_Types_Obj.LoctionValues.Zone = null
      this.Cells_Types_Obj.LoctionValues.SubArea = null

    if (this.selectedRegion !== 'All') {
      this.Count_Type_Obj.LoctionValues.Region = this.selectedRegion;
      this.Host_Version_Obj.LoctionValues.Region = this.selectedRegion;
      this.Busyness_data_Obj.LoctionValues.Region = this.selectedRegion;
      this.Cells_Types_Obj.LoctionValues.Region = this.selectedRegion;
      this.DisableArea = false;
      this.DisableSubArea = true;
      this.DisableZone = true;
      this.selectedArea = '';
      this.selectedZone = '';
      this.selectedSubArea = '';
      this.selectedSiteCode = '';
      this.selectedSiteName = '';
      this.tempRegions = this.siteLocation.GetTempRegion(this.selectedRegion, this.SiteInfo.DataHierarchy);
      this.model = this.siteLocation.GetFilteredArea(this.tempRegions);
      this.Area = this.model.area_list;
      this.SitesCodes = this.model.sitecode;
      this.SitesNames = this.model.sitename;
      // tempRegioncode and name
      this.RegionTempSiteCode = this.SitesCodes;
      this.RegionTempSiteName = this.SitesNames;

      console.log(this.SitesCodes);
      console.log(this.SitesNames);
    } else { // All
      this.Count_Type_Obj.LoctionValues.Region = null;
      this.Count_Type_Obj.LoctionValues.Area = null
      this.Count_Type_Obj.LoctionValues.Zone = null
      this.Count_Type_Obj.LoctionValues.SubArea = null

      this.Host_Version_Obj.LoctionValues.Region = null;
      this.Host_Version_Obj.LoctionValues.Area = null
      this.Host_Version_Obj.LoctionValues.Zone = null
      this.Host_Version_Obj.LoctionValues.SubArea = null

      this.Busyness_data_Obj.LoctionValues.Region = null;
      this.Busyness_data_Obj.LoctionValues.Area = null
      this.Busyness_data_Obj.LoctionValues.Zone = null
      this.Busyness_data_Obj.LoctionValues.SubArea = null

      this.Cells_Types_Obj.LoctionValues.Region = null;
      this.Cells_Types_Obj.LoctionValues.Area = null
      this.Cells_Types_Obj.LoctionValues.Zone = null
      this.Cells_Types_Obj.LoctionValues.SubArea = null

      this.DisableArea = false;
      this.DisableZone = true;
      this.DisableSubArea = true;
      this.selectedArea = '';
      this.selectedZone = '';
      this.selectedSubArea = '';
      this.selectedSiteCode = '';
      this.selectedSiteName = '';
      this.Area = this.AllArea;
      this.Zones = this.AllZone;
      this.SubAreas = this.AllSubAreas;
      this.SitesCodes = this.AllSiteCodes;
      this.SitesNames = this.AllSiteNames;
      this.tempRegions = this.siteLocation.GetTempRegion(this.selectedRegion, this.SiteInfo.DataHierarchy);
      console.log('tempRegion', this.tempRegions);
      console.log(this.SitesCodes);
      console.log(this.SitesNames);

    }


  }

  AreaSelect(event) {

    console.log(event);
    
      this.Count_Type_Obj.LoctionValues.Zone = null
      this.Count_Type_Obj.LoctionValues.SubArea = null

      
      this.Host_Version_Obj.LoctionValues.Zone = null
      this.Host_Version_Obj.LoctionValues.SubArea = null

      
      this.Busyness_data_Obj.LoctionValues.Zone = null
      this.Busyness_data_Obj.LoctionValues.SubArea = null

      
      
      this.Cells_Types_Obj.LoctionValues.Zone = null
      this.Cells_Types_Obj.LoctionValues.SubArea = null

    if (this.selectedRegion != null) {
      if (this.selectedRegion === 'All') { // Region ALL
        if (this.selectedArea === 'All') { // Region & Area == All
          this.Count_Type_Obj.LoctionValues.Area = null
          this.Count_Type_Obj.LoctionValues.Zone = null
          this.Count_Type_Obj.LoctionValues.SubArea = null

          this.Host_Version_Obj.LoctionValues.Area = null
          this.Host_Version_Obj.LoctionValues.Zone = null
          this.Host_Version_Obj.LoctionValues.SubArea = null

          this.Busyness_data_Obj.LoctionValues.Area = null
          this.Busyness_data_Obj.LoctionValues.Zone = null
          this.Busyness_data_Obj.LoctionValues.SubArea = null

          this.Cells_Types_Obj.LoctionValues.Area = null
          this.Cells_Types_Obj.LoctionValues.Zone = null
          this.Cells_Types_Obj.LoctionValues.SubArea = null

          this.DisableZone = true;
          this.DisableSubArea = true;
          this.selectedZone = '';
          this.selectedSubArea = '';
          this.selectedSiteCode = '';
          this.selectedSiteName = '';

          this.SitesCodes = this.AllSiteCodes;
          this.SitesNames = this.AllSiteNames;


          this.AreaTempSiteCode = this.SitesCodes;
          this.AreaTempSiteName = this.SitesNames;
          console.log(this.SitesCodes);
          console.log(this.SitesNames);
        } else { // Region = All & Area != All
          this.Count_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.Host_Version_Obj.LoctionValues.Area = this.selectedArea;
          this.Busyness_data_Obj.LoctionValues.Area = this.selectedArea;
          this.Cells_Types_Obj.LoctionValues.Area = this.selectedArea;

          this.DisableZone = false;
          this.DisableSubArea = true;
          this.selectedZone = '';
          this.selectedSubArea = '';
          this.selectedSiteCode = '';
          this.selectedSiteName = '';

          this.tempAreas = this.siteLocation.GetTempArea(this.selectedArea, this.tempRegions);
          this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
          this.Zones = this.model.zone_list;
          this.SitesCodes = this.model.sitecode;
          this.SitesNames = this.model.sitename;

          this.AreaTempSiteCode = this.SitesCodes;
          this.AreaTempSiteName = this.SitesNames;
          console.log(this.SitesCodes);
          console.log(this.SitesNames);
        }
      } else { // Region != ALL
        if (this.selectedArea === 'All') { // Region != All & area == All
          this.Count_Type_Obj.LoctionValues.Area = null
          this.Count_Type_Obj.LoctionValues.Zone = null
          this.Count_Type_Obj.LoctionValues.SubArea = null

          this.Host_Version_Obj.LoctionValues.Area = null
          this.Host_Version_Obj.LoctionValues.Zone = null
          this.Host_Version_Obj.LoctionValues.SubArea = null

          this.Busyness_data_Obj.LoctionValues.Area = null
          this.Busyness_data_Obj.LoctionValues.Zone = null
          this.Busyness_data_Obj.LoctionValues.SubArea = null

          this.Cells_Types_Obj.LoctionValues.Area = null
          this.Cells_Types_Obj.LoctionValues.Zone = null
          this.Cells_Types_Obj.LoctionValues.SubArea = null

          this.DisableZone = true;
          this.DisableSubArea = true;
          this.selectedZone = '';
          this.selectedSubArea = '';
          this.selectedSiteCode = '';
          this.selectedSiteName = '';

          this.SitesCodes = this.RegionTempSiteCode; // filtered by Region
          this.SitesNames = this.RegionTempSiteName; // filtered by Region

          this.AreaTempSiteCode = this.SitesCodes;
          this.AreaTempSiteName = this.SitesNames;
          console.log(this.SitesCodes);
          console.log(this.SitesNames);
        } else { // Region & Area != All
          this.Count_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.Host_Version_Obj.LoctionValues.Area = this.selectedArea;
          this.Busyness_data_Obj.LoctionValues.Area = this.selectedArea;
          this.Cells_Types_Obj.LoctionValues.Area = this.selectedArea;

          this.DisableZone = false;
          this.DisableSubArea = true;
          this.selectedZone = '';
          this.selectedSubArea = '';
          this.selectedSiteCode = '';
          this.selectedSiteName = '';

          this.tempAreas = this.siteLocation.GetTempArea(this.selectedArea, this.tempRegions);
          console.log(this.tempAreas);
          this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
          this.Zones = this.model.zone_list;
          this.SitesCodes = this.model.sitecode;
          this.SitesNames = this.model.sitename;

          this.AreaTempSiteCode = this.SitesCodes;
          this.AreaTempSiteName = this.SitesNames;
          console.log(this.SitesCodes);
          console.log(this.SitesNames);
        }
      }

    } else {// reagion == null
      // tslint:disable-next-line:one-line
      if (this.selectedArea !== 'All') {
        this.DisableZone = false;
        this.DisableSubArea = true;
        this.selectedZone = '';
        this.selectedSubArea = '';
        this.selectedSiteCode = '';
        this.selectedSiteName = '';
        this.tempRegions = this.siteLocation.GetTempRegion(this.selectedRegion, this.SiteInfo.DataHierarchy);
        this.tempAreas = this.siteLocation.GetTempArea(this.selectedArea, this.tempRegions);
        this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
        this.Zones = this.model.zone_list;
        this.SitesCodes = this.model.sitecode;
        this.SitesNames = this.model.sitename;

        this.AreaTempSiteCode = this.SitesCodes;
        this.AreaTempSiteName = this.SitesNames;
        console.log('tempArea', this.model);
        console.log(this.SitesCodes);
        console.log(this.SitesNames);

      } else {// area is All
        this.Count_Type_Obj.LoctionValues.Area = null
        this.Count_Type_Obj.LoctionValues.Zone = null
        this.Count_Type_Obj.LoctionValues.SubArea = null

        this.Host_Version_Obj.LoctionValues.Area = null
        this.Host_Version_Obj.LoctionValues.Zone = null
        this.Host_Version_Obj.LoctionValues.SubArea = null

        this.Busyness_data_Obj.LoctionValues.Area = null
        this.Busyness_data_Obj.LoctionValues.Zone = null
        this.Busyness_data_Obj.LoctionValues.SubArea = null

        this.Cells_Types_Obj.LoctionValues.Area = null
        this.Cells_Types_Obj.LoctionValues.Zone = null
        this.Cells_Types_Obj.LoctionValues.SubArea = null

        this.DisableZone = true;
        this.DisableSubArea = true;
        this.selectedZone = '';
        this.selectedSubArea = '';
        this.selectedSiteCode = '';
        this.selectedSiteName = '';
        this.Area = this.AllArea;
        this.Zones = this.AllZone;
        this.SubAreas = this.AllSubAreas;
        this.SitesCodes = this.AllSiteCodes;
        this.SitesNames = this.AllSiteNames;

        this.AreaTempSiteCode = this.SitesCodes;
        this.AreaTempSiteName = this.SitesNames;
        console.log(this.SitesCodes);
        console.log(this.SitesNames);
      }
    }


  }

  ZoneSelect(event) {

    
      this.Count_Type_Obj.LoctionValues.SubArea = null

      this.Host_Version_Obj.LoctionValues.SubArea = null

      this.Busyness_data_Obj.LoctionValues.SubArea = null

      this.Cells_Types_Obj.LoctionValues.SubArea = null

    if (this.selectedZone !== 'All') {
      this.Count_Type_Obj.LoctionValues.Zone = this.selectedZone;
      this.Host_Version_Obj.LoctionValues.Zone = this.selectedZone;
      this.Busyness_data_Obj.LoctionValues.Zone = this.selectedZone;
      this.Cells_Types_Obj.LoctionValues.Zone = this.selectedZone;

      this.DisableSubArea = false;
      this.selectedSubArea = '';
      this.selectedSiteCode = '';
      this.selectedSiteName = '';
      this.tempZones = this.siteLocation.GetTempZone(this.selectedZone, this.tempAreas);
      this.model = this.siteLocation.GetFilteredSubArea(this.tempZones);
      this.SubAreas = this.model.subarea_list;
      this.SitesCodes = this.model.sitecode;
      this.SitesNames = this.model.sitename;

      this.ZoneTempSiteCode = this.SitesCodes;
      this.ZoneTempSiteName = this.SitesNames;
      console.log(this.SitesCodes);
      console.log(this.SitesNames);
    }
    if (this.selectedZone === 'All') {
      this.Count_Type_Obj.LoctionValues.Zone = null
      this.Count_Type_Obj.LoctionValues.SubArea = null
      this.Host_Version_Obj.LoctionValues.Zone = null
      this.Host_Version_Obj.LoctionValues.SubArea = null
      this.Busyness_data_Obj.LoctionValues.Zone = null
      this.Busyness_data_Obj.LoctionValues.SubArea = null
      this.Cells_Types_Obj.LoctionValues.Zone = null
      this.Cells_Types_Obj.LoctionValues.SubArea = null

      this.selectedSubArea = '';
      this.selectedSiteCode = '';
      this.selectedSiteName = '';
      this.DisableSubArea = true;

      this.SitesCodes = this.AreaTempSiteCode;
      this.SitesNames = this.AreaTempSiteName;

      this.ZoneTempSiteCode = this.SitesCodes;
      this.ZoneTempSiteName = this.SitesNames;

      console.log(this.SitesCodes);
      console.log(this.SitesNames);

    }



  }
  SubAreaSelect(event) {

    if (this.selectedSubArea !== 'All') {
      this.Count_Type_Obj.LoctionValues.SubArea = this.selectedSubArea;

      this.Host_Version_Obj.LoctionValues.SubArea = this.selectedSubArea;

      this.Busyness_data_Obj.LoctionValues.SubArea = this.selectedSubArea;
      this.selectedSiteCode = '';
      this.selectedSiteName = '';
      this.tempSubAreas = this.siteLocation.GetTempSubArea(this.selectedSubArea, this.tempZones);
      this.model = this.siteLocation.GetFilteredSite(this.tempSubAreas);
      this.SitesCodes = this.model.sitecode;
      this.SitesNames = this.model.sitename;

      console.log(this.SitesCodes);
      console.log(this.SitesNames);
    } else { // SubArea is All
      this.Count_Type_Obj.LoctionValues.SubArea = null
      this.Host_Version_Obj.LoctionValues.SubArea = null
      this.Busyness_data_Obj.LoctionValues.SubArea = null
      this.Cells_Types_Obj.LoctionValues.SubArea = null

      this.selectedSiteCode = '';
      this.selectedSiteName = '';
      this.SitesCodes = this.ZoneTempSiteCode;
      this.SitesNames = this.ZoneTempSiteName;

      console.log(this.SitesCodes);
      console.log(this.SitesNames);
    }

  }

  CategorySelect($event) {
    this.loading = true;
    this.ChartService.get_NE_Levels(this.selectedCategory).subscribe(res => {
      this.NeLevel = res;
      this.NeLevel_Data = this.NeLevel.data;
    });
    this.Count_Type_Obj.ClassificationName = this.selectedCategory;
    this.Host_Version_Obj.ClassificationName = this.selectedCategory;
    this.Busyness_data_Obj.ClassificationName = this.selectedCategory
    this.Cells_Types_Obj.ClassificationName = "Radio"

    if (this.selectedCategory == "Controller") {
      this.Busyness_data_Obj.LevelSource.TableName = "Ran_slot";
      this.Busyness_data_Obj.LevelDistination.TableName = "Ran_board"
    }
    else {
      this.Busyness_data_Obj.LevelSource.TableName = "Radio_slot";
      this.Busyness_data_Obj.LevelDistination.TableName = "Radio_board"
    }
    setTimeout(() => {
      this.loading = false;
    }, 3000);
    
  }

  SelectedLevelSelect($event, tablename: string, columnname: string, columntype: string) {
    this.showGen = true;
    this.Count_Type_Obj.Level.TableName = tablename;
    this.Count_Type_Obj.Level.ColumnName = columnname;
    this.Count_Type_Obj.Level.ColumnType = columntype;


  }

  GenerateChart() {
    console.log("Busyness Object:", this.Busyness_data_Obj)
    this.loading = true;
    this.show = false;
    this.show2 = false;
    this.ShowBusynessReport = false;
    this.ShowTypeCountReport = false;
    this.barChartType === 'pie'

    //#region Intialize NeTypes objects and barchart
    this.NeTypes = new NeTypes();
    this.NeTypes.data = new DataTypesChart();
    this.NeTypes.data.LocationNames = new Array<string>();
    this.NeTypes.data.LocContValues = new Array<TypesCount>();
    this.NeTypes.data.Types = new Array<string>();
    this.charbarlist = new Array<string>();
    this.barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.charbarlist_temp = new Array<string>();
    this.ShowRepTypeCount = new Array<ReportTypeCount>()
    //#endregion


    //#region Intialize cells types count objects and barchart
    this.CellTypes = new CellsTypesCount();
    this.CellTypes.LocationNames = new Array<string>();
    this.CellTypes.LocCountValues = new Array<TypesCount>();
    this.CellTypes.Bands = new Array<string>();
    this.charbarlistCell = new Array<string>();
    this.barChartData_listCell = new Array<{ data: number[], label: string }>();
    this.ChartDataCell = new ChartsData();
    this.charbarlist_tempCell = new Array<string>();
    this.ShowRepTypeCountCell = new Array<ReportTypeCount>()
    //#endregion

    //#region Intialize Busyness objects
    this.busynessResult = new BusynessReturnedData();
    this.busynessResult.data = new BusynessDataObj();
    this.busynessResult.data.LocationNames = new Array<string>();
    this.busynessResult.data.Capacity = new Array<number>();
    this.busynessResult.data.Free = new Array<number>();
    this.busynessResult.data.Busy = new Array<number>();
    this.busynessChartLabel = new Array<string>();

    this.NeTypes.data.LocationNames = new Array<string>();
    //#endregion


    //#region  Intialize host version
    this.HostVersion = new HostVersionChart();
    this.HostVersion.LocationNames = new Array<string>();
    this.HostVersion.HostVersions = new Array<string>();
    this.HostVersion.LocCountValues = new Array<TypesCount>();
    this.ShowHostTypeCount = new Array<ReportTypeCount>()
    this.HostbarChartData_list = new Array<{ data: number[], label: string }>();
    //#endregion

    this.ChartService.GetCountTypes(this.Count_Type_Obj).subscribe(res => {
      this.NeTypes.success = res.success;
      this.NeTypes.errorMessage = res.errorMessage;
      this.NeTypes.data = res.data

      console.log("Ne Types finally", this.NeTypes.data)

    });
    console.log("celllllllllllllllllllllllll", this.Cells_Types_Obj);
    this.ChartService.GetCellsTypes(this.Cells_Types_Obj).subscribe(res => {
      this.CellTypes = res.data
      console.log("Ne cellTypes finally", this.CellTypes)
    })

    this.busynessResult = new BusynessReturnedData();
      this.ChartService.GetBusyness(this.Busyness_data_Obj).subscribe(res => {
      this.busynessResult.data = res.data;
      this.busynessResult.data.LocationNames = res.data.LocationNames;
      this.busynessResult.errorMessage = res.errorMessage;
      this.busynessResult.success = res.success;
      console.log("busyness results", this.busynessResult.data)
    })


    this.ChartService.Get_Host_Version(this.Host_Version_Obj).subscribe(res => {
      this.HostVersion = res.data;
      console.log("host version types", this.HostVersion)
    });


    this.charbarlist = new Array<string>();
    this.barChartData_list = [];
    this.barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.charbarlist_temp = new Array<string>();
    this.ShowRepBusyness = new Array<ReportList>();

    this.hostverbarlist_temp = new Array<string>();
    setTimeout(() => {
      if (this.NeTypes.data != null){
      for (let i = 0; i < this.NeTypes.data.LocationNames.length; i++) {
        this.charbarlist_temp.push(this.NeTypes.data.LocationNames[i]);
        for (let j = 0; j < this.NeTypes.data.LocContValues.length; j++) {
          this.ShowRepTypeCount.push({ NeSiteAtt: this.NeTypes.data.LocationNames[i], Type: this.NeTypes.data.Types[j], Count: this.NeTypes.data.LocContValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.NeTypes.data.Types.length; i++) {
        this.barChartData_list.push({ data: this.NeTypes.data.LocContValues[i].Values, label: this.NeTypes.data.Types[i] })

      }
    }
    else{
      this.barChartData_list = null;
    }



    if (this.CellTypes != null){

      for (let i = 0; i < this.CellTypes.LocationNames.length; i++) {
        this.charbarlist_tempCell.push(this.CellTypes.LocationNames[i]);
        for (let j = 0; j < this.CellTypes.LocCountValues.length; j++) {
          this.ShowRepTypeCountCell.push({ NeSiteAtt: this.CellTypes.LocationNames[i], Type: this.CellTypes.Bands[j], Count: this.CellTypes.LocCountValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.CellTypes.Bands.length; i++) {
        this.barChartData_listCell.push({ data: this.CellTypes.LocCountValues[i].Values, label: this.CellTypes.Bands[i] })

      }
    }

    else {
      this.barChartData_listCell = null;
    }



    if (this.HostVersion != null){
      for (let i = 0; i < this.HostVersion.LocationNames.length; i++) {
        this.hostverbarlist_temp.push(this.HostVersion.LocationNames[i]);
        for (let j = 0; j < this.HostVersion.LocCountValues.length; j++) {
          this.ShowHostTypeCount.push({ NeSiteAtt: this.HostVersion.LocationNames[i], Type: this.HostVersion.HostVersions[j], Count: this.HostVersion.LocCountValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.HostVersion.HostVersions.length; i++) {
        this.HostbarChartData_list.push({ data: this.HostVersion.LocCountValues[i].Values, label: this.HostVersion.HostVersions[i] })

      }
    }

    else {
      this.HostbarChartData_list = null;
    }

      

      

      
      this.charbarlist = this.charbarlist_temp;
      this.charbarlistCell = this.charbarlist_tempCell;
      this.hostbarlist = this.hostverbarlist_temp;

if (this.busynessResult.data != null){
      for (let i = 0; i < this.busynessResult.data.LocationNames.length; i++) {
        this.busynessChartLabel.push(this.busynessResult.data.LocationNames[i]);
        this.ShowRepBusyness.push({
          NeSiteAtt: this.busynessResult.data.LocationNames[i], CapcityLi: this.busynessResult.data.Capacity[i],
          FreeLi: this.busynessResult.data.Free[i], BusyLi: this.busynessResult.data.Busy[i]
        });
      }
      this.busynessChartData = [
        { data: this.busynessResult.data.Capacity, label: 'Capacity' },
        { data: this.busynessResult.data.Busy, label: 'Busy' },
        { data: this.busynessResult.data.Free, label: 'Free' }
      ]

    }
    else{
      this.busynessChartData = null;
    }

      this.show = true;
      this.show2 = true;
      this.loading = false;
      this.ShowBusynessReport = true;
      this.ShowTypeCountReport = true;
      this.ShowCharts2()
    }, 10000);


  }

}