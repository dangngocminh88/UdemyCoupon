import { Component } from "@angular/core";
import { CourseList } from "../models/courseList.model";
import { DataService } from "../services/data.service";

@Component({
    selector: 'courseDetail-app',
    templateUrl: './courseDetail.component.html'
  })
export class CourseDetailComponent {
    constructor(private dataService: DataService) { }
    
    get courseList(): CourseList {
        this.courseList.totalCount = 0;
        this.courseList.courses = [];
        return this.courseList;
    }
}