import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProfessor } from '../interfaces/interface';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class ProfessorService {
  constructor(private apiService: ApiService,
    private httpClient: HttpClient) {}

  getAllProfessors() {
    return this.httpClient.get<IProfessor[]>(`https://localhost:5001/User/GetAllProfessor`);
    // return [
    //   {
    //     licenseNumber: 12345,
    //     id: '63e022c83f58bc85b535aba2',
    //     name: 'Ema',
    //     surname: 'Ilijic',
    //     email: 'emailijic@gmail.com',
    //     password: '1234',
    //     subjects: [],
    //   },
    // ];
  }
}
