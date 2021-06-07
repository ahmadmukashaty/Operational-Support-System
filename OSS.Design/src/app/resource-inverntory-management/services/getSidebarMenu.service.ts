import { Injectable} from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {Observable} from 'rxjs/Observable';
import { JsonResponse } from '../extraClasses/JsonResponse';

@Injectable()

export class SidebarService {

    sidebarUrl: string;

    constructor(private http: Http) { }

    GetSidebarMenu(moduleBarId: number): Observable<JsonResponse>  {
    this.sidebarUrl = 'http://seserv112/RIM_API/API/DispalySidebarMenu?moduleBarId=';
    
    return this.http.get(this.sidebarUrl + moduleBarId)
                    .map(sub => sub.json() as JsonResponse)
                    .catch(this.handleError);
    }

 
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }


}