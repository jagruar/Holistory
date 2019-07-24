import { Component, OnInit } from '@angular/core';
import { ErasController } from 'src/core/controllers/eras.controller';
import { RegionsController } from 'src/core/controllers/regions.controller';
import { Era } from 'src/core/models/dtos/era';
import { Region } from 'src/core/models/dtos/region';
import { Topic } from 'src/core/models/dtos/topic';
import { TopicsController } from 'src/core/controllers/topics.controller';

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

  constructor(
    private erasController: ErasController,
    private regionsControlller: RegionsController,
    private topicsController: TopicsController) { }

  ngOnInit() {
    this.regionsControlller.getRegions().subscribe(x => this.regions = x);
    this.erasController.getEras().subscribe(x => this.eras = x);
    this.topicsController.getTopics().subscribe(x => { console.log(x); this.topics = x; });
  }
}
