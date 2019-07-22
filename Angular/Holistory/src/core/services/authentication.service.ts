import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { GenerateAuthTokenCommand } from '../models/commands/generateAuthToken.command';
import { LoggingService } from './logging.service';
import { Identity } from '../models/dtos/identity';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentTokenSubject: BehaviorSubject<string>;
    public currentToken: Observable<string>;
    private currentUserIdSubject: BehaviorSubject<string>;
    public currentUserId: Observable<string>;
    private isAuthed: boolean;

    constructor(private http: HttpClient, private logger: LoggingService) {
        this.currentTokenSubject = new BehaviorSubject<string>(localStorage.getItem('holistory-token'));
        this.currentToken = this.currentTokenSubject.asObservable();
        this.currentUserIdSubject = new BehaviorSubject<string>(localStorage.getItem('holistory-user-id'));
        this.currentUserId = this.currentUserIdSubject.asObservable();
    }

    public get token(): string {
        return this.currentTokenSubject.value;
    }

    public get userId(): string {
        return this.currentUserIdSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<Identity>(`${environment.apiUrl}/users/token`, { username, password })
            .pipe(map(response => {
                this.logger.log("response received from users/token")
                this.logger.log(response)
                // login successful if there's a jwt token in the response
                if (response && response.token && response.id) {
                    this.logger.log("setting local storage")
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('holistory-token', response.token);
                    localStorage.setItem('holistory-user-id', response.id);
                    this.currentTokenSubject.next(response.token);
                    this.currentUserIdSubject.next(response.id);
                }
                return response;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('holistory-token');
        this.currentTokenSubject.next(null);
        localStorage.removeItem('holistory-user-id');
        this.currentUserIdSubject.next(null);
    }
}