import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/app/environments/environment";

@Injectable()
export class MaterialService {


 constructor(private httpClient: HttpClient) {}
 getMaterial(fileid)
 {
    return this.httpClient.get(`${environment.apiUrl}/Material/get/${fileid}`)
 }
 setMaterial(file,subjectId)
 {
    return this.httpClient.post(`${environment.apiUrl}/Material/save/${subjectId}`,file)
 }
}