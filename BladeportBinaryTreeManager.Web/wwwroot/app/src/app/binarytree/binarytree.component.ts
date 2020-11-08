import { Component, OnInit } from '@angular/core';
import { BinaryTreeService } from './binarytree.service'
import { UserService } from '../user/user.service';
import { IUser } from '../user/user';
import OrgChart from "../../assets/js/orgchart";
//import 'jquery'; --enable when jquery is needed

@Component({
  selector: 'app-binarytree',
  templateUrl: './binarytree.component.html',
  styleUrls: ['./binarytree.component.css']
})
export class BinaryTreeComponent implements OnInit {

  nodeId: number = 0;
  sponsorId: number = 0;
  userList: IUser[] = [];

  constructor(private _binaryTreeService: BinaryTreeService, private _userService: UserService) { }

  ngOnInit(): void {

    this._binaryTreeService.getTreeSchema().subscribe((data: any) => { 
        var orgchart = new OrgChart({
          'chartContainer': '#chart-container',
          'data' : data.schema,
          'depth': data.maxDepth,
          'nodeContent': 'title',
          'toggleSiblingsResp': false,
          'parentNodeSymbol': 'fa-users',
          'draggable': false,
          'direction': 't2b',
          'pan': false,
          'zoom': false,
          'createNode': function(node, data) {
            let secondMenuIcon = document.createElement('i'),
              secondMenu = document.createElement('div');
    
            secondMenuIcon.setAttribute('class', 'fa fa-info-circle second-menu-icon');
            // secondMenuIcon.addEventListener('click', (event) => {
            //   event.target.nextElementSibling.classList.toggle('hidden');
            // });
            secondMenu.setAttribute('class', 'second-menu');
            secondMenu.innerHTML = `<img class="avatar" src="../assets/images/users/${data.id}.png">`;;
            node.appendChild(secondMenuIcon)
            node.appendChild(secondMenu);
          }
        });
        console.log(data);
      });

    //Load the user list into dropdown
    // this._userService.getUserList().subscribe((data: IUser[]) => {
    //     this.userList = data;
    //     console.log(data);
    // });
  }

  insertNode(): void {

  }

  setNodeId(event: any) {
    this.nodeId = event.target.value;
  }

  setSponsorId(event: any) {
    this.sponsorId = event.target.value;
  }

}
