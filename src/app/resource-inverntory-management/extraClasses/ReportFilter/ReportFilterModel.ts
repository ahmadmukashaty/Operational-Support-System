import { SiteInfo } from "./SiteInfo";
import { NETable } from "./NETable";
import { NEType } from "./NEType";


export class ReportFilterModel {
    
    CategoryName : string;
    SubCategory : string[];
    Tables : NETable[];
    Types : NEType[];
    SiteInfo : SiteInfo;
    
}