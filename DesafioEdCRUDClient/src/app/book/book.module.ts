import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from './../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCurrencyFormatModule } from 'mat-currency-format';
import { Routes, RouterModule } from '@angular/router';

import { BookListComponent } from './book-list/book-list.component';
import { BookCreateComponent } from './book-create/book-create.component';
import { BookUpdateComponent } from './book-update/book-update.component';
import { BookDeleteComponent } from './book-delete/book-delete.component';

import { HomeComponent } from '../home/home.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent},
  { path: 'book', loadChildren: () => import('../book/book.module').then(m => m.BookModule) },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  declarations: [BookListComponent, BookCreateComponent, BookUpdateComponent, BookDeleteComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    MatCurrencyFormatModule,
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
