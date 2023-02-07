import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { PrivateProfessorWrapperComponent } from './private-professor-wrapper/private-professor-wrapper.component';
import { StudentProfileComponent } from '../student/student-profile/student-profile.component';
import { PrivateProfessorNavbarComponent } from './private-professor-navbar/private-professor-navbar.component';
import { ProfessorSubjectsComponent } from './professor-subjects/professor-subjects.component';
import { SubjectProfileComponent } from './professor-subject/subject-profile.component';

const routes: Routes = [
  {
    path: '',
    component: PrivateProfessorWrapperComponent,
    children: [
      {
        path: 'my-subjects',
        component: ProfessorSubjectsComponent,
        pathMatch: 'full',
      },
      {
        path: 'subject/:_id',
        component: SubjectProfileComponent,
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [SharedModule, RouterModule.forChild(routes)],
  declarations: [
    PrivateProfessorWrapperComponent,
    PrivateProfessorNavbarComponent,
    ProfessorSubjectsComponent,
    SubjectProfileComponent
  ],
})
export class ProfessorModule {}
