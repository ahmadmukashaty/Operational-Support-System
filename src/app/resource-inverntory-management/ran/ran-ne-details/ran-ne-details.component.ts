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
import { RanDataRows } from '../../services/Object/RanDataRows';
import { AttributeTreeService } from '../../services/attributeTree.service';
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-ran-ne-details',
  templateUrl: './ran-ne-details.component.html',
  styleUrls: ['./ran-ne-details.component.css'],
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

export class RanNeDetailsComponent implements OnInit {

  categories: TreeNode[];

  RanData: RanDataRows[];
  RackData: RanDataRows[];
  SubRackData: RanDataRows[];
  SlotData: RanDataRows[];
  BoardData: RanDataRows[];
  PortData: RanDataRows[];
  IdentitiesData: RanDataRows[];
  CellData : RanDataRows[];
  AntennaData : RanDataRows[];
  HostVersionData : RanDataRows[];

  DataRanNe: any;
  DataRack: any[];
  DataSubRack: any[];
  DataSlot: any[];
  DataBoard: any[];
  DataPort: any[];

  ControlllerColunmns: any[] = [];
  ControlllerCols: any[] = [];
  ControlllerCols1: any[] = [];
  ControlllerCols2: any[] = [];
  ControlllerCols3: any[] = [];
  ControlllerCols4: any[] = [];
  ControlllerHostVersion: any[] = [];

  SiteColunmns: any[] = [];
  SiteCols: any[] = [];
  SiteCols1: any[] = [];
  SiteCols2: any[] = [];
  SiteCols3: any[] = [];
  SiteCols4: any[] = [];
  SiteIdentity: any[] = [];
  SiteCells: any[] = [];
  SiteAntennas: any[] = [];
  SiteHostVersion: any[] = [];

  SearchInput: string;
  private OrginalTypenodes: TreeNode[] = null;

  displayRadioNEAttr: boolean;
  displayRadioRackAttr: boolean;
  displayRadioSubRackAttr: boolean;
  displayRadioSlotAttr: boolean;
  displayRadioBoardAttr: boolean;
  displayRadioPortAttr: boolean;
  displayRadioIdentityAttr :boolean;
  displayRadioCellsAttr :boolean;
  displayRadioAntennasAttr :boolean;
  displayRadioHostVersionAttr :boolean;

  displayRanNEAttr: boolean;
  displayRanRackAttr: boolean;
  displayRanSubRackAttr: boolean;
  displayRanSlotAttr: boolean;
  displayRanBoardAttr: boolean;
  displayRanPortAttr: boolean;
  displayRanHostVersionAttr: boolean;

  loading: boolean;
  private items: MenuItem[];
  home: MenuItem;

  siteCandidateId : number;

  constructor(private nodeService: NodeService,private detailsTree:AttributeTreeService) {
    this.categories = [];
  }

  // Ran NE Data
  getRanNe(data: any) {
    this.RanData = [];
    let GridRouter: RanDataRows[] = [];
    let keyNames = Object.keys(data);
    for (let element of keyNames) {
      let f = data[element];
      GridRouter.push({ columnName: element, value: f, NEName: null,DisplayValue: null,ParentId: null,SId: null,SlId: null,PId: null });

      this.RanData = GridRouter;

    }
       console.log("RanNe Data2: ", this.RanData);
    
  }

  //Rack Data
  getRack(data: any) {
    this.RackData = [];
    let GridBoard: RanDataRows[] = [];

    for (let index = 0; index < data.length; index++) {
      GridBoard.push({
        columnName: data[index].Name,
        value: data[index].Id,
        NEName:data[index].NEName,
        DisplayValue:data[index].DisplayValue,
        ParentId: data[index].ParentId,
        SId:data[index].SID,
        SlId:data[index].SLID,
        PId:data[index].PID
      });
      this.RackData = GridBoard;
    }
    console.log("RackData: ", this.RackData);
  }

  //SubRack Data
  getSubRack(data: any) {
    this.SubRackData = [];
    let GridSubBoard: RanDataRows[] = [];
    for (let index = 0; index < data.length; index++) {
      GridSubBoard.push({
        columnName: data[index].Name,
        value: data[index].Id,
        NEName:data[index].NEName,
        DisplayValue:data[index].DisplayValue,
        ParentId: data[index].ParentId,
        SId:data[index].SId,
        SlId:data[index].SlId,
        PId:data[index].PId

      });
      this.SubRackData = GridSubBoard;
    }
    console.log("SubRackData: ", this.SubRackData);
  }

