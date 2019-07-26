import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Coordinates } from 'src/core/models/design/coordinates';

@Component({
  selector: 'holistory-quiz-map',
  templateUrl: './quiz-map.component.html',
  styleUrls: ['./quiz-map.component.css']
})
export class QuizMapComponent implements OnInit {
  @Output() gridClicked = new EventEmitter<Coordinates>();
  numbers = Array<number>(100).fill(1).map((x,i)=>i);

  constructor() { }

  ngOnInit() {
  }

  public sendGridClicked(i: number, j: number){
    this.gridClicked.emit(new Coordinates(i, j));
  }
}
