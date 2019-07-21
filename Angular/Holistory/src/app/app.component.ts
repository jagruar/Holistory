import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/core/services/authentication.service';

@Component({ selector: 'holistory-root', templateUrl: 'app.component.html' })
export class AppComponent {
    currentToken: string;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        this.authenticationService.currentToken.subscribe(x => this.currentToken = x);
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}