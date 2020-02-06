import { Category } from "./Category";
import { NEType } from "../ReportFilter/NEType";
import { SiteInfo } from "../ReportFilter/SiteInfo";


export class ReportLevelFilterModel {
    
    Categories_name  : Category[];
    Types : NEType[];
    SiteInfo : SiteInfo;
    Levels : string;
    
}