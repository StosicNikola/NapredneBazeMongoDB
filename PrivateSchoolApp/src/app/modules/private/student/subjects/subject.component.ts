import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { get } from 'lodash';
import { IActiveSubject, IUser} from '../../../shared/interfaces/interface';
import { SessionService } from '../../../shared/services/session.service';
import { environment } from 'src/app/environments/environment';
import { SubjectService } from 'src/app/modules/shared/services/subject.service';

@Component({
  selector: 'student-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.css']
})
export class StudentSubjectsComponent implements OnInit {
  public baseUrl = environment.baseUrl;
  public subjects: IActiveSubject[] = [];
  private user: IUser;

  constructor(
    private readonly router: Router,
    private readonly sessionService: SessionService,
    private readonly subjectService: SubjectService
  ) { }

  ngOnInit(): void {
    this.getSubjects();
    // this.user = get(this.sessionService, 'user');
  }

  getSubjects() {
    this.subjects = this.subjectService.getAllSubjects("63e021ff3f58bc85b535aba0");
  }

  join(subjectId: string) {
    this.subjectService.enrollToSubject("63e021ff3f58bc85b535aba0", subjectId);
  }
}