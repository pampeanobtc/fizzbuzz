import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FizzBuzzComponent } from './components/fizzbuzz/fizzbuzz.component';
import { MessagesComponent } from './components/messages/messages.component';
import { ButtonComponent } from './components/button/button/button.component';
import { HeaderComponent } from './components/header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    MessagesComponent,
    FizzBuzzComponent,
    ButtonComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [ HttpClientModule, HttpClient ],
  bootstrap: [AppComponent]
})
export class AppModule { }
