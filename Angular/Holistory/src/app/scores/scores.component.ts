import { Component, OnInit } from '@angular/core';
import { TopicsController } from 'src/core/controllers/topics.controller';
import { Attempt } from 'src/core/models/dtos/attempt';
import { Observable } from 'rxjs';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'holistory-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.css']
})
export class ScoresComponent implements OnInit {
  public attempts$: Observable<Attempt[]>

  constructor(
    private topicsController: TopicsController,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.attempts$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        return this.topicsController.getAttempts(+params.get('topicId'));
      })        
    );
  }

}
