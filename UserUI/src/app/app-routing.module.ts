import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegesiterUserComponent } from './Components/regesiter-user/regesiter-user.component';
import { GetUserDataComponent } from './Components/get-user-data/get-user-data.component';
import { ResetPasswordComponent } from './Components/reset-password/reset-password.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch:'full'},
  { path: 'home', component: HomeComponent},
  { path: 'register', component: RegesiterUserComponent },
  { path: 'getuser', component: GetUserDataComponent },
  { path: 'resetpassword', component: ResetPasswordComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule] 
})
export class AppRoutingModule { }
