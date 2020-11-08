import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { INode } from '../shared/node';

@Injectable()
export class ForcedMatrixService {
    
    private _server = 'https://localhost:44358/'
    private _getTreeSchemaUrl = 'api/forcedmatrix/treeschema';
    private _insertNodeUrl = 'api/forcedmatrix/insert'
    errorMessage: string;

    constructor(private _http: HttpClient) {}

    getTreeSchema(childLimit: number, levelLimit: number): Observable<any> {
    
        return this._http.get(this._server + this._getTreeSchemaUrl, {
            params: {
              childLimit: childLimit.toString(),
              levelLimit: levelLimit.toString()
            },
            observe: 'response'
          });
                        
    }
}
