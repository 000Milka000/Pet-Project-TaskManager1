import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task.service';
import { TaskItem } from '../../models/task.model';

@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './task-list.component.html',   // ← HTML в отдельном файле!
    styleUrls: ['./task-list.component.css']    // ← CSS в отдельном файле!
})
export class TaskListComponent implements OnInit {
    tasks: TaskItem[] = [];
    isLoading = false;
    error: string | null = null;

    constructor(private taskService: TaskService) {}

    ngOnInit(): void {
        this.loadTasks();
    }

    loadTasks(): void {
        this.isLoading = true;
        this.error = null;

        this.taskService.getAllTasks().subscribe({
            next: (data) => {
                this.tasks = data;
                this.isLoading = false;
            },
            error: (err) => {
                this.error = 'Ошибка загрузки: ' + err.message;
                this.isLoading = false;
                console.error(err);
            }
        });
    }

    deleteTask(title: string, userId: number): void {
        if (confirm(`Удалить задачу "${title}"?`)) {
            this.taskService.deleteTask(title, userId).subscribe({
                next: () => {
                    this.tasks = this.tasks.filter(t => t.title !== title || t.userId !== userId);
                },
                error: (err) => {
                    console.error('Ошибка удаления:', err);
                    alert('Не удалось удалить задачу');
                }
            });
        }
    }
}