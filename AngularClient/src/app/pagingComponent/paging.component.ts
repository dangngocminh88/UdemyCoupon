import { Component } from '@angular/core';

@Component({
    selector: 'paging-app',
    templateUrl: './paging.component.html'
})
export class PagingComponent {
    pageSize: number = 15;
    pageIndex: number = 3;
    totalItem: number = 151;

    constructor() { }

    get totalPage(): number {
        return this.precisionRound((this.totalItem / this.pageSize), 0) + (this.totalItem % this.pageSize > 0 ? 1 : 0);
    }

    changePage(pageNumber: number) {
        this.pageIndex = pageNumber;
    }

    precisionRound(number: number, precision: number) {
        if (precision < 0) {
            let factor = Math.pow(10, precision);
            return Math.round(number * factor) / factor;
        }
        else
            return +(Math.round(Number(number + "e+" + precision)) +
                "e-" + precision);
    }
}