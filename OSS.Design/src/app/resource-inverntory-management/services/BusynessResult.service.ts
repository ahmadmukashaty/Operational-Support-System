import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Observable';
import { BusynessRes } from '../extraClasses/Result';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { DataComTypes } from '../extraClasses/DataComTypes';
import { ElementType } from '../extraClasses/ElementType';

@Injectable()
export class BusynessResultService {
    BusyUrl: string;
    public showreport: boolean;
    constructor(private http: Http) { }

    getBusynessSer(SearchBy: string, SearchAtt: string, SearchWith: string, Source: string, Dest: string): Observable<BusynessRes>  {
       this.BusyUrl = 'http://seserv112/RIM_Busy/api/busynessper?searchby=';
      // tslint:disable-next-line:max-line-length
      return this.http.get(this.BusyUrl + SearchBy + '&searchatt=' + SearchAtt + '&searchwith=' + SearchWith + '&source=' + Source + '&dest=' + Dest)
                     .map(sub => sub.json() as BusynessRes[])
                     .catch(this.handleError);

    }

    getDataComTypes(SearchAtt: string, SearchWith: string): Observable<DataComTypes[]>  {
      this.BusyUrl = 'http://seserv112/RIM_Busy/api/datacomtypes?searchatt=';
     // tslint:disable-next-line:max-line-length
     return this.http.get(this.BusyUrl + SearchAtt + '&searchwith=' + SearchWith )
                    .map(sub => sub.json() as DataComTypes[])
                    .catch(this.handleError);

  }

  getElementTypes(SearchBy: string, SearchAtt: string, SearchWith: string, Source: string, Dest: string): Observable<ElementType[]>  {
    this.BusyUrl = 'http://seserv112/RIM_Busy/api/ElementCount?searchby=';
   // tslint:disable-next-line:max-line-length
   return this.http.get(this.BusyUrl + SearchBy + '&searchatt=' + SearchAtt + '&searchwith=' + SearchWith + '&source=' + Source + '&dest=' + Dest )
                  .map(sub => sub.json() as ElementType[])
                  .catch(this.handleError);

}

    private handleError(error: any): Promise<any> {
      console.error('An error occurred', error); // for demo purposes only
      return Promise.reject(error.message || error);
    }

}
