// tslint:disable-next-line:class-name
export class RanDataRows {
   /* columnName: any;
    value: any;
    disvalue: any;*/
    columnName: any;
    value: any;
    NEName: any;
    DisplayValue: any;
    ParentId: any;
    SId: any;
    SlId: any;
    PId: any;

    constructor(columnName:string,NEName: string, value: string, DisplayValue: string,ParentId: string,SID: string,SLID: string,PID: string) {
       /* this.columnName = name;
        this.value = val;
        this.disvalue = dval;*/
        this.columnName = columnName;
        this.value = value;
        this.NEName = NEName;
        this.DisplayValue = DisplayValue;
        this.ParentId = parent;
        this.SId = SID;
        this.SlId = SLID;
        this.PId = PID;
    }
}