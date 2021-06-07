import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { JsonResponse } from "../extraClasses/JsonResponse";
import { Parameter } from "../extraClasses/RIMGetDataServiceClasses/ApiParameters";
import { AppConfiguration } from "../extraClasses/AppConfiguration";
import { RIMGetDataService } from "./general services/RIMGetDataService.service";








@Injectable()

export class AttributeTreeService {

    attributeTreeUrl: string;

    constructor(private http: Http,private RimService:RIMGetDataService) { }

    GetAttributeTree(ModelName:string,CategoryName:string): Observable<JsonResponse>  {//http://seserv112/RIM_API/api/GetAttributeTree?moduleName=transmission&categoryName=DataCom

        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.AttributeTree_moduleName,ModelName));//new Parameter(AppConfiguration.AttributeTree_categoryName,CategoryName);
        parameter.push(new Parameter(AppConfiguration.AttributeTree_categoryName,CategoryName));
        return this.RimService.GetServerData(AppConfiguration.AttributeTree_URL,parameter,AppConfiguration.GetFunction,ModelName);
   
    // this.attributeTreeUrl = 'http://seserv112/RIM_API/api/GetAttributes?categoryId=';
    
    // return this.http.get(this.attributeTreeUrl + categoryId)
    //                 .map(sub => sub.json() as JsonResponse)
    //                 .catch(this.handleError);
    }

    GetRANAttributeTree(classificationName:string): Observable<JsonResponse>  {//http://seserv112/RIM_API/api/GetAttributeTree?moduleName=transmission&categoryName=DataCom

        let parameter:Parameter[]=[];
      //  parameter.push(new Parameter(AppConfiguration.AttributeTree_moduleName,ModelName));//new Parameter(AppConfiguration.AttributeTree_categoryName,CategoryName);
        parameter.push(new Parameter(AppConfiguration.AttributeTree_classificationName,classificationName));
        return this.RimService.GetServerData(AppConfiguration.RAN_AttributeTree_URL,parameter,AppConfiguration.GetFunction,classificationName);

    }

    GetTransmissionDetailsTree(ModuleName:string): Observable<JsonResponse>  {

        let parameter:Parameter[]=[];
      //  parameter.push(new Parameter(AppConfiguration.AttributeTree_moduleName,ModelName));//new Parameter(AppConfiguration.AttributeTree_categoryName,CategoryName);
        parameter.push(new Parameter(AppConfiguration.AttributeTree_moduleName,ModuleName));
        return this.RimService.GetServerData(AppConfiguration.Transmission_DEtailsTree_URL,parameter,AppConfiguration.GetFunction,ModuleName);

    }

    GetRANDetailsTree(ModuleName:string): Observable<JsonResponse>  {

        let parameter:Parameter[]=[];
      //  parameter.push(new Parameter(AppConfiguration.AttributeTree_moduleName,ModelName));//new Parameter(AppConfiguration.AttributeTree_categoryName,CategoryName);
        parameter.push(new Parameter(AppConfiguration.AttributeTree_moduleName,ModuleName));
        return this.RimService.GetServerData(AppConfiguration.RAN_DEtailsTree_URL,parameter,AppConfiguration.GetFunction,ModuleName);

    }

 
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }


}