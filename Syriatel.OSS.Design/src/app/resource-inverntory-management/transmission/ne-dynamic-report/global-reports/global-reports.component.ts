import { Component, OnInit, ViewChild } from '@angular/core';
import { AttributeTreeService } from '../../../services/attributeTree.service';
import { IActionMapping, TreeComponent } from 'angular-tree-component';

import { toJS } from 'mobx';


import { NeTypesService } from '../../../services/NeTypes.service';
import { trigger, state, style, transition, animate, AUTO_STYLE} from '@angular/animations';
import { SelectItem } from '../category-report/SelectItem';

import { ActivatedRoute, Router } from '@angular/router';
import { SiteLocationService } from '../../../services/getSiteInfo.service';
import { CategoryDropDownService } from '../../../services/getCategoryDropDown.service';

import { IDTypeDictionary } from 'angular-tree-component/dist/defs/api';

import { GenerateCategoryReportService } from '../../../services/generateFirstReport.service';

import { TreeNode } from 'primeng/primeng';
import { ReportLevelFilterModel } from '../../../extraClasses/ReportLevelFilter/ReportLevelModel';
import { NodeOfTree } from '../../../extraClasses/TreeNode';
import { ReportLevelReturnType } from '../../../extraClasses/ReportLevelFilter/ReportLevelReturnType';
import { ColumnItem } from '../../../extraClasses/ReportShow/ColumnItem';
import { LocationSiteData } from '../../../extraClasses/LocationSiteData';
import { Area } from '../../../extraClasses/LocationSiteHierarchy/Area';
import { Zone } from '../../../extraClasses/LocationSiteHierarchy/Zone';
import { SubArea } from '../../../extraClasses/LocationSiteHierarchy/SubArea';
import { Site } from '../../../extraClasses/Site';
import { FilterModel } from '../../../extraClasses/LocationSiteHierarchy/FilterModel';
import { SelectedColumn } from '../../../extraClasses/ReportShow/SelectedColumn';
import { AppConfiguration } from '../../../extraClasses/AppConfiguration';
import { Category } from '../../../extraClasses/ReportLevelFilter/Category';
import { SelectedTypeData } from '../../../extraClasses/ReportFilter/SelectedTypeData';
import { NEType } from '../../../extraClasses/ReportFilter/NEType';
import { SiteInfo } from '../../../extraClasses/ReportFilter/SiteInfo';
import { SelectedSubCategoryData } from '../../../extraClasses/ReportFilter/SelectedSubCategoryData';
import { SelectedAttributeData } from '../../../extraClasses/ReportFilter/SelectedAttributeData';
import { SelectedRanTypeData } from '../../../extraClasses/ReportFilter/SelectedRanTypeData';
import { AdhocReportData } from '../../../extraClasses/DynamicReport/AdhocReportData';
import { ReportCategoryRetunType } from '../../../extraClasses/ReportFilter/ReportCategoryRetunType';
import { WhereClause } from '../../../extraClasses/DynamicReport/WhereClause';
import { SelectClause } from '../../../extraClasses/DynamicReport/SelectClause';


declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-global-reports',
  templateUrl: './global-reports.component.html',
  styleUrls: ['./global-reports.component.css'],
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
export class GlobalReportsComponent implements OnInit {

  private reportFilterModel:ReportLevelFilterModel = null;

  private AdhocReportData : AdhocReportData = null;
  private selectClause : SelectClause = null;
  private whereClause: WhereClause = null;

  private Attributenodes: TreeNode[] = null;
  private OrginalAttributenodes:TreeNode[]=null;
  private NodesOfLevels: NodeOfTree []= null;
  private Typenodes: TreeNode[] = null;
  private Categorynodes: TreeNode[] = null;

  //private reportCategoryRetunType:ReportLevelReturnType = null
  private reportCategoryRetunType:ReportCategoryRetunType = null

  cols: ColumnItem[];
  columnOptions: SelectItem[];
  NES = [];
  nes = this.NES;
  datasource = this.NES;

  public loading = false;

  displaytable: boolean;
  
  DisableArea:boolean;
  DisableZone:boolean;
  DisableSubArea:boolean;
  DisableSiteCode:boolean;
  DisableSiteName:boolean;


  Region: SelectItem[];
  Area: SelectItem[];
  AllArea: SelectItem[];
  selectedRegion: string;
  selectedArea: string;
  selectedZone: string;
  Zones: SelectItem[];
  AllZone: SelectItem[];
  selectedSubArea: string;
  SubAreas: SelectItem[];
  AllSubAreas: SelectItem[];
  selectedSiteCode: string;
  SitesCodes: SelectItem[];
  AllSiteCodes: SelectItem[];
  selectedSiteName: string;
  SitesNames: SelectItem[];
  AllSiteNames: SelectItem[];
  SiteInfo : LocationSiteData;

  selectedCat: string;
  selectedColumns:SelectedColumn [];

  RegionTempSiteCode:SelectItem[];
  RegionTempSiteName:SelectItem[];
  AreaTempSiteCode:SelectItem[];
  AreaTempSiteName:SelectItem[];
  ZoneTempSiteCode:SelectItem[];
  ZoneTempSiteName:SelectItem[];

  //filter location site datamember
  tempRegions : Area[];
  tempAreas : Zone[];
  tempZones : SubArea[];
  tempSubAreas : Site[];
  selectedCategories:TreeNode[];
  selectedTypes:TreeNode[];
  display_dialog:boolean=false;
  empty_dialog:boolean=false;

  TreeType:string;
  selectedAttribute:SelectedColumn[];
  selectedAllAttributeTree:SelectedColumn []=[];
  display_Parent_Selection:boolean=false;
  
  
  actionMapping: IActionMapping = {
    mouse: {
      click: (tree, node) => this.check(node)
    }
  };

  options = {
    allowDrag: true,
    displayField: 'name',
    actionMapping: this.actionMapping,
    useCheckbox: true
    
  }

  optionsLevel = {
    allowDrag: true,
    displayField: 'value',
    actionMapping: this.actionMapping,
    useCheckbox: true
    
  }

  model:FilterModel;
  SelectedLevel: string;
  selectedSearchCriteria:SelectedColumn [];
  selectedAllSearchCriteriaTree:SelectedColumn []=[];
  cols1:any[];
  display_Dialog:boolean=false;
  childrenSelectedItem:SelectedColumn[]=[];
  SearchInput:string;
  private OrginalTypenodes:TreeNode[]=null;
  ModelName:string;
  wrongFilteration:boolean=false;
  dialogBody:string;
  dialogHeader:string;
  oneCategory:boolean=false;
  ClassificationName: string;

  constructor(private route: ActivatedRoute, private _router: Router,private siteLocation:SiteLocationService, private attributeTree:AttributeTreeService
    , private neTypes:NeTypesService, private categoryAll : CategoryDropDownService, private categoryReportService:GenerateCategoryReportService) {

    
   }

  ngOnInit() {
    this.loading = true;
    

    if(this._router.url.includes(AppConfiguration.ModelNameTransmission)){
      this.ClassificationName = AppConfiguration.TransmissionClassification;
      this.ModelName=AppConfiguration.ModelNameTransmission;
    }
    else if(this._router.url.includes(AppConfiguration.ModelNameRAN)){
      this.ClassificationName = AppConfiguration.RadioClassification;
      this.ModelName=AppConfiguration.ModelNameRAN;
    }

    this.displaytable = false;

    this.cols1 = [
      { field: 'columnName', header: 'Column' },
      { field: 'columnType', header: 'Type' }
    ];
    this.selectedCat='Transmission';
    this.attributeTree.GetRANAttributeTree(this.ClassificationName).subscribe(values => { 
      this.Attributenodes = <TreeNode[]>[values.data];
      this.OrginalAttributenodes= [];
      this.OrginalAttributenodes.push(Object.assign({},  this.Attributenodes[0]));
    this.siteLocation.GetSiteLocationData(this.ModelName).subscribe(values => {
      
        this.SiteInfo =  (<LocationSiteData>values.data);
        this.Region= this.siteLocation.GetSiteLocationDropdownParsing(this.SiteInfo.Region);
        this.Area=this.siteLocation.GetSiteLocationDropdownParsing(this.SiteInfo.Area);
        this.AllArea=this.Area;
        this.Zones=this.siteLocation.GetSiteLocationDropdownParsing(this.SiteInfo.Zone);
        this.AllZone=this.Zones;
        this.SubAreas=this.siteLocation.GetSiteLocationDropdownParsing(this.SiteInfo.SubArea);
        this.AllSubAreas=this.SubAreas;
        this.SitesCodes=this.siteLocation.GetSitecode(this.SiteInfo.Site);
        this.AllSiteCodes=this.SitesCodes;
        this.SitesNames=this.siteLocation.GetSiteName(this.SiteInfo.Site);
        this.AllSiteNames=this.SitesNames;
        
        this.neTypes.GetRANNeTypes(this.ClassificationName).subscribe(values => {
          
              this.Typenodes = <TreeNode[]>[values.data];
              //copy the orginal Type tree
              this.OrginalTypenodes = [];
              this.OrginalTypenodes.push(Object.assign({},  this.Typenodes[0]));

              this.categoryAll.getAllCategories(this.ClassificationName).subscribe(values => {

                    this.Categorynodes = <TreeNode[]>[values.data];
                    this.expandAll(this.Categorynodes);
                    this.loading = false;
              });
        });
  });
});
  
  }

  navigateToDetails(nes) {
    this._router.navigate(['../../Details'], { relativeTo: this.route });
  }

