import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { CourseList } from "../models/courseList.model";
import { DataService } from "../services/data.service";

@Component({
    selector: 'courseList-app',
    templateUrl: './courseList.component.html'
})
export class CourseListComponent {
    courseList: CourseList = new CourseList();
    pageSize: number = 10;
    pageIndex: number = 1;
    courseCategory: string | undefined;
    destroy$: Subject<boolean> = new Subject<boolean>();
    loading: boolean = true;
    api: string = "api/courselist/"

    constructor(private dataService: DataService, private activatedRoute: ActivatedRoute, private router: Router) {
    }

    ngOnInit() {
        this.activatedRoute.queryParams.subscribe(params => {
            if (params["page"] != undefined && !isNaN(Number(params["page"]))) {
                this.pageIndex = Number(params["page"]);
            } else {
                this.pageIndex = 1;
            }
            if (params["category"] != undefined) {
                this.courseCategory = params["category"];
            }
            this.getData(this.router.url.split('?')[0]);
        });
    }

    get totalPage(): number {
        return this.precisionRound((this.courseList.totalCount / this.pageSize), 0) + (this.courseList.totalCount % this.pageSize > 0 ? 1 : 0);
    }

    ngOnDestroy() {
        this.destroy$.next(true);
        this.destroy$.unsubscribe();
    }

    private getData(type: string) {
        this.loading = true;

        let body = {
            Page : this.pageIndex,
            PageSize : this.pageSize,
            Category : this.courseCategory,
            Type: type
        };

        this.dataService.sendPostRequest(this.api, body).pipe(takeUntil(this.destroy$)).subscribe((res: any) => {
            this.loading = false;
            this.courseList = res;
        })
    }

    private precisionRound(number: number, precision: number) {
        if (precision < 0) {
            let factor = Math.pow(10, precision);
            return Math.round(number * factor) / factor;
        }
        else
            return +(Math.round(Number(number + "e+" + precision)) +
                "e-" + precision);
    }
}