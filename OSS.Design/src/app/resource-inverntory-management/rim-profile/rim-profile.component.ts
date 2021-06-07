import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { trigger, state, style, transition, animate, AUTO_STYLE } from '@angular/animations';

@Component({
  selector: 'app-rim-profile',
  templateUrl: './rim-profile.component.html',
  styleUrls: ['./rim-profile.component.css'],
  animations: [
    trigger('mobileMenuTop', [
        state('no-block, void',
            style({
                overflow: 'hidden',
                height: '0px',
            })
        ),
        state('yes-block',
            style({
                height: AUTO_STYLE,
            })
        ),
        transition('no-block <=> yes-block', [
            animate('400ms ease-in-out')
        ])
    ])
  ]
})
export class RimProfileComponent implements OnInit {

  isCollapsedSideBar = 'no-block';
  
    constructor() { }
  
    ngOnInit() {
    }
  
    
    toggleOpenedSidebar() {
      this.isCollapsedSideBar = this.isCollapsedSideBar === 'yes-block' ? 'no-block' : 'yes-block';
      
    }

}
