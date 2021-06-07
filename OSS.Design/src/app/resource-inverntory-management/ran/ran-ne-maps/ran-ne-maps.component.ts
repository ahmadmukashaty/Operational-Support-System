import { Component, OnInit ,NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AgmCoreModule } from '@agm/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GenerateCategoryReportService } from '../../services/generateFirstReport.service';
import { AppConfiguration } from '../../extraClasses/AppConfiguration';
import { MapService } from '../../services/Map.service';
import { SiteLocation } from '../../extraClasses/Map/SiteLocation';
import { SiteLocationService } from '../../services/getSiteInfo.service';
import { SelectItem } from './SelectItem';
import { LocationSiteData } from '../../extraClasses/LocationSiteData';
import { trigger, state, style, transition, animate, AUTO_STYLE } from '@angular/animations';
import { Area } from '../../extraClasses/LocationSiteHierarchy/Area';
import { Zone } from '../../extraClasses/LocationSiteHierarchy/Zone';
import { SubArea } from '../../extraClasses/LocationSiteHierarchy/SubArea';
import { Site } from '../../extraClasses/Site';
import { FilterModel } from '../../extraClasses/LocationSiteHierarchy/FilterModel';
import { SelectedColumn } from '../../extraClasses/ReportShow/SelectedColumn';

declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-ran-ne-maps',
  templateUrl: './ran-ne-maps.component.html',
  styleUrls: ['./ran-ne-maps.component.css'],
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
export class RanNeMapsComponent implements OnInit {

  lat: number = 34.7363222;
  lng: number = 36.7123919;
  lat1: number = 33.489064;
  lng1: number = 36.339596;
  mapType : string = 'hybrid';

  ModelName:string;
  selectedCat: string = 'Radio';
  SiteInfo : LocationSiteData;
  site_list:SiteLocation;

  loading: boolean;

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
  tempRegions : Area[];
  tempAreas : Zone[];
  tempZones : SubArea[];
  tempSubAreas : Site[];
  model:FilterModel;
  RegionTempSiteCode:SelectItem[];
  RegionTempSiteName:SelectItem[];
  AreaTempSiteCode:SelectItem[];
  AreaTempSiteName:SelectItem[];
  ZoneTempSiteCode:SelectItem[];
  ZoneTempSiteName:SelectItem[];
  selectedColumns:SelectedColumn [];
  selectedAllSearchCriteriaTree:SelectedColumn []=[];
  
  constructor(private route: ActivatedRoute, private _router: Router,private map: MapService,private siteLocation:SiteLocationService) { }

  ngOnInit() {

    if(this._router.url.includes(AppConfiguration.ModelNameTransmission)){
      this.ModelName=AppConfiguration.ModelNameTransmission;
    }
    else if(this._router.url.includes(AppConfiguration.ModelNameRAN)){
      this.ModelName=AppConfiguration.ModelNameRAN;
    }

    this.siteLocation.GetSiteLocationData(this.ModelName).subscribe(values => {   
      this.SiteInfo =  (<LocationSiteData>values.data);
      console.log("this.SiteInfo",this.SiteInfo);
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
      });  
      
  }

      ConvertString(value){
          return parseFloat(value)
      }

      DrawSites(){
        this.loading = true;

        this.map.GetSitesMap(this.selectedCat).subscribe(values => {
          this.site_list = new SiteLocation();
          this.site_list = <SiteLocation>values.data;
          console.log("this.site_list",this.site_list);
 
          });

      setTimeout(() => {
        this.loading = false;
      }, 15000);

    }

      ClearSites()
      {
        this.site_list = null;
      }

      isCollapsedSideBar = 'yes-block';
      toggleOpenedSidebar(event) {
        //debugger;
        this.isCollapsedSideBar = this.isCollapsedSideBar === 'yes-block' ? 'no-block' : 'yes-block';
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
          console.log('this.model',this.model);
          this.Area=this.model.area_list;
          this.SitesCodes=this.model.sitecode;
          this.SitesNames=this.model.sitename;
          //tempRegioncode and name
          this.RegionTempSiteCode=this.SitesCodes;
          this.RegionTempSiteName=this.SitesNames;
          this.map.GetSitesMapbyRegion(this.selectedCat,this.selectedRegion).subscribe(values => {
            this.site_list = new SiteLocation();
            this.site_list = <SiteLocation>values.data;
            console.log("this.site_list from specific region",this.site_list);
            });
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
          this.map.GetSitesMap(this.selectedCat).subscribe(values => {
            this.site_list = new SiteLocation();
            this.site_list = <SiteLocation>values.data;
            console.log("this.site_list from All region",this.site_list);
            });

          console.log('tempRegion',this.tempRegions);
          //console.log( this.SitesCodes);
          //console.log(this.SitesNames);
          
        }
    /*    //for selection grid
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
        }*/

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

