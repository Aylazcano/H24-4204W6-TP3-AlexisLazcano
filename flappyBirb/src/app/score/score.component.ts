import { Component, OnInit } from '@angular/core';
import { Score } from '../models/score';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoreComponent implements OnInit {

  myScores: Score[] = [];
  publicScores: Score[] = [];
  userIsConnected: boolean = false;

  constructor(public http: HttpClient) { }

  async ngOnInit() {

    this.userIsConnected = sessionStorage.getItem("token") != null;
    if (this.userIsConnected) this.getMyScore();
    this.getPublicScore();

  }

  async getMyScore(): Promise<void> {
    let x = await lastValueFrom(this.http.get<Score[]>("https://localhost:7065/api/Scores/GetMyScores"));
    console.log(x);
    this.myScores = x;
  }


  async getPublicScore(): Promise<void> {
    let x = await lastValueFrom(this.http.get<Score[]>("https://localhost:7065/api/Scores/GetPublicScores"));
    console.log(x);
    this.publicScores = x;
  }

  async changeScoreVisibility(score: Score): Promise<void> {
    await this.http.put("https://localhost:7065/api/Scores/ChangeScoreVisibility/${score.id}", null);
    score.isPublic = !score.isPublic; // Mettre à jour localement la visibilité du score
  }

}
