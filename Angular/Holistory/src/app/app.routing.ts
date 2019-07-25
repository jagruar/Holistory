import { Routes, RouterModule } from '@angular/router';


import { LoginComponent } from './login';
import { AuthGuard } from 'src/core/guards/auth.guard';
import { TopicComponent } from './topic/topic.component';
import { WorldComponent } from './world/world.component';
import { QuizComponent } from './quiz/quiz.component';
import { EventComponent } from './event/event.component';
import { ScoresComponent } from './scores/scores.component';


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
        children: [
            {
                path: 'quiz',
                component: QuizComponent
            },
            {
                path: 'events/:eventId',
                component: EventComponent
            },
            {
                path: 'scores',
                component: ScoresComponent
            },
            {
                path: '',
                redirectTo: 'scores',
                pathMatch: 'prefix'
            }
        ]
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