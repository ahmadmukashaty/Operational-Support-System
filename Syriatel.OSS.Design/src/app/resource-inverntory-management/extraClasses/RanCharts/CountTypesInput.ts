import { LocationAttributes } from "./LocationAttributes";
import { CountLevelAttributes } from "./CountLevelAttributes";
import { DBSourceAttributes } from "./DBSourceAttributes";

export class CountTypesInput {
    "ClassificationName": String;
    "LoctionValues": LocationAttributes;
    "Level": CountLevelAttributes;
    "dbsource": DBSourceAttributes

}
