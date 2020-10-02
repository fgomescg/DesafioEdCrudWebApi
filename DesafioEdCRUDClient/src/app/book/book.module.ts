import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from './../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCurrencyFormatModule } from 'mat-currency-format';
import { NgxPaginationModule } from 'ngx-pagination';

import { BookListComponent } from './book-list/book-list.component';
import { BookCreateComponent } from './book-create/book-create.component';
import { BookUpdateComponent } from './book-update/book-update.component';
import { BookDeleteComponent } from './book-delete/book-delete.component';
import { BookDetailsComponent } from './book-details/book-details.component';

@NgModule({
  declarations: [BookListComponent, BookCreateComponent, BookUpdateComponent, BookDeleteComponent, BookDetailsComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    MatCurrencyFormatModule,
    NgxPaginationModule,
    RouterModule.forChild([
      { path: 'list', component: BookListComponent },
      { path: 'details/:id', component: BookDetailsComponent },
      { path: 'create', component: BookCreateComponent },
      { path: 'update/:id', component: BookUpdateComponent },
      { path: 'delete/:id', component: BookDeleteComponent }
    ])
  ]

})
export class BookModule { }
