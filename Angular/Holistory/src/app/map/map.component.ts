import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Event } from 'src/core/models/dtos/event';
import { LoggingService } from 'src/core/services/logging.service';

@Component({
  selector: 'holistory-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  @Input() events: Event[];
  @Output() eventSelected = new EventEmitter<Event>();

  constructor(private logger: LoggingService) { }

  ngOnInit() {
  }

  selectEvent(event: Event) {
    this.logger.log("event " + event.id + " selected");
    this.eventSelected.emit(event);
  }

}
