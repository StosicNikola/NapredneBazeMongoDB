import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/app/environments/environment";
import { INotification } from "../interfaces/interface";

@Injectable()
export class NotificationService
{
    constructor(private httpClient: HttpClient){}


    getNotification(id:string)
    {
        return this.httpClient.get<INotification[]>(`${environment.apiUrl}/Notification/GetNotificationsForSubject/${id}`)
    }
    createNotification(idSubject:string,idProfessor:string, description:string)
    {
        return this.httpClient.post(`${environment.apiUrl}/Notification/CreateNotification/${idProfessor}/${idSubject}/${description}`,null)
    }
    getAllNotificatins()
    {
        return this.httpClient.get<INotification[]>(`${environment.apiUrl}/Notification/GetNotifications`)
    }

}