import { Component, OnInit, OnDestroy } from '@angular/core';
import { SessionService } from '../../../shared/services/session.service';
import { get } from 'lodash';
import { AuthService } from '../../../shared/services/auth.service';
import { Subscription } from 'rxjs';
import { faBell } from '@fortawesome/free-solid-svg-icons';
import { UserType } from 'src/app/modules/shared/enum/user.type.enum';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'private-professor-navbar',
  templateUrl: './private-professor-navbar.component.html',
  styleUrls: ['./private-professor-navbar.component.css']
})  
export class PrivateProfessorNavbarComponent implements OnInit {
  private subscription: Subscription;
  // tslint:disable-next-line:variable-name
  public _user: string;
  constructor(
    private readonly sessionService: SessionService,
    private readonly authService: AuthService
  ) { }

  ngOnInit(): void {
    if (this.sessionService.user && (get(this.sessionService, 'user.role') === UserType.STUDENT)) {
      this._user = get(this.sessionService, 'user._id');
    }
  }

  logout() {
    this.authService.logout();
  }
}
