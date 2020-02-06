import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Parameter } from "../../extraClasses/RIMGetDataServiceClasses/ApiParameters";
import { Observable } from "rxjs/Observable";
import { JsonResponse } from "../../extraClasses/JsonResponse";
import { AppConfiguration } from "../../extraClasses/AppConfiguration";

@Injectable()

export class RIMGetDataService {
     //config: AppConfiguration=new AppConfiguration();
     FullURL:string; 
    
    

    constructor(private http: Http) {
        //RIMGetDataService.config=new AppConfiguration();
    }
     
    GetServerData(url:string, Parameters:Parameter[],TypeOfFunction:string,classificationName:string):Observable<JsonResponse>{
       if(Parameters==null){
            this.FullURL=AppConfiguration.ServerName+url;
            return this.http.get(this.FullURL,classificationName).map(sub => sub.json() as JsonResponse).catch(this.handleError);
       }
       else{
            if(TypeOfFunction==AppConfiguration.GetFunction){
                //get full url (http://seserv112/RIM_API/api/GetSubCategory?CategoryId=)
                this.FullURL=AppConfiguration.ServerName+url+AppConfiguration.GetParamDelemeter;
                let ParameterCount:number=Parameters.length;
                for(let param of Parameters){
                    this.FullURL=this.FullURL+param.ParamName+AppConfiguration.GetParamAssignParamDelemeter+param.ParamValue;
                    ParameterCount--;
                    if (ParameterCount!=0){
                        this.FullURL=this.FullURL+AppConfiguration.GetMoreParamDelemeter;
                    }
                    console.log("URL : ",this.FullURL);
                }
            }
            else if(TypeOfFunction==AppConfiguration.PostFunction){

            }

            return this.http.get(this.FullURL,classificationName).map(sub => sub.json() as JsonResponse).catch(this.handleError);
       }
     
        
       
    }
    
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }
    
}