import { DBSourceAttributes } from "./DBSourceAttributes";
import { CountLevelAttributes } from "./CountLevelAttributes";
import { LocationAttributes } from "./LocationAttributes";

export class BusynessInput {
    "ClassificationName": String;
    "LoctionValues": LocationAttributes;
    "LevelSource": CountLevelAttributes;
    "LevelDistination": CountLevelAttributes;
    "dbsource": DBSourceAttributes
}
