import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { saveUser } from '../Models/saveUser';
import { loginUser } from '../Models/loginUser';
import { userResource } from '../Models/userResource';
import { resetUser } from '../Models/resetUser';
import { environment } from '../../environments/environment.prod';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  baseUrl = environment.apiUrl;

  create(user:saveUser) {
    return this.http.post<userResource>(this.baseUrl+'user/new', user);
  }

  resetPassword(user:resetUser) {
    return this.http.put<userResource>(this.baseUrl + 'user/passwordreset', user);
  }
  getUserData(user:loginUser) {
  return this.http.post<userResource>(this.baseUrl+'user/', user);

  }
}
