import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { User, UserViewModel } from './user.model';
import { isNumeric } from 'jquery';

@Injectable()
export class UserService {
    
    // private _server = 'https://localhost:44358/'
    private _server = 'http://localhost:5000/'
    private _getUsersUrl = 'api/user/users';
    private _addUsersUrl = 'api/user/adduser';
    private _deleteUsersUrl = 'api/user/deleteuser';
    private _editUsersUrl = 'api/user/edituser';

    constructor(private _http: HttpClient, private _adapter: UserViewModel) {}

    getUserList(): Observable<any> {
        return this._http.get(this._server + this._getUsersUrl);
    }
    
    getUsers(): Observable<User[]> {
        const url = this._server + this._getUsersUrl;

        // Adapt each item in the raw data array
        return this._http.get(url).pipe(
            map((data: User[]) => data.map((item: User) => this._adapter.adapt(item)))
        );        
    }

    deleteuser(userInput: User): Observable<Object> {
        const url = this._server + this._deleteUsersUrl;

        let headers = new HttpHeaders({ 'content-type': 'application/json' });
        return this._http.post(url, JSON.stringify(userInput), { headers: headers });
    }
 
    addUser(userInput: User): Observable<Object> {
        const url = this._server + this._addUsersUrl;
        console.log ({ userInput });
        
        if (userInput.userName && userInput.firstName && userInput.lastName) {
            let headers = new HttpHeaders ({ 'content-type': 'application/json' });
            return this._http.post(url, JSON.stringify(userInput), {headers: headers});
        }
    }

    editUser(userInput: User): Observable<Object> {
        const url = this._server + this._editUsersUrl;

        let headers = new HttpHeaders({ 'content-type': 'application/json' });
        return this._http.post(url, JSON.stringify(userInput), { headers: headers });

    }

}
