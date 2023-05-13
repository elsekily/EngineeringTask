import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { RegesiterUserComponent } from './Components/regesiter-user/regesiter-user.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ResetPasswordComponent } from './Components/reset-password/reset-password.component';
import { HomeComponent } from './Components/home/home.component';
import { GetUserDataComponent } from './Components/get-user-data/get-user-data.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegesiterUserComponent,
    ResetPasswordComponent,
    GetUserDataComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    CommonModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
