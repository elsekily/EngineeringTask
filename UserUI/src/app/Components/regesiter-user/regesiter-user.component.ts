import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { saveUser } from 'src/app/Models/saveUser';
import { UserService } from '../../Services/user.service';

@Component({
  selector: 'app-regesiter-user',
  templateUrl: './regesiter-user.component.html',
  styleUrls: ['./regesiter-user.component.css']
})
export class RegesiterUserComponent {
  user: saveUser = {
    firstName: '',
    fatherName: '',
    familyName: '',
    birthDate: new Date(),
    address: '',
    userName: '',
    password: ''
  };
  hasPasswordInteracted = false;

  
  constructor(private userService: UserService) {
  }
  onSubmit() {
    this.userService.create(this.user).subscribe(
      (response) => {
        alert('user added Successfully');

    },
      (error) => {
        alert('Error: ' + error.error);
      }
    );
  
    this.user = {
          firstName: '',
          fatherName: '',
          familyName: '',
          birthDate: new Date(),
          address: '',
          userName: '',
          password: ''
    };
    this.hasPasswordInteracted = false;
  }
}
