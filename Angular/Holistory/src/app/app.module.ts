import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { routing } from './app.routing';

import { LoginComponent } from './login';
import { JwtInterceptor } from 'src/core/interceptors/jwt.interceptor';
import { ErrorInterceptor } from 'src/core/interceptors/error.interceptor';
import { HomeComponent } from './home/home.component';
import { TopicsComponent } from './topics/topics.component';
import { WorldComponent } from './world/world.component';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        routing
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        TopicsComponent,
        WorldComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }