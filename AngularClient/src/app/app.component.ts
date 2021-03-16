import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { DataService } from './services/data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  api: string = "api/coursecategory";
  destroy$: Subject<boolean> = new Subject<boolean>();
  courseCategories: string[] = [];

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.dataService.sendGetRequest(`${this.api}`).pipe(takeUntil(this.destroy$)).subscribe((res: any) => {
      this.courseCategories = res;
    });
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}
