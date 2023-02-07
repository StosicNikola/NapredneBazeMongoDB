import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { get } from 'lodash';
import { UserType } from '../../../shared/enum/user.type.enum';
import { ISubject, IUser } from '../../../shared/interfaces/interface';
import { UserService } from '../../../shared/services/user.service';
import { SessionService } from '../../../shared/services/session.service';
import { environment } from 'src/app/environments/environment';
import { SubjectService } from 'src/app/modules/shared/services/subject.service';

@Component({
  selector: 'professor-subjects',
  templateUrl: './professor-subjects.component.html',
  styleUrls: ['./professor-subjects.component.css']
})
export class ProfessorSubjectsComponent implements OnInit {
  public baseUrl = environment.baseUrl;
  public user: IUser;
  public subjects: ISubject[] = [];
  public where = {
    sort: '-createdAt',
    limit: 15,
    page: 1,
    search: '',
    role: UserType.STUDENT
  };

  constructor(
    private readonly router: Router,
    private readonly sessionService: SessionService,
    private readonly userService: UserService,
    private readonly subjectService: SubjectService
  ) { }

  ngOnInit(): void {
    // this.user = get(this.sessionService, 'user');
    // if (this.user && get(this.user, 'role') === 'ADMIN') {
    //   this.isAdmin = true;
    // }
    this.getSubjects();
  }

  getSubjects() {
    this.subjectService.getAllSubjectsForProfessor("63e022c83f58bc85b535aba2").subscribe((subjects)=> { this.subjects = subjects; })
  }

  goToSubjectProfile(id: string) {
    this.router.navigate(['/professor/subject', id]);
  }
}
