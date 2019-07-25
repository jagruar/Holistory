import { Component, OnInit } from '@angular/core';
import { Topic } from 'src/core/models/dtos/topic';
import { TopicsController } from 'src/core/controllers/topics.controller';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Event } from 'src/core/models/dtos/event';

@Component({
  selector: 'holistory-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.css']
})
export class TopicComponent implements OnInit {
  public topic$: Observable<Topic>
  public event: Event;

  constructor(
    private topicsController: TopicsController,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.topic$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        return this.topicsController.getTopic(+params.get('topicId'));
      })        
    );
  }

  public addAttempt() {
    this.topicsController.postAttempt(1, 2, 3);
  }

  public selectEvent(event: Event) {
    this.event = event;
  }
}
