import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { routing } from './app.routing';

import { LoginComponent } from './login';
import { JwtInterceptor } from 'src/core/interceptors/jwt.interceptor';
import { ErrorInterceptor } from 'src/core/interceptors/error.interceptor';
import { TopicComponent } from './topic/topic.component';
import { WorldComponent } from './world/world.component';
import { TopicTabComponent } from './topic-tab/topic-tab.component';
import { QuizComponent } from './quiz/quiz.component';
import { EventComponent } from './event/event.component';
import { MapComponent } from './map/map.component';
import { QuizMapComponent } from './quiz-map/quiz-map.component';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        routing
    ],
    declarations: [
        AppComponent,
        LoginComponent,
        TopicComponent,
        WorldComponent,
        TopicTabComponent,
        QuizComponent,
        EventComponent,
        MapComponent,
        QuizMapComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }