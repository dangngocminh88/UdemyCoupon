import { Component } from "@angular/core";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { CourseList } from "../models/courseList.model";
import { DataService } from "../services/data.service";

@Component({
    selector: 'courseList-app',
    templateUrl: './courseList.component.html'
})
export class CourseListComponent {
    courseList!: CourseList;
    destroy$: Subject<boolean> = new Subject<boolean>();
    api: string = "api/courselist/1"

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.dataService.sendGetRequest(this.api).pipe(takeUntil(this.destroy$)).subscribe((res: any) => {
            this.courseList = res;
        })
    }

    ngOnDestroy() {
        this.destroy$.next(true);
        this.destroy$.unsubscribe();
    }
}