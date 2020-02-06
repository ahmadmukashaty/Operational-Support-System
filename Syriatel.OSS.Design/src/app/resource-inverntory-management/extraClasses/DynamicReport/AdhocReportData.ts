import { SelectClause } from "./SelectClause";
import { WhereClause } from "./WhereClause";

export class AdhocReportData {
  //  ModelName   : string;
   // CategoryName   : string;
    ClassificationName : string;
    SelectClauses  : SelectClause[];
    WhereClauses  : WhereClause[];
  }