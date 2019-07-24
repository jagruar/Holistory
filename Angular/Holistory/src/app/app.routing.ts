import { Routes, RouterModule } from '@angular/router';


import { LoginComponent } from './login';
import { AuthGuard } from 'src/core/guards/auth.guard';
import { TopicsComponent } from './topics/topics.component';
import { WorldComponent } from './world/world.component';


const appRoutes: Routes = [
    {
        path: '',
        component: WorldComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'topics/:id',
        component: TopicsComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'world',
        component: WorldComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'login',
        component: LoginComponent
    },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);