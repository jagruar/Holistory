import { Component, OnInit } from '@angular/core';
import { TopicsController } from 'src/core/controllers/topics.controller';
import { Observable } from 'rxjs';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Topic } from 'src/core/models/dtos/topic';
import { Coordinates } from 'src/core/models/design/coordinates';
import { LoggingService } from 'src/core/services/logging.service';
import { QuizEvent } from 'src/core/models/design/quiz-event';

@Component({
  selector: 'holistory-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {
  public topic$: Observable<Topic>;
  public quizEvents: QuizEvent[];
  public activeEvent: QuizEvent;

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

    this.topic$.subscribe(x => {
      let order = this.getRandomOrder(x.events.length);
      this.quizEvents = x.events.map((y, i) => new QuizEvent(y, order[i]));
    });
  }

  setActiveEvent(event: QuizEvent) {
    this.logger.log("setting active event to event: " + event.event.id);
    this.activeEvent = event;
  }

  checkAnswer(coordinates: Coordinates ) {
    this.logger.log("checking coordinates");
    this.logger.log(coordinates);
    let distance = this.getDistance(this.activeEvent.event.x, this.activeEvent.event.y, coordinates);
    if (distance < 5) {
      this.activeEvent.located = true;
    }
  }

  getRandomOrder(count: number) {
    let array = Array<number>(count).fill(1).map((x,i)=>i);
    array.sort(() => Math.random() - 0.5);
    this.logger.log("random order created");
    this.logger.log(array);
    return array;
  }

  getDistance(x: number, y: number, coordinates: Coordinates): number {
    let distance = Math.sqrt(Math.pow(y - coordinates.y, 2) + Math.pow(x - coordinates.x, 2))
    this.logger.log("distance from target calculated as: " + distance);
    return distance;
  }
}
