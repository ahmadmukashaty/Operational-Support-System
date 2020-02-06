import { Site } from "./Site";
import { LocationSiteHierarchy } from "./LocationSiteHierarchy/LocationSiteHierarchy";
import { SiteLocationSource } from "./LocationSiteHierarchy/SiteLocationSource";

export class LocationSiteData {

    Region : string[];
    Area : string[];
    Zone : string[];
    SubArea : string[];
    Site : Site[];
    DataHierarchy : LocationSiteHierarchy;
    DB_Source: SiteLocationSource
  }