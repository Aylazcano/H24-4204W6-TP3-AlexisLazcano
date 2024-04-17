import { Component, OnDestroy, OnInit } from '@angular/core';
import { Game } from './gameLogic/game';
import { Score } from '../models/score';
import { Birb } from './gameLogic/birb';
import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.css']
})
export class PlayComponent implements OnInit, OnDestroy {

  game: Game | null = null;
  scoreSent: boolean = false;

  constructor(public http: HttpClient) { }

  ngOnDestroy(): void {
    // Ceci est crotté mais ne le retirez pas sinon le jeu bug.
    location.reload();
  }

  ngOnInit() {
    this.game = new Game();
  }

  replay() {
    if (this.game == null) return;
    this.game.prepareGame();
    this.scoreSent = false;
  }

  async sendScore(): Promise<void> {
    if (this.scoreSent) return;

    this.scoreSent = true;

    // ██ Appeler une requête pour envoyer le score du joueur ██
    // Le score est dans sessionStorage.getItem("score")
    // Le temps est dans sessionStorage.getItem("time")
    // La date sera choisie par le serveur

    const scoreValue = sessionStorage.getItem("score");
    const timeInSeconds = sessionStorage.getItem("time");
    if (!scoreValue || !timeInSeconds) {
      console.error("Score or time not found in session storage.");
      return;
    }

    let newScore = new Score(
      0,
      null,
      null,
      timeInSeconds,
      parseInt(scoreValue),
      true
    );

    let x = await lastValueFrom(this.http.post<Score>("https://localhost:7065/api/Scores/PostScore", newScore));
    console.log(x);
  }

}
