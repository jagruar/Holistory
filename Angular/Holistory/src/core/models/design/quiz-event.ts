import { Event } from '../dtos/event';

export class QuizEvent{
    constructor(public event: Event, order: number) {
        this.order = order;
    }
    order: number;
    ordered: boolean;
    located: boolean;
}