  isCollapsedSideBar = 'yes-block';
  toggleOpenedSidebar(event) {
    this.isCollapsedSideBar = this.isCollapsedSideBar === 'yes-block' ? 'no-block' : 'yes-block';
    
    let elm = event.srcElement.classList;
    if(elm.contains("fa-angle-double-up")){
      $("#narrow").removeClass('fa-angle-double-up');
      $("#narrow").addClass('fa-angle-double-down');
    }
    else{
      $("#narrow").removeClass('fa-angle-double-down');
      $("#narrow").addClass('fa-angle-double-up');
    }

  }

  isCollapsedSideBar2 = 'yes-block';
  toggleOpenedSidebar2(event) {
    this.isCollapsedSideBar2 = this.isCollapsedSideBar2 === 'yes-block' ? 'no-block' : 'yes-block';
    
    let elm = event.srcElement.classList;
    if(elm.contains("fa-angle-double-up")){
      $("#narrow2").removeClass('fa-angle-double-up');
      $("#narrow2").addClass('fa-angle-double-down');
    }
    else{
      $("#narrow2").removeClass('fa-angle-double-down');
      $("#narrow2").addClass('fa-angle-double-up');
    }

  }
  isCollapsedSideBar3 = 'yes-block';
  toggleOpenedSidebar3(event) {
    this.isCollapsedSideBar3 = this.isCollapsedSideBar3 === 'yes-block' ? 'no-block' : 'yes-block';
    
    let elm = event.srcElement.classList;
    if(elm.contains("fa-angle-double-up")){
      $("#narrow3").removeClass('fa-angle-double-up');
      $("#narrow3").addClass('fa-angle-double-down');
    }
    else{
      $("#narrow3").removeClass('fa-angle-double-down');
      $("#narrow3").addClass('fa-angle-double-up');
    }
  }
  isCollapsedSideBar4 = 'yes-block';
  toggleOpenedSidebar4(event) {
    this.isCollapsedSideBar4 = this.isCollapsedSideBar4 === 'yes-block' ? 'no-block' : 'yes-block';
    
    let elm = event.srcElement.classList;
    if(elm.contains("fa-angle-double-up")){
      $("#narrow4").removeClass('fa-angle-double-up');
      $("#narrow4").addClass('fa-angle-double-down');
    }
    else{
      $("#narrow4").removeClass('fa-angle-double-down');
      $("#narrow4").addClass('fa-angle-double-up');
    }
  }

  // RegionSelect(event){
  //   if(this.selectedRegion!="All"){
  //     this.DisableArea=false;
  //     this.DisableSubArea=true;
  //     this.DisableZone=true;
  //     this.selectedArea='';
  //     this.selectedZone='';
  //     this.selectedSubArea='';
  //     this.selectedSiteCode='';
  //     this.selectedSiteName='';
  //     this.tempRegions=this.siteLocation.GetTempRegion(this.selectedRegion,this.SiteInfo.DataHierarchy);
  //     this.model=this.siteLocation.GetFilteredArea(this.tempRegions);
  //     this.Area=this.model.area_list;
  //     this.SitesCodes=this.model.sitecode;
  //     this.SitesNames=this.model.sitename;
  //     //tempRegioncode and name
  //     this.RegionTempSiteCode=this.SitesCodes;
  //     this.RegionTempSiteName=this.SitesNames;
  //   }
  //   else{ //All
  //     this.DisableArea=false;
  //     this.DisableZone=true;
  //     this.DisableSubArea=true;
  //     this.selectedArea='';
  //     this.selectedZone='';
  //     this.selectedSubArea='';
  //     this.selectedSiteCode='';
  //     this.selectedSiteName='';
  //     this.Area=this.AllArea;
  //     this.Zones=this.AllZone;
  //     this.SubAreas=this.AllSubAreas;
  //     this.SitesCodes=this.AllSiteCodes;
  //     this.SitesNames=this.AllSiteNames;
  //     this.tempRegions=this.siteLocation.GetTempRegion(this.selectedRegion,this.SiteInfo.DataHierarchy);
      
  //   }
  //    //1) deleting selected region/regions
  // if(this.selectedSearchCriteria != null){
  //   let temp:SelectedColumn[]=this.selectedSearchCriteria;
  //   this.selectedSearchCriteria = [];
  //   for(let item of temp){
  //     if((item.columnType != AppConfiguration.SelectedRegion)&&(item.columnType!=AppConfiguration.SelectedArea)&&(item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedZone)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){
  //       this.selectedSearchCriteria.push(item);
  //     }
  //   }
  //   let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
  //   this.selectedAllSearchCriteriaTree=[];
  //   for (let item2 of temp2){
  //     if((item2.columnType != AppConfiguration.SelectedRegion)&&(item2.columnType!=AppConfiguration.SelectedArea)&&(item2.columnType != AppConfiguration.SelectedSubArea)&&(item2.columnType != AppConfiguration.SelectedZone)&&(item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
  //       this.selectedAllSearchCriteriaTree.push(item2);
  //     }
  //   }
  // }
  // //2)add new selected region/regions
  // if(this.selectedRegion!=AppConfiguration.AllLocation){
  //   let column:SelectedColumn=new SelectedColumn(this.selectedRegion,AppConfiguration.SelectedRegion,null);
  //     if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
  //       this.selectedSearchCriteria = [];
  //       this.selectedAllSearchCriteriaTree.push(column);
  //       for(let item of this.selectedAllSearchCriteriaTree)
  //       {
  //         this.selectedSearchCriteria.push(item);
  //       }
  //     }
  // }
  
  // }

