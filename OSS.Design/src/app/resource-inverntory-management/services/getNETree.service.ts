import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { TreeNode, TreeModule } from 'primeng/primeng';
import { Observable } from 'rxjs/Observable';
import { JsonResponse } from '../extraClasses/JsonResponse';
import 'rxjs/add/operator/toPromise';
import { promise } from 'protractor';
import { BrowserDomAdapter } from '@angular/platform-browser/src/browser/browser_adapter';
import { NeDetail } from '../extraClasses/GetNeDetails/NeDetail';
import { RIMGetDataService } from './general services/RIMGetDataService.service';
import { Parameter } from '../extraClasses/RIMGetDataServiceClasses/ApiParameters';
import { AppConfiguration } from '../extraClasses/AppConfiguration';
@Injectable()
export class NodeService {
    CategoriesUrl: string;
    SubCategoriesUrl: string;
    CategoryTree: TreeNode[];
    constructor(private http: Http, private RimService: RIMGetDataService) {
        this.CategoryTree = [];
    }

    getTreeRoot(rootId) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("rootId", rootId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/GetTreeRoot", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetTreeRoot?rootId=' + rootId').map(data => data.json()).catch(this.handleError);
    }

    getCategories(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/GetAllCategories", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllCategories?Id=' + Id).map(data => data.json()).catch(this.handleError);
    }

    getSubCategories(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/SubCategory", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/SubCategory?Id=' + Id).map(data => data.json()).catch(this.handleError);
    }