              this.map.GetSitesMap(this.selectedCat).subscribe(values => {
                this.site_list = new SiteLocation();
                this.site_list = <SiteLocation>values.data;
                console.log("this.site_list from All region & All Area",this.site_list);
                });
              
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

              this.map.GetSitesMapbyArea(this.selectedCat,this.selectedArea).subscribe(values => {
                this.site_list = new SiteLocation();
                this.site_list = <SiteLocation>values.data;
                console.log("this.site_list from All region & specific Area",this.site_list);
                });
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

              this.map.GetSitesMapbyRegion(this.selectedCat,this.selectedRegion).subscribe(values => {
                this.site_list = new SiteLocation();
                this.site_list = <SiteLocation>values.data;
                console.log("this.site_list from specific region",this.site_list);
                });
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

              this.map.GetSitesMapbyArea(this.selectedCat,this.selectedArea).subscribe(values => {
                this.site_list = new SiteLocation();
                this.site_list = <SiteLocation>values.data;
                console.log("this.site_list from specific region & specific Area",this.site_list);
                });
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

            this.map.GetSitesMapbyArea(this.selectedCat,this.selectedArea).subscribe(values => {
              this.site_list = new SiteLocation();
              this.site_list = <SiteLocation>values.data;
              console.log("this.site_list from null region & specific Area",this.site_list);
              });
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

            this.map.GetSitesMapbyRegion(this.selectedCat,this.selectedRegion).subscribe(values => {
              this.site_list = new SiteLocation();
              this.site_list = <SiteLocation>values.data;
              console.log("this.site_list from null region & All Area",this.site_list);
              });
            //console.log( this.SitesCodes);
            //console.log(this.SitesNames);
          }
        }
        
     /*   //for selection grid
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
        }*/
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

          this.map.GetSitesMapbyZone(this.selectedCat,this.selectedArea,this.selectedZone).subscribe(values => {
            this.site_list = new SiteLocation();
            this.site_list = <SiteLocation>values.data;
            console.log("this.site_list from specific Area & specific Zone",this.site_list);
            });
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

          this.map.GetSitesMapbyArea(this.selectedCat,this.selectedArea).subscribe(values => {
            this.site_list = new SiteLocation();
            this.site_list = <SiteLocation>values.data;
            console.log("this.site_list from specific Area & All Zone",this.site_list);
            });
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

          this.map.GetSitesMapbySubArea(this.selectedCat,this.selectedArea,this.selectedZone,this.selectedSubArea).subscribe(values => {
            this.site_list = new SiteLocation();
            this.site_list = <SiteLocation>values.data;
            console.log("this.site_list from specific Area & specific Zone & specific SubArea",this.site_list);
            });
          //console.log( this.SitesCodes);
          //console.log(this.SitesNames);
        }
        else { //SubArea is All
          this.selectedSiteCode='';
          this.selectedSiteName='';
          this.SitesCodes=this.ZoneTempSiteCode;
          this.SitesNames=this.ZoneTempSiteName;

          this.map.GetSitesMapbyZone(this.selectedCat,this.selectedArea,this.selectedZone).subscribe(values => {
            this.site_list = new SiteLocation();
            this.site_list = <SiteLocation>values.data;
            console.log("this.site_list from specific Area & specific Zone & All SubArea",this.site_list);
            });
          //console.log( this.SitesCodes);
          //console.log(this.SitesNames);
        }
        
   /*     //for selection grid
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
        }*/
    }

        SiteCodeSelect(event){
          this.map.GetSitesMapbySiteCode(this.selectedCat,this.selectedArea,this.selectedZone,this.selectedSubArea,this.selectedSiteCode).subscribe(values => {
            this.site_list = new SiteLocation();
            this.site_list = <SiteLocation>values.data;
            console.log("this.site_list from specific Area & specific Zone & specific SubArea & specific SiteCode",this.site_list);
            });

            this.map.GetSiteNameBySiteCode(this.selectedCat,this.selectedSiteCode).subscribe(values => {
              this.selectedSiteName = <string>values.data;
              console.log("this.selectedSiteName",this.selectedSiteName);
              });

           // this.selectedSiteName = "COBRA";
           // this.selectedSiteName=this.SitesNames[0].value;
        }
    
        SiteNameSelect(event){
          this.map.GetSitesMapbySiteName(this.selectedCat,this.selectedArea,this.selectedZone,this.selectedSubArea,this.selectedSiteName).subscribe(values => {
            this.site_list = new SiteLocation();
            this.site_list = <SiteLocation>values.data;
            console.log("this.site_list from specific Area & specific Zone & specific SubArea & specific SiteName",this.site_list);
            });

            this.map.GetSiteCodeBySiteName(this.selectedCat,this.selectedArea,this.selectedZone,this.selectedSubArea,this.selectedSiteName).subscribe(values => {
              this.selectedSiteCode = <string>values.data;
              console.log("this.selectedSiteCode",this.selectedSiteName);
              });
        }

}
  