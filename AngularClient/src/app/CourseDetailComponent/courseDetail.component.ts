import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { faStar as farStar, IconDefinition } from "@fortawesome/free-regular-svg-icons";
import { faStarHalf, faStar } from "@fortawesome/free-solid-svg-icons";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { Course } from "../models/cource.model";
import { DataService } from "../services/data.service";

@Component({
    selector: 'courseDetail-app',
    templateUrl: './courseDetail.component.html'
  })
export class CourseDetailComponent {
    api: string = "api/coursedetail";
    course: Course = new Course();
    destroy$: Subject<boolean> = new Subject<boolean>();
    faStar = faStar;
    faStarHalf = faStarHalf;
    farStar = farStar;

    constructor(private dataService: DataService, private activatedRoute: ActivatedRoute) { }
    
    ngOnInit() {
        this.activatedRoute.params.subscribe(params => {
            this.getData(Number(params['courseId']));
        });
    }

    getData(courseId: number) {
        this.dataService.sendGetRequest(`${this.api}/${courseId}`).pipe(takeUntil(this.destroy$)).subscribe((res: any) => {
            this.course = res;
        })
    }

    ngOnDestroy() {
        this.destroy$.next(true);
        this.destroy$.unsubscribe();
    }

    displayStarRate(rate: number, courseRate: number) : IconDefinition {
        if (courseRate > rate) {
            return faStar;
        }
        return farStar;
    }
}