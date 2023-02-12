import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/app/environments/environment';
import { IActiveSubject, ISubject } from '../interfaces/interface';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class SubjectService {
  constructor(private apiService: ApiService,
    private httpClient: HttpClient) {}

  getSubjectById(id) {
    return this.httpClient.get<ISubject>(`${environment.apiUrl}/Subject/GetSubjectById/${id}`);
    // return {
    //   id: '63e023653f58bc85b535aba4',
    //   name: 'Matematika',
    //   students: [
    //     {
    //       index: 15927,
    //       id: '63e021ff3f58bc85b535aba0',
    //       name: 'Nikola',
    //       surname: 'Stosic',
    //       email: 'mistoni@gmail.com',
    //       password: '1234',
    //       subjects: [],
    //     },
    //   ],
    //   professor: {
    //     licenseNumber: 12345,
    //     id: '63e022c83f58bc85b535aba2',
    //     name: 'Ema',
    //     surname: 'Ilijic',
    //     email: 'emailijic@gmail.com',
    //     password: '1234',
    //     subjects: [],
    //   },
    // };
  }

  getAllSubjects() {
    return this.httpClient.get<IActiveSubject[]>(`${environment.apiUrl}/Subject/GetAllSubjects`);
    // return [
    //   {
    //     id: '63e023613f58bc85b535aba3',
    //     name: 'Fizika',
    //     isEnroll: false,
    //   },
    //   {
    //     id: '63e023653f58bc85b535aba4',
    //     name: 'Matematika',
    //     isEnroll: true,
    //   },
    // ];
  }

  enrollToSubject(studentId: string, subjectId: string) {
    return this.httpClient.put(`${environment.apiUrl}/User/UpdateStudentAddSubject/${studentId}/${subjectId}`,null);
  }

  getAllSubjectsForProfessor(professorId: string) {
   return this.httpClient.get<ISubject[]>(`${environment.apiUrl}/Subject/GetProfessorSubjectsByProfessorId/${professorId}`);
   
  }
}
