import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDTO } from '../models/loginDTO';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { RegisterDTO } from '../models/registerDTO';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  hide = true;

  registerUsername: string = "";
  registerEmail: string = "";
  registerPassword: string = "";
  registerPasswordConfirm: string = "";

  loginUsername: string = "";
  loginPassword: string = "";

  constructor(public route: Router, public http: HttpClient) { }

  ngOnInit() {
  }

  async login(): Promise<void> {
    let loginDTO = new LoginDTO(this.loginUsername, this.loginPassword);

    try {
      let x = await lastValueFrom(this.http.post<any>("https://localhost:7065/api/Users/Login", loginDTO));
      console.log(x);
      localStorage.setItem("token", x.token);
      // Redirection si la connexion a réussi :
      this.route.navigate(["/play"]);
    } catch (e) {
      console.error("An error occurred during login:", e);
    }
  }

  async register(): Promise<void> {
    let registerDTO = new RegisterDTO(
      this.registerUsername,
      this.registerEmail,
      this.registerPassword,
      this.registerPasswordConfirm);

    let x = await lastValueFrom(this.http.post<RegisterDTO>("https://localhost:7065/api/Users/Register", registerDTO));
    console.log(x);
  }
}
