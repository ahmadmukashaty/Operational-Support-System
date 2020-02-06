import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { LevelTypes } from '../extraClasses/RanCharts/LevelTypes';
import { Observable } from 'rxjs/Observable';
import { CountTypesInput } from '../extraClasses/RanCharts/CountTypesInput';
import { NeTypes } from '../extraClasses/RanCharts/NeTypes';
import { BusynessInput } from '../extraClasses/RanCharts/BusynessInput';
import { BusynessReturnedData } from '../extraClasses/RanCharts/BusynessReturnedData';
import { CellsTypesObj } from '../extraClasses/RanCharts/CellsTypesObj';
import { HostVersionInput } from '../extraClasses/RanCharts/HostVersionInput';
import { HostVersionChart } from '../extraClasses/RanCharts/HostVersionChart';
import { HostVersionTypes } from '../extraClasses/RanCharts/HostVersionTypes';
import { CellsTypesCount } from '../extraClasses/RanCharts/CellsTypesCount';

@Injectable()
export class RAN_ChartsService {
    Url: string;
    constructor(private http: Http) { }
    get_NE_Levels(CatName: string): Observable<LevelTypes> {
        this.Url = 'http://seserv112/RIM_Charts/api/ChartLevels?ClassificationName=';
        // tslint:disable-next-line:max-line-length
        return this.http.get(this.Url + CatName + '&typeID=1')
            .map(sub => sub.json() as LevelTypes)
            .catch(this.handleError);
    }

    get_DBSource(): Observable<any> {
        this.Url = 'http://seserv112/RIM_API_ReportFilter/api/LocationSite';
        return this.http.get(this.Url)
            .map(sub => sub.json() as any)
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

    GetBusyness(Busyness_data_Obj: BusynessInput): Observable<BusynessReturnedData> {
        this.Url = 'http://seserv112/RIM_Charts/api/Busyness';
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        const body = JSON.stringify(Busyness_data_Obj);
        console.log("body in JSON format busyness", body);

        return this.http.post(this.Url, body, options)
            .map(sub => sub.json() as BusynessReturnedData)
            .catch(this.handleError);
    }

    GetCellsTypes(Cells_Types_Obj: CellsTypesObj): Observable<any>{
        this.Url = 'http://seserv112/RIM_Charts/api/CellCount';
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        const body = JSON.stringify(Cells_Types_Obj);
        console.log("body in JSON format cell type count", body);

        return this.http.post(this.Url, body, options)
            .map(sub => sub.json() as CellsTypesCount)
            .catch(this.handleError);
    }

    Get_Host_Version(Host_Version_Obj: HostVersionInput): Observable<HostVersionTypes>{
        this.Url = 'http://seserv112/RIM_Charts/api/HostVersionCount';
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        const body = JSON.stringify(Host_Version_Obj);
        console.log("body in JSON format host version", body);

        return this.http.post(this.Url, body, options)
            .map(sub => sub.json() as HostVersionTypes)
            .catch(this.handleError);
    }



    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}
