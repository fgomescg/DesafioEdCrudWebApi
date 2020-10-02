import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from './../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

import { AuthorListComponent } from './author-list/author-list.component';
import { AuthorCreateComponent } from './author-create/author-create.component';
import { AuthorUpdateComponent } from './author-update/author-update.component';
import { AuthorDeleteComponent } from './author-delete/author-delete.component';


@NgModule({
  declarations: [AuthorListComponent, AuthorCreateComponent, AuthorUpdateComponent, AuthorDeleteComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    RouterModule.forChild([
      { path: 'list', component: AuthorListComponent },
      { path: 'details/:id', component: AuthorListComponent },
      { path: 'create', component: AuthorCreateComponent },
      { path: 'update/:id', component: AuthorUpdateComponent },
      { path: 'delete/:id', component: AuthorDeleteComponent }
    ])
  ]

})
export class AuthorModule { }
