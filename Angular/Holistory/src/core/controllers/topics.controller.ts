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
import { Question } from '../models/dtos/question';
import { Event } from '../models/dtos/event';

@Injectable({ providedIn: 'root' })
export class TopicsController {
    private topics = new BehaviorSubject<Topic[]>(null);
    private topics$ = this.topics.asObservable();

    private topic = new BehaviorSubject<Topic>(null);
    private topic$ = this.topic.asObservable();

    constructor(
        private http: HttpClient,
        private auth: AuthenticationService,
        private logger: LoggingService) {
    }

    public getTopics(): Observable<Topic[]> {
        if (this.topics.value) {
            this.logger.log("topics exists");
            return this.topics$;
        } else {
            this.logger.log("making call for topics");
            return this.http.get<Topic[]>(`${environment.apiUrl}/topics`)
                .pipe(map(x => {
                    this.logger.log("succesfuly got topics");
                    this.topics.next(x);
                    return x;   
                }));            
        }
    }

    public getTopic(id: number): Observable<Topic> {
        if (this.topic.value) {
            this.logger.log("topic exists");
            return this.topic$;
        } else {
            this.logger.log("making call for topic " + id);
            return this.http.get<Topic>(`${environment.apiUrl}/topics/${id}`)
                .pipe(map(x => {
                    this.logger.log("succesfuly got topic " + id);
                    this.topic.next(x);
                    return x;   
                }));            
        }
    }

    public getAttempts(topicId: number): Observable<Attempt[]> {
        return this.getTopic(topicId).pipe(map(x => x.attempts));
    }

    public getQuestions(topicId: number): Observable<Question[]> {
        return this.getTopic(topicId).pipe(map(x => x.questions));
    }

    public getEvent(topicId: number, eventId: number): Observable<Event> {
        return this.getTopic(topicId).pipe(map(x => x.events.find(y => y.id == eventId)));
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