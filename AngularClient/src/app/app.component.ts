import { HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Course } from './models/cource.model';
import { DataService } from './services/data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngularClient';
  courses: Course[] = [];
  destroy$: Subject<boolean> = new Subject<boolean>();

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.dataService.sendGetRequest().pipe(takeUntil(this.destroy$)).subscribe((res:any)=>{
      this.courses = res;
    })
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }

  classRating(course: Course, compareNumber: number): string {
    if (course.avg_rating_recent >= compareNumber) {
      return "fa-star";
    }
    if (course.avg_rating_recent > compareNumber - 1) {
      return "fa-star-half-o";
    }
    return "fa-star-o";
  }
}
