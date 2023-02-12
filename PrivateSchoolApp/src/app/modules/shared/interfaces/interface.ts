import { UserType } from '../enum/user.type.enum';

export interface IUser {
  _id?: string;
  name: string;
  surname: string;
  email: string;
  password: string;
  phoneNumber: number;
  role: UserType;
  education: string;
  // image?: IImage;
}

export interface IStudent {
  _id?: string;
  name: string;
  surname: string;
  email: string;
  password: string;
  index: number;
  subjects: ISubject[];
}

export interface IProfessor {
  _id?: string;
  name: string;
  surname: string;
  email: string;
  password: string;
  licenseNumber: number;
}

export interface ISubject {
  id?: string;
  name: string;
  students: IStudent[];
  professor: IProfessor;
}

export interface IImage {
  originalName: string;
  fileName: string;
  path: string;
}

export interface IActiveSubject {
   id: string;
   name: string;
   isEnroll: boolean;
}


export interface ISingInResponse
{
    id:string;
    role:number;
}

export interface INotification
{
  id:string;
  date:Date;
  subject:IStudent;
  professor: IProfessor;
  opis: String;
}