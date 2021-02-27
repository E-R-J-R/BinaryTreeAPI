import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { OrgchartModule } from '@dabeng/ng-orgchart';

import { BinaryTreeComponent } from './binarytree/binarytree.component';
import { BinaryTreeService } from './binarytree/binarytree.service';
import { UserComponent } from './user/user.component';
import { UserService } from './user/user.service';
import { ForcedmatrixComponent } from './forcedmatrix/forcedmatrix.component';
import { ForcedMatrixService } from './forcedmatrix/forcedmatrix.service';

@NgModule({
  declarations: [
    AppComponent,
    BinaryTreeComponent,
    ForcedmatrixComponent,
    UserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    OrgchartModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [BinaryTreeService, UserService, ForcedMatrixService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
