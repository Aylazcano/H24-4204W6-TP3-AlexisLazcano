import { Component, OnInit } from '@angular/core';
import { Score } from '../models/score';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoreComponent implements OnInit {

  myScores : Score[] = [];
  publicScores : Score[] = [];
  userIsConnected : boolean = false;

  constructor() { }

  async ngOnInit() {

    this.userIsConnected = sessionStorage.getItem("token") != null;


  }

  async changeScoreVisibility(score : Score){


  }

}
