import { Component, OnInit, OnDestroy } from '@angular/core';
import { SessionService } from '../../../shared/services/session.service';
import { get } from 'lodash';
import { AuthService } from '../../../shared/services/auth.service';
import { Subscription } from 'rxjs';
import { faBell } from '@fortawesome/free-solid-svg-icons';
import { UserType } from 'src/app/modules/shared/enum/user.type.enum';

@Component({
  // tslint:disable-next-line:component-selector
  selector: "private-student-navbar",
  templateUrl: './private-student-navbar.component.html',
  styleUrls: ['./private-student-navbar.component.css']
})  
export class PrivateStudentNavbarComponent implements OnInit, OnDestroy {
  public readonly faBell = faBell;
  private subscription: Subscription;
  public newNotification = false;
  // tslint:disable-next-line:variable-name
  public _user: string;
  constructor(
    private readonly sessionService: SessionService,
    private readonly authService: AuthService
  ) { }

  ngOnInit(): void {
    
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  logout() {
    this.authService.logout();
  }
}
