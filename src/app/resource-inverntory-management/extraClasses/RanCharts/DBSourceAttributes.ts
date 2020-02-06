import { CountLevelAttributes } from "./CountLevelAttributes";
import { SiteSourceAttributres } from "./SiteSourceAttributres";

export class DBSourceAttributes {
    "RegionSource": CountLevelAttributes;
    "AreaSource": CountLevelAttributes;
    "ZoneSource": CountLevelAttributes;
    "SubAreaSource": CountLevelAttributes;
    "SiteSource": SiteSourceAttributres;
}
