import { Component, OnInit } from '@angular/core';
import { Event } from 'src/core/models/dtos/event';
import { TopicsController } from 'src/core/controllers/topics.controller';
import { Observable } from 'rxjs';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'holistory-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {
  public event$: Observable<Event>;

  constructor(
    private topicsController: TopicsController,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.event$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.topicsController.getEvent(+params.get('topicId'), +params.get('eventId'))));
  }

}
