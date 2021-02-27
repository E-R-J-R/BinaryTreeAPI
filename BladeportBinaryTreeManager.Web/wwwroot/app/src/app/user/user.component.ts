import { Component, OnInit } from '@angular/core';
import { UserService } from '../user/user.service';
import { IUser } from '../user/user';

@Component({
  selector: 'app-users',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit {

  users: IUser[];
  errorMessage: string;
  user: IUser = {} as IUser;
  isEditing = false;
  enableEditIndex = null;

  constructor(private _userService: UserService) { }

  ngOnInit() {
    this.listUsers();
  }

  listUsers() {
    this._userService.getUsers().subscribe((users: IUser[]) => {
      this.users = users;
    });
  }

  getUser() {
    // console.log('getUser By Search');
  }

  adduser() {    
    if (Object.keys(this.user).length > 0) {
      this._userService.addUser(this.user)
      .subscribe( 
        (data: boolean) => { // expected response is true or false
          /* TODO : need to have code here to handle and display a message if User was added or not */
          data ? console.log('Added User - ' + this.user.userName) : console.log('Not Added - User - ' + this.user.userName);
          this.listUsers();          
        }
      );
    } else {

      console.log('Empty Add User Form');
      /* TODO: need to have code here to handle empty form */
      
    }
  }

  edituser(user: IUser) {
    this.isEditing = false;
    this.enableEditIndex = null;

    this._userService.editUser(user)
      .subscribe(
        (data: boolean) => { // expected response is true or false
          /* TODO : need to have code here to handle and display a message if User was updated or not */
          data ? console.log('Updated User - ' + user.userName) : console.log('Not Updated - User - ' + user.userName);
          this.listUsers();
        }
      );
  }

  deleteuser(user: IUser) {
    this.user = user;
    this._userService.deleteuser(user)
      .subscribe(
        (data: boolean) => { // expected response is true or false
          /* TODO : need to have code here to handle and display a message if User was removed or not */
          data ? console.log('Removed User - ' + user.userName) : console.log('Not Removed - User - ' + user.userName);
          this.listUsers();
        }
      );
  }

  switchEditMode(i) {
    this.isEditing = true;
    this.enableEditIndex = i;
  }

  cancel() {
    this.isEditing = false;
    this.enableEditIndex = null;
  }
}
