import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FizzBuzzComponent } from './components/fizzbuzz/fizzbuzz.component';
import { ButtonComponent } from './components/button/button/button.component';
import { HeaderComponent } from './components/header/header.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatCardModule} from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    AppComponent,
    FizzBuzzComponent,
    ButtonComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatSnackBarModule
  ],
  providers: [ HttpClientModule, HttpClient, MatSnackBarModule ],
  bootstrap: [AppComponent]
})
export class AppModule { }
