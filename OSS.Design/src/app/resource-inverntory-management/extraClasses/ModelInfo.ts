import { TabDetails } from "./TabDetails";
import { AppConfiguration } from "./AppConfiguration";

export class ModelInfo {
    //ImgURL : string="../../../assets/images/final.png";
    public BackgroundImg_css_class:string;
    public Tabs_css_class:string;
    public tabs: TabDetails[];
    public ModelName:string;
    public ProfileMainMenu:string;
    public Profile:string;
    public ProfileMoreDetails:string;
    public NavigationEven:string;
    public NavigationOdd:string;
    public config:AppConfiguration=new AppConfiguration();
    

    constructor(backgroundImgClassName:string,TabsClassName:string,model_name:string) {
      
        this.BackgroundImg_css_class=backgroundImgClassName;
        this.Tabs_css_class=TabsClassName;
        console.log("modelName:",model_name);
        this.ModelName=model_name;
        if(this.ModelName=="Transmission"){
            this.tabs = AppConfiguration.TransmissionTabs;            
        }
        else if(this.ModelName=="RAN"){
            this.tabs=AppConfiguration.RANTabs;           
        }
    }

    public SetProfileInfo(main_menu_css_class:string,profile_css_class:string,details_menu_css_class:string){
        this.ProfileMainMenu=main_menu_css_class;
        this.Profile=profile_css_class;
        this.ProfileMoreDetails=details_menu_css_class;
    }

    public SetnavigationInfo(nav_even_css_class:string,nav_odd_css_class:string){
        this.NavigationEven=nav_even_css_class;
        this.NavigationOdd=nav_odd_css_class;
    }
}
