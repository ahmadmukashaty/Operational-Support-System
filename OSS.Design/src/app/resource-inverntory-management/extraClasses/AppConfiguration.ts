import { TabDetails } from "./TabDetails";
import { Http } from "@angular/http";
import { SubCategoryTreeService } from "../services/getSubCategory.service";

export class AppConfiguration{
    //model names
    public static ModelNameTransmission:string="Transmission";//Transmission
    public static ModelNameRAN:string="RAN";

    //Ran Classification
    public static RadioClassification:string="Radio";
    public static ControllerClassification:string="Controller";

    public static TransmissionClassification:string="Transmission";
    public static DatacomClassification:string="Datacom";

    // design configuration
    public static ModelBackgroundImgClassName_theme1:string = "trans-img-ngClass";
    public static ModelBackgroundImgClassName_theme2:string="ran-img-ngClass";

    public static ModelTabsClassName_theme1:string = "trans_cssmenu";
    public static ModelTabsClassName_theme2:string = "ran_cssmenu";

    public static ModelNavigationEvenBlock_theme1:string = "theme1_evenStyle";
    public static ModelNavigationOddBlock_theme1:string = "theme1_oddStyle";
    
    public static ModelNavigationEvenBlock_theme2:string="theme2_evenStyle";
    public static ModelNavigationOddBlock_theme2:string="theme2_oddStyle";
 //---------------------------------------------------------------------------------------   
    public static ModelProfileMainMenu_theme1:string = "trans_main-menu-header";
    public static ModelProfile_theme1:string="trans_prof";
    public static ModelProfileMoreDetails_theme1:string="trans_more-details";
 //---------------------------------------------------------------------------------------   
    public static ModelProfileMainMenu_theme2:string = "ran_main-menu-header";
    public static ModelProfile_theme2:string="ran_prof";
    public static ModelProfileMoreDetails_theme2:string="ran_more-details";
 //---------------------------------------------------------------------------------------   
    public static TransmissionTabs: TabDetails[]=[ new TabDetails("Reports","Reports"), new TabDetails("Charts","Charts"),new TabDetails("NE-Details","Details")];//, new TabDetails("Tree","PrimTree"),new TabDetails("NE-Details","Details"),, new TabDetails("Tree","PrimTree")
    public static RANTabs: TabDetails[]=[new TabDetails("Reports","RanReports"), new TabDetails("Charts","RanCharts"),new TabDetails("NE-Details","RanDetails"),new TabDetails("Maps","RanMaps")];
    
    //general api configuration:
    public static GetFunction:string="Get";
    public static PostFunction:string="Post";
    public static GetParamDelemeter:string="?";
    public static GetParamAssignParamDelemeter:string="=";
    public static GetMoreParamDelemeter:string="&";
    public static ServerName:string="http://seserv112/";
    //SubCategoryTreeService service configuration
    public static GetSubCategoryTree_URL:string="RIM_API_ReportFilter/api/GetSubCategoryTree";
    public static GetSubCategory_moduleName:string="moduleName";
    public static GetSubCategory_categoryName:string="categoryName";
    //SiteLocationService configuration
    public static SiteLocationService_URL:string="RIM_API_ReportFilter/API/LocationSite";

    //MapService configuration
    public static MapService_URL:string="RIM_API_ReportFilter/api/SiteMapInfo";
    public static MapbyRegionService_URL:string="RIM_API_ReportFilter/api/SiteMapInfo/ByRegion";
    public static MapbyAreaService_URL:string="RIM_API_ReportFilter/api/SiteMapInfo/ByArea";
    public static MapbyZoneService_URL:string="RIM_API_ReportFilter/api/SiteMapInfo/ByZone";
    public static MapbySubAreaService_URL:string="RIM_API_ReportFilter/api/SiteMapInfo/BySubArea";
    public static MapbySiteNameService_URL:string="RIM_API_ReportFilter/api/SiteMapInfo/BySiteName";
    public static MapbySiteCodeService_URL:string="RIM_API_ReportFilter/api/SiteMapInfo/BySiteCode";
    public static SiteNameByCodeService_URL:string="RIM_API_ReportFilter/api/SiteNameByCode";
    public static SiteCodeByNameService_URL:string="RIM_API_ReportFilter/api/SiteCodeByName";


    //NeTypesService configuration
      public static GetNeTypesService_URL:string="RIM_API_ReportFilter/api/GetCategoryTypeTree"; //RIM_API/api/GetNeTypes
      public static GetRANNeTypesService_URL:string="RIM_API_ReportFilter/api/GetCategoryTypeTree";

    public static GetNeTypesService_moduleName:string="moduleName";//"categoryId";//
    public static GetNeTypesService_categoryName:string="categoryName";//categoryName
    public static GetAllNeTypesService_URL:string="RIM_API_ReportFilter/api/GetAllCategoriesNeTypes"; //http://seserv112/RIM_API/api/GetCategoryTypeTree?moduleName=Transmission&categoryName=MW
                                                                                //http://seserv112/RIM_API/api/GetCategoryTypeTree?moduleName=transmission&categoryName=DataCom
    //CategoryDropDownService configuration
    public static CategoryDropDown_ModelName:string="moduleName";
    public static CategoryDropDown_URL:string="RIM_API_ReportFilter/api/DropDownCategory";
    //public static AllCategories_URL:string="RIM_API_ReportFilter/api/GetAllSubCategoriesTree";
    public static AllCategories_URL:string="RIM_API_ReportFilter/api/GetSubCategoryTree";
    
    //AttributeTreeService configuration
    public static AttributeTree_URL:string= "RIM_API_ReportFilter/api/GetAttributeTree";//"RIM_API/api/GetAttributes"; 
    public static RAN_AttributeTree_URL:string= "RIM_API_ReportFilter/api/GetAttributeTree";//"RIM_API/api/GetAttributes"; 
    public static RAN_DEtailsTree_URL:string= "RIM_API_ReportFilter/api/GetDetailsTree";//"RIM_API/api/GetAttributes"; 
    public static Transmission_DEtailsTree_URL:string= "RIM_API_ReportFilter/api/GetTransDetailsTree";//"RIM_API/api/GetAttributes";

    public static AttributeTree_moduleName:string="moduleName";
    public static AttributeTree_categoryName:string="categoryName";
    public static AttributeTree_classificationName:string="ClassificationName";

    //location filter configuration
    public static AllLocation:string="All";
    public static SelectedRegion:string="Region";
    public static SelectedArea:string="Area";
    public static SelectedZone:string="Zone";
    public static SelectedSubArea:string="SubArea";
    public static SelectedSiteCode:string="SiteCode";
    public static SelectedSiteName:string="SiteName";

    //Types of search criteria in report
    public static SubCategory:string="SubCategory";
    public static NEType:string="NEType";
    public static Attribute:string="Attribute";
    
    //levels array
    public static Levels:string[]=["Port","SubBoard","Board","NE"];//NE Board SubBoard Port


    //dialog messages
    public static addSearchCriteriaBody:string="Are You Sure you want to choose All below searching criteria";
    public static WrongGlobalFilterationTypeLevelBody:string="wrong filteration! 'You have to choose filter type level suitable with level filteration'";
    public static WrongGlobalFilterationTypeCategoryBody:string="wrong filteration! 'You have to choose filter type suitable with Category/SubCategory filteration'";
    public static WrongGlobalFilterationCategoryBody:string="You have to choose 2 or more Category, otherwise please navigate to Category Report!";
}


 
