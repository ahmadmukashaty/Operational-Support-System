import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { JsonResponse } from "../extraClasses/JsonResponse";
import { RIMGetDataService } from "./general services/RIMGetDataService.service";
import { AppConfiguration } from "../extraClasses/AppConfiguration";
import { Parameter } from "../extraClasses/RIMGetDataServiceClasses/ApiParameters";



@Injectable()

export class MapService {

    //subCategoryTreeUrl: string;
    //public config:AppConfiguration;
    constructor(private http: Http,private RimService:RIMGetDataService) { }
    //http://seserv112/RIM_API/api/GetSubCategoryTree?moduleName=transmission&categoryName=DataCom
    GetSitesMap(model_name:string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
      /*  parameter.push(new Parameter(AppConfiguration.GetSubCategory_moduleName,model_name));
        parameter.push(new Parameter(AppConfiguration.GetSubCategory_categoryName,categoryName));*/
        return this.RimService.GetServerData(AppConfiguration.MapService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }

    GetSitesMapbyRegion(model_name:string,Region: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.SelectedRegion,Region));
        console.log(AppConfiguration.SelectedRegion,Region);
       /* parameter.push(new Parameter(AppConfiguration.GetSubCategory_categoryName,categoryName));*/
        return this.RimService.GetServerData(AppConfiguration.MapbyRegionService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }

    GetSitesMapbyArea(model_name:string,Area: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.SelectedArea,Area));
        console.log(AppConfiguration.SelectedArea,Area);
       /* parameter.push(new Parameter(AppConfiguration.GetSubCategory_categoryName,categoryName));*/
        return this.RimService.GetServerData(AppConfiguration.MapbyAreaService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }

    GetSitesMapbyZone(model_name:string,Area: string,Zone: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.SelectedArea,Area));
        parameter.push(new Parameter(AppConfiguration.SelectedZone,Zone));
        console.log("Zone",Zone);
        return this.RimService.GetServerData(AppConfiguration.MapbyZoneService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }

    GetSitesMapbySubArea(model_name:string,Area: string,Zone: string,SubArea: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.SelectedArea,Area));
        parameter.push(new Parameter(AppConfiguration.SelectedZone,Zone));
        parameter.push(new Parameter(AppConfiguration.SelectedSubArea,SubArea));
        console.log("SubArea",SubArea);
        return this.RimService.GetServerData(AppConfiguration.MapbySubAreaService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }
    
    GetSitesMapbySiteName(model_name:string,Area: string,Zone: string,SubArea: string,SiteName: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.SelectedArea,Area));
        parameter.push(new Parameter(AppConfiguration.SelectedZone,Zone));
        parameter.push(new Parameter(AppConfiguration.SelectedSubArea,SubArea));
        parameter.push(new Parameter(AppConfiguration.SelectedSiteName,SiteName));
        console.log("SiteName",SiteName);
       /* parameter.push(new Parameter(AppConfiguration.GetSubCategory_categoryName,categoryName));*/
        return this.RimService.GetServerData(AppConfiguration.MapbySiteNameService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }

    GetSitesMapbySiteCode(model_name:string,Area: string,Zone: string,SubArea: string,SiteCode: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.SelectedArea,Area));
        parameter.push(new Parameter(AppConfiguration.SelectedZone,Zone));
        parameter.push(new Parameter(AppConfiguration.SelectedSubArea,SubArea));
        parameter.push(new Parameter(AppConfiguration.SelectedSiteCode,SiteCode));
        console.log("SiteCode",SiteCode);
       /* parameter.push(new Parameter(AppConfiguration.GetSubCategory_categoryName,categoryName));*/
        return this.RimService.GetServerData(AppConfiguration.MapbySiteCodeService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }

    GetSiteNameBySiteCode(model_name:string,SiteCode: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.SelectedSiteCode,SiteCode));
        console.log("SiteCode",SiteCode);
       /* parameter.push(new Parameter(AppConfiguration.GetSubCategory_categoryName,categoryName));*/
        return this.RimService.GetServerData(AppConfiguration.SiteNameByCodeService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }

    GetSiteCodeBySiteName(model_name:string,Area: string,Zone: string,SubArea: string,SiteName: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.SelectedArea,Area));
        parameter.push(new Parameter(AppConfiguration.SelectedZone,Zone));
        parameter.push(new Parameter(AppConfiguration.SelectedSubArea,SubArea));
        parameter.push(new Parameter(AppConfiguration.SelectedSiteName,SiteName));
        console.log("SiteName",SiteName);
       /* parameter.push(new Parameter(AppConfiguration.GetSubCategory_categoryName,categoryName));*/
        return this.RimService.GetServerData(AppConfiguration.SiteCodeByNameService_URL,parameter,AppConfiguration.GetFunction,model_name);
        //return null;
    }

 
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }



}