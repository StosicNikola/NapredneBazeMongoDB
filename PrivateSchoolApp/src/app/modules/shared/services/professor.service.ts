import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class ProfessorService {
  constructor(private apiService: ApiService) {}

  getAllProfessors() {
    // return this.apiService.get(`/User/GetAllProfessors`);
    return [
      {
        licenseNumber: 12345,
        id: '63e022c83f58bc85b535aba2',
        name: 'Ema',
        surname: 'Ilijic',
        email: 'emailijic@gmail.com',
        password: '1234',
        subjects: [],
      },
    ];
  }
}
