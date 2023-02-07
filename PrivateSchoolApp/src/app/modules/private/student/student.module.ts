import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { PrivateStudentWrapperComponent } from './private-student-wrapper/private-student-wrapper.component';
import { StudentProfileComponent } from './student-profile/student-profile.component';
import { PrivateStudentNavbarComponent } from './private-student-navbar/private-student-navbar.component';
import { StudentSubjectsComponent } from './subjects/subject.component';
import { ProfessorsComponent } from './professors/professors.component';
import { SubjectProfileComponent } from './subject-profile/subject-profile.component';

const routes: Routes = [
  {
    path: '',
    component: PrivateStudentWrapperComponent,
    children: [
      {
        path: 'profile',
        component: StudentProfileComponent,
        pathMatch: 'full',
      },
      {
        path: 'subjects',
        component: StudentSubjectsComponent,
        pathMatch: 'full',
      },
      {
        path: 'subject/:_id',
        component: SubjectProfileComponent,
        pathMatch: 'full',
      },
      { path: 'professors', component: ProfessorsComponent, pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [SharedModule, RouterModule.forChild(routes)],
  declarations: [
    PrivateStudentWrapperComponent,
    StudentProfileComponent,
    PrivateStudentNavbarComponent,
    StudentSubjectsComponent,
    ProfessorsComponent,
    SubjectProfileComponent,
  ],
})
export class StudentModule {}
