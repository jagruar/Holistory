import { Event } from './event';
import { Attempt } from './attempt';
import { Question } from './question';
import { TopicStatus } from './topic-status';

export class Topic {
    id: number;
    eraId: number;
    regionId: number;
    events: Event[];
    attempts: Attempt[];
    questions: Question[];
    status: TopicStatus;
}