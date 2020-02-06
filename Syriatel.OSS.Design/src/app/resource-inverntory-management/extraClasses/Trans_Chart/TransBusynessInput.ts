import { LocationAttributes } from "../RanCharts/LocationAttributes";
import { CountLevelAttributes } from "../RanCharts/CountLevelAttributes";
import { DBSourceAttributes } from "../RanCharts/DBSourceAttributes";
import { LevelSourceType } from "./LevelSourceType";

export class TransBusynessInput {
    "ClassificationName": String;
    "LoctionValues": LocationAttributes;
    "LevelSource": CountLevelAttributes;
    "LevelSourceType": LevelSourceType;
    "LevelDistination": CountLevelAttributes;
    "dbsource": DBSourceAttributes
}
