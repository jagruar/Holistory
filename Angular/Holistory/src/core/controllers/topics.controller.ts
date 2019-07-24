import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

    // public getTopics(): Observable<Topic[]> {
    //     console.log(this.topics);
    //     if (this.topics.value) {
    //         console.log("topics exists");
    //         return this.topics$;
    //     } else {
    //         console.log("making call for topics");
    //         return this.http.get<Topic[]>(`${environment.apiUrl}/topics`)
    //             .pipe(map(x => {
    //                 this.logger.log("succesful got topics");
    //                 this.logger.log(x);
    //                 this.topics.next(x);
    //                 return x;   
    //             }));            
    //     }
    // }

    public getTopics(): Observable<Topic[]> {
        return this.http.get<Topic[]>(`${environment.apiUrl}/topics`);
        //.pipe(map(topics => {
        //     topics.forEach((y) => {
        //         y.attempts.push(y.attempts);
        //         y.attempts.forEach(z => console.log(z));
        //         y.attempts = topics[i].attempts;
        //         console.log(topics[i].attempts);
        //         console.log(y);
        //     });
        //     console.log(topics);
        //     console.log(topics[0].attempts);
        //     return topics;
        // }))
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