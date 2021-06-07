import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Categories } from '../extraClasses/Trans_Chart/Categories';
import { GetCats } from '../extraClasses/Trans_Chart/getCats';
import { CountTypesInput } from '../extraClasses/RanCharts/CountTypesInput';
import { NeTypes } from '../extraClasses/RanCharts/NeTypes';
import { TransBusynessInput } from '../extraClasses/Trans_Chart/TransBusynessInput';
import { TransmissionLevel } from '../extraClasses/Trans_Chart/TransmissionLevel';
import { TranBusyness_Confg } from '../extraClasses/Trans_Chart/TranBusyness_Confg';
import { BusynessReturnedData } from '../extraClasses/RanCharts/BusynessReturnedData';


@Injectable()
export class Trans_ChartService {
Url: string;
constructor(private http: Http) { }
get_NE_Cats(): Observable<GetCats> {
    this.Url = 'http://seserv112/RIM_API_ReportFilter/api/DropDownCategory?moduleName=transmission';
    // tslint:disable-next-line:max-line-length
    return this.http.get(this.Url)
        .map(sub => sub.json() as GetCats)
        .catch(this.handleError);
}

GetCountTypes(Count_Type_Obj: CountTypesInput): Observable<NeTypes> {
    this.Url = 'http://seserv112/RIM_Charts/api/TypeCount';
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    const body = JSON.stringify(Count_Type_Obj);
    console.log("body in JSON format count type ", body);

    return this.http.post(this.Url, body, options)
        .map(sub => sub.json() as NeTypes)
        .catch(this.handleError);
}

get_Busyness_Input(Cat: string): Observable<TranBusyness_Confg> {
    this.Url = 'http://seserv112/RIM_Charts/api/BuysinessTransmissionLevels?ClassificationName='+ Cat +'&TypeNum=5';
    // tslint:disable-next-line:max-line-length
    return this.http.get(this.Url)
        .map(sub => sub.json() as TranBusyness_Confg)
        .catch(this.handleError);
}

GetBusyness(tranBusyness_data_Obj: TransBusynessInput): Observable<BusynessReturnedData> {
    this.Url = 'http://seserv112/RIM_Charts/api/Busyness';
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    const body = JSON.stringify(tranBusyness_data_Obj);
    console.log("body in JSON format busyness", body);

    return this.http.post(this.Url, body, options)
        .map(sub => sub.json() as BusynessReturnedData)
        .catch(this.handleError);
}


private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
}
}
    