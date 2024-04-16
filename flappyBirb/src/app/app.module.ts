import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PlayComponent } from './play/play.component';
import { ScoreComponent } from './score/score.component';
import { Round_00Pipe } from './pipes/round_00.pipe';

@NgModule({
  declarations: [				
    AppComponent,
      LoginComponent,
      PlayComponent,
      ScoreComponent,
      Round_00Pipe
   ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MaterialModule,
    HttpClientModule,
    RouterModule.forRoot([
      {path:"", redirectTo:"/play", pathMatch:"full"},
      {path:"login", component:LoginComponent},
      {path:"play", component:PlayComponent},
      {path:"scores", component:ScoreComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
