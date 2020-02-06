import { ValidateLevel } from "./ValidateLevel";//ValidateLevel

export class LevelsList{
    levels:ValidateLevel[];
    constructor (){
        this.levels=[];
        //NE Board SubBoard Port

        let SubBoard:ValidateLevel=new ValidateLevel("SubBoard",3);
        let NE :ValidateLevel=new ValidateLevel("NE",1);
        let SFP:ValidateLevel=new ValidateLevel("SFP",5);
        let Board: ValidateLevel=new ValidateLevel("Board",2);
        let Port:ValidateLevel=new ValidateLevel("Port",4);
        
        this.levels.push(NE);
        this.levels.push(Board);
        this.levels.push(SubBoard);
        this.levels.push(Port);
        this.levels.push(SFP);

        this.levels.sort((leftSide,rightSide):number =>{
            if(leftSide.order<rightSide.order) return -1;
            if(leftSide.order>rightSide.order) return 1;
        });
    }
}