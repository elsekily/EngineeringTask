import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { userResource } from 'src/app/Models/userResource';
import { UserService } from '../../Services/user.service';
import { loginUser } from 'src/app/Models/loginUser';

@Component({
  selector: 'app-get-user-data',
  templateUrl: './get-user-data.component.html',
  styleUrls: ['./get-user-data.component.css']
})
export class GetUserDataComponent {
  userResource: userResource | undefined;
  user: loginUser = {
    userName:'',
    password: ''
  }
   constructor(private userService: UserService) {
   }
  onSubmit() { 
    this.userService.getUserData(this.user).subscribe(
      (response) => {
        this.userResource = response;
    },
      (error) => {
        alert('Error: ' + error.error);
      }
    );
    this.userResource = undefined;
  }
}
