import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class StudentService {
  constructor(
    private apiService: ApiService
  ) {}

  getStudentById(id) {
    // return this.apiService.get(`/User/GetStudentById/${id}`);
    return {
        "index": 15927,
        "id": "63e021ff3f58bc85b535aba0",
        "name": "Nikola",
        "surname": "Stosic",
        "email": "mistoni@gmail.com",
        "password": "1234",
        "subjects": [
          {
            "id": "63e023653f58bc85b535aba4",
            "name": "Matematika",
            "students": [],
            "professor": {
              "licenseNumber": 12345,
              "id": "63e022c83f58bc85b535aba2",
              "name": "Ema",
              "surname": "Ilijic",
              "email": "emailijic@gmail.com",
              "password": "1234",
              "subjects": []
            }
          }
        ]
      }
  }
}
