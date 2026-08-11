import { Component, ChangeDetectorRef, NgZone } from "@angular/core";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router'; 
import { AuthService } from '../../services/auth.service';
import { AuthResponse } from '../../models/auth.model';


@Component({
    selector: 'app-auth',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './auth.component.html',
    styleUrl: './auth.component.css'
})

export class AuthComponent{
    authUser = {
        login: '',
        password: ''
    }

    isLoading = false;
    errorMessage = '';
    successMessage = '';

    constructor(private authService: AuthService, private cdr: ChangeDetectorRef, private router: Router, private ngZone: NgZone){}

    cancelForm(): void{
        console.log("Очистка формы");
        this.authUser.login = '';
        this.authUser.password = '';
        this.errorMessage = '';
        this.successMessage = '';
    }



    auth(): void{
        if(this.authUser.login.trim() == "" || this.authUser.password.trim() == ""){
            this.errorMessage = "Необходимо заполнить все поля";
            return
        }

        this.isLoading = true;
        this.errorMessage = '';
        this.successMessage = '';

        this.authService.GenerateToken(
            this.authUser.login,
            this.authUser.password
        ).subscribe({
            next: (response: AuthResponse) => {
                this.ngZone.run(() => {  
                    this.successMessage = 'Успешный вход!';
                    localStorage.setItem('token', response.token);
                    this.router.navigate(['/tasks']);
                    this.authUser = { login: '', password: '' };
                    this.isLoading = false;
                    this.cdr.detectChanges();
                });
            },
            error: (err) => {
                this.ngZone.run(() => { 
                    this.isLoading = false;
                    this.errorMessage = err.message || 'Ошибка при авторизации';
                    this.cdr.detectChanges();
                    console.error('Ошибка:', err);
                });
            }
        });
    }
}