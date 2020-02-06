import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { JsonResponse } from "../extraClasses/JsonResponse";
import { RIMGetDataService } from "./general services/RIMGetDataService.service";
import { Parameter } from "../extraClasses/RIMGetDataServiceClasses/ApiParameters";
import { AppConfiguration } from "../extraClasses/AppConfiguration";


@Injectable()

export class NeTypesService {

    NeTypeseUrl: string;

    constructor(private http: Http,private RimService:RIMGetDataService) { }

    GetNeTypes(categoryName: string,model_name:string): Observable<JsonResponse>  {
    // this.NeTypeseUrl = 'http://seserv112/RIM_API/api/GetNeTypes?categoryId='; seserv112/RIM_API/api/GetCategoryTypeTree?moduleName=transmission&categoryName=DataCom 
                                                                            //   seserv112/RIM_API/api/GetCategoryTypeTree?moduleName=transmission&categoryName=DATACOM
    
    // return this.http.get(this.NeTypeseUrl + categoryId)
    //                 .map(sub => sub.json() as JsonResponse)
    //                 .catch(this.handleError);
    let parameter:Parameter[]=[];
    parameter.push(new Parameter(AppConfiguration.GetNeTypesService_moduleName,model_name));
    parameter.push(new Parameter(AppConfiguration.GetNeTypesService_categoryName,categoryName));
    return this.RimService.GetServerData(AppConfiguration.GetNeTypesService_URL,parameter,AppConfiguration.GetFunction,model_name);   
    }

    GetRANNeTypes(classificationName:string): Observable<JsonResponse>  {

        let parameter:Parameter[]=[];
     //   parameter.push(new Parameter(AppConfiguration.GetNeTypesService_moduleName,model_name));
        parameter.push(new Parameter(AppConfiguration.AttributeTree_classificationName,classificationName));
        return this.RimService.GetServerData(AppConfiguration.GetRANNeTypesService_URL,parameter,AppConfiguration.GetFunction,classificationName);   
        }

    // http://seserv112/RIM_API/api/GetAllCategoriesNeTypes?moduleName=transmission
    GetAllNeTypes(model_name:string): Observable<JsonResponse>  {
        // this.NeTypeseUrl = 'http://seserv112/RIM_API/api/GetAllNeTypes';
        
        // return this.http.get(this.NeTypeseUrl)
        //                 .map(sub => sub.json() as JsonResponse)
        //                 .catch(this.handleError);
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.GetNeTypesService_moduleName,model_name));
        return this.RimService.GetServerData(AppConfiguration.GetAllNeTypesService_URL,parameter,AppConfiguration.GetFunction,model_name);   
       
    }

 
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }


}