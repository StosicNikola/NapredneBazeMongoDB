import { Injectable } from '@angular/core';
import { CanLoad, CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, Route, CanActivateChild } from '@angular/router';
import { get } from 'lodash';
import { SessionService } from '../shared/services/session.service';

const b = false;

@Injectable({ providedIn: 'root' })
export class AuthPrivateProfessorGuard implements CanActivate, CanLoad, CanActivateChild {
  constructor(
    private sessionService: SessionService,
    private router: Router
  ) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.handle();
  }

  canLoad(route: Route): boolean {
    return this.handle();
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.handle();
  }

  handle() {
    // const accessToken = get(this.sessionService, 'accessTokenObj');
    // const user = get(this.sessionService, 'user');
    // if (accessToken && get(user, 'role') === 'PROFESSOR') {
    //   return true;
    // }

    // this.router.navigate(['/student/home']);
    // return false;



    if (!b) {
      return true;
    }

    console.log("private 1 professor guard");
    this.router.navigate(['/student/profile']);
    return false;
    // if (!b) {
    //   return true;
    // }

    // console.log("private 1 professor guard");
    // this.router.navigate(['/student/profile']);
    // return false;
  }
}

@Injectable({ providedIn: 'root' })
export class AuthPrivateStudentGuard implements CanActivate, CanLoad {
  constructor(
    private sessionService: SessionService,
    private router: Router
  ) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.handle();
  }

  canLoad(route: Route): boolean {
    return this.handle();
  }

  handle() {
    console.log("private 2 student guard");
    // const accessToken = get(this.sessionService, 'accessTokenObj');
    // const user = this.sessionService.user;
    // if (accessToken && get(user, 'role') === 'STUDENT') { return true; }
    // this.router.navigate(['/professor/home']);
    
    if (b) { return true; }
    this.router.navigate(['/professor/my-subjects']);
    return false;
  }
}
export const appPrivateRoutingProviders: any[] = [AuthPrivateProfessorGuard, AuthPrivateStudentGuard];

