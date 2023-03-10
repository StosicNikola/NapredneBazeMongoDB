import { Component, OnInit } from '@angular/core';
import { get } from 'lodash';
import { Router, ActivatedRoute } from '@angular/router';
import { faBook } from '@fortawesome/free-solid-svg-icons';
import { IStudent } from '../../../shared/interfaces/interface';
import { SessionService } from 'src/app/modules/shared/services/session.service';
import { StudentService } from 'src/app/modules/shared/services/student.service';

import { map } from 'rxjs';

@Component({
  selector: 'student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit {
  public readonly faBook = faBook;
  public student: IStudent;
  public subjectsTotal: number = 0;
  

  constructor(
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly sessionService: SessionService,
    private readonly studentService: StudentService
    ) { }

  ngOnInit(): void {
    var id = localStorage.getItem("id")
    this.getStudentById(id);
    console.log(this.student)
  }

  getStudentById(id:string) {
    this.studentService.getStudentById(id).subscribe(s=> this.student =  s);
  }

  goToSubjectProfile(id: string) {
    this.router.navigate(['/student/subject', id]);
  }
}