  //Slot Data
  getSlot(data: any) {
    this.SlotData = [];
    let GridPort: RanDataRows[] = [];

    for (let index = 0; index < data.length; index++) {
      GridPort.push({
        columnName: data[index].Name,
        value: data[index].Id,
        NEName:data[index].NEName,
        DisplayValue:data[index].DisplayValue,
        ParentId: data[index].ParentId,
        SId:data[index].SId,
        SlId:data[index].SlId,
        PId:data[index].PId
      });
      this.SlotData = GridPort;
    }
    console.log("SlotData: ", this.SlotData);
  }

  //Board Data
  getBoard(data: any) {
    this.BoardData = [];
    let GridSFP: RanDataRows[] = [];
    for (let index = 0; index < data.length; index++) {
      GridSFP.push({
        columnName: data[index].Name,
        value: data[index].Id,
        NEName:data[index].NEName,
        DisplayValue:data[index].DisplayValue,
        ParentId: data[index].ParentId,
        SId:data[index].SId,
        SlId:data[index].SlId,
        PId:data[index].PId

      });
      this.BoardData = GridSFP;
    }
    console.log("BoardData: ", this.BoardData);
  }

  //Port Data
  getPort(data: any) {
    this.PortData = [];
    let GridBoard: RanDataRows[] = [];

    for (let index = 0; index < data.length; index++) {
      GridBoard.push({
        columnName: data[index].Name,
        value: data[index].Id,
        NEName:data[index].NEName,
        DisplayValue:data[index].DisplayValue,
        ParentId: data[index].ParentId,
        SId:data[index].SId,
        SlId:data[index].SlId,
        PId:data[index].PId
      });
      this.PortData = GridBoard;
    }
    console.log("PortData: ", this.PortData);
  }

    //HostVersion Data
    getHostVersion(data: any) {
      this.HostVersionData = [];
      let GridHostVersion: RanDataRows[] = [];

      for (let index = 0; index < data.length; index++) {
        GridHostVersion.push({
          columnName: data[index].Name,
          value: data[index].Id,
          NEName:data[index].NEName,
          DisplayValue:data[index].DisName,
          ParentId: data[index].ParentId,
          SId:data[index].SId,
          SlId:data[index].SlId,
          PId:data[index].PId
        });
        this.HostVersionData = GridHostVersion;
      }
      console.log("HostVersionData: ", this.HostVersionData);
    }

    //Antenna Data
    getAntenna(data: any) {
      this.AntennaData = [];
      let GridAntenna: RanDataRows[] = [];

      for (let index = 0; index < data.length; index++) {
        GridAntenna.push({
          columnName: data[index].Name,
          value: data[index].Id,
          NEName:data[index].NEName,
          DisplayValue:data[index].DisName,
          ParentId: data[index].ParentId,
          SId:data[index].SId,
          SlId:data[index].SlId,
          PId:data[index].PId
        });
        this.AntennaData = GridAntenna;
      }
      console.log("AntennaData: ", this.AntennaData);
    }

    //Cell Data
    getCell(data: any) {
      this.CellData = [];
      let GridCell: RanDataRows[] = [];

      for (let index = 0; index < data.length; index++) {
        GridCell.push({
            columnName: data[index].Name,
            value: data[index].Id,
            NEName:data[index].NEName,
            DisplayValue:data[index].DisName,
            ParentId: data[index].ParentId,
            SId:data[index].SId,
            SlId:data[index].SlId,
            PId:data[index].PId
      });
      this.CellData = GridCell;
        }
         console.log("CellData: ", this.CellData);
     }

