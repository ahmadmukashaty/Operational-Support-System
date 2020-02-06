export class ValidateLevel {
    level:string;
    order:number;
    chosen:boolean;
    constructor(level:string,order:number){
        this.level=level;
        this.order=order;
        this.chosen=false;
    }
}