  RegionSelect(event){
    if(this.selectedRegion!=AppConfiguration.AllLocation){
      this.DisableArea=false;
      this.DisableSubArea=true;
      this.DisableZone=true;
      this.selectedArea='';
      this.selectedZone='';
      this.selectedSubArea='';
      this.selectedSiteCode='';
      this.selectedSiteName='';
      this.tempRegions=this.siteLocation.GetTempRegion(this.selectedRegion,this.SiteInfo.DataHierarchy);
      this.model=this.siteLocation.GetFilteredArea(this.tempRegions);
      this.Area=this.model.area_list;
      this.SitesCodes=this.model.sitecode;
      this.SitesNames=this.model.sitename;
      //tempRegioncode and name
      this.RegionTempSiteCode=this.SitesCodes;
      this.RegionTempSiteName=this.SitesNames;
  
    }
    else{ //All
      this.DisableArea=false;
      this.DisableZone=true;
      this.DisableSubArea=true;
      this.selectedArea='';
      this.selectedZone='';
      this.selectedSubArea='';
      this.selectedSiteCode='';
      this.selectedSiteName='';
      this.Area=this.AllArea;
      this.Zones=this.AllZone;
      this.SubAreas=this.AllSubAreas;
      this.SitesCodes=this.AllSiteCodes;
      this.SitesNames=this.AllSiteNames;
      this.tempRegions=this.siteLocation.GetTempRegion(this.selectedRegion,this.SiteInfo.DataHierarchy);
      
    }
    //for selection grid
    //1) deleting selected region/regions
    if(this.selectedColumns != null){
      let temp:SelectedColumn[]=this.selectedColumns;
      this.selectedColumns = [];
      for(let item of temp){
        if((item.columnType != AppConfiguration.SelectedRegion)&&(item.columnType!=AppConfiguration.SelectedArea)&&(item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedZone)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){
          this.selectedColumns.push(item);
        }
      }
      let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
      this.selectedAllSearchCriteriaTree=[];
      for (let item2 of temp2){
        if((item2.columnType != AppConfiguration.SelectedRegion)&&(item2.columnType!=AppConfiguration.SelectedArea)&&(item2.columnType != AppConfiguration.SelectedSubArea)&&(item2.columnType != AppConfiguration.SelectedZone)&&(item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
          this.selectedAllSearchCriteriaTree.push(item2);
        }
      }
    }
    //2)add new selected region/regions
    if(this.selectedRegion!=AppConfiguration.AllLocation){
      let column:SelectedColumn=new SelectedColumn(this.selectedRegion,AppConfiguration.SelectedRegion,null);
        if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
          this.selectedColumns = [];
          this.selectedAllSearchCriteriaTree.push(column);
          for(let item of this.selectedAllSearchCriteriaTree)
          {
            this.selectedColumns.push(item);
          }
        }
    }
  }
  
//   AreaSelect(event){
    
//     if(this.selectedRegion!=null){ 
//       if(this.selectedRegion=="All"){ //Region ALL
//         if(this.selectedArea=="All") { //Region & Area == All
//           this.DisableZone=true;
//           this.DisableSubArea=true;
//           this.selectedZone='';
//           this.selectedSubArea='';
//           this.selectedSiteCode='';
//           this.selectedSiteName='';
  
//           this.SitesCodes=this.AllSiteCodes;
//           this.SitesNames=this.AllSiteNames;
  
  
//           this.AreaTempSiteCode=this.SitesCodes;
//           this.AreaTempSiteName=this.SitesNames;
//         }
//         else{ //Region = All & Area != All
//           this.DisableZone=false;
//           this.DisableSubArea=true;
//           this.selectedZone='';
//           this.selectedSubArea='';
//           this.selectedSiteCode='';
//           this.selectedSiteName='';
  
//           this.tempAreas=this.siteLocation.GetTempArea(this.selectedArea,this.tempRegions);
//           this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
//           this.Zones=this.model.zone_list;
//           this.SitesCodes=this.model.sitecode;
//           this.SitesNames=this.model.sitename;
  
//           this.AreaTempSiteCode=this.SitesCodes;
//           this.AreaTempSiteName=this.SitesNames;
//         }
//       }
//       else{ //Region != ALL
//         if(this.selectedArea=="All") { //Region != All & area == All
//           this.DisableZone=true;
//           this.DisableSubArea=true;
//           this.selectedZone='';
//           this.selectedSubArea='';
//           this.selectedSiteCode='';
//           this.selectedSiteName='';
  
//           this.SitesCodes=this.RegionTempSiteCode;//filtered by Region
//           this.SitesNames=this.RegionTempSiteName;//filtered by Region
  
//           this.AreaTempSiteCode=this.SitesCodes;
//           this.AreaTempSiteName=this.SitesNames;
//         }
//         else{ //Region & Area != All
//           this.DisableZone=false;
//           this.DisableSubArea=true;
//           this.selectedZone='';
//           this.selectedSubArea='';
//           this.selectedSiteCode='';
//           this.selectedSiteName='';
  
//           this.tempAreas=this.siteLocation.GetTempArea(this.selectedArea,this.tempRegions);
//           this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
//           this.Zones=this.model.zone_list;
//           this.SitesCodes=this.model.sitecode;
//           this.SitesNames=this.model.sitename;
  
//           this.AreaTempSiteCode=this.SitesCodes;
//           this.AreaTempSiteName=this.SitesNames;
//         }
//       }
//     }
//     else {//reagion == null
//       if(this.selectedArea!='All'){
//         this.DisableZone=false;
//         this.DisableSubArea=true;
//         this.selectedZone='';
//         this.selectedSubArea='';
//         this.selectedSiteCode='';
//         this.selectedSiteName='';
//         this.tempRegions=this.siteLocation.GetTempRegion(this.selectedRegion,this.SiteInfo.DataHierarchy);
//         this.tempAreas=this.siteLocation.GetTempArea(this.selectedArea,this.tempRegions);
//         this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
//         this.Zones=this.model.zone_list;
//         this.SitesCodes=this.model.sitecode;
//         this.SitesNames=this.model.sitename;
  
//         this.AreaTempSiteCode=this.SitesCodes;
//         this.AreaTempSiteName=this.SitesNames;
//       }
//       else{//area is All
//         this.DisableZone=true;
//         this.DisableSubArea=true;
//         this.selectedZone='';
//         this.selectedSubArea='';
//         this.selectedSiteCode='';
//         this.selectedSiteName='';
//         this.Area=this.AllArea;
//         this.Zones=this.AllZone;
//         this.SubAreas=this.AllSubAreas;
//         this.SitesCodes=this.AllSiteCodes;
//         this.SitesNames=this.AllSiteNames;
  
//         this.AreaTempSiteCode=this.SitesCodes;
//         this.AreaTempSiteName=this.SitesNames;
//       }
//     }
// //1) deleting selected Area/Areas
// if(this.selectedSearchCriteria != null){
//   let temp:SelectedColumn[]=this.selectedSearchCriteria;
//   this.selectedSearchCriteria = [];
//   for(let item of temp){
//     if((item.columnType != AppConfiguration.SelectedZone)&&(item.columnType != AppConfiguration.SelectedArea)&&(item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){
//       this.selectedSearchCriteria.push(item);
//     }
//   }
//   let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
//   this.selectedAllSearchCriteriaTree=[];
//   for (let item2 of temp2){
//     if((item2.columnType != AppConfiguration.SelectedZone)&&(item2.columnType != AppConfiguration.SelectedArea)&&(item2.columnType != AppConfiguration.SelectedSubArea)&&(item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
//       this.selectedAllSearchCriteriaTree.push(item2);
//     }
//   }
// }
// //2)Add Area/Areas
// if(this.selectedArea!=AppConfiguration.AllLocation){
//   let column:SelectedColumn=new SelectedColumn(this.selectedArea,AppConfiguration.SelectedArea,null);
//     if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
//       this.selectedSearchCriteria = [];
//       this.selectedAllSearchCriteriaTree.push(column);
//       for(let item of this.selectedAllSearchCriteriaTree)
//       {
//         this.selectedSearchCriteria.push(item);
//       }
//     }
// }


//   }

AreaSelect(event){
 // debugger;
  if(this.selectedRegion!=null){ 
    if(this.selectedRegion==AppConfiguration.AllLocation){ //Region ALL
      if(this.selectedArea==AppConfiguration.AllLocation) { //Region & Area == All
        this.DisableZone=true;
        this.DisableSubArea=true;
        this.selectedZone='';
        this.selectedSubArea='';
        this.selectedSiteCode='';
        this.selectedSiteName='';

        this.SitesCodes=this.AllSiteCodes;
        this.SitesNames=this.AllSiteNames;


        this.AreaTempSiteCode=this.SitesCodes;
        this.AreaTempSiteName=this.SitesNames;
      }
      else{ //Region = All & Area != All
        this.DisableZone=false;
        this.DisableSubArea=true;
        this.selectedZone='';
        this.selectedSubArea='';
        this.selectedSiteCode='';
        this.selectedSiteName='';

        this.tempAreas=this.siteLocation.GetTempArea(this.selectedArea,this.tempRegions);
        this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
        this.Zones=this.model.zone_list;
        this.SitesCodes=this.model.sitecode;
        this.SitesNames=this.model.sitename;

        this.AreaTempSiteCode=this.SitesCodes;
        this.AreaTempSiteName=this.SitesNames;
      }
    }
    else{ //Region != ALL
      if(this.selectedArea==AppConfiguration.AllLocation) { //Region != All & area == All
        this.DisableZone=true;
        this.DisableSubArea=true;
        this.selectedZone='';
        this.selectedSubArea='';
        this.selectedSiteCode='';
        this.selectedSiteName='';

        this.SitesCodes=this.RegionTempSiteCode;//filtered by Region
        this.SitesNames=this.RegionTempSiteName;//filtered by Region

        this.AreaTempSiteCode=this.SitesCodes;
        this.AreaTempSiteName=this.SitesNames;
      }
      else{ //Region & Area != All
        this.DisableZone=false;
        this.DisableSubArea=true;
        this.selectedZone='';
        this.selectedSubArea='';
        this.selectedSiteCode='';
        this.selectedSiteName='';

        this.tempAreas=this.siteLocation.GetTempArea(this.selectedArea,this.tempRegions);
        this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
        this.Zones=this.model.zone_list;
        this.SitesCodes=this.model.sitecode;
        this.SitesNames=this.model.sitename;

        this.AreaTempSiteCode=this.SitesCodes;
        this.AreaTempSiteName=this.SitesNames;
      }
    }

  }
  else {//reagion == null
    if(this.selectedArea!=AppConfiguration.AllLocation){
      this.DisableZone=false;
      this.DisableSubArea=true;
      this.selectedZone='';
      this.selectedSubArea='';
      this.selectedSiteCode='';
      this.selectedSiteName='';
      this.tempRegions=this.siteLocation.GetTempRegion(this.selectedRegion,this.SiteInfo.DataHierarchy);
      
      this.tempAreas=this.siteLocation.GetTempArea(this.selectedArea,this.tempRegions);
      this.model = this.siteLocation.GetFilteredZone(this.tempAreas);
      this.Zones=this.model.zone_list;
      this.SitesCodes=this.model.sitecode;
      this.SitesNames=this.model.sitename;

      this.AreaTempSiteCode=this.SitesCodes;
      this.AreaTempSiteName=this.SitesNames;

    }
    else{//area is All
      this.DisableZone=true;
      this.DisableSubArea=true;
      this.selectedZone='';
      this.selectedSubArea='';
      this.selectedSiteCode='';
      this.selectedSiteName='';
      this.Area=this.AllArea;
      this.Zones=this.AllZone;
      this.SubAreas=this.AllSubAreas;
      this.SitesCodes=this.AllSiteCodes;
      this.SitesNames=this.AllSiteNames;

      this.AreaTempSiteCode=this.SitesCodes;
      this.AreaTempSiteName=this.SitesNames;
    }
  }

//for selection grid
  //1) deleting selected Area/Areas
  if(this.selectedColumns != null){
    let temp:SelectedColumn[]=this.selectedColumns;
    this.selectedColumns = [];
    for(let item of temp){
      if((item.columnType != AppConfiguration.SelectedZone)&&(item.columnType != AppConfiguration.SelectedArea)&&(item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){
        this.selectedColumns.push(item);
      }
    }
    let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
    this.selectedAllSearchCriteriaTree=[];
    for (let item2 of temp2){
      if((item2.columnType != AppConfiguration.SelectedZone)&&(item2.columnType != AppConfiguration.SelectedArea)&&(item2.columnType != AppConfiguration.SelectedSubArea)&&(item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
        this.selectedAllSearchCriteriaTree.push(item2);
      }
    }
  }
  //2)Add Area/Areas
  if(this.selectedArea!=AppConfiguration.AllLocation){
    let column:SelectedColumn=new SelectedColumn(this.selectedArea,AppConfiguration.SelectedArea,null);
      if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
        this.selectedColumns = [];
        this.selectedAllSearchCriteriaTree.push(column);
        for(let item of this.selectedAllSearchCriteriaTree)
        {
          this.selectedColumns.push(item);
        }
      }
  }
}

  // ZoneSelect(event){
  //   if(this.selectedZone!="All"){
  //     this.DisableSubArea=false;
  //     this.selectedSubArea='';
  //     this.selectedSiteCode='';
  //     this.selectedSiteName='';
  //     this.tempZones=this.siteLocation.GetTempZone(this.selectedZone,this.tempAreas);
  //     this.model=this.siteLocation.GetFilteredSubArea(this.tempZones);
  //     this.SubAreas=this.model.subarea_list;
  //     this.SitesCodes=this.model.sitecode;
  //     this.SitesNames=this.model.sitename;
  
  //     this.ZoneTempSiteCode=this.SitesCodes;
  //     this.ZoneTempSiteName=this.SitesNames;
  //   }
  //   if(this.selectedZone=="All"){
  //     this.selectedSubArea='';
  //     this.selectedSiteCode='';
  //     this.selectedSiteName='';
  //     this.DisableSubArea=true;
  
  //     this.SitesCodes=this.AreaTempSiteCode;
  //     this.SitesNames=this.AreaTempSiteName;
  
  //     this.ZoneTempSiteCode=this.SitesCodes;
  //     this.ZoneTempSiteName=this.SitesNames;
  //   }
  
  // //1) deleting selected Zone/Zones
  // if(this.selectedSearchCriteria != null){
  //   let temp:SelectedColumn[]=this.selectedSearchCriteria;
  //   this.selectedSearchCriteria = [];
  //   for(let item of temp){
  //     if((item.columnType != AppConfiguration.SelectedZone)&&(item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){//edite
  //       this.selectedSearchCriteria.push(item);
  //     }
  //   }
  //   let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
  //   this.selectedAllSearchCriteriaTree=[];
  //   for (let item2 of temp2){
  //     if((item2.columnType != AppConfiguration.SelectedZone)&&(item2.columnType != AppConfiguration.SelectedSubArea)&&(item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
  //       this.selectedAllSearchCriteriaTree.push(item2);
  //     }
  //   }
  // }
  // //2)Add Zone/Zones
  // if(this.selectedZone!=AppConfiguration.AllLocation){
  //   let column:SelectedColumn=new SelectedColumn(this.selectedZone,AppConfiguration.SelectedZone,null);
  //     if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
  //       this.selectedSearchCriteria = [];
  //       this.selectedAllSearchCriteriaTree.push(column);
  //       for(let item of this.selectedAllSearchCriteriaTree)
  //       {
  //         this.selectedSearchCriteria.push(item);
  //       }
  //     }
  // }
    
  // }

  ZoneSelect(event){
    if(this.selectedZone!=AppConfiguration.AllLocation){
      this.DisableSubArea=false;
      this.selectedSubArea='';
      this.selectedSiteCode='';
      this.selectedSiteName='';
      this.tempZones=this.siteLocation.GetTempZone(this.selectedZone,this.tempAreas);
      this.model=this.siteLocation.GetFilteredSubArea(this.tempZones);
      this.SubAreas=this.model.subarea_list;
      this.SitesCodes=this.model.sitecode;
      this.SitesNames=this.model.sitename;
  
      this.ZoneTempSiteCode=this.SitesCodes;
      this.ZoneTempSiteName=this.SitesNames;
    }
    if(this.selectedZone==AppConfiguration.AllLocation){
      this.selectedSubArea='';
      this.selectedSiteCode='';
      this.selectedSiteName='';
      this.DisableSubArea=true;
  
      this.SitesCodes=this.AreaTempSiteCode;
      this.SitesNames=this.AreaTempSiteName;
  
      this.ZoneTempSiteCode=this.SitesCodes;
      this.ZoneTempSiteName=this.SitesNames;
    }
  
  //for selection grid
    //1) deleting selected Zone/Zones
    if(this.selectedColumns != null){
      let temp:SelectedColumn[]=this.selectedColumns;
      this.selectedColumns = [];
      for(let item of temp){
        if((item.columnType != AppConfiguration.SelectedZone)&&(item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){//edite
          this.selectedColumns.push(item);
        }
      }
      let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
      this.selectedAllSearchCriteriaTree=[];
      for (let item2 of temp2){
        if((item2.columnType != AppConfiguration.SelectedZone)&&(item2.columnType != AppConfiguration.SelectedSubArea)&&(item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
          this.selectedAllSearchCriteriaTree.push(item2);
        }
      }
    }
    //2)Add Zone/Zones
    if(this.selectedZone!=AppConfiguration.AllLocation){
      let column:SelectedColumn=new SelectedColumn(this.selectedZone,AppConfiguration.SelectedZone,null);
        if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
          this.selectedColumns = [];
          this.selectedAllSearchCriteriaTree.push(column);
          for(let item of this.selectedAllSearchCriteriaTree)
          {
            this.selectedColumns.push(item);
          }
        }
    }
    
  }

  // SubAreaSelect(event){
  //   if(this.selectedSubArea !="All"){
  //     this.selectedSiteCode='';
  //     this.selectedSiteName='';
  //     this.tempSubAreas=this.siteLocation.GetTempSubArea(this.selectedSubArea,this.tempZones);
  //     this.model=this.siteLocation.GetFilteredSite(this.tempSubAreas);
  //     this.SitesCodes=this.model.sitecode;
  //     this.SitesNames=this.model.sitename;
  //   }
  //   else { //SubArea is All
  //     this.selectedSiteCode='';
  //     this.selectedSiteName='';
  //     this.SitesCodes=this.ZoneTempSiteCode;
  //     this.SitesNames=this.ZoneTempSiteName;
  //   }
  //  //for selection grid
  // //1) deleting selected SubArea/SubAreas
  // if(this.selectedSearchCriteria != null){
  //   let temp:SelectedColumn[]=this.selectedSearchCriteria;
  //   this.selectedSearchCriteria = [];
  //   for(let item of temp){
  //     if((item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){//edite
  //       this.selectedSearchCriteria.push(item);
  //     }
  //   }
  //   let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
  //   this.selectedAllSearchCriteriaTree=[];
  //   for (let item2 of temp2){
  //     if((item2.columnType != AppConfiguration.SelectedSubArea)&&(item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
  //       this.selectedAllSearchCriteriaTree.push(item2);
  //     }
  //   }
  // }
  // //2)Add SubArea/SubAreas
  // if(this.selectedSubArea!=AppConfiguration.AllLocation){
  //   let column:SelectedColumn=new SelectedColumn(this.selectedSubArea,AppConfiguration.SelectedSubArea,null);
  //     if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
  //       this.selectedSearchCriteria = [];
  //       this.selectedAllSearchCriteriaTree.push(column);
  //       for(let item of this.selectedAllSearchCriteriaTree)
  //       {
  //         this.selectedSearchCriteria.push(item);
  //       }
  //     }
  // }
  
  // }

  SubAreaSelect(event){
    if(this.selectedSubArea !=AppConfiguration.AllLocation){
      this.selectedSiteCode='';
      this.selectedSiteName='';
      this.tempSubAreas=this.siteLocation.GetTempSubArea(this.selectedSubArea,this.tempZones);
      this.model=this.siteLocation.GetFilteredSite(this.tempSubAreas);
      this.SitesCodes=this.model.sitecode;
      this.SitesNames=this.model.sitename;
    }
    else { //SubArea is All
      this.selectedSiteCode='';
      this.selectedSiteName='';
      this.SitesCodes=this.ZoneTempSiteCode;
      this.SitesNames=this.ZoneTempSiteName;
    }
  
    //for selection grid
    //1) deleting selected SubArea/SubAreas
    if(this.selectedColumns != null){
      let temp:SelectedColumn[]=this.selectedColumns;
      this.selectedColumns = [];
      for(let item of temp){
        if((item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){//edite
          this.selectedColumns.push(item);
        }
      }
      let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
      this.selectedAllSearchCriteriaTree=[];
      for (let item2 of temp2){
        if((item2.columnType != AppConfiguration.SelectedSubArea)&&(item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
          this.selectedAllSearchCriteriaTree.push(item2);
        }
      }
    }
    //2)Add SubArea/SubAreas
    if(this.selectedSubArea!=AppConfiguration.AllLocation){
      let column:SelectedColumn=new SelectedColumn(this.selectedSubArea,AppConfiguration.SelectedSubArea,null);
        if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
          this.selectedColumns = [];
          this.selectedAllSearchCriteriaTree.push(column);
          for(let item of this.selectedAllSearchCriteriaTree)
          {
            this.selectedColumns.push(item);
          }
        }
    }
    
   
  }


  // GenerateReport()
  // {
  //   this.displaytable = true;
  //   //focus on report
  //   this.loading = true;
  //   window.scrollTo(0, 800);
  //   this.reportFilterModel = new ReportLevelFilterModel();
  //   this.reportFilterModel.Levels=this.SelectedLevel.toUpperCase();
  //   this.GetSiteInfoFilters();
  //   //edite accourding to api
  //   this.GetSelectedTypes(this.selectedSearchCriteria);
  //   //edite accourding to api
  //   this.GetSelectedCategories(this.selectedSearchCriteria);

  //   const body = JSON.stringify(this.reportFilterModel);
  //   console.log("Body is ",body);
  //   //validation
  //   if(this.ValidationCriteria_Attribute(this.reportFilterModel)){
  //     this.globalReportService.GetGlobalReport(this.reportFilterModel).subscribe(values => {
  //       if(values.success==-1){
  //         this.wrongFilteration=false;
  //         this.dialogHeader="a problem accured !!";
  //         this.dialogBody="Please contact RIM Team to solve your problem.";
  //         this.display_Dialog=true;
  //       }
  
  //       this.reportCategoryRetunType = <ReportLevelReturnType>values.data;
        
  //       this.cols = this.globalReportService.GetReportLevelColumns(this.reportCategoryRetunType.H_List);
          
  //       let data :any [];
  //       data=this.globalReportService.GetReportValues(this.reportCategoryRetunType.value_array,this.cols);
  //       this.NES=data;
          
      
  //       this.columnOptions = [];
  //       for (let i = 0; i < this.cols.length; i++) {
         
          
  //         this.columnOptions.push({ label: this.cols[i].header, value:<any> this.cols[i]});
  //       }
  
        
  //     });
  //   }
  //   this.loading = false;
  // }

  GenerateReport()
  {
    this.loading = true;

    this.AdhocReportData = new AdhocReportData();
    this.AdhocReportData.SelectClauses = [];
    this.AdhocReportData.WhereClauses = [];

    //Set Classification Name
  //  this.AdhocReportData.ClassificationName = this.ClassificationName;
    this.AdhocReportData.ClassificationName = this.selectedCat;
    console.log ('this.AdhocReportData.ClassificationName',this.selectedCat);

    //Set Type Criteria Data
    this.GetSelectedTypes(this.selectedColumns);
    console.log("Adhoc Report Data After Adding Type : ",this.AdhocReportData);
    //Set SubCategory Criteria Data
    this.GetSelectedSubCategories(this.selectedColumns);
    console.log("Adhoc Report Data After Adding SubCategory : ",this.AdhocReportData);
    //Set Site Filter Data
    this.GetSiteInfoFilters();
    console.log("Adhoc Report Data After Adding Site Filter : ",this.AdhocReportData);
    //Set Attributes
    this.GetSelectedAttributes(this.selectedAttribute);
    console.log("Adhoc Report Data : ",this.AdhocReportData);

//     const body = JSON.stringify(this.reportFilterModel);
    const body = JSON.stringify(this.AdhocReportData);
    console.log("Body is ",body);
    //validate time
  // this.ValidationCriteria_Attribute(this.reportFilterModel);
    
 //  if(this.display_Validation_dialog==false){
   //  this.categoryReportService.GetReport(this.reportFilterModel).subscribe(values => {
      //   this.categoryReportService.GetRanReport(this.AdhocReportData).subscribe(values => {
      //   console.log("values ",values);
      // if (values.data ==null)
      // {
      //     this.loading = false;
      //     this.empty_dialog=true;
      //     this.NES = [];
      // }
      // if(values.success == -1)
      // {
      //   this.loading = false;
      //   this.display_dialog=true; 
      //   this.NES = [];
      // }
      //       else{
      //             this.displaytable = true;
      //             //focus on report
      //              window.scrollTo(0, 800);
      //             this.reportCategoryRetunType = <ReportCategoryRetunType>values.data;
      //             console.log("this.reportCategoryRetunType ",this.reportCategoryRetunType);         
      //             console.log("values.data ",values.data);   
      //             this.cols = this.categoryReportService.GetReportColumns(this.reportCategoryRetunType.H_List);    
      //             let data :any [];
      //             data=this.categoryReportService.GetReportValues(this.reportCategoryRetunType.value_array,this.cols);
      //             this.NES=data;                
      //             this.columnOptions = [];
      //             for (let i = 0; i < this.cols.length; i++) {      
      //               this.columnOptions.push({ label: this.cols[i].header, value:<any> this.cols[i]});
      //             }        
      //             this.loading = false;
      //           }
      // });
      this.categoryReportService.GetRanReport(this.AdhocReportData).subscribe(values => {
        console.log("values ",values);
      if (values.data ==null)
      {
          this.loading = false;
          this.empty_dialog=true;
          this.NES = [];
      }
      if(values.success == -1)
      {
        this.loading = false;
        this.display_dialog=true; 
        this.NES = [];
      }
            else{
                  this.displaytable = true;
                  //focus on report
                   window.scrollTo(0, 800);
                  this.reportCategoryRetunType = <ReportCategoryRetunType>values.data;
                  console.log("this.reportCategoryRetunType ",this.reportCategoryRetunType);         
                  console.log("values.data ",values.data);   
                  this.cols = this.categoryReportService.GetReportColumns(this.reportCategoryRetunType.H_List);    
                  let data :any [];
                  data=this.categoryReportService.GetReportValues(this.reportCategoryRetunType.value_array,this.cols);
                  this.NES=data;                
                  this.columnOptions = [];
                  for (let i = 0; i < this.cols.length; i++) {      
                    this.columnOptions.push({ label: this.cols[i].header, value:<any> this.cols[i]});
                  }        
                  this.loading = false;
                }
      });
    }
  

  GetSelectedCategories(CategoryTree:SelectedColumn[])
  {
    if(CategoryTree!=null){
      this.reportFilterModel.Categories_name = [];
      for(let criteria of CategoryTree){
        if(criteria.columnType==AppConfiguration.SubCategory){
          let CategoryName:string = criteria.Data.CategoryName;
          let CategoryExist:Category;
          CategoryExist = this.reportFilterModel.Categories_name.filter(function (category) { return category.Name === CategoryName.toUpperCase(); })[0];
          if (CategoryExist == null) 
          {
            let newCategory:Category = new Category();
            newCategory.Name = CategoryName.toUpperCase();
            newCategory.subCategory = [];

            newCategory.subCategory.push(criteria.Data.subCategoryName);

            this.reportFilterModel.Categories_name.push(newCategory);

          }
          else
          {
            CategoryExist.subCategory.push(criteria.Data.subCategoryName);
          }
        }
      }
     
    }
    if(this.reportFilterModel.Categories_name.length == 0)
      this.reportFilterModel.Categories_name = null;
  }
    
  // GetSelectedTypes(TypeTree:SelectedColumn[])
  // {
  //     this.reportFilterModel.Types = [];
  //     if (TypeTree != null){
        
  //                 for(let criteria of TypeTree)
  //                 {
  //                   if(criteria.columnType==AppConfiguration.NEType){
  //                     let CriteriaData:SelectedTypeData = <SelectedTypeData>criteria.Data;
                   
                        
  //                       let tableExist:NEType;
  //                       tableExist = this.reportFilterModel.Types.filter(function (type) { return type.TableName === CriteriaData.TabelName; })[0];//TabelName
              
  //                       if (tableExist == null) 
  //                       {
  //                         let newType:NEType = new NEType();
  //                         newType.TableName = CriteriaData.TabelName;
  //                         newType.ColumnName = CriteriaData.ColumnName;
  //                         newType.ColumnType=CriteriaData.ColumnType;
  //                         newType.LevelName=CriteriaData.LevelName;
  //                         newType.LevelId=CriteriaData.LevelId;
  //                         newType.CategoryId=CriteriaData.CategoryId;
  //                         newType.CategoryName=CriteriaData.CategoryName;

  //                         newType.Values = [];
  //                         newType.Values.push(CriteriaData.Label);
          
  //                         this.reportFilterModel.Types.push(newType);
              
  //                       }
  //                       else
  //                       {
  //                         tableExist.Values.push(CriteriaData.Label);
  //                       }
  //                   }
                   
                   
  //                 }
        
  //                 if(this.reportFilterModel.Types.length == 0)
  //                   this.reportFilterModel.Types = null;
  //     }
  //     else{
  //       this.reportFilterModel.Types = null;
  //     }
  // }

  GetSelectedTypes(TypeTree:SelectedColumn[])
  {
      if (TypeTree != null)
      {
        for(let criteria of TypeTree)
        {
          if(criteria.columnType==AppConfiguration.NEType)
          {
            let CriteriaData:SelectedRanTypeData = <SelectedRanTypeData>criteria.Data;
            let tableExist:WhereClause = new WhereClause();
            tableExist = this.AdhocReportData.WhereClauses.filter(function (type) { return (type.TableName === CriteriaData.TabelName); })[0];              
            if (tableExist == null) 
            {
              let NewTable:WhereClause = new WhereClause();
              NewTable.Values = [];

              NewTable.TableName = CriteriaData.TabelName;
              NewTable.ColumnName = CriteriaData.ColumnName;
              NewTable.ColumnType = CriteriaData.ColumnType;
              NewTable.Values.push(CriteriaData.Label);

              this.AdhocReportData.WhereClauses.push(NewTable);
            }
            else
            {
              tableExist.Values.push(CriteriaData.Label);
            }
          }                                        
        }         
      }  
  }

  GetSelectedSubCategories(SubCategoryTree:SelectedColumn[])
  {
    console.log("Sub Category Data are ", SubCategoryTree);
    if (SubCategoryTree != null)
    {
      for(let criteria of SubCategoryTree)
      {
        if(criteria.columnType==AppConfiguration.SubCategory)
        {
          let CriteriaData:SelectedRanTypeData = <SelectedRanTypeData>criteria.Data;
          let tableExist:WhereClause = new WhereClause();
          tableExist = this.AdhocReportData.WhereClauses.filter(function (type) { return (type.TableName === CriteriaData.TabelName); })[0];              
          if (tableExist == null) 
          {
            let NewTable:WhereClause = new WhereClause();
            NewTable.Values = [];

            NewTable.TableName = CriteriaData.TabelName;
            NewTable.ColumnName = CriteriaData.ColumnName;
            NewTable.ColumnType = CriteriaData.ColumnType;
            NewTable.Values.push(CriteriaData.Label);

            this.AdhocReportData.WhereClauses.push(NewTable);
          }
          else
          {
            tableExist.Values.push(CriteriaData.Label);
          }
        }                                        
      }         
    }  
  }

  GetSelectedAttributes(AttributeTree:SelectedColumn[])
  {
      if(AttributeTree != null){
        for (let attr of AttributeTree){
          if(attr.columnType==AppConfiguration.Attribute){
            let AttrData=<SelectedAttributeData>attr.Data;
            this.selectClause = new SelectClause();
            this.selectClause.TableName = AttrData.TableName;
            this.selectClause.ColumnName = AttrData.ColumnName;
            this.selectClause.ColumnType = AttrData.ColumnType;
            this.selectClause.Name = AttrData.DisplayName;
            this.AdhocReportData.SelectClauses.push(this.selectClause);
            console.log("this.selectClause",this.selectClause);
          }
        }
      }
  }
  
  // GetSiteInfoFilters(){
    
  //         this.reportFilterModel.SiteInfo = new SiteInfo();
    
  //           this.reportFilterModel.SiteInfo.REGION = null;
  //           this.reportFilterModel.SiteInfo.AREA = null;
  //           this.reportFilterModel.SiteInfo.ZONE = null;
  //           this.reportFilterModel.SiteInfo.SUBAREA = null;
  //           this.reportFilterModel.SiteInfo.CODE = null;
  //           this.reportFilterModel.SiteInfo.ENGLISHNAME = null;
    
    
            
  //           if(this.selectedRegion=="All" || this.selectedRegion == ""){
  //             this.reportFilterModel.SiteInfo.REGION= null;
  //           }
  //           else{
  //             this.reportFilterModel.SiteInfo.REGION=this.selectedRegion;
  //           }
            
  //           if(this.selectedArea=="All" || this.selectedArea == ""){
  //             this.reportFilterModel.SiteInfo.AREA= null;
  //           }
  //           else{
  //             this.reportFilterModel.SiteInfo.AREA=this.selectedArea;
  //           }
            
  //           if(this.selectedZone =="All" || this.selectedZone == ""){
  //             this.reportFilterModel.SiteInfo.ZONE= null;
  //           }
  //           else{
  //             this.reportFilterModel.SiteInfo.ZONE=this.selectedZone;
  //           }
            
    
  //           if(this.selectedSubArea == "All" || this.selectedSubArea == ""){
  //             this.reportFilterModel.SiteInfo.SUBAREA= null;
  //           }
  //           else{
  //             this.reportFilterModel.SiteInfo.SUBAREA=this.selectedSubArea;
  //           }
            
    
  //           this.reportFilterModel.SiteInfo.CODE=this.selectedSiteCode;
  //           if(this.reportFilterModel.SiteInfo.CODE == "")
  //             this.reportFilterModel.SiteInfo.CODE = null;
            
  //           this.reportFilterModel.SiteInfo.ENGLISHNAME=this.selectedSiteName;
  //           if(this.reportFilterModel.SiteInfo.ENGLISHNAME == "")
  //             this.reportFilterModel.SiteInfo.ENGLISHNAME = null;
  // }

  GetSiteInfoFilters(){
    this.whereClause = new WhereClause();
    this.whereClause.ColumnName = null;
    this.whereClause.ColumnType = null;
    this.whereClause.TableName = null;
    this.whereClause.Values = [];
    if(this.selectedRegion !=AppConfiguration.AllLocation && this.selectedRegion != '' && this.selectedRegion != 'undefined' && this.selectedRegion != null)
    {
      console.log("this.whereClause.ColumnName",this.whereClause.ColumnName);
      console.log("this.SiteInfo.DB_Source.regionSource.ColumnName",this.SiteInfo.DB_Source.RegionSource);
     this.whereClause.ColumnName =  this.SiteInfo.DB_Source.RegionSource.ColumnName;
     this.whereClause.ColumnType = this.SiteInfo.DB_Source.RegionSource.ColumnType;
     this.whereClause.TableName = this.SiteInfo.DB_Source.RegionSource.TableName;
     this.whereClause.Values.push(this.selectedRegion);
     this.AdhocReportData.WhereClauses.push(this.whereClause);
    }
    
    if(this.selectedArea !=AppConfiguration.AllLocation && this.selectedArea != "")
    {
      this.whereClause.ColumnName =  this.SiteInfo.DB_Source.AreaSource.ColumnName;
      this.whereClause.ColumnType = this.SiteInfo.DB_Source.AreaSource.ColumnType;
      this.whereClause.TableName = this.SiteInfo.DB_Source.AreaSource.TableName;
      this.whereClause.Values.push(this.selectedArea);
      this.AdhocReportData.WhereClauses.push(this.whereClause);
    }
    
    if(this.selectedZone != AppConfiguration.AllLocation && this.selectedZone != "")
    {
      this.whereClause.ColumnName =  this.SiteInfo.DB_Source.ZoneSource.ColumnName;
      this.whereClause.ColumnType = this.SiteInfo.DB_Source.ZoneSource.ColumnType;
      this.whereClause.TableName = this.SiteInfo.DB_Source.ZoneSource.TableName;
      this.whereClause.Values.push(this.selectedZone);
      this.AdhocReportData.WhereClauses.push(this.whereClause);
    }
    
    if(this.selectedSubArea != AppConfiguration.AllLocation && this.selectedSubArea != "")
    {
      this.whereClause.ColumnName =  this.SiteInfo.DB_Source.SubAreaSource.ColumnName;
      this.whereClause.ColumnType = this.SiteInfo.DB_Source.SubAreaSource.ColumnType;
      this.whereClause.TableName = this.SiteInfo.DB_Source.SubAreaSource.TableName;
      this.whereClause.Values.push(this.selectedSubArea);
      this.AdhocReportData.WhereClauses.push(this.whereClause);
    }
    if(this.selectedSiteCode != "")
    {
        this.whereClause.ColumnName =  this.SiteInfo.DB_Source.SiteSource.EnglishNameColumnName;
        this.whereClause.ColumnType = this.SiteInfo.DB_Source.SiteSource.SiteCodeColumnType;
        this.whereClause.TableName = this.SiteInfo.DB_Source.SiteSource.TableName;
        this.whereClause.Values.push(this.selectedSiteCode);
        this.AdhocReportData.WhereClauses.push(this.whereClause);
    }
    if(this.selectedSiteName != "")
    {
        this.whereClause.ColumnName =  this.SiteInfo.DB_Source.SiteSource.EnglishNameColumnName;
        this.whereClause.ColumnType = this.SiteInfo.DB_Source.SiteSource.SiteCodeColumnType;
        this.whereClause.TableName = this.SiteInfo.DB_Source.SiteSource.TableName;
        this.whereClause.Values.push(this.selectedSiteName);
        this.AdhocReportData.WhereClauses.push(this.whereClause);
    }
  }

  CreateTreeOfLevels():NodeOfTree{
    
    let level_node :NodeOfTree = new NodeOfTree;
    level_node.id=1;
    level_node.value="NE Levels";
    level_node.children = [];
    let level_node_child :NodeOfTree = new NodeOfTree;
    level_node_child.id=2;
    level_node_child.value="NE";
    let NE_level:NodeOfTree = new NodeOfTree;
    NE_level.id=3;
    NE_level.value="Board";
    let Board_level:NodeOfTree = new NodeOfTree;
    Board_level.id=4;
    Board_level.value="SubBoard";
    let SubBoard_level:NodeOfTree = new NodeOfTree;
    SubBoard_level.id=5;
    SubBoard_level.value="Port";
    let Port_level : NodeOfTree = new NodeOfTree;
    Port_level.id=6;
    Port_level.value="SFP";


    level_node.children.push(level_node_child);
    level_node.children.push(NE_level);
    level_node.children.push(Board_level);
    level_node.children.push(SubBoard_level);
    level_node.children.push(Port_level);

    return level_node;
  }
  public check (node: any) {
    
  }

  AttributeSelect(event,typeName:string){
    this.TreeType="AttributeTree";
   // debugger;
    if(this.selectedAttribute==null){
      this.selectedAttribute=[];
    }  
    if(event.node.leaf)
    {
      let AttributeData:SelectedAttributeData=new SelectedAttributeData();
      AttributeData=this.GetSelectedLeafData(event.node,typeName);

      let selectItem:SelectedColumn=new SelectedColumn(event.node.parent.parent.label + ' :' + event.node.label,typeName,AttributeData);
      if(!selectItem.ContainIn(this.selectedAllAttributeTree)){
        this.selectedAttribute = [];
        this.selectedAllAttributeTree.push(selectItem);
        //this.selectedColumns = this.selectedSubCategoryTree;
        for(let item of this.selectedAllAttributeTree)
        {
          this.selectedAttribute.push(item);
        }
      }
    }
    else
    {
      this.AddAttributesSearchCriteriaToDialog(event.node,typeName);
      this.display_Parent_Selection=true;
    }
  }

  // SearchCriteriaSelect(event,typeName:string){
  //   if(this.selectedSearchCriteria==null){
  //     this.selectedSearchCriteria=[];
  //   }
  //   if(event.node.leaf){
  //     let leafData: any= this.GetSelectedLeafData(event.node,typeName);
  //     let selectItem:SelectedColumn=new SelectedColumn(event.node.label,typeName,leafData);
  //     if(!selectItem.ContainIn(this.selectedAllSearchCriteriaTree)){
  //       this.selectedSearchCriteria = [];
  //       this.selectedAllSearchCriteriaTree.push(selectItem);
  //       for(let item of this.selectedAllSearchCriteriaTree)
  //       {
  //         this.selectedSearchCriteria.push(item);
  //       }
  //     }
  //   }
  //   else{//parent
  //     if(this.dialogBody==null){
  //       this.dialogBody=AppConfiguration.addSearchCriteriaBody;
  //     }
  //     else{
  //       this.dialogBody+='<br/>'+AppConfiguration.addSearchCriteriaBody;
  //     }
  //     this.dialogHeader="Are You Sure !";
  //     this.wrongFilteration=false;
  //     this.oneCategory=false;
  //     this.AddSearchCriteriaToDialog(event.node,typeName);
  //     $('#myList li:last-child').remove();
  //     this.display_Dialog=true;
  //   }
  // }

  nodeSelect(event,typeName:string)
  { debugger;
    this.TreeType="TypeTree";

    if(this.selectedColumns==null)
    {
      this.selectedColumns=[];
    }
    if(event.node.leaf)
    {
      let leafData: any= this.GetSelectedLeafData(event.node,typeName);
      let selectItem:SelectedColumn=new SelectedColumn(event.node.parent.label + ' :' + event.node.label,typeName,leafData);
      if(!selectItem.ContainIn(this.selectedAllSearchCriteriaTree))
      {
        this.selectedColumns = [];
        this.selectedAllSearchCriteriaTree.push(selectItem);
        for(let item of this.selectedAllSearchCriteriaTree)
        {
          
          this.selectedColumns.push(item);
        }
      }
    }
    else
    {
      this.AddSearchCriteriaToDialog(event.node,typeName);
      $('#myList li:last-child').remove();
      this.display_Parent_Selection=true;
    }
  }

  /*AddSearchCriteriaToDialog(Child:TreeNode,typeName:string){
    for (let child of Child.children){
      if(child.leaf){
      
        let leafData: any= this.GetSelectedLeafData(child,typeName);
        let selectItem:SelectedColumn=new SelectedColumn(child.label,typeName,leafData);
        this.childrenSelectedItem.push(selectItem);

     
      }
      else{//child is also parent
        this.AddSearchCriteriaToDialog(child,typeName);
      }
    }
  }*/
  // AddAllSearchingCriteria(TreeType:string){
  //   this.dialogBody=null;
  //   this.display_Dialog=false;
  //     for(let child of this.childrenSelectedItem){
  //       if(!child.ContainIn(this.selectedAllSearchCriteriaTree)){
  //         this.selectedAllSearchCriteriaTree.push(child);
  //       }
  //     }
  //     this.selectedSearchCriteria = [];
  //       for(let item of this.selectedAllSearchCriteriaTree)
  //       {
  //         this.selectedSearchCriteria.push(item);
  //       }
  //   //}
  //   this.childrenSelectedItem=[];//reset list 
  //   this.dialogBody=null;
  // }

  AddAllSearchingCriteria(TreeType:string){
    this.display_Parent_Selection=false;
    if(TreeType=="AttributeTree"){
      for(let child of this.childrenSelectedItem){
        if(!child.ContainIn(this.selectedAllAttributeTree)){
          this.selectedAllAttributeTree.push(child);
        }
      }
      this.selectedAttribute = [];
      for(let item of this.selectedAllAttributeTree)
      {
        this.selectedAttribute.push(item);
      }
      
    }
    else if(TreeType=="TypeTree"){
      for(let child of this.childrenSelectedItem){
        if(!child.ContainIn(this.selectedAllSearchCriteriaTree)){
          this.selectedAllSearchCriteriaTree.push(child);
        }
      }
      this.selectedColumns = [];
        for(let item of this.selectedAllSearchCriteriaTree)
        {
          this.selectedColumns.push(item);
        }
    }
    this.childrenSelectedItem=[];//reset list 
  }

  CloseDialog(){//reset dialog list
    this.childrenSelectedItem=[];
    this.dialogBody=null;
  }

  // DeleteCriteria(data:SelectedColumn){
  //    let temp:SelectedColumn[]=this.selectedSearchCriteria;
  //    this.selectedSearchCriteria = [];
  //    for(let item of temp){
  //      if(item != data){
  //        this.selectedSearchCriteria.push(item);
  //      }
  //    }
  //    let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
  //    this.selectedAllSearchCriteriaTree=[];
  //    for (let item2 of temp2){
  //      if(item2 !=data){
  //        this.selectedAllSearchCriteriaTree.push(item2);
  //      }
  //    }
  
     
  //  }
  DeleteCriteria(data:SelectedColumn){
    if(data.columnType==AppConfiguration.Attribute){
       let temp:SelectedColumn[]=this.selectedAttribute;
       this.selectedAttribute = [];
       for(let item of temp){
         if(item != data){
           this.selectedAttribute.push(item);
         }
       }
       let temp2:SelectedColumn[]=this.selectedAllAttributeTree;
       this.selectedAllAttributeTree=[];
       for (let item2 of temp2){
         if(item2 !=data){
           this.selectedAllAttributeTree.push(item2);
         }
       }
    }
   else {
     let temp:SelectedColumn[]=this.selectedColumns;
     this.selectedColumns = [];
     for(let item of temp){
       if(item != data){
         this.selectedColumns.push(item);
       }
     }
     let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
     this.selectedAllSearchCriteriaTree=[];
     for (let item2 of temp2){
       if(item2 !=data){
         this.selectedAllSearchCriteriaTree.push(item2);
       }
     }
   }
   }

   AddSearchCriteriaToDialog(Child:TreeNode,typeName:string)
   {
     for (let child of Child.children)
     {
       if(child.leaf)
       {
         let leafData: any= this.GetSelectedLeafData(child,typeName);
         if(child.parent != null)
         {
           let selectItem:SelectedColumn=new SelectedColumn(child.parent.label + ' :' + child.label,typeName,leafData);
           this.childrenSelectedItem.push(selectItem);
         }
         else
         {
           let selectItem:SelectedColumn=new SelectedColumn(child.label,typeName,leafData);
           this.childrenSelectedItem.push(selectItem);
         }
               
       }
       else
       {
         this.AddSearchCriteriaToDialog(child,typeName);
       }
     }
   }

   AddAttributesSearchCriteriaToDialog(Child:TreeNode,typeName:string)
   {
     for (let child of Child.children)
     {
       if(child.leaf)
       {
         let leafData: any= this.GetSelectedLeafData(child,typeName);
         if(child.parent.parent != null)
         {
           let selectItem:SelectedColumn=new SelectedColumn(child.parent.parent.label + ' :' + child.label,typeName,leafData);
           this.childrenSelectedItem.push(selectItem);
         }
         else
         {
           let selectItem:SelectedColumn=new SelectedColumn(child.parent.label + ' :' + child.label,typeName,leafData);
           this.childrenSelectedItem.push(selectItem);
         }
               
       }
       else
       {
         this.AddAttributesSearchCriteriaToDialog(child,typeName);
       }
     }
   }



  //  SiteCodeSelect(event){
  //   //1) deleting brevious site code
  //   if(this.selectedSearchCriteria != null){
  //     let temp:SelectedColumn[]=this.selectedSearchCriteria;
  //     this.selectedSearchCriteria = [];
  //     for(let item of temp){
  //       if((item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){//edite
  //         this.selectedSearchCriteria.push(item);
  //       }
  //     }
  //     let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
  //     this.selectedAllSearchCriteriaTree=[];
  //     for (let item2 of temp2){
  //       if((item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
  //         this.selectedAllSearchCriteriaTree.push(item2);
  //       }
  //     }
  //   }
  //   //2)Add site code
  //   if(this.selectedSiteCode!=AppConfiguration.AllLocation){
  //     let column:SelectedColumn=new SelectedColumn(this.selectedSiteCode,AppConfiguration.SelectedSiteCode,null);
  //       if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
  //         this.selectedSearchCriteria = [];
  //         this.selectedAllSearchCriteriaTree.push(column);
  //         for(let item of this.selectedAllSearchCriteriaTree)
  //         {
  //           this.selectedSearchCriteria.push(item);
  //         }
  //       }
  //   }
    
  // }

  SiteCodeSelect(event){
    //1) deleting brevious site code
    if(this.selectedColumns != null){
      let temp:SelectedColumn[]=this.selectedColumns;
      this.selectedColumns = [];
      for(let item of temp){
        if((item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){//edite
          this.selectedColumns.push(item);
        }
      }
      let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
      this.selectedAllSearchCriteriaTree=[];
      for (let item2 of temp2){
        if((item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
          this.selectedAllSearchCriteriaTree.push(item2);
        }
      }
    }
    //2)Add site code
    if(this.selectedSiteCode!=AppConfiguration.AllLocation){
      let column:SelectedColumn=new SelectedColumn(this.selectedSiteCode,AppConfiguration.SelectedSiteCode,null);
        if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
          this.selectedColumns = [];
          this.selectedAllSearchCriteriaTree.push(column);
          for(let item of this.selectedAllSearchCriteriaTree)
          {
            this.selectedColumns.push(item);
          }
        }
    }
    
  }

  // SiteNameSelect(event){
  //   //1) deleting brevious site code
  //   if(this.selectedSearchCriteria != null){
  //     let temp:SelectedColumn[]=this.selectedSearchCriteria;
  //     this.selectedSearchCriteria = [];
  //     for(let item of temp){
  //       if((item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){//edite
  //         this.selectedSearchCriteria.push(item);
  //       }
  //     }
  //     let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
  //     this.selectedAllSearchCriteriaTree=[];
  //     for (let item2 of temp2){
  //       if((item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
  //         this.selectedAllSearchCriteriaTree.push(item2);
  //       }
  //     }
  //   }
  //   //2)Add site code
  //   if(this.selectedSiteName!=AppConfiguration.AllLocation){
  //     let column:SelectedColumn=new SelectedColumn(this.selectedSiteName,AppConfiguration.SelectedSiteName,null);
  //       if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
  //         this.selectedSearchCriteria = [];
  //         this.selectedAllSearchCriteriaTree.push(column);
  //         for(let item of this.selectedAllSearchCriteriaTree)
  //         {
  //           this.selectedSearchCriteria.push(item);
  //         }
  //       }
  //   }
  // }

  SiteNameSelect(event){
    //1) deleting brevious site code
    if(this.selectedColumns != null){
      let temp:SelectedColumn[]=this.selectedColumns;
      this.selectedColumns = [];
      for(let item of temp){
        if((item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){//edite
          this.selectedColumns.push(item);
        }
      }
      let temp2:SelectedColumn[]=this.selectedAllSearchCriteriaTree;
      this.selectedAllSearchCriteriaTree=[];
      for (let item2 of temp2){
        if((item2.columnType != AppConfiguration.SelectedSiteCode)&&(item2.columnType != AppConfiguration.SelectedSiteName)){
          this.selectedAllSearchCriteriaTree.push(item2);
        }
      }
    }
    //2)Add site code
    if(this.selectedSiteName!=AppConfiguration.AllLocation){
      let column:SelectedColumn=new SelectedColumn(this.selectedSiteName,AppConfiguration.SelectedSiteName,null);
        if(!column.ContainIn(this.selectedAllSearchCriteriaTree)){
          this.selectedColumns = [];
          this.selectedAllSearchCriteriaTree.push(column);
          for(let item of this.selectedAllSearchCriteriaTree)
          {
            this.selectedColumns.push(item);
          }
        }
    }
  }

  SearchingNode(event){
    let Label:string =this.SearchInput;
    var ref = { exist: false };
    var ref2 = { cutTree: null};
    this.TraversalTree(Label, ref, this.OrginalTypenodes, ref2); 
    if(ref.exist)//found node
    {
       this.Typenodes = [];
       this.Typenodes.push(Object.assign({}, ref2.cutTree));
       this.expandAll(this.Typenodes);
    }
    else //not found node
    {
        this.Typenodes=Object.assign({}, this.OrginalTypenodes);
        this.expandAll(this.Typenodes);
    }
  }
  TraversalTree(lable:string,ref:{exist:boolean},orginalTree:TreeNode[],ref2:{cutTree:TreeNode})
  {
      if(lable.length == 0)
      {
        ref.exist = true;
        ref2.cutTree = Object.assign({}, orginalTree[0]);
      }
      else
      {
         if(!ref.exist)
         {
            for(let child of  orginalTree)
            {
                if(ref.exist)
                {
                    break;
                }
                
                if(child.label.toLowerCase().includes(lable.toLocaleLowerCase()))
                {
                    ref.exist = true;
                    ref2.cutTree = Object.assign({}, child);
                }
                if(!ref.exist)
                {
                    if(child.children != null)
                    {
                        this.TraversalTree(lable,ref,child.children,ref2);
                        if(ref.exist)
                        {
                            let tempTree:TreeNode;
                            tempTree = Object.assign({}, child);
                            tempTree.children = [];
                            tempTree.children.push(ref2.cutTree);
                            ref2.cutTree = tempTree;
                        }
                    }
                }
            }
         }
      }
  }
  expandAll(Tree:TreeNode[]){
    Tree.forEach( node => {
        this.expandRecursive(node, true);
    } );
    
}
private expandRecursive(node:TreeNode, isExpand:boolean){
  node.expanded = isExpand;
  if(node.children){
      node.children.forEach( childNode => {
          this.expandRecursive(childNode, isExpand);
      } );
  }
}

ValidationCriteria_Attribute(SelectedData:ReportLevelFilterModel):boolean{
  let level=SelectedData.Levels;
  let types:NEType[]=SelectedData.Types;
  let catgories:Category[]=SelectedData.Categories_name;
  if(catgories!=null){
    if(types!=null){
      for (let type of types){
        let TypeCategory:string=type.CategoryName;
        let TypeLevel:string=type.LevelName;
        //1) validate 1
        let x:number=AppConfiguration.Levels.indexOf(TypeLevel);
        let y:number=AppConfiguration.Levels.indexOf(level);
        
        if(y>x){
          //not valid
          this.GetDialogMeassage(AppConfiguration.WrongGlobalFilterationTypeLevelBody);
          this.dialogHeader="Rong Choose!!";
          this.wrongFilteration=true;
          this.oneCategory=false;
          this.display_Dialog=true;
          //return false;
        }
        //2) validate 2
        let CategoryExist = catgories.filter(function (item) {return item.Name === TypeCategory.toUpperCase(); })[0];//TabelName
        if (!CategoryExist){
          //not valid
          this.GetDialogMeassage(AppConfiguration.WrongGlobalFilterationTypeCategoryBody);
          this.dialogHeader="Rong Choose!!";
          this.oneCategory=false;
          this.wrongFilteration=true;
          this.display_Dialog=true;
          //return false;
        }
      }
    }
    
    //3)validate 3 category count
    let category_count=catgories.length;
    if(category_count<=1){
      this.GetDialogMeassage(AppConfiguration.WrongGlobalFilterationCategoryBody);
      this.dialogHeader="Rong Choose!!";
      //here oneCategory
      this.oneCategory=true;
      this.wrongFilteration=true;
      this.display_Dialog=true;
      //return false;
    }
  }
  else{//category == null
    //validate 3
    this.GetDialogMeassage(AppConfiguration.WrongGlobalFilterationCategoryBody);
    this.dialogHeader="Rong Choose!!";
    this.oneCategory=true;
    this.wrongFilteration=true;
    this.display_Dialog=true;
  }
  
  
  if(this.dialogBody!=null){
    return false;
  }
  else{
    return true;
  }
}
closeValidationDialog(){
  this.display_Dialog=false;
  this.dialogBody=null;
  //reset list
  this.childrenSelectedItem=[];
}
/*GetSelectedLeafData(selectedNode:TreeNode,TreeType:string):any{ //SubCategory, NEType
  //here where we select node
  if (TreeType=="NEType"){
    if(selectedNode.leaf){//child
      let TypeData:SelectedTypeData=new SelectedTypeData();
      TypeData.TabelName=selectedNode.parent.data.TableName;
      TypeData.ColumnName=selectedNode.parent.data.ColumnName;
      TypeData.ColumnType=selectedNode.parent.data.ColumnType;
      TypeData.LevelName=selectedNode.parent.data.LevelName;
      TypeData.Label=selectedNode.label;
      TypeData.CategoryId=selectedNode.parent.data.CategoryId;
      TypeData.LevelId=selectedNode.parent.data.LevelId;
      TypeData.CategoryName=selectedNode.parent.data.CategoryName;
      return TypeData;
    }
  }
  else if(TreeType=="SubCategory"){
      if(selectedNode.leaf){
        let subCategoryData:SelectedSubCategoryData=new SelectedSubCategoryData();
        subCategoryData.CategoryName=selectedNode.parent.label;
        subCategoryData.subCategoryName=selectedNode.label;
        return subCategoryData;
      }
    
  }
    
    
    
}*/

GetSelectedLeafData(selectedNode:TreeNode,TreeType:string):any{
  console.log(selectedNode);
  //here where we select node
  if (TreeType=="NEType"){
    if(selectedNode.leaf){//child
      let TypeData:SelectedRanTypeData=new SelectedRanTypeData();
      TypeData.TabelName=selectedNode.parent.data.TableName;
      TypeData.ColumnName=selectedNode.parent.data.ColumnName;
      TypeData.ColumnType=selectedNode.parent.data.ColumnType;
    //  TypeData.LevelName=selectedNode.parent.data. LevelName;
      TypeData.Label=selectedNode.label;
      //console.log("selected column data ",TypeData);
      return TypeData;
    }//else if parent (i should edite the ok button event click)
  }
  else {
    if(TreeType=="SubCategory"){
      

      if(selectedNode.leaf){
        let subCategoryData:SelectedRanTypeData= new SelectedRanTypeData();
        subCategoryData.TabelName=selectedNode.parent.data.TableName;
        subCategoryData.ColumnName=selectedNode.parent.data.ColumnName;
        subCategoryData.ColumnType=selectedNode.parent.data.ColumnType;
        subCategoryData.Label=selectedNode.label
      //  let data:any=selectedNode.data;
     //   console.log("subCategoryData ",subCategoryData);
        return subCategoryData;
        }


    
    }
    else if(TreeType==AppConfiguration.Attribute){
      let AttributeData:SelectedAttributeData=new SelectedAttributeData();
      AttributeData.ColumnName=selectedNode.data.ColumnName;
      AttributeData.ColumnType=selectedNode.data.ColumnType;
      AttributeData.TableName=selectedNode.parent.data.TableName;
      AttributeData.LevelName=selectedNode.parent.data.LevelName;
      AttributeData.LevelType=selectedNode.parent.data.LevelType;
      AttributeData.DisplayName=selectedNode.label;
      console.log("AttributeData ",AttributeData);
      return AttributeData;
    }
  }                     
}


GetDialogMeassage(MessageToAdd:string){
  if(this.dialogBody!=null){
    if(!this.dialogBody.includes(MessageToAdd)){
      this.dialogBody+='<br/>'+MessageToAdd;
    }
  }
  else{
    this.dialogBody=MessageToAdd;
  }

}
navigateToCategoryReport(){
  this._router.navigate(['../CategoryReport'], { relativeTo: this.route });
}
// ClearSearchCriteriaList(){
//   this.selectedAllSearchCriteriaTree=[];
//   this.selectedSearchCriteria=[];
// }

ClearSearchCriteriaList(){
  this.selectedAllSearchCriteriaTree=[];
  this.selectedColumns=[];
}
ClearAttributeList(){
  this.selectedAttribute=[];
  this.selectedAllAttributeTree=[];
}

ShowSearchCriteria(event){ 
  // *ngIf="attributeShow;"
  $(event.target.parentNode.parentNode.nextElementSibling.nextElementSibling).fadeOut(function(){
    $(event.target.parentNode.parentNode.nextElementSibling).fadeIn();
  });
  
  $(event.target.parentNode).addClass("elegantshadow");
  $(event.target.parentNode.nextElementSibling).removeClass("elegantshadow");
}
ShowAttribute(event){
  $(event.target.parentNode.parentNode.nextElementSibling).fadeOut(function(){
    $(event.target.parentNode.parentNode.nextElementSibling.nextElementSibling).fadeIn();
  });
  $(event.target.parentNode).addClass("elegantshadow");
  $(event.target.parentNode.previousElementSibling).removeClass("elegantshadow");
  
}

}//class end
