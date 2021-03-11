import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CourseDetailComponent } from './CourseDetailComponent/courseDetail.component';
import { CourseListComponent } from './CourseListComponent/courseList.component';

const routes: Routes = [
  { path: "all", component: CourseListComponent },
  { path: "100off", component: CourseListComponent },
  { path: "discount", component: CourseListComponent },
  { path: "coursedetail/:courseId", component: CourseDetailComponent },
  { path: "", redirectTo: "/all", pathMatch: "full" },
  { path: '**', redirectTo: "/all", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
