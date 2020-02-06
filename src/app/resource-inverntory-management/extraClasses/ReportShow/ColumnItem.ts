export class ColumnItem {
    field: string;
    header: string;
    // styleClass?: string;
    // icon?: string;
    // title?: string;

    /**
     *
     */
    constructor(field:string, header:string) {
        this.field = field;
        this.header = header;
    };
}