  //Identity Data
  getIdentity(data: any) {
    console.log("IdentitiesData befor: ", data);

    this.IdentitiesData = [];
    let GridIdentity: RanDataRows[] = [];
    for (let index = 0; index < data.length; index++) {
      GridIdentity.push({
        columnName: data[index].Name,
        value: data[index].Id,
        NEName:data[index].Name,
        DisplayValue:data[index].DisplayValue,
        ParentId: data[index].ParentId,
        SId:data[index].SID,
        SlId:data[index].SLID,
        PId:data[index].PID
      });
      this.IdentitiesData = GridIdentity;
    }
    console.log("IdentitiesData: ", this.IdentitiesData);
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
    //  setTimeout(() => {
    //    this.categories = this.nodeService.fillRanRoot(2); this.loading = false;
       
    //  }, 4000);

     console.log("RAN TREE ",this.categories);
    this.detailsTree.GetRANDetailsTree("RAN").subscribe(values => {
      //this.loading = true;
      
      this.categories = <TreeNode[]>[values.data];
      console.log("RAN TREE ",this.categories);
      this.loading = false;

    });

    this.OrginalTypenodes = [];
    this.OrginalTypenodes.push(Object.assign({}, this.categories[0]));
    console.log("this.Typenodes ",this.OrginalTypenodes);




    this.ControlllerColunmns = [{ field: 'columnName', header: 'Name' },
    { field: 'value', header: 'Value' }];
    this.ControlllerCols = [{ field: 'ParentId', header: 'RACK NUMBER' }
  ];
    this.ControlllerCols1 = [{ field: 'ParentId', header: 'RACK NUMBER' }
   ,{ field: 'SId', header: 'SUBRACK NUMBER' }
  ];
    this.ControlllerCols2 = [{ field: 'ParentId', header: 'RACK NUMBER' }
    ,{ field: 'SId', header: 'SUBRACK NUMBER' }
    ,{ field: 'SlId', header: 'SLOT NUMBER' }
  ];
  this.ControlllerCols3 = [{ field: 'ParentId', header: 'RACK NUMBER' }
  ,{ field: 'SId', header: 'SUBRACK NUMBER' }
  ,{ field: 'SlId', header: 'SLOT NUMBER' }
  ];
  this.ControlllerCols4 = [{ field: 'ParentId', header: 'RACK NUMBER' }
  ,{ field: 'SId', header: 'SUBRACK NUMBER' }
  ,{ field: 'SlId', header: 'SLOT NUMBER' }
  ,{ field: 'PId', header: 'PORT NUMBER' }
  ];


  this.ControlllerHostVersion = [{ field: 'columnName', header: 'TYPE' },
  { field: 'DisplayValue', header: 'HOST VERSION' }];
  this.SiteColunmns = [{ field: 'columnName', header: 'Name' },
  { field: 'value', header: 'Value' }];
  this.SiteCols = [{ field: 'ParentId', header: 'RACK NUMBER' }
];
  this.SiteCols1 = [{ field: 'ParentId', header: 'RACK NUMBER' }
 ,{ field: 'SId', header: 'SUBRACK NUMBER' }
];
  this.SiteCols2 = [{ field: 'ParentId', header: 'RACK NUMBER' }
  ,{ field: 'SId', header: 'SUBRACK NUMBER' }
  ,{ field: 'SlId', header: 'SLOT NUMBER' }
];
this.SiteCols3 = [{ field: 'ParentId', header: 'RACK NUMBER' }
,{ field: 'SId', header: 'SUBRACK NUMBER' }
,{ field: 'SlId', header: 'SLOT NUMBER' }
];
this.SiteCols4 = [{ field: 'ParentId', header: 'RACK NUMBER' }
,{ field: 'SId', header: 'SUBRACK NUMBER' }
,{ field: 'SlId', header: 'SLOT NUMBER' }
,{ field: 'PId', header: 'PORT NUMBER' }
];
this.SiteIdentity = [{ field: 'columnName', header: 'NE Name' }
];

this.SiteCells = [{ field: 'DisplayValue', header: 'CELL NAME' },
{ field: 'columnName', header: 'Type' }];
this.SiteAntennas = [{ field: 'DisplayValue', header: 'SERIAL NUMBER' },
{ field: 'columnName', header: 'INVENTORY UNIT ID' }];
this.SiteHostVersion = [{ field: 'columnName', header: 'TYPE' },
{ field: 'DisplayValue', header: 'HOST VERSION' }];

    this.RanData = [];

    this.items = [
      { label: 'Ran' },
    ];

    this.home = { icon: 'fa fa-home' };

  }

  SetRadioDisplayFalse()
  {
    this.displayRadioNEAttr = false;
    this.displayRadioRackAttr = false;
    this.displayRadioSubRackAttr = false;
    this.displayRadioSlotAttr = false;
    this.displayRadioBoardAttr = false;
    this.displayRadioPortAttr = false;
    this.displayRadioIdentityAttr = false;
    this.displayRadioAntennasAttr = false;
    this.displayRadioCellsAttr = false;
    this.displayRadioHostVersionAttr = false;
  }

