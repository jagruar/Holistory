import { Routes, RouterModule } from '@angular/router';


import { LoginComponent } from './login';
import { AuthGuard } from 'src/core/guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { TopicsComponent } from './topics/topics.component';
import { WorldComponent } from './world/world.component';


const appRoutes: Routes = [
    {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'topics',
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