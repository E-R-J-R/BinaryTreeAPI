import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { OrgchartModule } from '@dabeng/ng-orgchart';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
/*
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
*/
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
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
    // ,
    // MatFormFieldModule,
    // MatToolbarModule,
    // MatButtonModule,
    // MatDialogModule,
    // MatInputModule,
    // MatTableModule,
    // MatPaginatorModule 
  ],
  providers: [BinaryTreeService, UserService, ForcedMatrixService ],
  bootstrap: [AppComponent]
})
export class AppModule { }

// export class MaterialModule { }
