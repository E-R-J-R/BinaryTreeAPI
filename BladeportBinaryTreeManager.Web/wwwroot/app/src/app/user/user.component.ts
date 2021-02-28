import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from './user.service';
import { IUser } from './user';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-users',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit {

  @ViewChild('form') form: NgForm;
  stringTitle = 'User List';

  constructor(public _userService: UserService, private toastr: ToastrService) { }
    
  ngOnInit() {
    this.listUsers();
  }

  listUsers() {
    this._userService.getUsers();
  }

  populateForm(selectedRecord: IUser) {
    this._userService.formData = Object.assign({}, selectedRecord);
  }

  onSubmit(form: NgForm) {    
    if (this._userService.formData.userId == 0) {
      this.addUser(form);
    } else {
      this.editUser(form);
    }
  }

  addUser(form: NgForm) {
    this._userService.addUser()
      .subscribe(
        res => {
          this.resetForm(form);
          this.listUsers();
          this.toastr.success('User added successfully', this.stringTitle);
        },
        err => { 
          this.toastr.error(err.error, this.stringTitle);
        } 
    )
  }

  editUser(form: NgForm) {
    const id = this._userService.formData.userId;
    this._userService.editUser(id)
      .subscribe(
        res => {
          this.resetForm(form);
          this.listUsers();
          this.toastr.success('User ID '+id+' updated successfully', this.stringTitle);
        },
        err => { 
          this.toastr.error(err.error, this.stringTitle);
        }
      )
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this._userService.formData = {
      userId: 0,
      userName: '',
      firstName: '',
      lastName: '',
      joinDate: new Date()
    } as IUser;
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record for User ID '+id+' ?')) {
      this._userService.deleteuser(id)
        .subscribe(
          res => {
            this.listUsers();
            this.toastr.error('User ID ' + id + ' deleted successfully', this.stringTitle);            
          },
          err => { 
            this.toastr.error(err.error, this.stringTitle);
          }
        )

    }
  } 

  onDeleteUser(selectedRecord: IUser) {
    this.populateForm(selectedRecord);
    const id = selectedRecord.userId;    
    if (confirm('Are you sure to delete this record for User ID ' + id + ' ?')) {
      this._userService.deleteuser(id)
        .subscribe(
          res => {
            this.resetForm(this.form);
            this.listUsers();
            this.toastr.info('User ID ' + id + ' deleted successfully', this.stringTitle);
          },
          err => { 
            this.toastr.error(err.error, this.stringTitle);
          }
        )
    }
  }
}
