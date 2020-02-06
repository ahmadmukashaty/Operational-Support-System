import { Component, OnInit, ViewChild, ViewEncapsulation, ElementRef } from '@angular/core';
import { TreeDataService } from '../../services/TreeData.service';
import { trigger, state, style, transition, animate, AUTO_STYLE } from '@angular/animations';
import { TreeModule } from 'primeng/primeng';
import { TreeNode } from 'primeng/primeng';
import { Optional } from '@angular/core';
import { NodeService } from '../../services/getNETree.service';
import { MatDialogConfig, MatDialogRef, MatDialog } from '@angular/material';

import { ActivatedRoute, Router } from '@angular/router';
import { SelectItem } from 'primeng/components/common/selectitem';
import { Observable } from 'rxjs/Observable';
import { MenuItem } from 'primeng/components/common/api';
import { GetNeDetailsService } from '../../services/GetNeDetails.service';
import { NeAllDetails } from '../../extraClasses/GetNeDetails/NeAllDetails';
import { NeDetail } from '../../extraClasses/GetNeDetails/NeDetail';
import { filter } from 'rxjs/operator/filter';
import { element } from 'protractor';
import { Subscription } from 'rxjs/Subscription';
import { DATACOM_NE } from '../../services/Object/NE';
import { RouterDataRows } from '../../services/Object/RouterDataRows';
import { TreeObjectSearch } from '../../extraClasses/treeObjectSearch';
import { AttributeTreeService } from '../../services/attributeTree.service';
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-ne-details',
  providers: [NodeService],
  templateUrl: './ne-details.component.html',
  styleUrls: ['./ne-details.component.css'],
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
        animate('800ms ease-in-out')
      ])
    ])
  ],
  encapsulation: ViewEncapsulation.None
})
export class NeDetailsComponent {
  categories: TreeNode[];

  RouterData: RouterDataRows[];
  BoardData: RouterDataRows[];
  SubBoardData: RouterDataRows[];
  PortData: RouterDataRows[];
  SfpData: RouterDataRows[];

  Data: any;
  DataB: any[];
  DataS: any[];
  DataP: any[];
  DataSF: any[];

  cols: any[] = [];
  cols1: any[] = [];
  cols2: any[] = [];
  cols3: any[] = [];
  cols4: any[] = [];
  SearchInput: string;
  private OrginalTypenodes: TreeNode[] = null;
  displayRouterAttr: boolean;
  displayBoardAttr: boolean;
  displaySubBoardAttr: boolean;
  displayPortAttr: boolean;
  displaysfpAttr: boolean;

  displayMWBoardAttr: boolean;
  displayMWPortAttr: boolean;
  displayMWPSfpAttr: boolean;

  displayOPSubRackAttr: boolean;
  displayOPBoardAttr: boolean;
  displayOPPortAttr: boolean;
  displayOPSfpAttr: boolean;

  displayFWBoardAttr: boolean;
  displayFWSubBoardAttr: boolean;
  displayFWPortAttr: boolean;

  loading: boolean;
  private items: MenuItem[];
  home: MenuItem;

  constructor(private nodeService: NodeService, private detailsTree:AttributeTreeService) {
    this.categories = [];
  }

  // Router Data
  getRouter(data: any) {
    this.RouterData = [];
    let GridRouter: RouterDataRows[] = [];
    let keyNames = Object.keys(data);
    for (let element of keyNames) {
      let f = data[element];
      GridRouter.push({ columnName: element, value: f, disvalue: null });
      
      this.RouterData = GridRouter;
     
    }
console.log("Router Data: ", this.RouterData);

  }
  //Board Data
  getBoard(data: any) {
    this.BoardData = [];
    let GridBoard: RouterDataRows[] = [];

    for (let index = 0; index < data.length; index++) {
      GridBoard.push({
        columnName: data[index].ParentId,
        value: data[index].Id,
        disvalue: data[index].DisName
      });
      this.BoardData = GridBoard;
    }


  }
  //SubBoard Data
  getSubBoard(data: any) {
    this.SubBoardData = [];
    let GridSubBoard: RouterDataRows[] = [];
    for (let index = 0; index < data.length; index++) {
      GridSubBoard.push({
        columnName: data[index].ParentId,
        value: data[index].Id,
        disvalue: data[index].Name

      });
      this.SubBoardData = GridSubBoard;
    }
  }