  SetRanDisplayFalse()
  {
    this.displayRanNEAttr = false;
    this.displayRanRackAttr = false;
    this.displayRanSubRackAttr = false;
    this.displayRanSlotAttr = false;
    this.displayRanBoardAttr = false;
    this.displayRanPortAttr = false;
    this.displayRanHostVersionAttr = false;
  }


  // Clear Filter
  ClearF() {
    this.categories = [];
  //  this.categories = this.nodeService.fillRanRoot(2);
  // this.categories = this.OrginalTypenodes;
    this.detailsTree.GetRANDetailsTree("RAN").subscribe(values => {
    
    this.categories = <TreeNode[]>[values.data];
    console.log("RAN TREE ",this.categories);
    this.loading = false;

  });
  $("#float-input").val("");
  }

  // Tree Node Select
  nodeSelect(event) {
debugger;
    // BreadCrump
    this.items = [];
    this.BreadCrumpRecursive(event.node);

   //Site
    if (event.node.type === 'Site') {
      this.siteCandidateId = event.node.data;
      this.nodeService.getRanSitesDetails(event.node.data).subscribe(result => {
        this.DataRanNe = result[0];
        this.getRanNe(this.DataRanNe);
        this.loading = false;
      });

    } else if (event.node.type === 'Cell') {
      this.nodeService.getSiteCellsBy_Site(event.node.data).subscribe(res => {
        this.CellData = res;
        this.getCell(this.CellData);
        this.loading = false;
      });
    } else if (event.node.type === 'Antenna') {
      this.nodeService.getSiteAntennasBy_Site(event.node.data).subscribe(res => {
        this.AntennaData = res;
        this.getAntenna(this.AntennaData);
        this.loading = false;
      });
    } else if (event.node.type === 'SHostVersion') {
      this.nodeService.getSiteHostVersionsBy_Site(event.node.data).subscribe(res => {
        this.HostVersionData = res;
        this.getHostVersion(this.HostVersionData);
        this.loading = false;
      });

    } else if (event.node.type === 'SRack') {
      this.nodeService.getSiteRacksBy_Site(event.node.data).subscribe(res => {
        this.DataRack = res;
        this.getRack(this.DataRack);
        this.loading = false;
      });
    } else if (event.node.type === 'SsubRack') {
      this.nodeService.getSiteSubRacksBy_Site(event.node.data).subscribe(res => {
        this.DataSubRack = res;
        this.getSubRack(this.DataSubRack);
        this.loading = false;
      });
    } else if (event.node.type === 'SSlot') {
      this.nodeService.getSiteSlotsBy_Site(event.node.data).subscribe(res => {
        this.DataSlot = res;
        this.getSlot(this.DataSlot);
        this.loading = false;
      });
    } else if (event.node.type === 'SBoard') {
      this.nodeService.getSiteBoardsBy_Site(event.node.data).subscribe(res => {
        this.DataBoard = res;
        this.getBoard(this.DataBoard);
        this.loading = false;
      });
    }else if (event.node.type === 'SPort') {
      this.nodeService.getSitePortsBy_Site(event.node.data).subscribe(res => {
        this.DataPort = res;
        this.getPort(this.DataPort);
        this.loading = false;
      });
      //Controller
    } else if (event.node.type === 'Controller') {
      this.nodeService.getRanControllerDetails(event.node.data).subscribe(result => {
        this.DataRanNe = result[0];
        //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
        this.getRanNe(this.DataRanNe);
        this.loading = false;
      });

    } else if (event.node.type === 'CHostVersion') {
      this.nodeService.getControllerHostVersionsBy_Controller(event.node.data).subscribe(res => {
        this.DataRack = res;
        this.getHostVersion(this.DataRack);
        this.loading = false;
      });

    } else if (event.node.type === 'CRack') {
      this.nodeService.getControllerRacksBy_Controller(event.node.data).subscribe(res => {
        this.DataRack = res;
        this.getRack(this.DataRack);
        this.loading = false;
      });
    } else if (event.node.type === 'CsubRack') {
      this.nodeService.getControllerSubRacksBy_Controller(event.node.data).subscribe(res => {
        this.DataSubRack = res;
        this.getSubRack(this.DataSubRack);
        this.loading = false;
      });
    } else if (event.node.type === 'CSlot') {
      this.nodeService.getControllerSlotsBy_Controller(event.node.data).subscribe(res => {
        this.DataSlot = res;
        this.getSlot(this.DataSlot);
        this.loading = false;
      });
    } else if (event.node.type === 'CBoard') {
      this.nodeService.getControllerBoardsBy_Controller(event.node.data).subscribe(res => {
        this.DataBoard = res;
        this.getBoard(this.DataBoard);
        this.loading = false;
      });
    } else if (event.node.type === 'CPort') {
      this.nodeService.getControllerPortsBy_Controller(event.node.data).subscribe(res => {
        this.DataPort = res;
        this.getPort(this.DataPort);
        this.loading = false;
      });
     }
     else { this.loading = false; }

     this.SetRadioDisplayFalse();
     this.SetRanDisplayFalse();
     switch(event.node.type) {
      case 'Site': {
        this.SetRadioDisplayFalse();
        this.displayRadioNEAttr = true;
         break;
      }
      case 'Controller': {
        this.SetRanDisplayFalse();
        this.displayRanNEAttr = true;
         break;
      }
      case 'SHostVersion': {
        this.SetRadioDisplayFalse();
        this.displayRadioHostVersionAttr = true;
         break;
      }
      case 'CHostVersion': {
        this.SetRanDisplayFalse();
        this.displayRanHostVersionAttr = true;
         break;
      }
      case 'Cell': {
        this.SetRadioDisplayFalse();
        this.displayRadioCellsAttr = true;
         break;
      }
      case 'Antenna': {
        this.SetRadioDisplayFalse();
        this.displayRadioAntennasAttr = true;
         break;
      }
      case 'SRack': {
        this.SetRadioDisplayFalse();
        this.displayRadioRackAttr = true;
         break;
      }
      case 'CRack': {
        this.SetRanDisplayFalse();
        this.displayRanRackAttr = true;
         break;
      }
      case 'SsubRack': {
        this.SetRadioDisplayFalse();
        this.displayRadioSubRackAttr = true;
         break;
      }
      case 'CsubRack': {
        this.SetRanDisplayFalse();
        this.displayRanSubRackAttr = true;
         break;
      }
      case 'SSlot': {
        this.SetRadioDisplayFalse();
        this.displayRadioSlotAttr = true;
         break;
      }
      case 'CSlot': {
        this.SetRanDisplayFalse();
        this.displayRanSlotAttr = true;
         break;
      }
      case 'SBoard': {
        this.SetRadioDisplayFalse();
        this.displayRadioBoardAttr = true;
         break;
      }
      case 'CBoard': {
        this.SetRanDisplayFalse();
        this.displayRanBoardAttr = true;
         break;
      }
      case 'SPort': {
        this.SetRadioDisplayFalse();
        this.displayRadioPortAttr = true;
         break;
      }
      case 'CPort': {
        this.SetRanDisplayFalse();
        this.displayRanPortAttr = true;
         break;
      }
      default: {
        this.SetRadioDisplayFalse();
        this.SetRanDisplayFalse();
         //statements;
         break;
      }
   }

    // Display P-datatable
    /*
    if (event.node.type === 'Site' || event.node.type === 'Controller') {
      this.SetRanDisplayFalse();
      this.displayRanNEAttr = true;
    }
    else if (event.node.type === 'SRack' || event.node.type === 'CRack') {
      this.SetRanDisplayFalse();
      this.displayRanRackAttr = true;
    }
    else if (event.node.type === 'SsubRack' || event.node.type === 'CsubRack') {
      this.SetRanDisplayFalse();
      this.displayRanSubRackAttr = true;
    }
    else if (event.node.type === 'SSlot' || event.node.type === 'CSlot') {
      this.SetRanDisplayFalse();
      this.displayRanSlotAttr = true;
    }
    else if (event.node.type === 'SBoard' || event.node.type === 'CBoard') {
      this.SetRanDisplayFalse();
      this.displayRanBoardAttr = true;
    }
    else if (event.node.type === 'SPort' || event.node.type === 'CPort') {
      this.SetRanDisplayFalse();
      this.displayRanPortAttr = true;
    }
    else {
      this.SetRanDisplayFalse();
    }*/
  }

