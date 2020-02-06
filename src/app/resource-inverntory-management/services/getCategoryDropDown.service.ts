import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { JsonResponse } from "../extraClasses/JsonResponse";
import { Observable } from "rxjs/Observable";
import { CategoryList } from "../extraClasses/DropDownCategory/CategoryList";

import { CategoryItem } from "../extraClasses/DropDownCategory/CategoryItem";
import { RIMGetDataService } from "./general services/RIMGetDataService.service";
import { Parameter } from "../extraClasses/RIMGetDataServiceClasses/ApiParameters";
import { AppConfiguration } from "../extraClasses/AppConfiguration";
import { SelectItem } from "../transmission/ne-dynamic-report/category-report/SelectItem";
import { SelectRanItem } from "../transmission/ne-dynamic-report/category-report/SelectRanItem";
import { Pairs } from "../transmission/ne-dynamic-report/category-report/Pairs";

@Injectable()

export class CategoryDropDownService{
    CategoryDropDownUrl: string;
    CategoriesUrl: string;

    constructor(private http: Http,private RimService:RIMGetDataService) { }
    
    getCategoryDropDownList(ModelName: string):Observable<JsonResponse>{
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.CategoryDropDown_ModelName,ModelName));
        return this.RimService.GetServerData(AppConfiguration.CategoryDropDown_URL,parameter,AppConfiguration.GetFunction,ModelName);
    }

    getAllCategories(ClassificationName: string):Observable<JsonResponse>{//GetAllSubCategoriesTree
        let parameter:Parameter[]=[];
        parameter.push(new Parameter(AppConfiguration.AttributeTree_classificationName,ClassificationName));
        return this.RimService.GetServerData(AppConfiguration.AllCategories_URL,parameter,AppConfiguration.GetFunction,ClassificationName);
    }

    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }

    getCategoryDropDownListParsing(Categories: CategoryList) : SelectItem[]
    {
        let listItems : SelectItem[];
        listItems = [];
        
       
        for(let Category of Categories.DropDownCategoryList)
        {

            let dropdownItem : SelectItem = new SelectItem(Category.Name, Category.Id);
            listItems.push(dropdownItem);
        }
        return listItems;
    }

    getRanCategoryDropDownListParsing(Categories: CategoryList) : SelectRanItem[]
    {
        let listItems : SelectRanItem[];
        listItems = [];
        
       
        for(let Category of Categories.DropDownCategoryList)
        {

            let dropdownItem : SelectRanItem = new SelectRanItem(Category.Name,new Pairs(Category.Id,Category.Order) );
            listItems.push(dropdownItem);
        }
        return listItems;
    }

}