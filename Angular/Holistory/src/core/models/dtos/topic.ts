import { Event } from './event';
import { Attempt } from './attempt';
import { Question } from './question';
import { TopicStatus } from './topic-status';

export class Topic {
    id: number;
    title: string;
    description: string;
    eraId: number;
    regionId: number;
    events: Event[];
    attempts: Attempt[];
    questions: Question[];
    status: TopicStatus;

    constructor() {
        this.setStatus();
        console.log("status set to " + this.status);
    }

    public addAttempt(attempt: Attempt) {
        this.attempts.push(attempt);
        this.setStatus();
    }

    private setStatus() {
        if (this.attempts.length = 0) {
            this.status = TopicStatus.NotAttempted;
        } else if (this.attempts.some(x => x.incorrect == 0)) {
            this.status = TopicStatus.Completed
        } else {
            this.status = TopicStatus.Attempted;
        }
    }
}