import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { JsonResponse } from "../extraClasses/JsonResponse";
import { RIMGetDataService } from "./general services/RIMGetDataService.service";
import { AppConfiguration } from "../extraClasses/AppConfiguration";
import { Parameter } from "../extraClasses/RIMGetDataServiceClasses/ApiParameters";



@Injectable()

export class SubCategoryTreeService {

    //subCategoryTreeUrl: string;
    //public config:AppConfiguration;
    constructor(private http: Http,private RimService:RIMGetDataService) { }
    //http://seserv112/RIM_API/api/GetSubCategoryTree?moduleName=transmission&categoryName=DataCom
    GetSubCategoryTree(ClassificationName: string): Observable<JsonResponse>  {
        let parameter:Parameter[]=[];
     //   parameter.push(new Parameter(AppConfiguration.GetSubCategory_moduleName,model_name));
        parameter.push(new Parameter(AppConfiguration.AttributeTree_classificationName,ClassificationName));
     //   parameter.push(new Parameter(AppConfiguration.GetSubCategory_categoryName,categoryName));
        return this.RimService.GetServerData(AppConfiguration.GetSubCategoryTree_URL,parameter,AppConfiguration.GetFunction,ClassificationName);
        //return null;
    }

 
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }



}