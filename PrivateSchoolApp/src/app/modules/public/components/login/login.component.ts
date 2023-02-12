import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../../shared/services/auth.service';
import { get } from 'lodash';
import { SessionService } from '../../../shared/services/session.service';
import { NotifierService } from 'angular-notifier';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public showPassword = false;
  public data = {
    email: '',
    password: ''
  };
  public isLoginInProgress = false;
  public faEye = faEye;
  public faEyeSlash = faEyeSlash;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly notifier: NotifierService,
    private readonly authService: AuthService,
    private readonly sessionService: SessionService,
  ) {}

  ngOnInit(): void {
    const { email } = this.route.snapshot.queryParams;
    if (email) { this.data.email = email; }
  }

  login(): void {

    this.authService.login(this.data.email, this.data.password).subscribe(s=>{
        localStorage.setItem("id", s.id);
        if(s.role ===0)
        {
          localStorage.setItem("role","STUDENT");
        }
        else
        {
          localStorage.setItem("role", "PROFESSOR");
        }
        if(s.role === 0)
          {
        this.router.navigate(['/student/profile']);
            console.log("ne radi")
          }
        else
        {
          this.router.navigate(['/professor/my-subjects']);
          console.log("radi")
        }
      
  })
    // this.isLoginInProgress = true;
    // this.isLoginInProgress = false;
    // this.handleRedirect(get(this.sessionService, 'user'));
  }

  goToRegister() {
    this.router.navigate(['/private-school/register']);
  }

  toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }

  handleRedirect(user: any) {
    this.router.navigate(['/student/profile']);
    // if (get(user, 'role') === 'PROFESSOR') {
    //   this.router.navigate(['/professor/home']);
    // } else if (get(user, 'role') === 'STUDENT') {
    //   this.router.navigate(['/student/home']);
    // } else {
    //   this.router.navigate(['/']);
    // }
  }

}
