import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Topic } from '../models/dtos/topic';
import { Attempt } from '../models/dtos/attempt';

@Injectable({ providedIn: 'root' })
export class TopicsController {
    private topics = new BehaviorSubject<Topic[]>(null);
    private topics$ = this.topics.asObservable();

    constructor(private http: HttpClient) {
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
    
    public addAttempt(topicId: number, correct: number, incorrect: number) {
        let topics = this.topics.getValue();
        let topic = topics.find(x => x.id === topicId);
        console.log(topics);
        let attempt = new Attempt();
        attempt.correct = correct;
        attempt.incorrect = incorrect;
        attempt.topicId = topicId;
        topic.attempts.push(attempt);
        console.log(topics);
        this.topics.next(topics);        
    }
}