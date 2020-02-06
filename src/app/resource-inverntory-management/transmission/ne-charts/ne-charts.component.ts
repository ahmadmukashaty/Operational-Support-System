import { Component, OnInit, Directive, ViewChild } from '@angular/core';
import { trigger, state, style, transition, animate, AUTO_STYLE } from '@angular/animations';
import { LocationSiteData } from '../../extraClasses/LocationSiteData';
import { Area } from '../../extraClasses/LocationSiteHierarchy/Area';
import { Zone } from '../../extraClasses/LocationSiteHierarchy/Zone';
import { SubArea } from '../../extraClasses/LocationSiteHierarchy/SubArea';
import { Site } from '../../extraClasses/Site';
import { FilterModel } from '../../extraClasses/LocationSiteHierarchy/FilterModel';
import { SiteLocationService } from '../../services/getSiteInfo.service';
import { NodeOfTree } from '../../extraClasses/TreeNode';
import { BusynessResultService } from '../../services/BusynessResult.service';
import { NeTypesService } from '../../services/NeTypes.service';
import { BusynessRes } from '../../extraClasses/Result';
import { PlatformLocation } from '@angular/common';
import { DataComTypes } from '../../extraClasses/DataComTypes';
import { ElementType } from '../../extraClasses/ElementType';
import { ReportList } from '../../extraClasses/ReportList';
import { SelectItem } from 'primeng/components/common/selectitem';
import { BaseChartDirective } from '../../../../../node_modules/ng2-charts';
//import { delay } from 'core-js';
import { ngxLoadingAnimationTypes } from "ngx-loadingV2";
import { CountLevelAttributes } from '../../extraClasses/RanCharts/CountLevelAttributes';
import { BusynessInput } from '../../extraClasses/RanCharts/BusynessInput';
import { LevelTypes } from '../../extraClasses/RanCharts/LevelTypes';
import { LevelAttributes } from '../../extraClasses/RanCharts/LevelAttributes';
import { DBSourceAttributes } from '../../extraClasses/RanCharts/DBSourceAttributes';
import { CountTypesInput } from '../../extraClasses/RanCharts/CountTypesInput';
import { HostVersionInput } from '../../extraClasses/RanCharts/HostVersionInput';
import { NeTypes } from '../../extraClasses/RanCharts/NeTypes';
import { HostVersionChart } from '../../extraClasses/RanCharts/HostVersionChart';
import { BusynessReturnedData } from '../../extraClasses/RanCharts/BusynessReturnedData';
import { ChartsData } from '../../extraClasses/RanCharts/ChartsData';
import { CellsTypesObj } from '../../extraClasses/RanCharts/CellsTypesObj';
import { ReportTypeCount } from '../../extraClasses/RanCharts/ReportTypeCount';
import { CellsTypesCount } from '../../extraClasses/RanCharts/CellsTypesCount';
import { RAN_ChartsService } from '../../services/RAN_Charts.service';
import { LocationAttributes } from '../../extraClasses/RanCharts/LocationAttributes';
import { DataTypesChart } from '../../extraClasses/RanCharts/DataTypesChart';
import { TypesCount } from '../../extraClasses/RanCharts/TypesCount';
import { BusynessDataObj } from '../../extraClasses/RanCharts/BusynessDataObj';
import { Trans_ChartService } from '../../services/Trans_Chart.service';
import { Categories } from '../../extraClasses/Trans_Chart/Categories';
import { NEType } from '../../extraClasses/ReportFilter/NEType';
import { TransmissionLevel } from '../../extraClasses/Trans_Chart/TransmissionLevel';
import { TransBusynessInput } from '../../extraClasses/Trans_Chart/TransBusynessInput';
import { LevelSourceType } from '../../extraClasses/Trans_Chart/LevelSourceType';

declare var jquery: any;
declare var $: any;

const PrimaryWhite = '#ffffff';
const SecondaryGrey = '#ccc';
const PrimaryRed = '#dd0031';
const SecondaryBlue = '#006ddd';

