import { Component, OnInit ,ViewChild} from '@angular/core';
import { trigger, state, style, transition, animate, AUTO_STYLE } from '@angular/animations';
import {AutoCompleteModule} from 'primeng/primeng';
import {FormControl} from '@angular/forms';
import {DropdownModule,DialogModule} from 'primeng/primeng';
import { SelectItem } from './SelectItem';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import {ButtonModule} from 'primeng/primeng';
//import {TableModule} from 'primeng/primeng';
import { SiteLocationService } from '../../../services/getSiteInfo.service';
import { LocationSiteData } from '../../../extraClasses/LocationSiteData';
import { Region } from '../../../extraClasses/LocationSiteHierarchy/Region';
import { Area } from '../../../extraClasses/LocationSiteHierarchy/Area';
import { Zone } from '../../../extraClasses/LocationSiteHierarchy/Zone';
import { SubArea } from '../../../extraClasses/LocationSiteHierarchy/SubArea';
import { Site } from '../../../extraClasses/Site';
import { FilterModel } from '../../../extraClasses/LocationSiteHierarchy/FilterModel';
import { NodeOfTree } from '../../../extraClasses/TreeNode';
import { AttributeTreeService } from '../../../services/attributeTree.service';
import { toJS } from 'mobx';
import { ReportFilterModel } from '../../../extraClasses/ReportFilter/ReportFilterModel';
import { NETable } from '../../../extraClasses/ReportFilter/NETable';
import { NEAttribute } from '../../../extraClasses/ReportFilter/NEAttribute';
import { NeTypesService } from '../../../services/NeTypes.service';
import { IDTypeDictionary } from 'angular-tree-component/dist/defs/api';
import { NEType } from '../../../extraClasses/ReportFilter/NEType';
import { SiteInfo } from '../../../extraClasses/ReportFilter/SiteInfo';
import { SubCategoryTreeService } from '../../../services/getSubCategory.service';
import { GenerateCategoryReportService } from '../../../services/generateFirstReport.service';
import { CategoryDropDownService } from '../../../services/getCategoryDropDown.service';
import { CategoryList } from '../../../extraClasses/DropDownCategory/CategoryList';
import { ReportCategoryRetunType } from '../../../extraClasses/ReportFilter/ReportCategoryRetunType';
import { ColumnItem } from '../../../extraClasses/ReportShow/ColumnItem';
import { CategoryItem } from '../../../extraClasses/DropDownCategory/CategoryItem';
import { TreeNode } from 'primeng/components/common/treenode';
import { TreeComponent } from 'angular-tree-component';
import { Parameter } from '../../../extraClasses/RIMGetDataServiceClasses/ApiParameters';
import { RIMGetDataService } from '../../../services/general services/RIMGetDataService.service';
import { AppConfiguration } from '../../../extraClasses/AppConfiguration';
import { SelectedColumn } from '../../../extraClasses/ReportShow/SelectedColumn';
import { SelectedTypeData } from '../../../extraClasses/ReportFilter/SelectedTypeData';
import { SelectedAttributeData } from '../../../extraClasses/ReportFilter/SelectedAttributeData';
import { LevelsList } from '../../../extraClasses/validation report/LevelsList';
import { AdhocReportData } from '../../../extraClasses/DynamicReport/AdhocReportData';
import { WhereClause } from '../../../extraClasses/DynamicReport/WhereClause';
import { SelectClause } from '../../../extraClasses/DynamicReport/SelectClause';
import { TouchSequence } from 'selenium-webdriver';
import { SelectedRanTypeData } from '../../../extraClasses/ReportFilter/SelectedRanTypeData';
import { SelectRanItem } from '../../../transmission/ne-dynamic-report/category-report/SelectRanItem';
import { Pairs } from '../../../transmission/ne-dynamic-report/category-report/Pairs';




declare var jquery: any;
declare var $: any;


