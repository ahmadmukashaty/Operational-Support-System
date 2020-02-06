import { Injectable } from "@angular/core";
import { Http, RequestOptions, Headers } from "@angular/http";
import { JsonResponse } from "../extraClasses/JsonResponse";
import { Observable } from "rxjs/Observable";
import { ReportFilterModel } from "../extraClasses/ReportFilter/ReportFilterModel";
import { NEAttribute } from "../extraClasses/ReportFilter/NEAttribute";
import { ColumnItem } from "../extraClasses/ReportShow/ColumnItem";
import { ReportLevelFilterModel } from "../extraClasses/ReportLevelFilter/ReportLevelModel";
import { AdhocReportData } from "../extraClasses/DynamicReport/AdhocReportData";



@Injectable()

export class GenerateCategoryReportService {

    generateReportrUrl: string;
    RangenerateReportrUrl: string;

    constructor(private http: Http) { }

    // ReportCategoryRetunType data = null;
    //GetReportData(url,data1,data2)
    //{        
               //url = Constants.ServerName + url;
               //type = Constants.GET
               //Params = [];  
//             this.data = <ReportCategoryRetunType>RIMService.executive();
    //}

    GetReport(reportFilterModel: ReportFilterModel): Observable<JsonResponse>  {
    this.generateReportrUrl = 'http://seserv112/RIM_API_Report/api/Report/report1';
  //  this.RangenerateReportrUrl = 'http://seserv112/RIM_API_ReportFilter/api/GenerateAdhocCategoryReport';
    
    
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    const body = JSON.stringify(reportFilterModel);

    return this.http.post(this.generateReportrUrl, body, options)
                    .map(sub => sub.json() as JsonResponse)
                    .catch(this.handleError);
    }

    GetRanReport(AdhocReportData: AdhocReportData): Observable<JsonResponse>  {
    //    this.generateReportrUrl = 'http://seserv112/RIM_API_Report/api/Report/report1';
        this.RangenerateReportrUrl = 'http://seserv112/RIM_API_ReportFilter/api/GenerateAdhocCategoryReport';
        
        
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });
    
        const body = JSON.stringify(AdhocReportData);
    
        return this.http.post(this.RangenerateReportrUrl, body, options)
                        .map(sub => sub.json() as JsonResponse)
                        .catch(this.handleError);
        }

    GetGlobalReport(reportFilterModel: ReportLevelFilterModel): Observable<JsonResponse>  {
        this.generateReportrUrl = 'http://seserv112/RIM_API_Report/api/Report/report2';
        
        
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });
    
        const body = JSON.stringify(reportFilterModel);
    
        return this.http.post(this.generateReportrUrl, body, options)
                        .map(sub => sub.json() as JsonResponse)
                        .catch(this.handleError);
    }

    GetReportColumns(columns:NEAttribute[]) : ColumnItem[]
    {
        let cols:ColumnItem[] = [];

        for(let col of columns)
        {
            let colItem : ColumnItem = new ColumnItem(col.DisplayName,col.DisplayName);
            cols.push(colItem);
        }

        return cols;
    }

    GetReportLevelColumns(columns:string[]) : ColumnItem[]
    {
        let cols:ColumnItem[] = [];

        for(let col of columns)
        {
            let colItem : ColumnItem = new ColumnItem(col,col);
            cols.push(colItem);
        }

        return cols;
    }

    GetReportValues(data:string[][],columns:ColumnItem[]):any[]
    {
        let values:any[] = [];

        for(let i=0;i<data.length;i++)
        {
           var obj ={};
          
        //    var data = {};
        //    let test : any ="PropertyD";
        //    data[test] = 4;
        //    console.log(data);
           

            for(let j=0; j<data[i].length;j++)
            {
                let test: any = columns[j].field;
                obj[test] = data[i][j];
            }
            values.push(obj);
        }
        return values;

        // var data = {
        //     'PropertyA': 1,
        //     'PropertyB': 2,
        //     'PropertyC': 3
        // };

        // data["PropertyD"] = 4;

        // let testArry:any = [];
        // let Key:string = "name";
        // let Value:string = "ahmed";
        // testArry.push({ [Key]: Value});
        // console.log("dynamic object : ",testArry);
    }

 
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }


}