@Component({
  selector: 'app-ne-charts',
  templateUrl: './ne-charts.component.html',
  styleUrls: ['./ne-charts.component.css'],
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


export class NeChartsComponent implements OnInit {
  Categories: any[] = [{ "label": "DataCom", "value": "DataCom" }, { "label": "Optical", "value": "Optical" },
  { "label": "MW", "value": "MW" }, { "label": "FIREWALL", "value": "FIREWALL" }];
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
  public DC_Type_Obj: CountTypesInput;
  public OP_Type_Obj: CountTypesInput;
  public MW_Type_Obj: CountTypesInput;
  public FW_Type_Obj: CountTypesInput;
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
  ShowDefReport: boolean;
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
  TransCat: Categories[];
  DC_NeTypes: NeTypes;
  OP_NeTypes: NeTypes;
  MW_NeTypes: NeTypes;
  FW_NeTypes: NeTypes;
  DC_ShowRepTypeCount: ReportTypeCount[];
  DC_barChartData_list: { data: number[]; label: string; }[] = [];
  DC_charbarlist: string[];
  OP_ShowRepTypeCount: ReportTypeCount[];
  OP_barChartData_list: { data: number[]; label: string; }[] = [];
  OP_charbarlist: string[];
  MW_ShowRepTypeCount: ReportTypeCount[];
  MW_barChartData_list: { data: number[]; label: string; }[] = [];
  MW_charbarlist: string[];
  FW_ShowRepTypeCount: ReportTypeCount[];
  FW_barChartData_list: { data: number[]; label: string; }[] = [];
  FW_charbarlist: string[];
  DC_charbarlist_temp: string[];
  OP_charbarlist_temp: string[];
  MW_charbarlist_temp: string[];
  FW_charbarlist_temp: string[];
  TranBusyness_confg_Obj: TransmissionLevel[];
  tranBusyness_data_Obj: TransBusynessInput;
  NeLevel_Data_Des: TransmissionLevel[];
  showDes: boolean;
  TranbusynessResult: BusynessReturnedData;
  TranbusynessChartLabel: string[];


  public colors: Array<any> = [
    {
      backgroundColor: 
        'rgba(255, 0, 0,0.3)'
    },
    {
      backgroundColor: 
        'rgba(255, 128, 0, 0.4)'
    },
    {
      backgroundColor: 
        'rgba(51, 255, 51, 0.4)'
    },
    {
      backgroundColor: 
        'rgba(51, 153, 255, 0.4)'
    },
    // {
    //   backgroundColor: 
    //     'rgba(127, 0, 255, 0.3)'
    // },
    {
      backgroundColor: 
        'rgba(128, 128, 128, 0.5)'
    },
    {
      backgroundColor: 
        'rgba(0, 0, 0, 0.4)'
    },
    {
      backgroundColor: 
        'rgba(102, 0, 51, 0.4)'
    },
    {
      backgroundColor: 
        'rgba(0, 102, 51, 0.4)'
    },
    {
      backgroundColor: 
        'rgba(0, 0, 102, 0.4)'
    },
    {
      backgroundColor: 
        'rgba(102, 51, 0, 0.4)'
    }


  ];


  constructor(private siteLocation: SiteLocationService, private ChartService: RAN_ChartsService, private transChartservice: Trans_ChartService) { }

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

    this.tranBusyness_data_Obj = new TransBusynessInput();
    this.tranBusyness_data_Obj.LevelSource = new CountLevelAttributes();
    this.tranBusyness_data_Obj.LevelSourceType = new LevelSourceType();
    this.tranBusyness_data_Obj.LevelDistination = new CountLevelAttributes();
    this.tranBusyness_data_Obj.dbsource = new DBSourceAttributes();
    this.tranBusyness_data_Obj.LoctionValues = new LocationAttributes();
    this.tranBusyness_data_Obj.LoctionValues.Area = null;
    this.tranBusyness_data_Obj.LoctionValues.Region = null;
    this.tranBusyness_data_Obj.LoctionValues.SubArea = null;
    this.tranBusyness_data_Obj.LoctionValues.Zone = null;

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
        optionsVariable: { scales: { xAxes: [{ ticks: { beginAtZero: true, /*stepValue: 1000, max: 50, */} }], yAxes: [{ ticks: { beginAtZero: true } }], } },
        responsive: true
      };



      console.log("regions: ", this.Region)

    });




    this.ChartService.get_DBSource().subscribe(res => {

      this.To_GET_DBSource = res.data.DB_Source;

      this.DC_Type_Obj.dbsource = this.To_GET_DBSource;
      this.OP_Type_Obj.dbsource = this.To_GET_DBSource;
      this.MW_Type_Obj.dbsource = this.To_GET_DBSource;
      this.FW_Type_Obj.dbsource = this.To_GET_DBSource;

      this.Count_Type_Obj.dbsource = this.To_GET_DBSource;
      this.Host_Version_Obj.dbsource = this.To_GET_DBSource;
      this.Busyness_data_Obj.dbsource = this.To_GET_DBSource;
      this.Cells_Types_Obj.dbsource = this.To_GET_DBSource;

      this.tranBusyness_data_Obj.dbsource = this.To_GET_DBSource;

    });


    this.Count_Type_Obj = { "Level": { "TableName": "RADIO_NETYPE", "ColumnName": "TYPE", "ColumnType": "VARCHAR2" }, "LoctionValues": { "Area": null, "Region": null, "SubArea": null, "Zone": null }, "dbsource": { "RegionSource": { "TableName": "REGION", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "AreaSource": { "TableName": "AREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "ZoneSource": { "TableName": "ZONE", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SubAreaSource": { "TableName": "SUBAREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SiteSource": { "TableName": "SITE_CANDIDATE", "SiteCodeColumnName": "SITE_CODE", "SiteCodeColumnType": "VARCHAR2", "EnglishNameColumnName": "ENGLISH_NAME", "EnglishNameColumnType": "VARCHAR2" } }, "ClassificationName": "Radio" }
    this.Busyness_data_Obj = { "LevelSource": { "ColumnName": "ID", "ColumnType": "number", "TableName": "Radio_slot" }, "LevelDistination": { "ColumnName": "ID", "ColumnType": "number", "TableName": "Radio_board" }, "LoctionValues": { "Area": null, "Region": null, "SubArea": null, "Zone": null }, "dbsource": { "RegionSource": { "TableName": "REGION", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "AreaSource": { "TableName": "AREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "ZoneSource": { "TableName": "ZONE", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SubAreaSource": { "TableName": "SUBAREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SiteSource": { "TableName": "SITE_CANDIDATE", "SiteCodeColumnName": "SITE_CODE", "SiteCodeColumnType": "VARCHAR2", "EnglishNameColumnName": "ENGLISH_NAME", "EnglishNameColumnType": "VARCHAR2" } }, "ClassificationName": "Radio" }
    this.Host_Version_Obj = { "LoctionValues": { "Area": null, "Region": null, "SubArea": null, "Zone": null }, "dbsource": { "RegionSource": { "TableName": "REGION", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "AreaSource": { "TableName": "AREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "ZoneSource": { "TableName": "ZONE", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SubAreaSource": { "TableName": "SUBAREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SiteSource": { "TableName": "SITE_CANDIDATE", "SiteCodeColumnName": "SITE_CODE", "SiteCodeColumnType": "VARCHAR2", "EnglishNameColumnName": "ENGLISH_NAME", "EnglishNameColumnType": "VARCHAR2" } }, "ClassificationName": "Radio" }
    this.Cells_Types_Obj = { "LoctionValues": { "Area": null, "Region": null, "SubArea": null, "Zone": null }, "dbsource": { "RegionSource": { "TableName": "REGION", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "AreaSource": { "TableName": "AREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "ZoneSource": { "TableName": "ZONE", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SubAreaSource": { "TableName": "SUBAREA", "ColumnName": "NAME", "ColumnType": "VARCHAR2" }, "SiteSource": { "TableName": "SITE_CANDIDATE", "SiteCodeColumnName": "SITE_CODE", "SiteCodeColumnType": "VARCHAR2", "EnglishNameColumnName": "ENGLISH_NAME", "EnglishNameColumnType": "VARCHAR2" } }, "ClassificationName": "Radio" }

    this.DC_Type_Obj = {
      "ClassificationName": "DataCom",
      "LoctionValues": {
        "Region": null,
        "Area": null,
        "Zone": null,
        "SubArea": null
      },
      "Level": {
        "TableName": "DATACOM_NE_TYPE",
        "ColumnName": "MODEL",
        "ColumnType": "VARCHAR2"
      },
      "dbsource": {
        "RegionSource": {
          "TableName": "REGION",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "AreaSource": {
          "TableName": "AREA",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "ZoneSource": {
          "TableName": "ZONE",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "SubAreaSource": {
          "TableName": "SUBAREA",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "SiteSource": null
      }
    };

    this.OP_Type_Obj = {
      "ClassificationName": "Optical",
      "LoctionValues": {
        "Region": null,
        "Area": null,
        "Zone": null,
        "SubArea": null
      },
      "Level": {
        "TableName": "OPTICAL_NE_TYPE",
        "ColumnName": "TYPE",
        "ColumnType": "VARCHAR2"
      },
      "dbsource": {
        "RegionSource": {
          "TableName": "REGION",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "AreaSource": {
          "TableName": "AREA",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "ZoneSource": {
          "TableName": "ZONE",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "SubAreaSource": {
          "TableName": "SUBAREA",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "SiteSource": null
      }
    };

    this.MW_Type_Obj = {
      "ClassificationName": "MW",
      "LoctionValues": {
        "Region": null,
        "Area": null,
        "Zone": null,
        "SubArea": null
      },
      "Level": {
        "TableName": "MW_NE_TYPE",
        "ColumnName": "TYPE",
        "ColumnType": "VARCHAR2"
      },
      "dbsource": {
        "RegionSource": {
          "TableName": "REGION",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "AreaSource": {
          "TableName": "AREA",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "ZoneSource": {
          "TableName": "ZONE",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "SubAreaSource": {
          "TableName": "SUBAREA",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "SiteSource": null
      }
    };

    this.FW_Type_Obj = {
      "ClassificationName": "FIREWALL",
      "LoctionValues": {
        "Region": null,
        "Area": null,
        "Zone": null,
        "SubArea": null
      },
      "Level": {
        "TableName": "FIREWALL_NE_TYPE",
        "ColumnName": "MODEL",
        "ColumnType": "VARCHAR2"
      },
      "dbsource": {
        "RegionSource": {
          "TableName": "REGION",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "AreaSource": {
          "TableName": "AREA",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "ZoneSource": {
          "TableName": "ZONE",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "SubAreaSource": {
          "TableName": "SUBAREA",
          "ColumnName": "NAME",
          "ColumnType": "VARCHAR2"
        },
        "SiteSource": null
      }
    }

    //#region Intialize NeTypes objects and barchart
    this.DC_NeTypes = new NeTypes();
    this.DC_NeTypes.data = new DataTypesChart();
    this.DC_NeTypes.data.LocationNames = new Array<string>();
    this.DC_NeTypes.data.LocContValues = new Array<TypesCount>();
    this.DC_NeTypes.data.Types = new Array<string>();
    this.DC_charbarlist = new Array<string>();
    this.DC_barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.DC_charbarlist_temp = new Array<string>();
    this.DC_ShowRepTypeCount = new Array<ReportTypeCount>()

    this.OP_NeTypes = new NeTypes();
    this.OP_NeTypes.data = new DataTypesChart();
    this.OP_NeTypes.data.LocationNames = new Array<string>();
    this.OP_NeTypes.data.LocContValues = new Array<TypesCount>();
    this.OP_NeTypes.data.Types = new Array<string>();
    this.OP_charbarlist = new Array<string>();
    this.OP_barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.OP_charbarlist_temp = new Array<string>();
    this.OP_ShowRepTypeCount = new Array<ReportTypeCount>()

    this.MW_NeTypes = new NeTypes();
    this.MW_NeTypes.data = new DataTypesChart();
    this.MW_NeTypes.data.LocationNames = new Array<string>();
    this.MW_NeTypes.data.LocContValues = new Array<TypesCount>();
    this.MW_NeTypes.data.Types = new Array<string>();
    this.MW_charbarlist = new Array<string>();
    this.MW_barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.MW_charbarlist_temp = new Array<string>();
    this.MW_ShowRepTypeCount = new Array<ReportTypeCount>()

    this.FW_NeTypes = new NeTypes();
    this.FW_NeTypes.data = new DataTypesChart();
    this.FW_NeTypes.data.LocationNames = new Array<string>();
    this.FW_NeTypes.data.LocContValues = new Array<TypesCount>();
    this.FW_NeTypes.data.Types = new Array<string>();
    this.FW_charbarlist = new Array<string>();
    this.FW_barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.FW_charbarlist_temp = new Array<string>();
    this.FW_ShowRepTypeCount = new Array<ReportTypeCount>()
    //#endregion



    this.transChartservice.GetCountTypes(this.DC_Type_Obj).subscribe(res => {
      this.DC_NeTypes.success = res.success;
      this.DC_NeTypes.errorMessage = res.errorMessage;
      this.DC_NeTypes.data = res.data

    });

    this.transChartservice.GetCountTypes(this.OP_Type_Obj).subscribe(res => {
      this.OP_NeTypes.success = res.success;
      this.OP_NeTypes.errorMessage = res.errorMessage;
      this.OP_NeTypes.data = res.data

    });

    this.transChartservice.GetCountTypes(this.MW_Type_Obj).subscribe(res => {
      this.MW_NeTypes.success = res.success;
      this.MW_NeTypes.errorMessage = res.errorMessage;
      this.MW_NeTypes.data = res.data

    });

    this.transChartservice.GetCountTypes(this.FW_Type_Obj).subscribe(res => {
      this.FW_NeTypes.success = res.success;
      this.FW_NeTypes.errorMessage = res.errorMessage;
      this.FW_NeTypes.data = res.data

    });


    setTimeout(() => {
      for (let i = 0; i < this.DC_NeTypes.data.LocationNames.length; i++) {
        this.DC_charbarlist_temp.push(this.DC_NeTypes.data.LocationNames[i]);
        for (let j = 0; j < this.DC_NeTypes.data.LocContValues.length; j++) {
          this.DC_ShowRepTypeCount.push({ NeSiteAtt: this.DC_NeTypes.data.LocationNames[i], Type: this.DC_NeTypes.data.Types[j], Count: this.DC_NeTypes.data.LocContValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.DC_NeTypes.data.Types.length; i++) {
        this.DC_barChartData_list.push({ data: this.DC_NeTypes.data.LocContValues[i].Values, label: this.DC_NeTypes.data.Types[i] })

      }

      this.DC_charbarlist = this.DC_charbarlist_temp;




      for (let i = 0; i < this.OP_NeTypes.data.LocationNames.length; i++) {
        this.OP_charbarlist_temp.push(this.OP_NeTypes.data.LocationNames[i]);
        for (let j = 0; j < this.OP_NeTypes.data.LocContValues.length; j++) {
          this.OP_ShowRepTypeCount.push({ NeSiteAtt: this.OP_NeTypes.data.LocationNames[i], Type: this.OP_NeTypes.data.Types[j], Count: this.OP_NeTypes.data.LocContValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.OP_NeTypes.data.Types.length; i++) {
        this.OP_barChartData_list.push({ data: this.OP_NeTypes.data.LocContValues[i].Values, label: this.OP_NeTypes.data.Types[i] })

      }

      this.OP_charbarlist = this.OP_charbarlist_temp;



      for (let i = 0; i < this.MW_NeTypes.data.LocationNames.length; i++) {
        this.MW_charbarlist_temp.push(this.MW_NeTypes.data.LocationNames[i]);
        for (let j = 0; j < this.MW_NeTypes.data.LocContValues.length; j++) {
          this.MW_ShowRepTypeCount.push({ NeSiteAtt: this.MW_NeTypes.data.LocationNames[i], Type: this.MW_NeTypes.data.Types[j], Count: this.MW_NeTypes.data.LocContValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.MW_NeTypes.data.Types.length; i++) {
        this.MW_barChartData_list.push({ data: this.MW_NeTypes.data.LocContValues[i].Values, label: this.MW_NeTypes.data.Types[i] })

      }

      this.MW_charbarlist = this.MW_charbarlist_temp;


      for (let i = 0; i < this.FW_NeTypes.data.LocationNames.length; i++) {
        this.FW_charbarlist_temp.push(this.FW_NeTypes.data.LocationNames[i]);
        for (let j = 0; j < this.FW_NeTypes.data.LocContValues.length; j++) {
          this.FW_ShowRepTypeCount.push({ NeSiteAtt: this.FW_NeTypes.data.LocationNames[i], Type: this.FW_NeTypes.data.Types[j], Count: this.FW_NeTypes.data.LocContValues[j].Values[i] })
        }
      }

      for (let i = 0; i < this.FW_NeTypes.data.Types.length; i++) {
        this.FW_barChartData_list.push({ data: this.FW_NeTypes.data.LocContValues[i].Values, label: this.FW_NeTypes.data.Types[i] })

      }

      this.FW_charbarlist = this.FW_charbarlist_temp;

      this.show = true;
      this.loading = false;
      this.ShowDefReport = true;
      this.SelectedLevel = null;
      this.ShowCharts(event)
    }, 12000);


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
      $('.chart-style.box.shadow10[_ngcontent-c6]').animate({
        height: this.chart1HeightClosed
      }, 500);
      $('.canvas1').css('display', 'none');
      this.chart1IsOpened = false;
    }
    else {
      $('.chart-style.box.shadow10[_ngcontent-c6]').animate({
        height: this.chart1HeightOpened
      }, 500);
      $('.canvas1').css('display', 'block');
      this.chart1IsOpened = true;
    }
  }

  //chart 2
  chart2HeightClosed = '55px';
  chart2HeightOpened = '425px';
  chart2IsOpened = true;
  toggleOpenedChart2() {
    if (this.chart2IsOpened) {
      $('.chart-style.box.shadow11[_ngcontent-c6]').animate({
        height: this.chart2HeightClosed
      }, 500);
      $('.canvas2').css('height', '0px');
      this.chart2IsOpened = false;
    }
    else {
      $('.chart-style.box.shadow11[_ngcontent-c6]').animate({
        height: this.chart2HeightOpened
      }, 500);
      $('.canvas2').css('height', this.chart1Height);
      this.chart2IsOpened = true;
    }
  }

  //chart 3
  chart3HeightClosed = '55px';
  chart3HeightOpened = '425px';
  chart3IsOpened = true;
  toggleOpenedChart3() {
    if (this.chart3IsOpened) {
      $('.chart-style.box.shadow12[_ngcontent-c6]').animate({
        height: this.chart3HeightClosed
      }, 500);
      $('.canvas3').css('height', '0px');
      this.chart3IsOpened = false;
    }
    else {
      $('.chart-style.box.shadow12[_ngcontent-c6]').animate({
        height: this.chart3HeightOpened
      }, 500);
      $('.canvas3').css('height', this.chart1Height);
      this.chart3IsOpened = true;
    }
  }


  //chart 4
  chart4HeightClosed = '55px';
  chart4HeightOpened = '425px';
  chart4IsOpened = true;
  toggleOpenedChart4() {
    if (this.chart4IsOpened) {
      $('.chart-style.box.shadow13[_ngcontent-c6]').animate({
        height: this.chart4HeightClosed
      }, 500);
      $('.canvas4').css('height', '0px');
      this.chart4IsOpened = false;
    }
    else {
      $('.chart-style.box.shadow13[_ngcontent-c6]').animate({
        height: this.chart4HeightOpened
      }, 500);
      $('.canvas4').css('height', this.chart1Height);
      this.chart4IsOpened = true;
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

    this.DC_Type_Obj.LoctionValues.Area = null
    this.DC_Type_Obj.LoctionValues.Zone = null
    this.DC_Type_Obj.LoctionValues.SubArea = null

    this.OP_Type_Obj.LoctionValues.Area = null
    this.OP_Type_Obj.LoctionValues.Zone = null
    this.OP_Type_Obj.LoctionValues.SubArea = null

    this.MW_Type_Obj.LoctionValues.Area = null
    this.MW_Type_Obj.LoctionValues.Zone = null
    this.MW_Type_Obj.LoctionValues.SubArea = null

    this.FW_Type_Obj.LoctionValues.Area = null
    this.FW_Type_Obj.LoctionValues.Zone = null
    this.FW_Type_Obj.LoctionValues.SubArea = null

    this.Host_Version_Obj.LoctionValues.Area = null
    this.Host_Version_Obj.LoctionValues.Zone = null
    this.Host_Version_Obj.LoctionValues.SubArea = null

    this.Busyness_data_Obj.LoctionValues.Area = null
    this.Busyness_data_Obj.LoctionValues.Zone = null
    this.Busyness_data_Obj.LoctionValues.SubArea = null

    this.tranBusyness_data_Obj.LoctionValues.Area = null
    this.tranBusyness_data_Obj.LoctionValues.Zone = null
    this.tranBusyness_data_Obj.LoctionValues.SubArea = null


    this.Cells_Types_Obj.LoctionValues.Area = null
    this.Cells_Types_Obj.LoctionValues.Zone = null
    this.Cells_Types_Obj.LoctionValues.SubArea = null

    if (this.selectedRegion !== 'All') {
      this.Count_Type_Obj.LoctionValues.Region = this.selectedRegion;
      this.DC_Type_Obj.LoctionValues.Region = this.selectedRegion;
      this.OP_Type_Obj.LoctionValues.Region = this.selectedRegion;
      this.MW_Type_Obj.LoctionValues.Region = this.selectedRegion;
      this.FW_Type_Obj.LoctionValues.Region = this.selectedRegion;
      this.Host_Version_Obj.LoctionValues.Region = this.selectedRegion;
      this.Busyness_data_Obj.LoctionValues.Region = this.selectedRegion;
      this.tranBusyness_data_Obj.LoctionValues.Region = this.selectedRegion;
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

      this.DC_Type_Obj.LoctionValues.Region = null;
      this.DC_Type_Obj.LoctionValues.Area = null
      this.DC_Type_Obj.LoctionValues.Zone = null
      this.DC_Type_Obj.LoctionValues.SubArea = null

      this.OP_Type_Obj.LoctionValues.Region = null;
      this.OP_Type_Obj.LoctionValues.Area = null
      this.OP_Type_Obj.LoctionValues.Zone = null
      this.OP_Type_Obj.LoctionValues.SubArea = null

      this.MW_Type_Obj.LoctionValues.Region = null;
      this.MW_Type_Obj.LoctionValues.Area = null
      this.MW_Type_Obj.LoctionValues.Zone = null
      this.MW_Type_Obj.LoctionValues.SubArea = null

      this.FW_Type_Obj.LoctionValues.Region = null;
      this.FW_Type_Obj.LoctionValues.Area = null
      this.FW_Type_Obj.LoctionValues.Zone = null
      this.FW_Type_Obj.LoctionValues.SubArea = null

      this.Host_Version_Obj.LoctionValues.Region = null;
      this.Host_Version_Obj.LoctionValues.Area = null
      this.Host_Version_Obj.LoctionValues.Zone = null
      this.Host_Version_Obj.LoctionValues.SubArea = null

      this.Busyness_data_Obj.LoctionValues.Region = null;
      this.Busyness_data_Obj.LoctionValues.Area = null
      this.Busyness_data_Obj.LoctionValues.Zone = null
      this.Busyness_data_Obj.LoctionValues.SubArea = null

      this.tranBusyness_data_Obj.LoctionValues.Region = null;
      this.tranBusyness_data_Obj.LoctionValues.Area = null
      this.tranBusyness_data_Obj.LoctionValues.Zone = null
      this.tranBusyness_data_Obj.LoctionValues.SubArea = null

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

    this.DC_Type_Obj.LoctionValues.Zone = null
    this.DC_Type_Obj.LoctionValues.SubArea = null

    this.OP_Type_Obj.LoctionValues.Zone = null
    this.OP_Type_Obj.LoctionValues.SubArea = null

    this.MW_Type_Obj.LoctionValues.Zone = null
    this.MW_Type_Obj.LoctionValues.SubArea = null

    this.FW_Type_Obj.LoctionValues.Zone = null
    this.FW_Type_Obj.LoctionValues.SubArea = null


    this.Host_Version_Obj.LoctionValues.Zone = null
    this.Host_Version_Obj.LoctionValues.SubArea = null


    this.Busyness_data_Obj.LoctionValues.Zone = null
    this.Busyness_data_Obj.LoctionValues.SubArea = null

    this.tranBusyness_data_Obj.LoctionValues.Zone = null
    this.tranBusyness_data_Obj.LoctionValues.SubArea = null



    this.Cells_Types_Obj.LoctionValues.Zone = null
    this.Cells_Types_Obj.LoctionValues.SubArea = null

    if (this.selectedRegion != null) {
      if (this.selectedRegion === 'All') { // Region ALL
        if (this.selectedArea === 'All') { // Region & Area == All
          this.Count_Type_Obj.LoctionValues.Area = null
          this.Count_Type_Obj.LoctionValues.Zone = null
          this.Count_Type_Obj.LoctionValues.SubArea = null

          this.DC_Type_Obj.LoctionValues.Area = null
          this.DC_Type_Obj.LoctionValues.Zone = null
          this.DC_Type_Obj.LoctionValues.SubArea = null

          this.OP_Type_Obj.LoctionValues.Area = null
          this.OP_Type_Obj.LoctionValues.Zone = null
          this.OP_Type_Obj.LoctionValues.SubArea = null

          this.MW_Type_Obj.LoctionValues.Area = null
          this.MW_Type_Obj.LoctionValues.Zone = null
          this.MW_Type_Obj.LoctionValues.SubArea = null

          this.FW_Type_Obj.LoctionValues.Area = null
          this.FW_Type_Obj.LoctionValues.Zone = null
          this.FW_Type_Obj.LoctionValues.SubArea = null

          this.Host_Version_Obj.LoctionValues.Area = null
          this.Host_Version_Obj.LoctionValues.Zone = null
          this.Host_Version_Obj.LoctionValues.SubArea = null

          this.Busyness_data_Obj.LoctionValues.Area = null
          this.Busyness_data_Obj.LoctionValues.Zone = null
          this.Busyness_data_Obj.LoctionValues.SubArea = null

          this.tranBusyness_data_Obj.LoctionValues.Area = null
          this.tranBusyness_data_Obj.LoctionValues.Zone = null
          this.tranBusyness_data_Obj.LoctionValues.SubArea = null

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
          this.DC_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.OP_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.MW_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.FW_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.Host_Version_Obj.LoctionValues.Area = this.selectedArea;
          this.Busyness_data_Obj.LoctionValues.Area = this.selectedArea;
          this.tranBusyness_data_Obj.LoctionValues.Area = this.selectedArea;
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

          this.DC_Type_Obj.LoctionValues.Area = null
          this.DC_Type_Obj.LoctionValues.Zone = null
          this.DC_Type_Obj.LoctionValues.SubArea = null

          this.OP_Type_Obj.LoctionValues.Area = null
          this.OP_Type_Obj.LoctionValues.Zone = null
          this.OP_Type_Obj.LoctionValues.SubArea = null

          this.MW_Type_Obj.LoctionValues.Area = null
          this.MW_Type_Obj.LoctionValues.Zone = null
          this.MW_Type_Obj.LoctionValues.SubArea = null

          this.FW_Type_Obj.LoctionValues.Area = null
          this.FW_Type_Obj.LoctionValues.Zone = null
          this.FW_Type_Obj.LoctionValues.SubArea = null

          this.Host_Version_Obj.LoctionValues.Area = null
          this.Host_Version_Obj.LoctionValues.Zone = null
          this.Host_Version_Obj.LoctionValues.SubArea = null

          this.Busyness_data_Obj.LoctionValues.Area = null
          this.Busyness_data_Obj.LoctionValues.Zone = null
          this.Busyness_data_Obj.LoctionValues.SubArea = null

          this.tranBusyness_data_Obj.LoctionValues.Area = null
          this.tranBusyness_data_Obj.LoctionValues.Zone = null
          this.tranBusyness_data_Obj.LoctionValues.SubArea = null

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
          this.DC_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.OP_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.MW_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.FW_Type_Obj.LoctionValues.Area = this.selectedArea;
          this.Host_Version_Obj.LoctionValues.Area = this.selectedArea;
          this.Busyness_data_Obj.LoctionValues.Area = this.selectedArea;
          this.tranBusyness_data_Obj.LoctionValues.Area = this.selectedArea;
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

        this.DC_Type_Obj.LoctionValues.Area = null
        this.DC_Type_Obj.LoctionValues.Zone = null
        this.DC_Type_Obj.LoctionValues.SubArea = null

        this.OP_Type_Obj.LoctionValues.Area = null
        this.OP_Type_Obj.LoctionValues.Zone = null
        this.OP_Type_Obj.LoctionValues.SubArea = null

        this.MW_Type_Obj.LoctionValues.Area = null
        this.MW_Type_Obj.LoctionValues.Zone = null
        this.MW_Type_Obj.LoctionValues.SubArea = null

        this.FW_Type_Obj.LoctionValues.Area = null
        this.FW_Type_Obj.LoctionValues.Zone = null
        this.FW_Type_Obj.LoctionValues.SubArea = null

        this.Host_Version_Obj.LoctionValues.Area = null
        this.Host_Version_Obj.LoctionValues.Zone = null
        this.Host_Version_Obj.LoctionValues.SubArea = null

        this.Busyness_data_Obj.LoctionValues.Area = null
        this.Busyness_data_Obj.LoctionValues.Zone = null
        this.Busyness_data_Obj.LoctionValues.SubArea = null

        this.tranBusyness_data_Obj.LoctionValues.Area = null
        this.tranBusyness_data_Obj.LoctionValues.Zone = null
        this.tranBusyness_data_Obj.LoctionValues.SubArea = null

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
    this.DC_Type_Obj.LoctionValues.SubArea = null
    this.OP_Type_Obj.LoctionValues.SubArea = null
    this.MW_Type_Obj.LoctionValues.SubArea = null
    this.FW_Type_Obj.LoctionValues.SubArea = null

    this.Host_Version_Obj.LoctionValues.SubArea = null

    this.Busyness_data_Obj.LoctionValues.SubArea = null

    this.Cells_Types_Obj.LoctionValues.SubArea = null

    if (this.selectedZone !== 'All') {
      this.Count_Type_Obj.LoctionValues.Zone = this.selectedZone;
      this.DC_Type_Obj.LoctionValues.Zone = this.selectedZone;
      this.OP_Type_Obj.LoctionValues.Zone = this.selectedZone;
      this.MW_Type_Obj.LoctionValues.Zone = this.selectedZone;
      this.FW_Type_Obj.LoctionValues.Zone = this.selectedZone;
      this.Host_Version_Obj.LoctionValues.Zone = this.selectedZone;
      this.Busyness_data_Obj.LoctionValues.Zone = this.selectedZone;
      this.tranBusyness_data_Obj.LoctionValues.Zone = this.selectedZone;
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

      this.DC_Type_Obj.LoctionValues.Zone = null
      this.DC_Type_Obj.LoctionValues.SubArea = null

      this.OP_Type_Obj.LoctionValues.Zone = null
      this.OP_Type_Obj.LoctionValues.SubArea = null

      this.MW_Type_Obj.LoctionValues.Zone = null
      this.MW_Type_Obj.LoctionValues.SubArea = null

      this.FW_Type_Obj.LoctionValues.Zone = null
      this.FW_Type_Obj.LoctionValues.SubArea = null

      this.Host_Version_Obj.LoctionValues.Zone = null
      this.Host_Version_Obj.LoctionValues.SubArea = null
      this.Busyness_data_Obj.LoctionValues.Zone = null
      this.Busyness_data_Obj.LoctionValues.SubArea = null

      this.tranBusyness_data_Obj.LoctionValues.Zone = null
      this.tranBusyness_data_Obj.LoctionValues.SubArea = null

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
      this.tranBusyness_data_Obj.LoctionValues.SubArea = this.selectedSubArea;
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
      this.DC_Type_Obj.LoctionValues.SubArea = null
      this.OP_Type_Obj.LoctionValues.SubArea = null
      this.MW_Type_Obj.LoctionValues.SubArea = null
      this.FW_Type_Obj.LoctionValues.SubArea = null
      this.Host_Version_Obj.LoctionValues.SubArea = null
      this.Busyness_data_Obj.LoctionValues.SubArea = null
      this.tranBusyness_data_Obj.LoctionValues.SubArea = null
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
    console.log("sel cat", this.selectedCategory);
    this.NeLevel_Data = Array<LevelAttributes>();
    this.NeLevel_Data_Des = new Array<TransmissionLevel>();
    this.ChartService.get_NE_Levels(this.selectedCategory).subscribe(res => {
      this.NeLevel = res;
      this.NeLevel_Data = this.NeLevel.data;
    });
    this.Count_Type_Obj.ClassificationName = this.selectedCategory;
    this.DC_Type_Obj.ClassificationName = this.selectedCategory;
    this.OP_Type_Obj.ClassificationName = this.selectedCategory;
    this.MW_Type_Obj.ClassificationName = this.selectedCategory;
    this.FW_Type_Obj.ClassificationName = this.selectedCategory;
    this.Host_Version_Obj.ClassificationName = this.selectedCategory;
    this.Busyness_data_Obj.ClassificationName = this.selectedCategory

    this.tranBusyness_data_Obj.ClassificationName = this.selectedCategory;

    this.TranBusyness_confg_Obj = new Array<TransmissionLevel>();

    this.transChartservice.get_Busyness_Input(this.selectedCategory).subscribe(res => {
      this.TranBusyness_confg_Obj = res.data;
    });

    setTimeout(() => {
      let i = 0;
      this.TranBusyness_confg_Obj.forEach(nld => {
          if (i < this.TranBusyness_confg_Obj.length - 1) {
            
              this.NeLevel_Data_Des.push(this.TranBusyness_confg_Obj[i]);
            
          }
          i++;
        });
      this.loading = false;
    }, 3000);

  }

  SelectedLevelSelect($event, displayname: string, tablename: string, columnname: string, columntype: string) {
    
    this.loading = true;
    console.log("sel sub cat", this.SelectedLevel);
    this.showDes = false;
    //this.NeLevel_Data_Des = null; 
    
    
    this.Count_Type_Obj.Level.TableName = tablename;
    this.Count_Type_Obj.Level.ColumnName = columnname;
    this.Count_Type_Obj.Level.ColumnType = columntype;

    //#region temp
    this.DC_Type_Obj.Level.TableName = tablename;
    this.DC_Type_Obj.Level.ColumnName = columnname;
    this.DC_Type_Obj.Level.ColumnType = columntype;

    this.OP_Type_Obj.Level.TableName = tablename;
    this.OP_Type_Obj.Level.ColumnName = columnname;
    this.OP_Type_Obj.Level.ColumnType = columntype;

    this.MW_Type_Obj.Level.TableName = tablename;
    this.MW_Type_Obj.Level.ColumnName = columnname;
    this.MW_Type_Obj.Level.ColumnType = columntype;

    this.FW_Type_Obj.Level.TableName = tablename;
    this.FW_Type_Obj.Level.ColumnName = columnname;
    this.FW_Type_Obj.Level.ColumnType = columntype;
    //#endregion

    console.log("level", this.NeLevel_Data);
    console.log("levDes", this.NeLevel_Data_Des)

    

    setTimeout(() => {
      if (this.NeLevel_Data_Des.length != 0) {
        this.showDes = true;
      }

      this.loading = false;
    }, 1000);

  }

  SelectedLevelSelectDes($event, displayname: string, ) {
    let i = 0;
    console.log("sel display name", displayname);
    this.TranBusyness_confg_Obj.forEach(element => {
      if (element.TypeLevel.DISPLAYNAME == displayname) {
        this.tranBusyness_data_Obj.LevelSource.TableName = element.level.TABLENAME;
        this.tranBusyness_data_Obj.LevelSource.ColumnName = element.level.COLUMNNAME;
        this.tranBusyness_data_Obj.LevelSource.ColumnType = element.level.COLUMNTYPE;
        this.tranBusyness_data_Obj.LevelSourceType.TableName = element.TypeLevel.TABLENAME;
        element.TypeLevel.COLUMNS.forEach(ColElement => {
          if (ColElement.COLUMNPRIORITY == 1) {
            this.tranBusyness_data_Obj.LevelSourceType.ColumnName = ColElement.COLUMNNAME;
          }
          else if (ColElement.COLUMNPRIORITY == 2) {
            this.tranBusyness_data_Obj.LevelSourceType.SecondaryColumnName = ColElement.COLUMNNAME;
            this.tranBusyness_data_Obj.LevelSourceType.ColumnType = ColElement.COLUMNTYPE;
          }
          i=-2;
        });
      }
      if (i == -1){
        this.tranBusyness_data_Obj.LevelDistination.TableName = element.level.TABLENAME;
        this.tranBusyness_data_Obj.LevelDistination.ColumnName = element.level.COLUMNNAME;
        this.tranBusyness_data_Obj.LevelDistination.ColumnType = element.level.COLUMNTYPE;
        console.log("sel des subcat", element.DISPLAYNAME,i);
      }
      i++;
    });
    this.showGen = true;

    // this.NeLevel_Data.forEach(nld => {
    //   if (nld.DISPLAYNAME == displayname && i < this.NeLevel_Data.length - 1) {
    //     for (let j = i + 1; j < this.NeLevel_Data.length; j++) {
    //       this.NeLevel_Data_Des.push(this.NeLevel_Data[j]);
    //     }
    //   }
    //   i++;
    // });

    // this.TranBusyness_confg_Obj.forEach(element => {
    //   if (element.TypeLevel.DISPLAYNAME == displayname) {
    //     this.tranBusyness_data_Obj.LevelDistination.TableName = element.level.TABLENAME;
    //     this.tranBusyness_data_Obj.LevelDistination.ColumnName = element.level.COLUMNNAME;
    //     this.tranBusyness_data_Obj.LevelDistination.ColumnType = element.level.COLUMNTYPE;
    //   }
    // });
    console.log("final confg obj", this.TranBusyness_confg_Obj);
    console.log("final data obj", JSON.stringify(this.tranBusyness_data_Obj));
  }

  GenerateChart() {
    console.log("Busyness Object:", this.Busyness_data_Obj)
    this.loading = true;
    this.show = false;
    this.show2 = false;
    this.ShowBusynessReport = false;
    this.ShowDefReport = false;
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

    this.TranbusynessResult = new BusynessReturnedData();
    this.TranbusynessResult.data = new BusynessDataObj();
    this.TranbusynessResult.data.LocationNames = new Array<string>();
    this.TranbusynessResult.data.Capacity = new Array<number>();
    this.TranbusynessResult.data.Free = new Array<number>();
    this.TranbusynessResult.data.Busy = new Array<number>();
    this.TranbusynessChartLabel = new Array<string>();

    this.NeTypes.data.LocationNames = new Array<string>();
    //#endregion


    this.ChartService.GetCountTypes(this.Count_Type_Obj).subscribe(res => {
      this.NeTypes.success = res.success;
      this.NeTypes.errorMessage = res.errorMessage;
      this.NeTypes.data = res.data

      console.log("Ne Types finally", this.NeTypes.data)

    });
  

    this.transChartservice.GetBusyness(this.tranBusyness_data_Obj).subscribe(res => {
      this.TranbusynessResult.data = res.data;
      this.TranbusynessResult.data.LocationNames = res.data.LocationNames;
      this.TranbusynessResult.errorMessage = res.errorMessage;
      this.TranbusynessResult.success = res.success;
      console.log("busyness results", this.TranbusynessResult.data)
    })


    this.charbarlist = new Array<string>();
    this.barChartData_list = [];
    this.barChartData_list = new Array<{ data: number[], label: string }>();
    this.ChartData = new ChartsData();
    this.charbarlist_temp = new Array<string>();
    this.ShowRepBusyness = new Array<ReportList>();

    this.hostverbarlist_temp = new Array<string>();
    setTimeout(() => {
      if (this.NeTypes.data != null) {
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
      else {
        this.barChartData_list = null;
      }






      this.charbarlist = this.charbarlist_temp;
      this.charbarlistCell = this.charbarlist_tempCell;
      this.hostbarlist = this.hostverbarlist_temp;
      this.busynessChartLabel = new Array<string>();

      if (this.TranbusynessResult.data != null) {
        for (let i = 0; i < this.TranbusynessResult.data.LocationNames.length; i++) {
          this.busynessChartLabel.push(this.TranbusynessResult.data.LocationNames[i]);
          this.ShowRepBusyness.push({
            NeSiteAtt: this.TranbusynessResult.data.LocationNames[i], CapcityLi: this.TranbusynessResult.data.Capacity[i],
            FreeLi: this.TranbusynessResult.data.Free[i], BusyLi: this.TranbusynessResult.data.Busy[i]
          });
        }
        this.busynessChartData = [
          { data: this.TranbusynessResult.data.Capacity, label: 'Capacity' },
          { data: this.TranbusynessResult.data.Busy, label: 'Busy' },
          { data: this.TranbusynessResult.data.Free, label: 'Free' }
        ]

      }
      else {
        this.busynessChartData = null;
      }
      this.show2 = true;
      this.loading = false;
      
      this.ShowBusynessReport = true;
      this.ShowTypeCountReport = true;
      this.ShowCharts2()
    }, 10000);


  }

}