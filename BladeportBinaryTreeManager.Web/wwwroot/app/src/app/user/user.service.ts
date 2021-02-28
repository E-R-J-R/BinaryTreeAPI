import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUser } from './user';

@Injectable()
export class UserService {   

    // private _server = 'https://localhost:44358/'
    private _server = 'http://localhost:5000/'
    private _getUsersUrl = 'api/user/users';
    private _addUsersUrl = 'api/user';
    private _deleteUsersUrl = 'api/user/';
    private _editUsersUrl = 'api/user/';

    formData: IUser = {
        userId : 0,
        userName : '',
        firstName : '',
        lastName : '',
        joinDate : new Date()
    } as IUser;
    users: IUser[];      

    constructor(private _http: HttpClient ) { }

    getUserList(): Observable<Object> {
        return this._http.get(this._server + this._getUsersUrl);
    }
    
    getUsers() {
        const url = this._server + this._getUsersUrl;
        return this._http.get(url)
            .toPromise()
            .then(res => {                
                this.users = res as IUser[]
            });
    }

    addUser() {
        const url = this._server + this._addUsersUrl;        
        let headers = new HttpHeaders ({ 'content-type': 'application/json' });
        return this._http.post(url, this.formData, { headers: headers });        
    }

    editUser(id: number) {
        const url = this._server + this._editUsersUrl + id;
        let headers = new HttpHeaders({ 'content-type': 'application/json' });
        return this._http.put(url, this.formData, { headers: headers });
    }

    deleteuser(id: number) {
        const url = this._server + this._deleteUsersUrl + id;                
        let _httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            }),
            body: this.formData
        }
        return this._http.delete(url, _httpOptions);
    }
}