  //PortData
  getPort(data: any) {
    this.PortData = [];
    let GridPort: RouterDataRows[] = [];

    for (let index = 0; index < data.length; index++) {
      GridPort.push({
        columnName: data[index].Name,
        value: data[index].Id,
        disvalue: data[index].ParentId
      });
      this.PortData = GridPort;
    }
  }

  //SFPData
  getSfp(data: any) {
    this.SfpData = [];
    let GridSFP: RouterDataRows[] = [];
    for (let index = 0; index < data.length; index++) {
      GridSFP.push({
        columnName: data[index].Name,
        value: data[index].Id,
        disvalue: data[index].DisName

      });
      this.SfpData = GridSFP;
    }
  }

  //MWBoard Data
  getMWBoard(data: any) {
    this.BoardData = [];
    let GridBoard: RouterDataRows[] = [];

    for (let index = 0; index < data.length; index++) {
      GridBoard.push({
        columnName: data[index].Name,
        value: data[index].Id,
        disvalue: data[index].DisName
      });
      this.BoardData = GridBoard;
    }


  }
  private BreadCrumpRecursive(node: TreeNode) {
    const obj: any = {};
    if (node) {
      obj.label = node.label;
      this.BreadCrumpRecursive(node.parent);
      this.items.push(obj);
    }
  }

  ngOnInit() {
    this.loading = true;
    // setTimeout(() => {
    //   this.categories = this.nodeService.fillRoot(1); this.loading = false;
    // }, 15000);

    // this.OrginalTypenodes = [];
    // this.OrginalTypenodes.push(Object.assign({}, this.categories[0]));

    console.log("Transmission TREE ",this.categories);
    this.detailsTree.GetTransmissionDetailsTree("Transmission").subscribe(values => {  
      this.categories = <TreeNode[]>[values.data];
      console.log("Transmission TREE ",this.categories);
      this.loading = false;

    });

    this.OrginalTypenodes = [];
    this.OrginalTypenodes.push(Object.assign({}, this.categories[0]));
    console.log("this.Typenodes ",this.OrginalTypenodes);

    this.cols1 = [{ field: 'columnName', header: 'Name' },
    { field: 'value', header: 'Value' }];
    this.cols = [{ field: 'columnName', header: 'Slot Id' },
    { field: 'disvalue', header: 'Serial Number' }];
    this.cols2 = [{ field: 'columnName', header: 'SubSlot Id' },
    { field: 'disvalue', header: 'Serial Number' }];
    this.cols3 = [{ field: 'columnName', header: 'Port Name' },
    { field: 'disvalue', header: 'Port Id' }];
    this.cols4 = [{ field: 'columnName', header: 'SFP Name' },
    { field: 'disvalue', header: 'SFP Type' }];

    this.RouterData = [];

    this.items = [
      { label: 'Transmission' },
    ];

    this.home = { icon: 'fa fa-home' };
    
  }
  

  // Clear Filter
  ClearF() {
    this.categories = [];
    this.categories = this.nodeService.fillRoot(1);
  }

