import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class UserService {
    
    private _server = 'https://localhost:44358/'
    private _getUsersUrl = 'api/user/users';

    constructor(private _http: HttpClient) {}

    getUserList(): Observable<any> {
        return this._http.get(this._server + this._getUsersUrl);            
    }

}