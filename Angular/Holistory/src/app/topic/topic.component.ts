import { Component, OnInit } from '@angular/core';
import { Topic } from 'src/core/models/dtos/topic';
import { TopicsController } from 'src/core/controllers/topics.controller';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Event } from 'src/core/models/dtos/event';
import { LoggingService } from 'src/core/services/logging.service';

@Component({
  selector: 'holistory-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.css']
})
export class TopicComponent implements OnInit {
  public topic$: Observable<Topic>
  public event: Event;
  public quizActive: boolean;
  numbers = Array<number>(100).fill(1).map((x,i)=>i);

  constructor(
    private topicsController: TopicsController,
    private route: ActivatedRoute,
    private logger: LoggingService) { }

  ngOnInit() {
    this.topic$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        return this.topicsController.getTopic(+params.get('topicId'));
      })        
    );
    console.log(this.numbers);
  }

  public addAttempt() {
    this.topicsController.postAttempt(1, 2, 3);
  }

  public selectEvent(event: Event) {
    this.logger.log("selecting event " + event.id);
    this.event = event;
  }

  public startQuiz() {
    this.quizActive = true;
  }
}
