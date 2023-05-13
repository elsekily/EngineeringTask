import { Component } from '@angular/core';
import { resetUser } from 'src/app/Models/resetUser';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  user: resetUser = {
    userName: '',
    oldPassword: '',
    newPassword:''
  }
  hasPasswordInteracted = false;

   constructor(private userService: UserService) {
  }
  onSubmit() {
    this.userService.resetPassword(this.user).subscribe(
      (response) => {
        alert('user Password Successfully Updated');

    },
      (error) => {
        alert('Error: ' + error.error);
      }
    );
  
    this.user = {
      userName: '',
      oldPassword: '',
      newPassword:'', 
    };
    this.hasPasswordInteracted = false;
  }
}
