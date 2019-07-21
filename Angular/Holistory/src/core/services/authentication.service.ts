import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { GenerateAuthTokenCommand } from '../models/commands/generateAuthToken.command';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentTokenSubject: BehaviorSubject<string>;
    public currentToken: Observable<string>;
    private isAuthed: boolean;

    constructor(private http: HttpClient) {
        this.currentTokenSubject = new BehaviorSubject<string>(localStorage.getItem('holistory-token'));
        this.currentToken = this.currentTokenSubject.asObservable();
    }

    public get currentTokenValue(): string {
        return this.currentTokenSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/users/token`, { username, password })
            .pipe(map(response => {
                console.log(response)
                // login successful if there's a jwt token in the response
                if (response && response.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('holistory-token', response.token);
                    this.currentTokenSubject.next(response.token);
                }
                return response;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('holistory-token');
        this.currentTokenSubject.next(null);
    }
}