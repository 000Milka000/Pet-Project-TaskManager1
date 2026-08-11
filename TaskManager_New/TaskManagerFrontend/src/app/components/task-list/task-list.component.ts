import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task.service';
import { Router } from '@angular/router'; 
import { TaskItem } from '../../models/task.model';

@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './task-list.component.html', 
    styleUrls: ['./task-list.component.css']    
})

export class TaskListComponent{
    tasks: TaskItem[] = [];
    myTasks: TaskItem[] = [];
    isLoading = false;



    constructor(private taskService: TaskService, private router: Router){}

    newTask(): void{
        this.router.navigate(['/tasks/create']);
    }

    deleteTask(title: string, userId: number): void{
        // this.taskService.deleteTask(title, userId).subscribe({
        //     next: () => {
        //         alert("Задача удалена");
        //     },
        //     error: () => {
        //         alert("Возникла ошибка при удалении");
        //     }
        // })
    }

    selectAllTasks(){
        
    }

    toggleMyTaskSelection(){

    }

    loadTasks(): void{
        this.isLoading = true;
        this.taskService.getAllTasks().subscribe({
            next: (tasks) => {
                this.tasks = tasks.map(task => ({
                    ...task,
                    selected: false
                }))
            }
        })
    }
}