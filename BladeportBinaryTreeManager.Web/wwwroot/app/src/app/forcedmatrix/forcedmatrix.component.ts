import { Component, OnInit } from '@angular/core';
import { ForcedMatrixService } from './forcedmatrix.service'
import OrgChart from "../../assets/js/orgchart";

@Component({
  selector: 'app-forcedmatrix',
  templateUrl: './forcedmatrix.component.html',
  styleUrls: ['./forcedmatrix.component.css']
})
export class ForcedmatrixComponent implements OnInit {

  constructor(private _forcedMatrixService: ForcedMatrixService) { }

  ngOnInit(): void {
      this._forcedMatrixService.getTreeSchema(3,3).subscribe((data: any) => {
        var orgchart = new OrgChart({
          'chartContainer': '#chart-container2',
          'data' : data.body.schema,
          'depth': data.body.maxDepth, 
          'nodeContent': 'title',
          'toggleSiblingsResp': false,
          'parentNodeSymbol': 'fa-users',
          'draggable': false,
          'direction': 't2b',
          'pan': true,
          'zoom': true,
          'createNode': function(node, data) {
            let secondMenuIcon = document.createElement('i'),
              secondMenu = document.createElement('div');
    
            secondMenuIcon.setAttribute('class', 'fa fa-info-circle second-menu-icon');
            // secondMenuIcon.addEventListener('click', (event) => {
            //   event.target.nextElementSibling.classList.toggle('hidden');
            // });
            secondMenu.setAttribute('class', 'second-menu');
            secondMenu.innerHTML = `<img class="avatar" src="../assets/images/users/${data.id}.png">`;
            node.appendChild(secondMenuIcon)
            node.appendChild(secondMenu);
          }
        });
        console.log(data);
      });
  }

}
