import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from './../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

import { BookListComponent } from './book-list/book-list.component';
import { BookCreateComponent } from './book-create/book-create.component';
import { BookUpdateComponent } from './book-update/book-update.component';
import { BookDeleteComponent } from './book-delete/book-delete.component';


@NgModule({
  declarations: [BookListComponent, BookCreateComponent, BookUpdateComponent, BookDeleteComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'list', component: BookListComponent },
      { path: 'details/:id', component: BookListComponent },
      { path: 'create', component: BookCreateComponent },
      { path: 'update/:id', component: BookUpdateComponent },
      { path: 'delete/:id', component: BookDeleteComponent }
    ])
  ]

})
export class BookModule { }
