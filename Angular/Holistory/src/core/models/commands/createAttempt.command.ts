export class CreateAttemptCommand {
    constructor(
        public userId: string,
        public topicId: number,
        public correct: number,
        public incorrect: number
    ) {        
    }
}