  // Tree Node Select
  nodeSelect(event) {

    // BreadCrump
    this.items = [];
    this.BreadCrumpRecursive(event.node);

   
    if (event.node.type === 'Router') {
      this.nodeService.getRouterDetails(event.node.data).subscribe(result => {
        this.Data = result[0];
        this.getRouter(this.Data);
        this.loading = false;
      });
    } else if (event.node.type === 'Board') {
      this.nodeService.getBoardsBy_Ne(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getBoard(this.DataB);
        this.loading = false;
      });
    } else if (event.node.type === 'subBoard') {
      this.nodeService.getSubBoardsBy_NE(event.node.data).subscribe(res => {
        this.DataS = res;
        this.getSubBoard(this.DataS);
        this.loading = false;
      });
    } else if (event.node.type === 'Port') {
      this.nodeService.getPortsBy_NE(event.node.data).subscribe(res => {
        this.DataP = res;
        this.getPort(this.DataP);
        this.loading = false;
      });
    } else if (event.node.type === 'Sfp') {
      this.nodeService.getSfpsBy_NE(event.node.data).subscribe(res => {
        this.DataSF = res;
        this.getSfp(this.DataSF);
        this.loading = false;
      });
      //MW
    } else if (event.node.type === 'MWRouter') {
      this.nodeService.getMWNEDetails(event.node.data).subscribe(result => {
        this.Data = result[0];
        //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
        this.getRouter(this.Data);
        this.loading = false;
      });
    } else if (event.node.type === 'MBoard') {
      this.nodeService.getAllMWBoardsByNE(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getMWBoard(this.DataB);
        this.loading = false;
      });
    } else if (event.node.type === 'MPort') {
      this.nodeService.getAllMWPortsByNE(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getPort(this.DataB);
        this.loading = false;
      });
    } else if (event.node.type === 'MSfp') {
      this.nodeService.getAllMWSfpsByNE(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getSfp(this.DataB);
        this.loading = false;
      });
      // Optical
    } else if (event.node.type === 'OPRouter') {
      this.nodeService.getOpticalNEDetails(event.node.data).subscribe(result => {
        this.Data = result[0];
        //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
        this.getRouter(this.Data);
        this.loading = false;
      });
    } else if (event.node.type === 'OsubRack') {
      this.nodeService.getAllOpticalSubRackByNE(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getMWBoard(this.DataB);
        this.loading = false;
      });
    } else if (event.node.type === 'OBoard') {
      this.nodeService.getAllOpticalBoardByNE(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getBoard(this.DataB);
        this.loading = false;
      });
    } else if (event.node.type === 'OPort') {
      this.nodeService.getAllOpticalPortsByNE(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getPort(this.DataB);
        this.loading = false;
      });
    } else if (event.node.type === 'OSfp') {
      this.nodeService.getAllOpticalSfpsByNE(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getSfp(this.DataB);
        this.loading = false;
      });
      // Firewall
    } else if (event.node.type === 'FRouter') {
      this.nodeService.getFirewallNEDetails(event.node.data).subscribe(result => {
        this.Data = result[0];
        //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
        this.getRouter(this.Data);
        this.loading = false;
      });
    } else if (event.node.type === 'FBoard') {
      this.nodeService.getAllFirewallBoardsByNE(event.node.data).subscribe(res => {
        this.DataB = res;
        this.getBoard(this.DataB);
        this.loading = false;
      });
    } else if (event.node.type === 'FsubBoard') {
      this.nodeService.getAllFirewallSubBoardsByNE(event.node.data).subscribe(res => {
        this.DataS = res;
        this.getSubBoard(this.DataS);
        this.loading = false;
      });
    } else if (event.node.type === 'FPort') {
      this.nodeService.getAllFirewallPortsByNE(event.node.data).subscribe(res => {
        this.DataP = res;
        this.getPort(this.DataP);
        this.loading = false;
      });
    } else { this.loading = false; }

    // Display P-datatable
    if (event.node.type === 'Router' || event.node.type === 'MWRouter' || event.node.type === "OPRouter" || event.node.type === "FRouter") {
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;

    }
    else if (event.node.type === 'Board') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = true;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'subBoard') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = true;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'Port') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = true;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'Sfp') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = true;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'MBoard') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = true;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'MPort') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = true;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'MSfpPort') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = true;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'OsubRack') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = true;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'OBoard') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = true;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'OPort') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = true;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'OSfp') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = true;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'FBoard') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = true;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'FsubBoard') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = true;
      this.displayFWPortAttr = false;
    }
    else if (event.node.type === 'FPort') {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = true;
    }
    else {
      this.displayRouterAttr = false;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
    }
  }

  //Datacom
  // Board Details
  navigateToDetails(id: number) {
    this.nodeService.getBoardDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }

  // Sub Board Details
  navigateSubBoardToDetails(id: number) {
    this.nodeService.getSubBoardDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);

      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  // Port Details
  navigatePortToDetails(id: number) {
    this.nodeService.getPortDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  // SFP Details
  navigateSfpToDetails(id: number) {
    debugger;
    this.nodeService.getSFPDetails(id).subscribe(result => {
      this.Data = result[0];
      this.getRouter(this.Data);

      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }


  //MW
  //MWBoard
  navigateMWBoardToDetails(id: number) {
    this.nodeService.getMWBoardDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  navigateMWPortToDetails(id: number) {
    this.nodeService.getMWPortDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  navigateMWsfpToDetails(id: number) {
    this.nodeService.getMWSFpDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.loading = false;
    });
  }

  //Optical
  navigateOPSubRackToDetails(id: number) {
    this.nodeService.getOpticalSubRackDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  navigateOPBoardToDetails(id: number) {
    this.nodeService.getOpticalBoardDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  navigateOpPortToDetails(id: number) {
    this.nodeService.getOpticalPortDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  //navigateOPsfpToDetails
  navigateOPsfpToDetails(id: number) {
    this.nodeService.getOpticalSFPDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }

  //Firewall
  navigateFWBoardToDetails(id: number) {
    // debugger;
    this.nodeService.getFirewallBoardDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  navigateFWSubBoardToDetails(id: number) {
    // debugger;
    this.nodeService.getFirewallSubBoardDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }
  navigateFWPortToDetails(id: number) {
    // debugger;
    this.nodeService.getFirewallPortDetails(id).subscribe(result => {
      this.Data = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRouter(this.Data);
      this.displayRouterAttr = true;
      this.displayBoardAttr = false;
      this.displaySubBoardAttr = false;
      this.displayPortAttr = false;
      this.displaysfpAttr = false;
      this.displayMWBoardAttr = false;
      this.displayMWPortAttr = false;
      this.displayMWPSfpAttr = false;
      this.displayOPSubRackAttr = false;
      this.displayOPBoardAttr = false;
      this.displayOPPortAttr = false;
      this.displayOPSfpAttr = false;
      this.displayFWBoardAttr = false;
      this.displayFWSubBoardAttr = false;
      this.displayFWPortAttr = false;
      this.loading = false;
    });
  }


  SearchingNode(event) {
    let Label: string = this.SearchInput;
    var ref = { exist: false };
    var ref2 = { cutTree: null };
    // debugger;
    
    this.TraversalTree(Label, ref, this.categories, ref2);
    if (ref.exist)//found node
    {
      this.categories = [];
      this.categories.push(Object.assign({}, ref2.cutTree));
      this.expandAll1(this.categories);
    }
    else //not found node
    {
      this.categories = Object.assign({}, this.categories);
      this.expandAll1(this.categories);
    }
  }
  

  TraversalTree(lable: string, ref: { exist: boolean }, orginalTree: TreeNode[], ref2: { cutTree: TreeNode }) {
    if (lable.length === 0) {
      ref.exist = true;
      ref2.cutTree = Object.assign({}, orginalTree[0]);
    }
    else {
      if (!ref.exist) {
        for (let child of orginalTree) {
          if (ref.exist) {
            break;
          }
          if (child.label.toLowerCase().includes(lable.toLocaleLowerCase())) {
            ref.exist = true;
            ref2.cutTree = Object.assign({}, child);
            continue;
          }
          if (!ref.exist) {
            if (child.children != null) {
              this.TraversalTree(lable, ref, child.children, ref2);
              if (ref.exist) {
                let tempTree: TreeNode;
                tempTree = Object.assign({}, child);
                tempTree.children = [];
                tempTree.children.push(ref2.cutTree);
                ref2.cutTree = tempTree;
              }
            }
          }
        }
      }
    }
  }

  expandAll1(Tree: TreeNode[]) {
    Tree.forEach(node => {
      this.expandRecursive(node, true);
    });

  }

  expandAll() {
    this.categories.forEach(node => {
      this.expandRecursive(node, true);
    });
  }

  collapseAll() {
    this.categories.forEach(node => {
      this.expandRecursive(node, false);
    });
  }
  private expandRecursive(node: TreeNode, isExpand: boolean) {
    node.expanded = isExpand;
    if (node.children) {
      node.children.forEach(childNode => {
        this.expandRecursive(childNode, isExpand);
      });
    }
  }

  // tree collapse
  isCollapsedSideBar2 = 'yes-block';
  toggleOpenedSidebar2(event) {
    this.isCollapsedSideBar2 = this.isCollapsedSideBar2 === 'yes-block' ? 'no-block' : 'yes-block';
    const elm = event.srcElement.classList;
    if (elm.contains('fa-angle-double-up')) {
      $('#narrow2').removeClass('fa-angle-double-up');
      $('#narrow2').addClass('fa-angle-double-down');
    }
    else {
      $('#narrow2').removeClass('fa-angle-double-down');
      $('#narrow2').addClass('fa-angle-double-up');
    }

  }
}



