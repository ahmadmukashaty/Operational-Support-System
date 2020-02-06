import { SelectItem } from "../../transmission/ne-dynamic-report/category-report/SelectItem";




export class FilterModel{ 
    area_list:SelectItem[];
    zone_list:SelectItem[];
    subarea_list:SelectItem[];
    sitecode:SelectItem [];
    sitename:SelectItem[];

    constructor() { 
        this.area_list= [];
        this.sitecode=[];
        this.sitename=[];
    }
};