import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class TreeDataService {

  private NeColumns = new BehaviorSubject<string>("");;
  selectedColumns = this.NeColumns.asObservable();

  constructor() { }

  PushNeColumns(columns: string) {
    
    this.NeColumns.next(columns);
  }

}