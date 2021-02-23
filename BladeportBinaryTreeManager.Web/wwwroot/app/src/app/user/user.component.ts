import { Component, OnInit } from '@angular/core';
import { UserService } from '../user/user.service';
import { UserObj } from '../user/user.model';

@Component({
  selector: 'app-users',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit {

  userList: UserObj[];
  
  input: any;

  todaysDate: Date;

  enableEdit = false;

  isEditing: boolean = false;

  enableEditIndex = null;

  constructor(private _userService: UserService) { 
    this.userList = [];

    this.input = {
      'userName':'',
      'firstName': '',
      'lastName': ''
    };

    this.todaysDate = new Date();
  }

  ngOnInit() {
    this._userService.getUsers().subscribe((userList: UserObj[]) => {
      this.userList = userList;
    });    
  }

  adduser() {
    this._userService.addUser(this.input)
      .subscribe(() => {
        this.ngOnInit(); 
      }),
      (err: any) => { 
        console.log("Error"); 
      };
  }

  deleteuser(user:UserObj) {
    this._userService.deleteuser(user)
      .subscribe(() => {
        this.ngOnInit();
      }),
      (err: any) => {
        console.log("Error");
      };
  }

  edituser(user:UserObj) {
    this.isEditing = false;
    this.enableEditIndex = null;

    this._userService.editUser(user)
      .subscribe(() => {
        this.ngOnInit();
      }),
      (err: any) => {
        console.log("Error");
      };
  }

  enableEditMode(e, i) {
    this.enableEdit = true;
    this.enableEditIndex = i;
    console.log(i);
    console.log(e);
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
