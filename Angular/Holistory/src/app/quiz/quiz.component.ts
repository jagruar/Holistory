import { Component, OnInit } from '@angular/core';
import { TopicsController } from 'src/core/controllers/topics.controller';
import { Question } from 'src/core/models/dtos/question';
import { Observable } from 'rxjs';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'holistory-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {
  public questions$: Observable<Question[]>

  constructor(
    private topicsController: TopicsController,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.questions$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        return this.topicsController.getQuestions(+params.get('topicId'));
      })        
    );

  }

}
