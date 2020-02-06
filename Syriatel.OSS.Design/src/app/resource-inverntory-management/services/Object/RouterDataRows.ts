// tslint:disable-next-line:class-name
export class RouterDataRows {
    columnName: any;
    value: any;
    disvalue: any;

    constructor(name: string, val: string, dval: string) {
        this.columnName = name;
        this.value = val;
        this.disvalue = dval;
    }
}