import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { UserObj, UserObjAdapter } from './user.model';
import { isNumeric } from 'jquery';

@Injectable()
export class UserService {
    
    private _server = 'https://localhost:44358/'
    //private _server = 'http://localhost:5000/'
    private _getUsersUrl = 'api/user/users';
    private _addUsersUrl = 'api/user/adduser';
    private _deleteUsersUrl = 'api/user/deleteuser';
    private _editUsersUrl = 'api/user/edituser';

    constructor(private _http: HttpClient, private _adapter: UserObjAdapter) {}

    getUserList(): Observable<any> {
        return this._http.get(this._server + this._getUsersUrl);
    }
    
    getUsers(): Observable<UserObj[]> {
        const url = this._server + this._getUsersUrl;

        // Adapt each item in the raw data array
        return this._http.get(url).pipe(
            map((data: any[]) => data.map((item: any) => this._adapter.adapt(item)))
        );        
    }

    deleteuser(userInput): Observable<any> {
        const url = this._server + this._deleteUsersUrl;

        let headers = new HttpHeaders({ 'content-type': 'application/json' });
        return this._http.post(url, JSON.stringify(userInput), { headers: headers });
    }

    addUser(userInput): Observable<any> {
        const url = this._server + this._addUsersUrl;
        
        if (userInput.userName && userInput.firstName && userInput.lastName) {
            let headers = new HttpHeaders ({ 'content-type': 'application/json' });
            return this._http.post(url, JSON.stringify(userInput), {headers: headers});
        }

    }

    editUser(userInput): Observable<any> {
        const url = this._server + this._editUsersUrl;

        let headers = new HttpHeaders({ 'content-type': 'application/json' });
        return this._http.post(url, JSON.stringify(userInput), { headers: headers });

    }

}
