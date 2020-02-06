export class SelectedColumn {
    columnName: string;
    columnType: string;
    Data:any;
  
    constructor(columnName:string, columnType:string,data:any) {
        this.columnName = columnName;
        this.columnType = columnType;
        this.Data=data;
    };

    public ContainIn(AllItems:SelectedColumn[]):boolean{
        for (let item of AllItems){
            if((item.columnName==this.columnName)&&(item.columnType==this.columnType)){
                return true;
            }
        }
        return false;

    }
}