import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { JsonResponse } from "../extraClasses/JsonResponse";
import { NeDetail } from "../extraClasses/GetNeDetails/NeDetail";

@Injectable()

export class GetNeDetailsService
{
    GetNeDetailsURL:string;

    constructor(private http: Http) { }

    GetNeDetailsData(category:string,type:string,Id:number): Observable<JsonResponse>
    {
        this.GetNeDetailsURL = 'http://seserv112/RIM_API/api/GetNeDetails?Category='+category+'&Type='+type+'&id='+Id;
    
        return this.http.get(this.GetNeDetailsURL)
                        .map(sub => sub.json() as JsonResponse)
                        .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> 
    {
        return Promise.reject(error.message || error);
    }

    GetDetailsValues(data:NeDetail[]):any[]
    {
        let values:any[] = [];
        
        for (let det of data){
            var obj ={};
            obj["Name"] = det.ColumnName;
            obj["Value"] = det.Value;

            values.push(obj);
        }        

        return values;
    }
     
}