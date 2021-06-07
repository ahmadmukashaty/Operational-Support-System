import { Pairs } from "./Pairs";

export class SelectRanItem {
    label: string;
    value: Pairs;
    // styleClass?: string;
    // icon?: string;
    // title?: string;

    /**
     *
     */
    constructor(label:string, value:Pairs) {
        this.label = label;
        this.value = value;
    };
}