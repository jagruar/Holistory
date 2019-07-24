import { Event } from './event';
import { Attempt } from './attempt';
import { Question } from './question';

export class Topic {
    id: number;
    title: string;
    description: string;
    eraId: number;
    regionId: number;
    events: Event[];
    attempts: Attempt[];
    questions: Question[];

    constructor() {
    }

    public addAttempt(attempt: Attempt) {
        this.attempts.push(attempt);
    }
}