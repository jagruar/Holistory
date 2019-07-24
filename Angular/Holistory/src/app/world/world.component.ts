import { Component, OnInit } from '@angular/core';
import { ErasController } from 'src/core/controllers/eras.controller';
import { RegionsController } from 'src/core/controllers/regions.controller';
import { Era } from 'src/core/models/dtos/era';
import { Region } from 'src/core/models/dtos/region';
import { Topic } from 'src/core/models/dtos/topic';
import { TopicsController } from 'src/core/controllers/topics.controller';
import { Observable, VirtualTimeScheduler } from 'rxjs';

@Component({
  selector: 'holistory-world',
  templateUrl: './world.component.html',
  styleUrls: ['./world.component.css']
})
export class WorldComponent implements OnInit {
  eras: Era[];
  regions: Region[];
  topics: Topic[];
  boobs: boolean;
  // topics$: Observable<Topic[]>;

  constructor(
    private erasController: ErasController,
    private regionsControlller: RegionsController,
    private topicsController: TopicsController) { }

  ngOnInit() {
    this.regionsControlller.getRegions().subscribe(x => this.regions = x);
    this.erasController.getEras().subscribe(x => this.eras = x);
    this.topicsController.getTopics().subscribe(x => { console.log(x); this.topics = x; });
    // this.topics$ = this.topicsController.getTopics();
  }

  // public getFilteredTopics(eraId: number, regionId: number): Topic[] {
  //   this.topics$;
  //   console.log(topics);
  //   return topics.filter(x => x.eraId == eraId && x.regionId == regionId);
  // }
  public showBoobs() {
    this.boobs = true;
    console.log(this.topics);
  }

  public getFilteredTopics(eraId: number, regionId: number): Topic[] {
    let filteredTopics: Topic[] = [];
    for (let i = 0; i < this.topics.length; i++) {
      // console.log(this.topics[i].attempts);
      if (this.topics[i].eraId == eraId && this.topics[i].regionId == regionId) {
        // let topic = new Topic();
        // topic.title = this.topics[i].title;
        // topic.attempts = this.topics[i].attempts;
        // console.log("topic found");
        // console.log(topic);
        console.log("pre push");
        console.log(this.topics[i]);
        //filteredTopics.push(this.topics[i]); 
        console.log("post push");
        console.log(this.topics[i]);
        //filteredTopics[filteredTopics.length - 1].attempts = this.topics[i].attempts;
      }
    }
    return filteredTopics;
    //return filteredTopics;

    //return this.topics.filter(x => x.eraId == eraId && x.regionId == regionId);
  }
}
