import { Component, OnInit ,OnChanges, SimpleChanges, ChangeDetectorRef, Input, ViewRef, ViewChild, AfterViewInit, TemplateRef, ViewContainerRef,} from '@angular/core';
import '../../assets/js/jquery.isotope.js';
import '../../assets/js/tmStickUp.js';
import '../../assets/js/cbpFWTabs.js';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { PlatformLocation } from '@angular/common';
import { MenuItem } from 'primeng/components/common/menuitem';
import { AppConfiguration } from './extraClasses/AppConfiguration';
// import '../../assets/js/modernizr.custom.js';


declare var jquery: any;
declare var $: any;


@Component({
  selector: 'app-resource-inverntory-management',
  templateUrl: './resource-inverntory-management.component.html',
  styleUrls: ['./resource-inverntory-management.component.css']
})
export class ResourceInverntoryManagementComponent implements OnInit ,AfterViewInit {//,OnChanges
 // @Input()
  model:string;
  //@ViewChild('vc', {read: ViewContainerRef}) vc: ViewContainerRef;
  //@ViewChild('tpl', {read: TemplateRef}) tpl: TemplateRef<any>;
  homePage:boolean = true;
  items: MenuItem[];
  childViewRef: ViewRef;
  

  constructor(private route:ActivatedRoute,private _router: Router,location: PlatformLocation,private cdr: ChangeDetectorRef) {
    
    location.onPopState(() => {
      window.scrollTo(0, 0);
      window.location.reload();
      
    });

  }
  
  // ngOnChanges(changes: SimpleChanges) {
  //     console.log("navigate to new route");
  //     for (let propName in changes) {  
  //       if (propName === 'model') {
  //         console.log("here is parent, model to is: ",this.model);
  //       this.cdr.detectChanges();
     
  //           }
  //   }}
  
  ngOnInit() 
  {
      if(this._router.url.includes("Transmission") || this._router.url.includes("RAN"))
      {
        this.homePage = false;
        //this.childViewRef = this.tpl.createEmbeddedView(null);
      }
      else
      {
        this.homePage = true;
        
        this.items = [
          {label: 'Login', icon: 'fa fa-book',routerLink: ['Login']}, //, queryParams: {'recent': 'true'}
          {label: 'Profile', icon: 'fa fa-user',routerLink: ['Profile']}
        ];
      }
     
  }
  

  ngAfterViewInit()
  {
    

    if(this._router.url.includes("Transmission") || this._router.url.includes("RAN"))
    {
      this.homePage = false;
      //this.childViewRef = this.tpl.createEmbeddedView(null);
    }
    else
    {
      this.homePage = true;
      
    }

    if(this.homePage)
    {
      
      $('.st-container').css("height", $( window ).height() + 'px');
      $('.st-pusher').css("height", $( window ).height() + 'px');
      $('.st-content').css("height", $( window ).height() + 'px');


      $('#stuck_container').tmStickUp({});
      
      this.Filter();

      $("#loginSlideBtn").click(function(ev)
      {
        $("#st-container").addClass('st-effect-11');
        setTimeout( function() {
          $("#st-container").addClass('st-menu-open');
        }, 25 );
      });
  
  
      $('.st-pusher').click(function(){
        $("#st-container").removeClass('st-menu-open');
      });

    }

    // if(this.homePage==false){
    //   this.childViewRef = this.tpl.createEmbeddedView(null);
    // }
    
  }

  Filter(): boolean  {

    var $container = $('.portfolioContainer');
    var colW = 375;

    $container.isotope({
      // disable window resizing
      resizable: true,
      masonry: {
        columnWidth: colW
      }
    });
    
    $('.portfolioFilter .current').removeClass('current');
    $('.selectedITem').addClass('current');
    
    var selector = $('.selectedITem').attr('data-filter');
    $container.isotope({
  
        filter: selector,
     });
     return false;

    }

    Filter2(event): boolean  {

      
          console.log(event);
          var $container = $('.portfolioContainer');
          var colW = 375;
      
          $container.isotope({
            // disable window resizing
            resizable: true,
            masonry: {
              columnWidth: colW
            }
          });
      
          $('.portfolioFilter .current').removeClass('current');
          $('.selectedITem2').addClass('current');
      
          var selector = $('.selectedITem2').attr('data-filter');
          $container.isotope({
        
              filter: selector,
           });
           return false;
      
    }

    Filter3(): boolean  {
      
          var $container = $('.portfolioContainer');
          var colW = 375;
      
          $container.isotope({
            // disable window resizing
            resizable: true,
            masonry: {
              columnWidth: colW
            }
          });
      
          $('.portfolioFilter .current').removeClass('current');
          $('.selectedITem3').addClass('current');
      
          var selector = $('.selectedITem3').attr('data-filter');
          $container.isotope({
        
              filter: selector,
           });
           return false;
      
    }

    Filter4(): boolean  {
      
          var $container = $('.portfolioContainer');
          var colW = 375;
      
          $container.isotope({
            // disable window resizing
            resizable: true,
            masonry: {
              columnWidth: colW
            }
          });
      
          $('.portfolioFilter .current').removeClass('current');
          $('.selectedITem4').addClass('current');
      
          var selector = $('.selectedITem4').attr('data-filter');
          $container.isotope({
        
              filter: selector,
           });
           return false;
      
    }

    TransmissionClick(): void
    {
      this.model=AppConfiguration.ModelNameTransmission;
      console.log("test ok");
      this.homePage = false;

      this._router.navigate(['Transmission'], {relativeTo: this.route});
    }
    RANClick(): void
    {
      this.model=AppConfiguration.ModelNameRAN;
      console.log("test ok");
      this.homePage = false;

      this._router.navigate(['RAN'], {relativeTo: this.route});
    }

    // NavigateModelHandler(modelName:string){
    //   console.log("from parent : ",modelName);
    //   this.model=modelName;
    //   //this.reloadChildView();
    //   //this.ngOnInit();
    //   // this.cdr.detectChanges();
    //   // //this.cdr.markForCheck();
    //   // this.cdr.detach();
      

    // }

    // reloadChildView(){
    //   this.removeChildView();
    //   setTimeout(() =>{
    //     this.insertChildView();
    //   }, 3000);
    // }
    // removeChildView(){
    //   this.vc.detach();
    // }
    // insertChildView(){
    //   this.vc.insert(this.childViewRef);
    // }
    // ngAfterViewInit(){
    //   this.childViewRef = this.tpl.createEmbeddedView(null);
    // }
}

function newFunction(): string {
  return "imports-loader?$=jquery!./example.js";
}



