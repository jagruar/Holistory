import { Component, OnInit } from '@angular/core';
import { Topic } from 'src/core/models/dtos/topic';
import { TopicsController } from 'src/core/controllers/topics.controller';
import { Attempt } from 'src/core/models/dtos/attempt';

@Component({
  selector: 'holistory-topics',
  templateUrl: './topics.component.html',
  styleUrls: ['./topics.component.css']
})
export class TopicsComponent implements OnInit {
  public topics: Topic[]

  constructor(private topicsController: TopicsController) { }

  ngOnInit() {
    this.getTopics();
  }

  public addAttempt() {
    this.topicsController.postAttempt(1, 2, 3);
    this.getTopics();
  }

  private getTopics() {
    this.topicsController.getTopics()
    .subscribe(x =>{
      console.log("topics controller subscription");
      console.log(x);
      this.topics = x;
    });
  }
}
