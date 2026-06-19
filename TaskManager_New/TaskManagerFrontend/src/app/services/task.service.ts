import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaskItem } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})

export class TaskService {
  private baseurl = 'https://localhost:7056';
  private apiUrl = `${this.baseurl}/Tasks`;

  constructor(private http: HttpClient) { }

  public getAllTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(`${this.apiUrl}/GetAllTasks`);
  }

  public getTaskByTitle(title: string): Observable<TaskItem> {
    return this.http.get<TaskItem>(`${this.apiUrl}/GetTaskByTitle`, {
      params: {
        title
      }
    });
  }

  public getUserTasks(id: number): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(`${this.apiUrl}/GetUserTasks`, {
      params: {
        id: id.toString()
      }
    })
  }

  public createTask(title: string, description: string, userId: number): Observable<TaskItem> {
    const body = { title, description, userId };
    return this.http.post<TaskItem>(`${this.apiUrl}/CreateTask`, body);
  }

  public deleteTask(title: string, userId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteTask`, {
      params: {
        title: title,
        userId: userId.toString()
      }
    });
  }
}
