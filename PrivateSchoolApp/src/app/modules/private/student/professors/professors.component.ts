import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { get } from 'lodash';
import { NotifierService } from 'angular-notifier';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserType } from '../../../shared/enum/user.type.enum';
import { IProfessor, IUser } from '../../../shared/interfaces/interface';
import { UserService } from '../../../shared/services/user.service';
import { SessionService } from '../../../shared/services/session.service';
import { environment } from 'src/app/environments/environment';
import { ConfirmComponent } from 'src/app/modules/public/components/confirm/confirm.component';
import { ProfessorService } from 'src/app/modules/shared/services/professor.service';

@Component({
  selector: 'professors',
  templateUrl: './professors.component.html',
  styleUrls: ['./professors.component.css']
})
export class ProfessorsComponent implements OnInit {
  public baseUrl = environment.baseUrl;
  private bsModalRef: BsModalRef;
  public professors: IProfessor[] = [];

  constructor(
    private readonly router: Router,
    private readonly sessionService: SessionService,
    private readonly modalService: BsModalService,
    private readonly notifier: NotifierService,
    private readonly professorService: ProfessorService
  ) { }

  ngOnInit(): void {
    this.getProfessors();
    // this.user = get(this.sessionService, 'user');
    // if (this.user && get(this.user, 'role') === 'ADMIN') {
    //   this.isAdmin = true;
    // }
  }

  getProfessors() {
   this.professorService.getAllProfessors().subscribe(m=> this.professors = m);
  }
}
