import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { INode } from '../shared/node';

@Injectable()
export class BinaryTreeService {
    
    private _server = 'https://localhost:44358/'
    private _getTreeSchemaUrl = 'api/binarytree/treeschema';
    private _insertNodeUrl = 'api/binarytree/insert'
    errorMessage: string;

    constructor(private _http: HttpClient) {}

    getTreeSchema(): Observable<any> {

        let headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
    
        return this._http.get(this._server + this._getTreeSchemaUrl);
                        
    }

    insertNode(node: INode): Observable<any> {

        const headers = { 'Content-Type': 'application/json' };
        const body = { NodeId: node.NodeId, ParentId: node.ParentId, SponsorId: node.SponsorId };
            
        return this._http.post(this._server + this._insertNodeUrl, body, {headers});
    }

}
