import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { SessionService } from './session.service';
import { ISingInResponse } from '../interfaces/interface';
import { environment } from 'src/app/environments/environment';

@Injectable()
export class AuthService {
  constructor(
    private readonly apiService: ApiService,
    private readonly sessionService: SessionService,
    private readonly router: Router,
    private httpClient: HttpClient
  ) {}

  register_student(data: any): Observable<object> {
    return this.httpClient.post(`${environment.apiUrl}/User/CreateStudent/${data.name}/${data.surname}/${data.index}/${data.email}/${data.password}`, null);
  }

  register_profesor(data: any): Observable<object> {
    return this.httpClient.post(`${environment.apiUrl}/User/CreateProfessor/${data.name}/${data.surname}/${data.licenseNumber}/${data.email}/${data.password}`, null);
  }
  login(email, password) { 
    return this.httpClient.post<ISingInResponse>(`https://localhost:5001/User/signIn/${email}/${password}`,null)
    
  }

  logout() {
    this.sessionService.accessTokenObj = null;
    this.sessionService.user = null;
    this.router.navigate(['/vet/home']);
  }

  getCurrentUser(): Observable<any> {
    return this.apiService.get('/auth/me');
  }
}
