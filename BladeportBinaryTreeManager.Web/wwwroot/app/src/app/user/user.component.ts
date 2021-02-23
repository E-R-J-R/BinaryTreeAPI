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

  constructor(private _userService: UserService) { 
    this.userList = [];
  }

  ngOnInit() {
    this._userService.getUsers().subscribe((userList: UserObj[]) => {
      this.userList = userList;
    });
    
  }
}
