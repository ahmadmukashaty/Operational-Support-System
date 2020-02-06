import { Injectable} from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {Observable} from 'rxjs/Observable';
import { JsonResponse } from '../extraClasses/JsonResponse';

@Injectable()

export class TreeService {

    treeUrl: string;

    constructor(private http: Http) { }

    GetTreeDetails(): Observable<JsonResponse>  {
    this.treeUrl = 'http://seserv112/RIM_API_EF/api/Tree';
    
    return this.http.get(this.treeUrl)
                    .map(sub => sub.json() as JsonResponse)
                    .catch(this.handleError);
    }

 
    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }


}