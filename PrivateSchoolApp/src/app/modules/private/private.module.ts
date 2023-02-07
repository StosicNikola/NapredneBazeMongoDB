import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
import {
  AuthPrivateProfessorGuard,
  AuthPrivateStudentGuard,
} from './auth.private.guard';

const routes: Routes = [
  {
    path: 'professor',
    loadChildren: () =>
      import('src/app/modules/private/professor/professor.module').then(
        (m) => m.ProfessorModule
      ),
    canLoad: [AuthPrivateProfessorGuard],
  },
  {
    path: 'student',
    loadChildren: () =>
      import('src/app/modules/private/student/student.module').then(
        (m) => m.StudentModule
      ),
    canLoad: [AuthPrivateStudentGuard],
  },
  { path: '**', redirectTo: '/professor/my-subjects' },
];

@NgModule({
  imports: [SharedModule, RouterModule.forChild(routes)],
  declarations: [
  ],
  exports: [],
  providers: [],
})
export class PrivateModule {}
