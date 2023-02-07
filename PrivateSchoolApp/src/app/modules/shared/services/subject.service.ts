import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class SubjectService {
  constructor(private apiService: ApiService) {}

  getSubjectById(id) {
    // return this.apiService.get(`/Subject/GetSubjectById/${id}`);
    return {
      id: '63e023653f58bc85b535aba4',
      name: 'Matematika',
      students: [
        {
          index: 15927,
          id: '63e021ff3f58bc85b535aba0',
          name: 'Nikola',
          surname: 'Stosic',
          email: 'mistoni@gmail.com',
          password: '1234',
          subjects: [],
        },
      ],
      professor: {
        licenseNumber: 12345,
        id: '63e022c83f58bc85b535aba2',
        name: 'Ema',
        surname: 'Ilijic',
        email: 'emailijic@gmail.com',
        password: '1234',
        subjects: [],
      },
    };
  }

  getAllSubjects(studentId: string) {
    // return this.apiService.get(`/Subject/GetAllSubjectsForActiveStudent/${studentId}`);
    return [
      {
        id: '63e023613f58bc85b535aba3',
        name: 'Fizika',
        isEnroll: false,
      },
      {
        id: '63e023653f58bc85b535aba4',
        name: 'Matematika',
        isEnroll: true,
      },
    ];
  }

  enrollToSubject(studentId: string, subjectId: string) {
    return this.apiService.put(`User/UpdateStudentAddSubject/${studentId}/${subjectId}`);
  }

  getAllSubjectsForProfessor(professorId: string) {
   return this.apiService.get(`Subject/GetProfessorSubjectsByProfessorId/${professorId}`);
   
  }
}
