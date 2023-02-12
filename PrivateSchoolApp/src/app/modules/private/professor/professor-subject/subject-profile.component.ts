import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { get } from 'lodash';
import * as moment from 'moment';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SessionService } from '../../../shared/services/session.service';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { ISubject, IUser } from 'src/app/modules/shared/interfaces/interface';
import { environment } from 'src/app/environments/environment';
import { SubjectService } from 'src/app/modules/shared/services/subject.service';
import { MaterialService } from 'src/app/modules/shared/services/material.service';

@Component({
  selector: 'subject-profile',
  templateUrl: './subject-profile.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./subject-profile.component.css'],
  providers: []
  
})
export class SubjectProfileComponent implements OnInit {
  public baseUrl = environment.baseUrl;
  public previewUrl: any;
  public imgFile = null;
  closeResult: string;
  public subject: ISubject;
  public _id: string;
  public userWantToAddEduc = false;
  public isLoggedVet = false;
  public feedbacks = [];
  public vetFeedbacks = [];
  public totalFeedbacks = 0;
  public selected = 0;
  public vetComment: string;
  public filter: string;
  public whereFeedback = {
    sort: '',
    limit: 0,
    page: 1,
    search: ''
  };
  public error = {
    image: false
  };
  constructor(
    private readonly notifier: NotifierService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly userService: UserService,
    private readonly sessionService: SessionService,
    private readonly modalService: NgbModal,
    private readonly subjectService: SubjectService,
    private readonly materialService: MaterialService
  ) { }

  ngOnInit(): void {
    
      this.isLoggedVet = true;
    
    this.getSubject();
  }

  getSubject() {
    this.subjectService.getSubjectById(this._id).subscribe(s=>this.subject = s);
  }

  getDate(date) {
    return moment(date).format('DD.MM.YYYY');
  }

  calculateAverage() {
    let totalSum = 0;
    let total = 0;
    this.feedbacks.forEach(feedback => {
      const vet = get(feedback, '_medicalRecord._veterinarian');
      if (this._id === vet) {
        totalSum += get(feedback, 'mark');
        total += 1;
        this.vetFeedbacks.push(feedback);
      }
    });
    this.totalFeedbacks = total;
    // this.subject = { ...this.subject, averageMark: Math.round((totalSum / total + Number.EPSILON) * 100) / 100 };
  }

  editProfile() {
   
    this.userService.updateUser({ _id: localStorage.getItem("id") }, this.imgFile)
      .subscribe(res => {
        // this.subject.image = res.image;
        this.imgFile = null;
        this.previewUrl = null;
        this.notifier.notify('success', 'Slika je sacuvana');
      }, err => {
        this.notifier.notify('error', 'Slika nije sacuvana');
      }
      );
  }

  changeEducation() {
    this.userWantToAddEduc = true;
  }

  updateUser() {
    this.userService.updateUser(this.subject)
    .subscribe(res => {
      this.userWantToAddEduc = false;
      this.notifier.notify('success', 'Obrazovanje izmenjeno');
    }, err => {
      this.notifier.notify('error', 'Obrazovanje nije izmenjeno');
    }
    );
  }

  openVerticallyCentered(content) {
    this.modalService.open(content, { centered: true });
  }

  uploadFile(event) {
    this.error.image = false;
    const file = (event.target as HTMLInputElement).files[0];
    if (!file) { return; }
    const mimeType = get(file, 'type');
    if (!mimeType) {
      this.notifier.notify('error', 'Ovaj tip fajla nije dozvoljen.');
      this.error.image = true;
      return;
    } else if (mimeType && mimeType.match(/image\/jpg|jpeg|png/) == null) {
      this.notifier.notify('error', 'Ovaj tip fajla nije dozvoljen.');
      this.error.image = true;
      return;
    }

    this.imgFile = file;
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.previewUrl = reader.result;
    };
    this.materialService.setMaterial(this.imgFile, this._id)

  }
}
