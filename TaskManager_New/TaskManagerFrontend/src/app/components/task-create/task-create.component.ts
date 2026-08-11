import { Component, ChangeDetectorRef } from '@angular/core'; 
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../services/task.service';


@Component({
    selector: 'app-task-create',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './task-create.component.html',
    styleUrl: './task-create.component.css'
})

export class TaskCreateComponent{
    task = {
        title: '',
        description: '',
        userId: 0
    }

    showForm = false;
    isLoading = false;
    successMessage = '';
    errorMessage = '';

    constructor(private taskService: TaskService, private cdr: ChangeDetectorRef){}

    addTask(): void{
        this.showForm = true;
        this.errorMessage = '';
        this.successMessage = ''
    }

    cancelForm(): void{
        this.showForm = false;
        this.task.title = '';
        this.task.description = '';
        this.task.userId = 0;
        this.errorMessage = '';
        this.successMessage = ''
    }

    createTask(): void{
        if(this.task.title.trim() == "" || this.task.userId === 0){
            this.errorMessage = 'Необходимо заполнить все поля';
            return
        }

        this.isLoading = true;
        this.successMessage = '';
        this.errorMessage = '';

        this.taskService.createTask(
            this.task.title,
            this.task.description,
            this.task.userId
        ).subscribe({
            next: (createdTask) => {
                this.successMessage = `Задача ${createdTask.title} создана!`;
                this.task = {
                    title: '',
                    description: '',
                    userId: 0
                }
                this.showForm = false;
                this.isLoading = false;
                this.cdr.detectChanges();
            },
            error: (err) => {
                this.errorMessage = `Ошибка при создании`;
                this.isLoading = false;
                console.error(err);
            }
        });

    }


}
