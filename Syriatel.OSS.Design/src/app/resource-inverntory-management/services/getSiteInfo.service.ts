import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { JsonResponse } from "../extraClasses/JsonResponse";

import { Site } from "../extraClasses/Site";
import { LocationSiteHierarchy } from "../extraClasses/LocationSiteHierarchy/LocationSiteHierarchy";
import { Region } from "../extraClasses/LocationSiteHierarchy/Region";
import { Area } from "../extraClasses/LocationSiteHierarchy/Area";
import { Zone } from "../extraClasses/LocationSiteHierarchy/Zone";
import { SubArea } from "../extraClasses/LocationSiteHierarchy/SubArea";
import { FilterModel } from "../extraClasses/LocationSiteHierarchy/FilterModel";
import { RIMGetDataService } from "./general services/RIMGetDataService.service";
import { AppConfiguration } from "../extraClasses/AppConfiguration";
import { SelectItem } from "../transmission/ne-dynamic-report/category-report/SelectItem";



@Injectable()

export class SiteLocationService {

    SiteLocationUrl: string;

    constructor(private http: Http,private RimService:RIMGetDataService) { }

    GetSiteLocationData(model_name:string): Observable<JsonResponse>  {
    // this.SiteLocationUrl = 'http://seserv112/RIM_API/API/LocationSite';
    
    // return this.http.get(this.SiteLocationUrl)
    //                 .map(sub => sub.json() as JsonResponse)
    //                 .catch(this.handleError);
    //let parameter:Parameter[]=[];
    //parameter.push(new Parameter(AppConfiguration.GetSubCategory_CategoryId,categoryId));
    return this.RimService.GetServerData(AppConfiguration.SiteLocationService_URL,null,AppConfiguration.GetFunction,model_name);
    }

 
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }

    GetSiteLocationDropdownParsing(Items: string[]) : SelectItem[]
    {
        let listItems : SelectItem[];
        listItems = [];
        let first :SelectItem=new SelectItem('All','All');
        listItems.push(first);
        for(let item of Items)
        {
            let dropdownItem : SelectItem = new SelectItem(item,item);
            listItems.push(dropdownItem);
        }
        return listItems;
    }
    GetSitecode(sites: Site[]) : SelectItem[]
    {
        let listItems : SelectItem[];
        listItems = [];
        for(let site of sites)
        {
            let dropdownItem : SelectItem = new SelectItem(site.CODE,site.CODE);
            listItems.push(dropdownItem);
        }
        return listItems;
    }

    GetSiteName(sites: Site[]) : SelectItem[]
    {
        let listItems : SelectItem[];
        listItems = [];
        for(let site of sites)
        {
            let dropdownItem : SelectItem = new SelectItem(site.ENGLISH_NAME,site.ENGLISH_NAME);
            listItems.push(dropdownItem);
        }
        return listItems;
    }

    GetTempRegion(SelectedRegion:string, DataHierarchy:LocationSiteHierarchy) :Area[]{
        let AllAreas:Area[]; AllAreas=[];
        if(!((SelectedRegion ==null) || (SelectedRegion=='All'))){
         
        
            for(let region of DataHierarchy.Regions){
                if(region.REGION == SelectedRegion){
                    return region.Areas;
                }
            }
        }
        else{//All or null
            for (let region of DataHierarchy.Regions){
                for(let area of region.Areas){
                    AllAreas.push(area)
                }
                
            }
            return AllAreas;
        }
    }
    GetFilteredArea(Areas :Area []):FilterModel{
        let model :FilterModel =new FilterModel();
        model.area_list = [];
        model.sitecode=[];
        model.sitename=[];
        model.area_list.push(new SelectItem('All','All'));
        for(let area of Areas){
            let dropdownItem : SelectItem = new SelectItem(area.AREA,area.AREA);
            
            model.area_list.push(dropdownItem);
            for(let zone of area.Zones){
                for(let subarea of zone.SubAreas){
                    for(let site of subarea.Sites){
                        let dropdownItem1 : SelectItem = new SelectItem(site.CODE,site.CODE);
                        model.sitecode.push(dropdownItem1);
                        let dropdownItem2 : SelectItem = new SelectItem(site.ENGLISH_NAME,site.ENGLISH_NAME);
                        model.sitename.push(dropdownItem2);
                    }
                }
            }
            
        }
        console.log("model",model);
        return model;
    }

    GetTempArea(SelectedArea:string, Areas:Area[]) :Zone[]{
        for(let area of Areas){
            if(area.AREA == SelectedArea){
                return area.Zones;
            }
        }
    }

    GetFilteredZone(Zones :Zone[]):FilterModel{
        let model :FilterModel =new FilterModel();
        model.zone_list = [];
        model.sitecode=[];
        model.sitename=[];
        model.zone_list.push(new SelectItem('All','All'));
        for(let zone of Zones){
            let dropdownItem : SelectItem = new SelectItem(zone.ZONE,zone.ZONE);
            
            model.zone_list.push(dropdownItem);
            for(let subarea of zone.SubAreas){
                    for(let site of subarea.Sites){
                        let dropdownItem1 : SelectItem = new SelectItem(site.CODE,site.CODE);
                        model.sitecode.push(dropdownItem1);
                        let dropdownItem2 : SelectItem = new SelectItem(site.ENGLISH_NAME,site.ENGLISH_NAME);
                        model.sitename.push(dropdownItem2);
                    }
                
            }
            
        }
        console.log("model",model);
        return model;
    }

    GetTempZone(SelectedZone:string, Zones:Zone[]) :SubArea[]{
        for(let zone of Zones){
            if(zone.ZONE == SelectedZone){
                return zone.SubAreas;
            }
        }
    }

    GetFilteredSubArea( SubAreas:SubArea[]):FilterModel{
        let model :FilterModel =new FilterModel();
        model.subarea_list = [];
        model.sitecode=[];
        model.sitename=[];
        model.subarea_list.push(new SelectItem('All','All'));
        for(let subarea of SubAreas){
            let dropdownItem : SelectItem = new SelectItem(subarea.SUBAREA,subarea.SUBAREA);
            
            model.subarea_list.push(dropdownItem);
                    for(let site of subarea.Sites){
                        let dropdownItem1 : SelectItem = new SelectItem(site.CODE,site.CODE);
                        model.sitecode.push(dropdownItem1);
                        let dropdownItem2 : SelectItem = new SelectItem(site.ENGLISH_NAME,site.ENGLISH_NAME);
                        model.sitename.push(dropdownItem2);
                    }
        }
        console.log("model",model);
        return model;
    }
    
    GetTempSubArea(SelectedSubArea:string, SubAreas:SubArea[]) :Site[]{
        for(let subarea of SubAreas){
            if(subarea.SUBAREA == SelectedSubArea){
                return subarea.Sites;
            }
        }
    }

    GetFilteredSite( Sites:Site[]):FilterModel{
        let model :FilterModel =new FilterModel();
        model.sitecode=[];
        model.sitename=[];
        
                    for(let site of Sites){
                        let dropdownItem1 : SelectItem = new SelectItem(site.CODE,site.CODE);
                        model.sitecode.push(dropdownItem1);
                        let dropdownItem2 : SelectItem = new SelectItem(site.ENGLISH_NAME,site.ENGLISH_NAME);
                        model.sitename.push(dropdownItem2);
                    }
       
        console.log("model",model);
        return model;
    }

}