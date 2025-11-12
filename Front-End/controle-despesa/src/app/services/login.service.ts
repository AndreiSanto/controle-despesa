import { Inject, Injectable } from "@angular/core";
import { environment } from "../environment/environment";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
 private apiUrl = `${environment.apiUrl}`;
 
    constructor(private httpClient: HttpClient) { }  


}