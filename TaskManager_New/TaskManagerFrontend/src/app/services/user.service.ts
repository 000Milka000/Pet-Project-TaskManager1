import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { TaskItem } from "../models/task.model";

@Injectable({
  providedIn: 'root'
})

export class UserServices{
    private baseurl = 'https://localhost:7056';
    private apiUrl = `${this.baseurl}/User`;

    constructor(private http: HttpClient) { }

    public getAllUsers(): Observable<User[]>{
        return this.http.get<User[]>(`${this.apiUrl}/GetAllUsers`)
    }

    public getTasksByUser(id: number): Observable<TaskItem[]>{
        return this.http.get<TaskItem[]>(`${this.apiUrl}/GetTasksByUser`, {
            params: {
                id: id.toString()
            }
        })
    }

    public createUser(name: string, login: string, password: string): Observable<User>{
        const body = {name, login, password};
        return this.http.post<User>(`${this.apiUrl}/CreateUser`, body)
    }

    public deleteUser(login: string): Observable<void>{
        return this.http.delete<void>(`${this.apiUrl}/DeleteUser`, {
            params: {
                login
            }
        })
    }
}