import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login';
import { AuthGuard } from 'src/core/guards/auth.guard';
import { TopicComponent } from './topic/topic.component';
import { WorldComponent } from './world/world.component';
import { QuizComponent } from './quiz/quiz.component';

const appRoutes: Routes = [
    {
        path: '',
        component: WorldComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'topics/:topicId',
        component: TopicComponent,
        canActivate: [AuthGuard],
    },
    {
        path: 'topics/:topicId/quiz',
        component: QuizComponent,
        canActivate: [AuthGuard],
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