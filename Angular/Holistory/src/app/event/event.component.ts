import { Component, OnInit, Input } from '@angular/core';
import { Event } from 'src/core/models/dtos/event';

@Component({
  selector: 'holistory-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {
  @Input() event: Event;

  constructor() { }

  ngOnInit() {
  }

}
