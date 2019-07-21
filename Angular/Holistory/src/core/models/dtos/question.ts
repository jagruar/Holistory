import { Answer } from './answer';

export class Question {
    id: number;
    topicId: number;
    eventId: number;
    text: string;
    answers: Answer[];
}