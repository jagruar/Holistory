import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Topic } from '../models/dtos/topic';
import { Attempt } from '../models/dtos/attempt';
import { LoggingService } from '../services/logging.service';
import { CreateAttemptCommand } from '../models/commands/createAttempt.command';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({ providedIn: 'root' })
export class TopicsController {
    private topics = new BehaviorSubject<Topic[]>(null);
    private topics$ = this.topics.asObservable();

    constructor(
        private http: HttpClient,
        private auth: AuthenticationService,
        private logger: LoggingService) {
    }

    public getTopics(): Observable<Topic[]> {
        console.log(this.topics);
        if (this.topics.value) {
            console.log("topics exists");
            return this.topics$;
        } else {
            console.log("making call for topics");
            return this.http.get<Topic[]>(`${environment.apiUrl}/topics`)
                .pipe(map(x => {
                    this.topics.next(x);
                    return x;   
                }));            
        }
    }

    public postAttempt(topicId: number, correct: number, incorrect: number) {

        let command = new CreateAttemptCommand(
            this.auth.userId,
            topicId,
            correct,
            incorrect
        );

        this.http.post<Attempt>(`${environment.apiUrl}/topics/${topicId}/attempts`, command)
            .subscribe(attempt => {         
                this.logger.log("attempt created with id: " + attempt.id);
                let topics = this.topics.getValue();
                let topic = topics.find(x => x.id === attempt.topicId);
                topic.attempts.push(attempt);
            });
    }
}