    getNEs(categoryId: Number, SubCategoryId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("categoryId", categoryId));
        parameter.push(new Parameter("SubCategoryId", SubCategoryId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/AllNEForSubCategory", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/AllNEForSubCategory?categoryId=' + categoryId + '&SubCategoryId=' + SubCategoryId).map(data => data.json()).catch(this.handleError);
    }
    getOplicalNEs(categoryId: Number, SubCategoryId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("categoryId", categoryId));
        parameter.push(new Parameter("SubCategoryId", SubCategoryId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllOpticalNEsForSubCategory", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllOpticalNEsForSubCategory?categoryId=' + categoryId + '&SubCategoryId=' + SubCategoryId).map(data => data.json()).catch(this.handleError);
    }
    getMWNEs(categoryId: Number, SubCategoryId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("categoryId", categoryId));
        parameter.push(new Parameter("SubCategoryId", SubCategoryId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllMWNEsForSubCategory", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllMWNEsForSubCategory?categoryId=' + categoryId + '&SubCategoryId=' + SubCategoryId).map(data => data.json()).catch(this.handleError);
    }
    getFirewallNEs(categoryId: Number, SubCategoryId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("categoryId", categoryId));
        parameter.push(new Parameter("SubCategoryId", SubCategoryId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllFirewallNEsForSubCategory", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllFirewallNEsForSubCategory?categoryId=' + categoryId + '&SubCategoryId=' + SubCategoryId).map(data => data.json()).catch(this.handleError);
    }

    getBoardsByNE(categoryId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("categoryId", categoryId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/Tree", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/Tree?categoryId=' + categoryId).map(data => data.json()).catch(this.handleError);
    }

  /*  getRacksBySiteCandidateId(categoryId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("categoryId", categoryId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/Tree", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/Tree?categoryId=' + categoryId).map(data => data.json()).catch(this.handleError);
    }*/

    getRouterDetails(NeID: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", NeID));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/NEDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/NEDetails/' + NeID).map(data => data.json()).catch(this.handleError);
    }

    getBoardsBy_Ne(neid: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("neid", neid));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/GetAllBoardsByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getBoardDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/BoardDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }

    getSubBoardsBy_NE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/GetAllSubBoardsByBoard", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllSubBoardsByBoard?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getSubBoardDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/SubBoardDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/SubBoardDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    getPortsBy_NE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/GetAllPortsBySubBoard", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllPortsBySubBoard?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getPortDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/PortDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/PortDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    getSfpsBy_NE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/GetAllSFPsByPort", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllSFPsByPort?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getSFPDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/SFPDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/SFPDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    //Op
    getAllOpticalSubRackByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllOpticalSubRackByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllOpticalSubRackByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getAllOpticalBoardByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllOpticalBoardsByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllOpticalBoardsByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getAllOpticalPortsByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllOpticalPortsByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllOpticalPortsByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getAllOpticalSfpsByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllOpticalSfpsByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllOpticalSfpsByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getOpticalNEDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/OpticalNEDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/OpticalNEDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    getOpticalSubRackDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/OpticalSubRackDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/OpticalSubRackDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    getOpticalBoardDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/OpticalBoardDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/OpticalBoardDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    getOpticalPortDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/OpticalPortDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/OpticalPortDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }
    getOpticalSFPDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/OpticalSfpDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/OpticalSfpDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    // Firewall
    getAllFirewallBoardsByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllFirewallBoardByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllFirewallBoardByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }
    getAllFirewallSubBoardsByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllFirewallSubBoardsByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllFirewallSubBoardsByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }
    getAllFirewallPortsByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllFirewallPortsByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllFirewallPortsByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }
    getFirewallNEDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/FirewallNEDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/FirewallNEDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }
    getFirewallBoardDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/FirewallBoardDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/FirewallBoardDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }
    getFirewallSubBoardDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/FirewallSubBoardDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/FirewallSubBoardDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }
    getFirewallPortDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/FirewallPortDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/FirewallPortDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }
    //MW
    getAllMWBoardsByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllMWBoardByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllMWBoardByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }
    getAllMWPortsByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllMWPortsByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllMWPortsByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }
    getAllMWSfpsByNE(NeId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("NeId", NeId));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/getAllMWSfpsByNE", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/getAllMWSfpsByNE?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }
    getMWNEDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/MWNEDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/MWNEDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }
    getMWBoardDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/MWBoardDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/MWBoardDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }
    getMWPortDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/MWPortDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/MWPortDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }
    getMWSFpDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_API_TREE/api/MWSFPDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/MWSFPDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    //Site
    getRanSites() {
        let parameter: Parameter[] = [];
     /*   parameter.push(new Parameter("categoryId", categoryId));
        parameter.push(new Parameter("SubCategoryId", SubCategoryId));*/
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getAllSiteCandidates", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/AllNEForSubCategory?categoryId=' + categoryId + '&SubCategoryId=' + SubCategoryId).map(data => data.json()).catch(this.handleError);
    }
    getRanControllers(RANTypeId: Number) {
        let parameter: Parameter[] = [];
      /*  parameter.push(new Parameter("categoryId", categoryId));
        parameter.push(new Parameter("SubCategoryId", SubCategoryId));*/
        parameter.push(new Parameter("RANTypeId", RANTypeId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getAllRANNEs", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/AllNEForSubCategory?categoryId=' + categoryId + '&SubCategoryId=' + SubCategoryId).map(data => data.json()).catch(this.handleError);
    }

    //Identity
    getSiteIdentities(CandidateId: number) {
        let parameter: Parameter[] = [];
         /*   parameter.push(new Parameter("categoryId", categoryId));*/
        parameter.push(new Parameter("siteCandidateId", CandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getSiteIdentities", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/AllNEForSubCategory?categoryId=' + categoryId + '&SubCategoryId=' + SubCategoryId).map(data => data.json()).catch(this.handleError);
    }

    getRanSitesDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getSiteCandidatesDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/NEDetails/' + NeID).map(data => data.json()).catch(this.handleError);
    }

    getSiteIdentityDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getSiteIdentityDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/NEDetails/' + NeID).map(data => data.json()).catch(this.handleError);
    }

    getSiteRacksBy_Site(siteCandidateId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("siteCandidateId", siteCandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioRacks", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getSiteRackDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioRackDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }
//////////////////////////////////////////////////////////////////////////////////////////////////
    getSiteCellsBy_Site(siteCandidateId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("siteCandidateId", siteCandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getSiteCells", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getSiteCellDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioCellDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }

    getSiteAntennasBy_Site(siteCandidateId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("siteCandidateId", siteCandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getSiteAntennas", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getSiteAntennaDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioAntennaDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }

    getSiteHostVersionsBy_Site(siteCandidateId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("siteCandidateId", siteCandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getSiteHostVer", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getSiteHostVersionDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioHostverDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }
//////////////////////////////////////////////////////////////////////////////////////////////////
    getSiteSubRacksBy_Site(siteCandidateId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("siteCandidateId", siteCandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioSubRacks", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllSubBoardsByBoard?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getSiteSubRackDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioSUBRackDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/SubBoardDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    getSiteSlotsBy_Site(siteCandidateId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("siteCandidateId", siteCandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioSlots", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getSiteSlotDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioSlotDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }

    getSiteBoardsBy_Site(siteCandidateId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("siteCandidateId", siteCandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioBoards", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getSiteBoardDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioBoardDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }

    getSitePortsBy_Site(siteCandidateId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("siteCandidateId", siteCandidateId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioPorts", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllPortsBySubBoard?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getSitePortDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRadioPortDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/PortDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

//Controllers
    getRanControllerDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANNEDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/NEDetails/' + NeID).map(data => data.json()).catch(this.handleError);
    }

    getControllerRacksBy_Controller(RANControllerId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("RANControllerId", RANControllerId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANRacks", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getControllerRackDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANRACKDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }

///////////////////////////////////////////////////////////////////////////////////////////
getControllerHostVersionsBy_Controller(RANControllerId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("RANControllerId", RANControllerId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANHostVers", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getControllerHostVersionDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANHostverDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }
///////////////////////////////////////////////////////////////////////////////////////////
    getControllerSubRacksBy_Controller(RANControllerId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("RANControllerId", RANControllerId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANSUBRacks", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllSubBoardsByBoard?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getControllerSubRackDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANSUBRACKDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/SubBoardDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }

    getControllerSlotsBy_Controller(RANControllerId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("RANControllerId", RANControllerId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANSlots", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getControllerSlotDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANSlotDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }

    getControllerBoardsBy_Controller(RANControllerId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("RANControllerId", RANControllerId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANBoards", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllBoardsByNE?neid=' + neid).map(data => data.json()).catch(this.handleError);
    }

    getControllerBoardDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANBoardDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/BoardDetails/' + Td).map(data => data.json()).catch(this.handleError);
    }

    getControllerPortsBy_Controller(RANControllerId: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("RANControllerId", RANControllerId));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANPorts", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/GetAllPortsBySubBoard?NeId=' + NeId).map(data => data.json()).catch(this.handleError);
    }

    getControllerPortDetails(Id: Number) {
        let parameter: Parameter[] = [];
        parameter.push(new Parameter("Id", Id));
        return <any>this.RimService.GetServerData("RIM_RAN_API_TREE/api/getRANPortDetails", parameter, AppConfiguration.GetFunction, null);
        // return this.http.get('http://seserv112/RIM_API_TREE/api/PortDetails/' + Id).map(data => data.json()).catch(this.handleError);
    }


    fillRoot(rootId: Number) {
        //static
        // this.CategoryTree.push({
        //     label:'Transmission',
        //     children: this.fillChild_1(),
        //     leaf:false,
        //     expandedIcon:'fa-folder-open',
        //     collapsedIcon:'fa-folder'

        // });

        let CategoryTree: TreeNode[] = [];
        // for( var index = 0; index < data.length; index++){
        this.getTreeRoot(rootId).subscribe(result => {
            if (result != null && result != undefined) {
                for (let index = 0; index < result.length; index++) {
                    CategoryTree.push({
                        label: result[index].Name,
                        data: result[index].Id,
                        children: this.fillChild_1(result[index].Id),
                        leaf: false,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder'

                    });
                    //  console.log(result[index]);
                }
            }
        });
        return CategoryTree;

    }

    fillRanRoot(rootId: Number) {

        let CategoryTree: TreeNode[] = [];
        // for( var index = 0; index < data.length; index++){
        this.getTreeRoot(rootId).subscribe(result => {
            if (result != null && result != undefined) {
                for (let index = 0; index < result.length; index++) {
                    CategoryTree.push({
                        label: result[index].Name,
                        data: result[index].Id,
                        children: this.fillRanChild_1(result[index].Id),
                        leaf: false,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder'

                    });
                    //  console.log(result[index]);
                }
            }
        });

        console.log("RAN TREE 2",this.CategoryTree);
        return CategoryTree;
    }

    fillChild_1(_ModuleId: Number) {
        let TempCategory: TreeNode[] = [];
        this.getCategories(_ModuleId).subscribe(result => {
            if (result != null && result != undefined) {
                for (let index = 0; index < result.length; index++) {
                    TempCategory.push({
                        label: result[index].Name,
                        data: result[index].Id,
                        children: this.fillChild_subCategory(result[index].Id),
                        leaf: false,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder'
                    });
                    //  console.log(result[index]);
                }
            }
        });
        return TempCategory;

    }

    fillRanChild_1(_ModuleId: Number) {
        let TempCategory: TreeNode[] = [];
        this.getCategories(_ModuleId).subscribe(result => {
            if (result != null && result != undefined) {
                
                for (let index = 0; index < result.length; index++) {
                    if(result[index].Id === 5 ){
                        TempCategory.push({
                            label: result[index].Name,
                            data: result[index].Id,
                            children: this.fillRanChild_NE(result[index].Id,null),
                            leaf: false,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder'
                        });
                    }else 
                    TempCategory.push({
                        label: result[index].Name,
                        data: result[index].Id,
                        children: this.fillRanChild_subCategory(result[index].Id),
                        leaf: false,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder'
                    });
                    //  console.log(result[index]);
                }
            }
        });
        return TempCategory;

    }

    fillChild_subCategory(_categoryId: Number) {
        // console.log(_categoryId);
        const subCategory: TreeNode[] = [];
        this.getSubCategories(_categoryId).subscribe(result => {
            if (result != null && result != undefined) {
                for (let index = 0; index < result.length; index++) {
                    subCategory.push({
                        label: result[index].Name,
                        data: result[index].Id,
                        // debugeer;
                        children: this.fillChild_NE(_categoryId, result[index].Id),
                        leaf: false,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder'
                    });
                    // console.log(result[index].index);
                }
            }
        });
        return subCategory;
    }

    fillRanChild_subCategory(_categoryId: Number) {
        const subCategory: TreeNode[] = [];
        console.log(_categoryId);
        this.getSubCategories(_categoryId).subscribe(result => {
           // debugger;
           console.log(result);
            if (result != null && result != undefined) {
                for (let index = 0; index < result.length; index++) {
                    subCategory.push({
                        label: result[index].Name,
                        data: result[index].Id,
                        children: this.fillRanChild_NE(_categoryId, result[index].Id),
                        leaf: false,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder'
                    });
                    // console.log(result[index].index);
                  //  console.log(result);
                }
            }
        });
        return subCategory;
    }

    fillChild_NE(_categoryId: Number, _subCategoryId: Number) {
        let NE: TreeNode[] = [];
        if (_categoryId == 1) {
            this.getNEs(_categoryId, _subCategoryId).subscribe(result => {
                if (result != null && result != undefined) {
                    for (let index = 0; index < result.length; index++) {
                        NE.push({
                            label: result[index].Name,
                            data: result[index].Id,
                            type: 'Router',
                            children: this.fillChild_Board(_categoryId, result[index].Id),
                            leaf: false,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder'
                        });
                        // console.log(result[index].index);
                    }
                }
            });
        }
        if (_categoryId == 2) {
            this.getOplicalNEs(_categoryId, _subCategoryId).subscribe(result => {
                if (result != null && result != undefined) {
                    for (let index = 0; index < result.length; index++) {
                        NE.push({
                            label: result[index].Name,
                            data: result[index].Id,
                            type: 'OPRouter',
                            children: this.fillOpticalChild_Board(_categoryId, result[index].Id),
                            leaf: false,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder'
                        });
                        // console.log(result[index].index);
                    }
                }
            });
        }
        if (_categoryId == 3) {
            this.getMWNEs(_categoryId, _subCategoryId).subscribe(result => {
                if (result != null && result != undefined) {
                    for (let index = 0; index < result.length; index++) {
                        NE.push({
                            label: result[index].Name,
                            data: result[index].Id,
                            type: 'MWRouter',
                            children: this.fillMWChild_Board(_categoryId, result[index].Id),
                            leaf: false,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder'
                        });
                        // console.log(result[index].index);
                    }
                }
            });
        }
        if (_categoryId == 4){
            this.getFirewallNEs(_categoryId, _subCategoryId).subscribe(result => {
                if (result != null && result != undefined) {
                    for (let index = 0; index < result.length; index++) {
                        NE.push({
                            label: result[index].Name,
                            data: result[index].Id,
                            type: 'FRouter',
                            children: this.fillFirewallChild_Board(_categoryId, result[index].Id),
                            leaf: false,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder'
                        });
                        // console.log(result[index].index);
                    }
                }
            });
        }
        return NE;
    }

    fillRanChild_NE(_categoryId: Number, _subCategoryId: Number) {
       // debugger;
        let NE: TreeNode[] = [];
        if (_categoryId == 5) {
            this.getRanSites().subscribe(result => {
                if (result != null && result != undefined) {
                    for (let index = 0; index < 100; index++) {  //result.length
                        NE.push({
                            label: result[index].Name,
                            data: result[index].Id,
                            type: 'Site',
                            children: this.fillRanChild_SiteRack(_categoryId, result[index].Id),
                            leaf: false,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder'
                        });
                        // console.log(result[index].index);
                    }
                }
             //   console.log("Site is : ",NE);
            });
        }
        if (_categoryId == 6) {
            //RanNeType    
            this.getRanControllers(_subCategoryId).subscribe(result => {
                if (result != null && result != undefined) {
                    for (let index = 0; index < result.length; index++) {
                        NE.push({
                            label: result[index].Name,
                            data: result[index].Id,
                            type: 'Controller',
                            children: this.fillRanChild_ControllerRack(_categoryId, result[index].Id),
                            leaf: false,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder'
                        });
                        // console.log(result[index].index);
                    }
                }
            });
        }
        return NE;
    }

    fillChild_Board(_categoryId, NeID: Number) {
        const Board: TreeNode[] = [];
        // debugger;
        // if (_categoryId = 1) {
        this.getBoardsByNE(_categoryId).subscribe(result => {
            Board.push({
                label: result[0].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: false,
                type: 'Board',
                children: [{
                    label: result[1].Name,
                    data: NeID,
                    expandedIcon: 'fa-folder-open',
                    collapsedIcon: 'fa-folder',
                    leaf: false,
                    type: 'subBoard',
                    children: [{
                        label: result[2].Name,
                        data: NeID,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder',
                        type: 'Port',
                        children: [{
                            label: result[3].Name,
                            data: NeID,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder',
                            type: 'Sfp',
                        }]
                    }]
                }]
            });
        });

        return Board;
    }
    fillOpticalChild_Board(_categoryId, NeID: Number) {
        const Board: TreeNode[] = [];
        this.getBoardsByNE(_categoryId).subscribe(result => {
            console.log(result);
            Board.push({
                label: result[0].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: false,
                type: 'OsubRack',
                children: [{
                    label: result[1].Name,
                    data: NeID,
                    expandedIcon: 'fa-folder-open',
                    collapsedIcon: 'fa-folder',
                    leaf: false,
                    type: 'OBoard',
                    children: [{
                        label: result[2].Name,
                        data: NeID,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder',
                        type: 'OPort',
                        children: [{
                            label: result[3].Name,
                            data: NeID,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder',
                            type: 'OSfp',
                            leaf: true
                        }]
                    }]
                }]
            });
        });
        return Board;
    }

    fillMWChild_Board(_categoryId, NeID: Number) {
        const Board: TreeNode[] = [];
        this.getBoardsByNE(_categoryId).subscribe(result => {
            Board.push({
                label: result[0].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: false,
                type: 'MBoard',
                children: [{
                    label: result[1].Name,
                    data: NeID,
                    expandedIcon: 'fa-folder-open',
                    collapsedIcon: 'fa-folder',
                    leaf: false,
                    type: 'MPort',
                    children: [{
                        label: result[2].Name,
                        data: NeID,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder',
                        type: 'MSfpPort',
                        leaf: true
                    }]
                }]
            });
        });
        return Board;
    }
    fillFirewallChild_Board(_categoryId, NeID: Number) {
        const Board: TreeNode[] = [];
        this.getBoardsByNE(_categoryId).subscribe(result => {
            Board.push({
                label: result[0].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: false,
                type: 'FBoard',
                children: [{
                    label: result[1].Name,
                    data: NeID,
                    expandedIcon: 'fa-folder-open',
                    collapsedIcon: 'fa-folder',
                    leaf: false,
                    type: 'FsubBoard',
                    children: [{
                        label: result[2].Name,
                        data: NeID,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder',
                        type: 'FPort',
                        leaf: true
                    }]
                }]
            });
        });
        return Board;
    }

    fillRanChild_SiteRack(_categoryId, NeID: Number) {
        const Board: TreeNode[] = [];
        // debugger;
        // if (_categoryId = 1) {
        this.getBoardsByNE(_categoryId).subscribe(result => {
            Board.push({
                label: result[0].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: true,
                type: 'Cell'
            },{
                label: result[1].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: true,
                type: 'Antenna'
            },{          
            label: result[2].Name,
            data: NeID,
            expandedIcon: 'fa-folder-open',
            collapsedIcon: 'fa-folder',
            leaf: true,
            type: 'SHostVersion'
            },{
                label: result[3].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: false,
                type: 'SRack',
                children: [{
                    label: result[4].Name,
                    data: NeID,
                    expandedIcon: 'fa-folder-open',
                    collapsedIcon: 'fa-folder',
                    leaf: false,
                    type: 'SsubRack',
                    children: [{
                        label: result[5].Name,
                        data: NeID,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder',
                        type: 'SSlot',
                        children: [{
                            label: result[6].Name,
                            data: NeID,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder',
                            type: 'SBoard',
                            children: [{
                                label: result[7].Name,
                                data: NeID,
                                expandedIcon: 'fa-folder-open',
                                collapsedIcon: 'fa-folder',
                                type: 'SPort',
                            }]
                        }]
                    }]
                }]
            });
        });
        return Board;
    }
    fillRanChild_ControllerRack(_categoryId, NeID: Number) {
        const Board: TreeNode[] = [];
        this.getBoardsByNE(_categoryId).subscribe(result => {
            Board.push({
                label: result[0].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: true,
                type: 'CHostVersion'
            },{
                label: result[1].Name,
                data: NeID,
                expandedIcon: 'fa-folder-open',
                collapsedIcon: 'fa-folder',
                leaf: false,
                type: 'CRack',
                children: [{
                    label: result[2].Name,
                    data: NeID,
                    expandedIcon: 'fa-folder-open',
                    collapsedIcon: 'fa-folder',
                    leaf: false,
                    type: 'CsubRack',
                    children: [{
                        label: result[3].Name,
                        data: NeID,
                        expandedIcon: 'fa-folder-open',
                        collapsedIcon: 'fa-folder',
                        type: 'CSlot',
                        children: [{
                            label: result[4].Name,
                            data: NeID,
                            expandedIcon: 'fa-folder-open',
                            collapsedIcon: 'fa-folder',
                            type: 'CBoard',
                            children: [{
                                label: result[5].Name,
                                data: NeID,
                                expandedIcon: 'fa-folder-open',
                                collapsedIcon: 'fa-folder',
                                type: 'CPort',
                            }]
                        }]
                    }]
                }]
            });
        });
        return Board;
    }

    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }

}