@Component({
  selector: 'app-ran-category-report',
  
  templateUrl: './ran-category-report.component.html',
  styleUrls: ['./ran-category-report.component.css'],
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


export class RanCategoryReportComponent implements OnInit {

  display_Validation_dialog: boolean=false;
  displaytable: boolean;

  private Attributenodes: TreeNode[] = null;
  private OrginalAttributenodes:TreeNode[]=null;
  private Typenodes: TreeNode[] = null;
  private OrginalTypenodes:TreeNode[]=null;

  private SubCategoryNodes: TreeNode[];

//private reportFilterModel:ReportFilterModel = null;
private reportCategoryRetunType:ReportCategoryRetunType = null

private AdhocReportData : AdhocReportData = null;
private selectClause : SelectClause = null;
private whereClause: WhereClause = null;

NES = [];
nes = this.NES;
cols: ColumnItem[];
columnOptions: SelectItem[];
displayDialog: boolean;
public loading = false;

DisableArea:boolean;
DisableZone:boolean;
DisableSubArea:boolean;
DisableSiteCode:boolean;
DisableSiteName:boolean;

Region: SelectItem[];
Area: SelectItem[];
AllArea: SelectItem[];
selectedRegion: string = null;
selectedArea: string = '';
selectedZone: string = '';
Zones: SelectItem[];
AllZone: SelectItem[];
selectedSubArea: string = '';
SubAreas: SelectItem[];
AllSubAreas: SelectItem[];
selectedSiteCode: string = '';
SitesCodes: SelectItem[];
AllSiteCodes: SelectItem[];
selectedSiteName: string = '';
SitesNames: SelectItem[];
AllSiteNames: SelectItem[];
SiteInfo : LocationSiteData;

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

model:FilterModel;

selectedCategory:string = '5';
selectCategoryOrder: string = '1';
category_dropdown_list:SelectRanItem[];
category_list:CategoryList;

selectedCat: string;
categoryid:number;
selectedSubCategory:TreeNode[];
selectedAttributes:TreeNode[];
selectedTypes:TreeNode[];
display_dialog:boolean=false;
empty_dialog:boolean=false;

ModelName:string;
ClassificationName: string;
cols1:any[];
selectedColumns:SelectedColumn [];
selectedAllSearchCriteriaTree:SelectedColumn []=[];
childrenSelectedItem:SelectedColumn[]=[];
display_Parent_Selection:boolean=false;
SearchInput:string;
SearchAttributeInput:string;
selectedAttribute:SelectedColumn[];
selectedAllAttributeTree:SelectedColumn []=[];

TreeType:string;
dialogMessage:string;
attributeShow:boolean=false;
selectedCategoryDDL: Pairs;

constructor(private route: ActivatedRoute, private _router: Router,private siteLocation:SiteLocationService,
  private attributeTree:AttributeTreeService, private neTypes:NeTypesService, private subCategoryTree:SubCategoryTreeService, 
  private categoryReportService:GenerateCategoryReportService, private categoryDropdownList : CategoryDropDownService) {}

  ngOnInit() {
    this.loading = true; 
    //get model name to pass it to api
    //this.selectedColumns=[];
    if(this._router.url.includes(AppConfiguration.ModelNameTransmission)){
      this.ClassificationName = AppConfiguration.DatacomClassification;
      this.ModelName=AppConfiguration.ModelNameTransmission;
    }
    else if(this._router.url.includes(AppConfiguration.ModelNameRAN)){
      this.ClassificationName = AppConfiguration.RadioClassification;
      this.ModelName=AppConfiguration.ModelNameRAN;
    }
    this.cols1 = [
      { field: 'columnName', header: 'Column' },
      { field: 'columnType', header: 'Type' }
    ];

    this.categoryid =parseInt(this.selectedCategory);

    //this.selectedCat = this.getCategoryName(this.selectedCategory.toString());

    this.categoryDropdownList.getCategoryDropDownList(this.ModelName).subscribe(values => {
      
     this.category_list= new CategoryList;
     this.category_list.DropDownCategoryList=[];
     this.category_list.DropDownCategoryList=<CategoryItem[]>values.data;
     //console.log("Drop Down List befor",this.category_list.DropDownCategoryList);
     this.category_dropdown_list=[];
     this.category_dropdown_list=this.categoryDropdownList.getRanCategoryDropDownListParsing(this.category_list);
     //console.log("Drop Down List after",this.category_dropdown_list);
     //default value of category drop down list 
     this.selectedCategory=this.category_dropdown_list[0].value.Id;
     this.selectedCat=this.category_dropdown_list[0].label;

     this.selectCategoryOrder = this.category_list.DropDownCategoryList[0].Order;
      
    //  this.selectedCat = this.getCategoryName(this.selectedCategory.toString());
    //console.log("this.ModelName ",this.ModelName);
    //  console.log("this.selectedCategory ",this.selectedCat);

     this.attributeTree.GetRANAttributeTree(this.ClassificationName).subscribe(values => {      

       this.Attributenodes = <TreeNode[]>[values.data];
       this.OrginalAttributenodes= [];
       this.OrginalAttributenodes.push(Object.assign({},  this.Attributenodes[0]));
       this.siteLocation.GetSiteLocationData(this.ModelName).subscribe(values => {
        
                      this.SiteInfo =  (<LocationSiteData>values.data);
                      //console.log("this.SiteInfo",this.SiteInfo);
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
                        //console.log("this.Typenodes ",this.Typenodes[0]);
                        //copy the orginal Type tree
                        this.OrginalTypenodes = [];
                        this.OrginalTypenodes.push(Object.assign({},  this.Typenodes[0])); 
                        
                        this.subCategoryTree.GetSubCategoryTree(this.ClassificationName).subscribe(values => {

                            if(values.data !== null)
                            {
                                this.SubCategoryNodes = <TreeNode[]>[values.data];
                                //console.log("this.Typenodes ",this.SubCategoryNodes);
                            }
                            this.loading = false;                                                                   
                        });
                                            
                      }); 


                     
        
              });   
     });
});
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

  isCollapsedSideBar = 'yes-block';
  toggleOpenedSidebar(event) {
    //debugger;
    this.isCollapsedSideBar = this.isCollapsedSideBar === 'yes-block' ? 'no-block' : 'yes-block';
    this.changeIcon(event);
  }

  isCollapsedSideBar2 = 'yes-block';
  toggleOpenedSidebar2(event) {
    this.isCollapsedSideBar2 = this.isCollapsedSideBar2 === 'yes-block' ? 'no-block' : 'yes-block';
    this.changeIcon(event);
  }
  isCollapsedSideBar3 = 'yes-block';
  toggleOpenedSidebar3(event) {
    this.isCollapsedSideBar3 = this.isCollapsedSideBar3 === 'yes-block' ? 'no-block' : 'yes-block';
    this.changeIcon(event);
  }
  isCollapsedSideBar4 = 'yes-block';
  toggleOpenedSidebar4(event) {
    this.isCollapsedSideBar4 = this.isCollapsedSideBar4 === 'yes-block' ? 'no-block' : 'yes-block';
    this.changeIcon(event);
  }

  changeIcon(item:any){
    let elm = item.srcElement.classList;
    if(elm.contains("fa-angle-double-up")){
      $(item.srcElement).removeClass('fa-angle-double-up');
      $(item.srcElement).addClass('fa-angle-double-down');
    }
    else{
      $(item.srcElement).removeClass('fa-angle-double-down');
      $(item.srcElement).addClass('fa-angle-double-up');
    }
  }

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

  //console.log( this.SitesCodes);
  //console.log(this.SitesNames);
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
  //console.log('tempRegion',this.tempRegions);
  //console.log( this.SitesCodes);
  //console.log(this.SitesNames);
  
}
//for selection grid
//1) deleting selected region/regions
if(this.selectedColumns != null){
  let temp:SelectedColumn[]=this.selectedColumns;
  this.selectedColumns = [];
  for(let item of temp){
    if((item.columnType != AppConfiguration.SelectedRegion)&&(item.columnType!=AppConfiguration.SelectedArea)&&(item.columnType != AppConfiguration.SelectedSubArea)&&(item.columnType != AppConfiguration.SelectedZone)&&(item.columnType != AppConfiguration.SelectedSiteCode)&&(item.columnType != AppConfiguration.SelectedSiteName)){
      console.log("is not any of drop down list")
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
console.log("ttttttttttttttttttt   ",this.selectedColumns);
}

AreaSelect(event){
//debugger;
console.log("I'm selected region",this.selectedRegion);
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
      //console.log( this.SitesCodes);
      //console.log(this.SitesNames);
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
      //console.log( this.SitesCodes);
      //console.log(this.SitesNames);
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
      //console.log( this.SitesCodes);
      //console.log(this.SitesNames);
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
      //console.log( this.SitesCodes);
      //console.log(this.SitesNames);
    }
  }
}
else {//reagion == null
  if(this.selectedArea!=AppConfiguration.AllLocation){
    console.log("i'm here");
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
    //console.log('tempArea',this.model);
    //console.log( this.SitesCodes);
    //console.log(this.SitesNames);
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
    //console.log( this.SitesCodes);
    //console.log(this.SitesNames);
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
  //console.log( this.SitesCodes);
  //console.log(this.SitesNames);
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
  //console.log( this.SitesCodes);
  //console.log(this.SitesNames);
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
SubAreaSelect(event){
if(this.selectedSubArea !=AppConfiguration.AllLocation){
  this.selectedSiteCode='';
  this.selectedSiteName='';
  this.tempSubAreas=this.siteLocation.GetTempSubArea(this.selectedSubArea,this.tempZones);
  this.model=this.siteLocation.GetFilteredSite(this.tempSubAreas);
  this.SitesCodes=this.model.sitecode;
  this.SitesNames=this.model.sitename;
  //console.log( this.SitesCodes);
  //console.log(this.SitesNames);
}
else { //SubArea is All
  this.selectedSiteCode='';
  this.selectedSiteName='';
  this.SitesCodes=this.ZoneTempSiteCode;
  this.SitesNames=this.ZoneTempSiteName;
  //console.log( this.SitesCodes);
  //console.log(this.SitesNames);
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

public check (node: any) {
  
}

// Body is  {"CategoryName":"DATACOM","Types":[{"TabelName":"DATACOM_NE_TYPE","ColumnName":"MODEL","ColumnType":"VARCHAR2","LevelName":"VARCHAR2","Values":["NE40E-X3"]},{"TabelName":"DATACOM_BOARD_TYPE","ColumnName":"TYPE","ColumnType":"VARCHAR2","LevelName":"VARCHAR2","Values":["CR5DLPUF517E","DEVSRU"]},{"TabelName":"DATACOM_SUBBOARD_TYPE","ColumnName":"ALIAS_NAME","ColumnType":"VARCHAR2","LevelName":"VARCHAR2","Values":["Virtual Subboard"]},{"TabelName":"DATACOM_PORT_TYPE","ColumnName":"TYPE","ColumnType":"VARCHAR2","LevelName":"VARCHAR2","Values":["POS","Ethernet"]}],"SubCategory":null,"Tables":[{"Attributes":[{"DisplayName":"U2000 REF Key","ColumnName":"U2000_REF_ID","ColumnType":"NUMBER"},{"DisplayName":"NE Name","ColumnName":"NAME","ColumnType":"VARCHAR2"}],"TableName":"DATACOM_NE","LevelName":"NE","LevelType":"S"},{"Attributes":[{"DisplayName":"Subrack ID","ColumnName":"SUBRACK_ID","ColumnType":"NUMBER"},{"DisplayName":"Slot ID","ColumnName":"SLOT_ID","ColumnType":"NUMBER"}],"TableName":"DATACOM_BOARD","LevelName":"Board","LevelType":"S"},{"Attributes":[{"DisplayName":"Subslot ID","ColumnName":"SUBSLOT_ID","ColumnType":"NUMBER"}],"TableName":"DATACOM_SUBBOARD","LevelName":"SubBoard","LevelType":"S"},{"Attributes":[{"DisplayName":"Port ID","ColumnName":"PORT_ID","ColumnType":"NUMBER"}],"TableName":"DATACOM_PORT","LevelName":"Port","LevelType":"S"},{"Attributes":[{"DisplayName":"SFP Type","ColumnName":"TYPE","ColumnType":"VARCHAR2"}],"TableName":"DATACOM_SFP","LevelName":"SFP","LevelType":"S"}],"SiteInfo":{"REGION":null,"AREA":null,"ZONE":null,"SUBAREA":null,"CODE":null,"ENGLISHNAME":null}}

ValidationCriteria_Attribute(SelectedData:ReportFilterModel){
  let Attributes:NETable[]; //console.log("Attttttttttttttt ",Attributes);
  Attributes=SelectedData.Tables;
  let Types:NEType[];
  Types=SelectedData.Types; //NE Board SubBoard Port

  //validate hirarchy attrivute
  let AttributeHirarchy:LevelsList=new LevelsList();
  let AttrTypesHirarchy:LevelsList=new LevelsList();
  if(Attributes!=null){
    for(let attr of Attributes){
      if(!attr.TableName.includes("TYPE")){//DATACOM_NE_TYPE
        if(attr.LevelName!="Site"){
          // 
             AttributeHirarchy.levels.find(x => x.level == attr.LevelName).chosen=true; 
         } 
      }
      else//type attribute table
      {
        AttrTypesHirarchy.levels.find(x => x.level == attr.LevelName).chosen=true;
      }             
    }
  }
  //validate Type filter
  let TypeHirarchy:LevelsList=new LevelsList();
  if(Types !=null){
    for(let type of Types){
      TypeHirarchy.levels.find(x => x.level == type.LevelName).chosen=true;
    }
  }
  //console.log(TypeHirarchy);
  console.log("AttrTypesHirarchy ",AttrTypesHirarchy);
  this.CheckValidity(AttributeHirarchy,TypeHirarchy,AttrTypesHirarchy);
}

CheckValidity(AttributeLevels:LevelsList,TypeLevels:LevelsList,AttrTypeLevels:LevelsList){
  //console.log("AttributeLevels",AttributeLevels);
  //attribute hirarchy validation
  for(let item of AttributeLevels.levels){
    if (item.chosen){
      let ParentIndex=item.order-2;
      if(ParentIndex>=0){
        if(!AttributeLevels.levels[item.order-2].chosen){
          //dialog message
          if(this.dialogMessage !=null)
             this.dialogMessage+="\nYou have chosen "+item.level+ " Attribute but you have to choose from "+AttributeLevels.levels[item.order-2].level +" as well.";
          else
           this.dialogMessage="\nYou have chosen "+item.level+ " Attribute but you have to choose from "+AttributeLevels.levels[item.order-2].level +" as well.";  
        }
      }   
    }
  }
  //type filter validation
  for(let item of TypeLevels.levels){
    if(item.chosen){
      if(!AttributeLevels.levels[item.order-1].chosen){
        if(this.dialogMessage !=null)
          this.dialogMessage+="\nYou have chosen "+item.level+ " Type Search Criteria but you have to choose from "+AttributeLevels.levels[item.order-1].level +" Attributes as well.";
        else
          this.dialogMessage="\nYou have chosen "+item.level+ " Type Search Criteria but you have to choose from "+AttributeLevels.levels[item.order-1].level +" Attributes as well.";
      }
    }
  }
  //type attribute validation
  for(let item of AttrTypeLevels.levels){
    if(item.chosen){
      if(!AttributeLevels.levels[item.order-1].chosen){
        if(this.dialogMessage !=null)
        this.dialogMessage+="\nYou have chosen "+item.level+ " Type Attribute but you have to choose from "+AttributeLevels.levels[item.order-1].level +" Attributes as well.";
      else
        this.dialogMessage="\nYou have chosen "+item.level+ " Type Attribute but you have to choose from "+AttributeLevels.levels[item.order-1].level +" Attributes as well.";
      }
    }
  }
  if(this.dialogMessage != null){
    this.loading = false;
    this.display_Validation_dialog=true;
  }
  console.log(this.dialogMessage);
}

closeValidationDialog(){
  this.display_Validation_dialog=false;
  this.dialogMessage=null;
}

GenerateReport()
    {
      this.loading = true;

      this.AdhocReportData = new AdhocReportData();
      this.AdhocReportData.SelectClauses = [];
      this.AdhocReportData.WhereClauses = [];

      //Set Classification Name
      this.AdhocReportData.ClassificationName = this.ClassificationName;
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

      
      const body = JSON.stringify(this.AdhocReportData);
      console.log("Body is ",body);

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
          else
          {
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
      // if(SubCategoryTree !=null){
      //    this.reportFilterModel.SubCategory = [];
      //   for(let criteria of SubCategoryTree){
      //     if(criteria.columnType==AppConfiguration.SubCategory){
      //       let subCategoryName:string = criteria.columnName; 
      //       this.reportFilterModel.SubCategory.push(subCategoryName);
      //     //  this.AdhocReportData.CategoryName = subCategoryName;
      //     }
      //   }
      /*  if(this.reportFilterModel.SubCategory.length == 0)
        this.reportFilterModel.SubCategory = null;*/
      //}
     /* else{
        this.reportFilterModel.SubCategory = null;
      } */
    }

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

    GetSelectedAttributes(AttributeTree:SelectedColumn[])
    {
        //console.log(" Attribute Tree ",AttributeTree);
        //this.reportFilterModel.Tables = [];


        if(AttributeTree != null){
          for (let attr of AttributeTree){
           // console.log(" Attribute Tree ",attr.IsLeaf);
            if(attr.columnType==AppConfiguration.Attribute){
              let AttrData=<SelectedAttributeData>attr.Data;
             /* let tableName:string = AttrData.TableName;
              let LevelName:string = AttrData.LevelName;
              let LevelType:string = AttrData.LevelType;*/
           //   debugger;
              this.selectClause = new SelectClause();
              this.selectClause.TableName = AttrData.TableName;
              this.selectClause.ColumnName = AttrData.ColumnName;
              this.selectClause.ColumnType = AttrData.ColumnType;
              this.selectClause.Name = AttrData.DisplayName;
              this.AdhocReportData.SelectClauses.push(this.selectClause);
              console.log("this.selectClause",this.selectClause);
            /*  let tableExist:NETable;
              tableExist = this.reportFilterModel.Tables.filter(function (table) { return table.TableName === tableName; })[0];    
              if (tableExist == null) 
              {
                let newTable:NETable = new NETable();
                newTable.Attributes = [];
                newTable.TableName = tableName;
                newTable.LevelName = LevelName;
                newTable.LevelType = LevelType;

                let neAttribute:NEAttribute = new NEAttribute();
                neAttribute.DisplayName = AttrData.DisplayName;
                neAttribute.ColumnName = AttrData.ColumnName;
                neAttribute.ColumnType = AttrData.ColumnType;    
                newTable.Attributes.push(neAttribute);
    
                this.reportFilterModel.Tables.push(newTable);    
              }
              else
              {
                let neAttribute:NEAttribute = new NEAttribute();
                neAttribute.DisplayName = AttrData.DisplayName;
                neAttribute.ColumnName = AttrData.ColumnName;
                neAttribute.ColumnType = AttrData.ColumnType;
    
                tableExist.Attributes.push(neAttribute);
              }*/
            }
          }
        }
      
        // if(this.reportFilterModel.Tables.length == 0)
        // this.reportFilterModel.Tables = null;

        //console.log("this.reportFilterModel attribute",this.reportFilterModel.Tables);
    }

    GetSiteInfoFilters(){
      this.whereClause = new WhereClause();
      this.whereClause.ColumnName = null;
      this.whereClause.ColumnType = null;
      this.whereClause.TableName = null;
      this.whereClause.Values = [];
     /* this.reportFilterModel.SiteInfo= new SiteInfo();
      this.reportFilterModel.SiteInfo.REGION = null;
      this.reportFilterModel.SiteInfo.AREA = null;
      this.reportFilterModel.SiteInfo.ZONE = null;
      this.reportFilterModel.SiteInfo.SUBAREA = null;
      this.reportFilterModel.SiteInfo.CODE = null;
      this.reportFilterModel.SiteInfo.ENGLISHNAME = null;*/
      
    /*  if(this.selectedRegion==AppConfiguration.AllLocation || this.selectedRegion == '' || this.selectedRegion == 'undefined'){  
        this.reportFilterModel.SiteInfo.REGION= null;   
      }
      else*/
   //   debugger;
      if(this.selectedRegion !=AppConfiguration.AllLocation && this.selectedRegion != '' && this.selectedRegion != 'undefined' && this.selectedRegion != null)
      {
        console.log("this.whereClause.ColumnName",this.whereClause.ColumnName);
        console.log("this.SiteInfo.DB_Source.regionSource.ColumnName",this.SiteInfo.DB_Source.RegionSource);
       this.whereClause.ColumnName =  this.SiteInfo.DB_Source.RegionSource.ColumnName;
       this.whereClause.ColumnType = this.SiteInfo.DB_Source.RegionSource.ColumnType;
       this.whereClause.TableName = this.SiteInfo.DB_Source.RegionSource.TableName;
       this.whereClause.Values.push(this.selectedRegion);
       this.AdhocReportData.WhereClauses.push(this.whereClause);
      //  this.reportFilterModel.SiteInfo.REGION = this.selectedRegion;
        //console.log(this.selectedRegion);
      }
      
     /* if(this.selectedArea==AppConfiguration.AllLocation || this.selectedArea == ""){
        this.reportFilterModel.SiteInfo.AREA= null;
      }
      else*/
      if(this.selectedArea !=AppConfiguration.AllLocation && this.selectedArea != "")
      {
        this.whereClause.ColumnName =  this.SiteInfo.DB_Source.AreaSource.ColumnName;
        this.whereClause.ColumnType = this.SiteInfo.DB_Source.AreaSource.ColumnType;
        this.whereClause.TableName = this.SiteInfo.DB_Source.AreaSource.TableName;
        this.whereClause.Values.push(this.selectedArea);
        this.AdhocReportData.WhereClauses.push(this.whereClause);
     //   this.reportFilterModel.SiteInfo.AREA=this.selectedArea;
      }
      
   /*   if(this.selectedZone ==AppConfiguration.AllLocation || this.selectedZone == ""){
        this.reportFilterModel.SiteInfo.ZONE= null;
      }
      else*/
      if(this.selectedZone != AppConfiguration.AllLocation && this.selectedZone != "")
      {
        this.whereClause.ColumnName =  this.SiteInfo.DB_Source.ZoneSource.ColumnName;
        this.whereClause.ColumnType = this.SiteInfo.DB_Source.ZoneSource.ColumnType;
        this.whereClause.TableName = this.SiteInfo.DB_Source.ZoneSource.TableName;
        this.whereClause.Values.push(this.selectedZone);
        this.AdhocReportData.WhereClauses.push(this.whereClause);
      //  this.reportFilterModel.SiteInfo.ZONE=this.selectedZone;
      }
      

    /*  if(this.selectedSubArea == AppConfiguration.AllLocation || this.selectedSubArea == ""){
        this.reportFilterModel.SiteInfo.SUBAREA= null;
      }
      else*/
      if(this.selectedSubArea != AppConfiguration.AllLocation && this.selectedSubArea != "")
      {
        this.whereClause.ColumnName =  this.SiteInfo.DB_Source.SubAreaSource.ColumnName;
        this.whereClause.ColumnType = this.SiteInfo.DB_Source.SubAreaSource.ColumnType;
        this.whereClause.TableName = this.SiteInfo.DB_Source.SubAreaSource.TableName;
        this.whereClause.Values.push(this.selectedSubArea);
        this.AdhocReportData.WhereClauses.push(this.whereClause);
       // this.reportFilterModel.SiteInfo.SUBAREA=this.selectedSubArea;
      }
      if(this.selectedSiteCode != "")
      {
          this.whereClause.ColumnName =  this.SiteInfo.DB_Source.SiteSource.EnglishNameColumnName;
          this.whereClause.ColumnType = this.SiteInfo.DB_Source.SiteSource.SiteCodeColumnType;
          this.whereClause.TableName = this.SiteInfo.DB_Source.SiteSource.TableName;
          this.whereClause.Values.push(this.selectedSiteCode);
          this.AdhocReportData.WhereClauses.push(this.whereClause);
      }
    /*  this.reportFilterModel.SiteInfo.CODE=this.selectedSiteCode;
      if(this.reportFilterModel.SiteInfo.CODE == "")
        this.reportFilterModel.SiteInfo.CODE = null;*/
      if(this.selectedSiteName != "")
      {
          this.whereClause.ColumnName =  this.SiteInfo.DB_Source.SiteSource.EnglishNameColumnName;
          this.whereClause.ColumnType = this.SiteInfo.DB_Source.SiteSource.SiteCodeColumnType;
          this.whereClause.TableName = this.SiteInfo.DB_Source.SiteSource.TableName;
          this.whereClause.Values.push(this.selectedSiteName);
          this.AdhocReportData.WhereClauses.push(this.whereClause);
      }
  /*    this.reportFilterModel.SiteInfo.ENGLISHNAME=this.selectedSiteName;
      if(this.reportFilterModel.SiteInfo.ENGLISHNAME == "")
        this.reportFilterModel.SiteInfo.ENGLISHNAME = null;*/
    }

    public CategorySelect(event){
        
      //reset the list:
      this.selectedAllSearchCriteriaTree=[];
      this.selectedColumns=[];
      this.selectedAllAttributeTree=[];
      this.selectedAttribute=[];

      this.selectedRegion = '';
      this.selectedArea = '';
      this.selectedZone = '';
      this.selectedSubArea = '';
      this.selectedSiteCode = '';
      this.selectedSiteName = '';
      
      //apis
        this.categoryDropdownList.getCategoryDropDownList('ran').subscribe(values => {
            this.loading = true;      
            console.log("values.data",values.data);
            let pairs = this.selectedCategoryDDL;
            console.log("pairs ID",pairs.Id);
            console.log("pairs Order",pairs.Order);
            console.log("this.selectedCategory after select",this.selectedCategory);
            console.log("this.selectedCategoryOrder after select",this.selectCategoryOrder);
            //this.categoryid =parseInt(this.selectedCategory);
            this.selectedCat = this.getCategoryName(pairs.Order);//3
            this.ClassificationName = this.getCategoryName(pairs.Order);//3

            console.log("this.selectedCat ",this.selectedCat); 
            console.log("this.selectedCat ",this.selectedCategory);                      

            this.category_list= new CategoryList;
            this.category_list.DropDownCategoryList=[];

            this.category_list.DropDownCategoryList=<CategoryItem[]>values.data;
          
            this.category_dropdown_list=[];

            this.category_dropdown_list=this.categoryDropdownList.getRanCategoryDropDownListParsing(this.category_list);
            
            this.attributeTree.GetRANAttributeTree(this.ClassificationName).subscribe(values => {                       
            
            this.Attributenodes = <TreeNode[]>[values.data];
                

            this.neTypes.GetRANNeTypes(this.ClassificationName).subscribe(values => {
                    this.Typenodes = <TreeNode[]>[values.data];
                    this.subCategoryTree.GetSubCategoryTree(this.ClassificationName).subscribe(values => {
                        console.log("fffffffffffffffffffffff",values.data);
                        this.SubCategoryNodes = <TreeNode[]>[values.data];
                        this.loading = false;                              
                    });          
                });                                
            });                                
        });
    }

    getCategoryName(id: string): string {
      let categoryid=parseInt(id);
      console.log("categoryid ",categoryid);
      console.log("this.category_dropdown_list ",this.category_dropdown_list);
      this.selectedCategory=this.category_dropdown_list[categoryid-1].value.Id;
        return this.category_dropdown_list[categoryid-1].label;       
    }

    OnAttributeSelect(event){
        console.log("Selected Node", event.node);
        console.log("testTreeAttribute", this.Attributenodes);
        console.log("selectedAttributes", this.selectedAttributes);

        let NodeExist:TreeNode;
        NodeExist = this.selectedAttributes.filter(function (node) { return (node.parent.data.TabelName === event.node.parent.data.TabelName) && (node.data.columnName === event.node.data.columnName); })[0];
        if(NodeExist == null)
        {
          this.selectedAttributes.push(NodeExist);
        }    
    }

    AttributeSelect(event,typeName:string){
      this.TreeType="AttributeTree";

      if(this.selectedAttribute==null){
        this.selectedAttribute=[];
      }  
      if(event.node.leaf)
      {
        let AttributeData:SelectedAttributeData=new SelectedAttributeData();
        AttributeData=this.GetSelectedLeafData(event.node,typeName);
        debugger;
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
        this.AddSearchCriteriaToDialog(event.node,typeName);
        this.display_Parent_Selection=true;
      }
    }

    nodeSelect(event,typeName:string)
    {
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
    AddSearchCriteriaToDialog(Child:TreeNode,typeName:string)
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
          this.AddSearchCriteriaToDialog(child,typeName);
        }
      }
    }
  

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
        //console.log(data);
      
    }

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
      console.log("this.selectedAllAttributeTree",this.selectedAllAttributeTree);
    }
   
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
    SearchingAttribute(){
      let Label:string =this.SearchAttributeInput;
      var ref = { exist: false };
      var ref2 = { cutTree: null};
      this.TraversalTree(Label, ref, this.OrginalAttributenodes, ref2); 
      if(ref.exist)//found node
      {
         this.Attributenodes = [];
         this.Attributenodes.push(Object.assign({}, ref2.cutTree));
         this.expandAll(this.Attributenodes);
      }
      else //not found node
      {
          this.Attributenodes=Object.assign({}, this.OrginalAttributenodes);
          this.expandAll(this.Attributenodes);
      }
    }

    CloseDialog(){//reset dialog list
      this.childrenSelectedItem=[];
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

ShowSearchCriteria(event){ 
  // *ngIf="attributeShow;"
  $(event.target.parentNode.parentNode.nextElementSibling.nextElementSibling).fadeOut(function(){
    $(event.target.parentNode.parentNode.nextElementSibling).fadeIn();
  });
  
  //this.attributeShow=false;
  $(event.target.parentNode).addClass("elegantshadow");
  $(event.target.parentNode.nextElementSibling).removeClass("elegantshadow");
  // if (this.attributeShow)
  // this.attributeShow=false;
  // else{
  //   this.attributeShow=true;
  // }
  console.log("this.attributeShow",this.attributeShow);
}
ShowAttribute(event){
  $(event.target.parentNode.parentNode.nextElementSibling).fadeOut(function(){
    $(event.target.parentNode.parentNode.nextElementSibling.nextElementSibling).fadeIn();
  });
 
  //this.attributeShow=true;
  $(event.target.parentNode).addClass("elegantshadow");
  $(event.target.parentNode.previousElementSibling).removeClass("elegantshadow");
  
}

ClearSearchCriteriaList(){
  this.selectedAllSearchCriteriaTree=[];
  this.selectedColumns=[];
}
ClearAttributeList(){
  this.selectedAttribute=[];
  this.selectedAllAttributeTree=[];
}
}


