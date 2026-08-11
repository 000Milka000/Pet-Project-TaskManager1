import { Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list.component';
import { TaskCreateComponent } from './components/task-create/task-create.component';
// import { UserListComponent } from './components/user-list/user-list.component';
// import { UserCreateComponent } from './components/user-create/user-create.component';
import { AuthComponent } from './components/auth/auth.component'; 

export const routes: Routes = [
    {path: '', redirectTo: '/tasks', pathMatch: 'full'},
    {path: 'tasks', component: TaskListComponent},
    {path: 'tasks/create', component: TaskCreateComponent},
    // { path: 'users', component: UserListComponent },
    // { path: 'users/create', component: UserCreateComponent }
    {path: 'auth', component: AuthComponent} 
];
