import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError, Observable } from 'rxjs';
import { AuthRequest, AuthResponse } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
    private baseurl = 'https://localhost:7056';
    private apiUrl = `${this.baseurl}/Auth`;

    constructor(private http: HttpClient){};

    public GenerateToken(login: string, password: string): Observable<AuthResponse>{
        const body: AuthRequest = {login, password};
        return this.http.post<AuthResponse>(`${this.apiUrl}/Token`, body)
            .pipe(catchError(this.handleError));
    }

    private handleError(error: HttpErrorResponse) : Observable<never>{
        let errorMessage = 'Неизвестная ошибка';
        if (error.error instanceof ErrorEvent){
            errorMessage = "Неверно введен логин и(или) пароль";
        } else{
            errorMessage = error.error?.message || `Ошибка сервера: ${error.status}`;
        }

        console.error('❌ Ошибка:', errorMessage);
        return throwError(() => new Error(errorMessage));
    }

}