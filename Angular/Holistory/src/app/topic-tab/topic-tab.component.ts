import { Component, OnInit, Input } from '@angular/core';
import { Topic } from 'src/core/models/dtos/topic';
import { TopicStatus } from 'src/core/models/dtos/topic-status';
import { Router } from '@angular/router';

@Component({
  selector: 'holistory-topic-tab',
  templateUrl: './topic-tab.component.html',
  styleUrls: ['./topic-tab.component.css']
})
export class TopicTabComponent implements OnInit {
  @Input() topic: Topic;
  public color: string;

  constructor() { }

  ngOnInit() {    
      console.log("calculating status");
      if (this.topic.attempts.length == 0) {
          this.color = "red";
      } else if (this.topic.attempts.some(x => x.incorrect == 0)) {
        this.color = "green";
      } else {
        this.color = "yellow";
      }  
  }
}
