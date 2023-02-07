import { Injectable } from '@angular/core';
import { CanLoad, CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, Route, CanActivateChild } from '@angular/router';
import { get } from 'lodash';

const a = true;

@Injectable({ providedIn: 'root' })
export class AuthPrivateGuard implements CanActivate, CanLoad, CanActivateChild {
  constructor(
    // private sessionService: SessionService,
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
    // if (accessToken && user) { return true; }
    // this.router.navigate(['/private-school/home']);
    console.log("public 1");
    if (a) { return true; }
    this.router.navigate(['/private-school/home']);
    return false;
  }
}

@Injectable()
export class AuthPublicGuard implements CanActivate, CanLoad {
  constructor(
    // private sessionService: SessionService,
    private router: Router
  ) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.handle();
  }

  canLoad(route: Route): boolean {
    return this.handle();
  }

  handle() {
    // const accessToken = get(this.sessionService, 'accessTokenObj');
    // const user = this.sessionService.user;
    // if (
    //   (!accessToken && !user) ||
    //   (!accessToken || !user)
    // ) { return true; }
    // this.router.navigate(['/student/home']);
    // console.log("public 2");
    // if (a) { return true; }
    // this.router.navigate(['/professor/home']);
    // return false;
    console.log("public 2");
    if (a) { return true; }
    this.router.navigate(['/professor/my-subjects']);
    return false;
  }
}

export const appRoutingProviders: any[] = [AuthPrivateGuard, AuthPublicGuard];