  viewSiteIdentities(){
    this.nodeService.getSiteIdentities(this.siteCandidateId).subscribe(res => {
      console.log("res: ", res);
      this.DataRanNe = res;
      this.getIdentity(this.DataRanNe);
      this.SetRadioDisplayFalse();
      this.displayRadioIdentityAttr = true;
      this.loading = false;

      /*this.nodeService.getControllerPortsBy_Controller(event.node.data).subscribe(res => {
        this.DataPort = res;
        this.getPort(this.DataPort);
        this.loading = false;*/
    });
  }

  //Site
  //Site Identity Details
  navigateSiteIdentityToDetails(id: number) {
    this.nodeService.getSiteIdentityDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRadioDisplayFalse();
      this.displayRadioNEAttr = true;
      this.loading = false;
    });
  }

  // Antenna Details
  navigateSiteAntennaToDetails(id: number) {
    this.nodeService.getSiteAntennaDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRadioDisplayFalse();
      this.displayRadioNEAttr = true;
      this.loading = false;
    });
  }

    // Cell Details
    navigateSiteCellToDetails(id: number) {
      this.nodeService.getSiteCellDetails(id).subscribe(result => {
        this.DataRanNe = result[0];
        //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
        this.getRanNe(this.DataRanNe);
        this.SetRadioDisplayFalse();
        this.displayRadioNEAttr = true;
        this.loading = false;
      });
    }

    // HostVersion Details
    navigateSiteHostVersionToDetails(id: number) {
          this.nodeService.getSiteHostVersionDetails(id).subscribe(result => {
            this.DataRanNe = result[0];
            //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
            this.getRanNe(this.DataRanNe);
            this.SetRadioDisplayFalse();
            this.displayRadioNEAttr = true;
            this.loading = false;
          });
    }

  // Rack Details
  navigateSiteRackToDetails(id: number) {
    this.nodeService.getSiteRackDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRadioDisplayFalse();
      this.displayRadioNEAttr = true;
      this.loading = false;
    });
  }

  // SubRack Details
  navigateSiteSubRackToDetails(id: number) {
    this.nodeService.getSiteSubRackDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRadioDisplayFalse();
      this.displayRadioNEAttr = true;
      this.loading = false;
    });
  }

    // Slot Details
    navigateSiteSlotToDetails(id: number) {
      debugger;
      this.nodeService.getSiteSlotDetails(id).subscribe(result => {
        this.DataRanNe = result[0];
        this.getRanNe(this.DataRanNe);
        this.SetRadioDisplayFalse();
        this.displayRadioNEAttr = true;
        this.loading = false;
      });
    }

  // Board Details
  navigateSiteBoardToDetails(id: number) {
    debugger;
    this.nodeService.getSiteBoardDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRadioDisplayFalse();
      this.displayRadioNEAttr = true;
      this.loading = false;
    });
  }

  // Port Details
  navigateSitePortToDetails(id: number) {
    this.nodeService.getSitePortDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRadioDisplayFalse();
      this.displayRadioNEAttr = true;
      this.loading = false;
    });
  }

 //Controller
  // Rack Details
  navigateControllerRackToDetails(id: number) {
    this.nodeService.getControllerRackDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRanDisplayFalse();
      this.displayRanNEAttr = true;
      this.loading = false;
    });
  }

    // HostVersion Details
    navigateControllerHostVersionToDetails(id: number) {
      this.nodeService.getControllerHostVersionDetails(id).subscribe(result => {
        this.DataRanNe = result[0];
        console.log("result",result);
        //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
        this.getRanNe(this.DataRanNe);
        this.SetRanDisplayFalse();
        this.displayRanNEAttr = true;
        this.loading = false;
      });
    }

  // SubRack Details
  navigateControllerSubRackToDetails(id: number) {
    this.nodeService.getControllerSubRackDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      console.log("result",result);
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRanDisplayFalse();
      this.displayRanNEAttr = true;
      this.loading = false;
    });
  }

    // Slot Details
    navigateControllerSlotToDetails(id: number) {
      debugger;
      this.nodeService.getControllerSlotDetails(id).subscribe(result => {
        this.DataRanNe = result[0];
        this.getRanNe(this.DataRanNe);
        this.SetRanDisplayFalse();
        this.displayRanNEAttr = true;
        this.loading = false;
      });
    }

  // Board Details
  navigateControllerBoardToDetails(id: number) {
    debugger;
    this.nodeService.getControllerBoardDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRanDisplayFalse();
      this.displayRanNEAttr = true;
      this.loading = false;
    });
  }

  // Port Details
  navigateControllerPortToDetails(id: number) {
    this.nodeService.getControllerPortDetails(id).subscribe(result => {
      this.DataRanNe = result[0];
      //test if removing <DATACOM_NE>   this.Data = <DATACOM_NE>result[0];
      this.getRanNe(this.DataRanNe);
      this.SetRanDisplayFalse();
      this.displayRanNEAttr = true;
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



