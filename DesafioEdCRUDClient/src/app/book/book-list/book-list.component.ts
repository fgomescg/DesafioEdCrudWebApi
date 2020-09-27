import { Component, OnInit } from '@angular/core';
import { RepositoryService } from './../../shared/services/repository.service';
import { Book } from './../../_interfaces/book.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  public books: Book[];
  public errorMessage: string = '';

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService) { }
  ngOnInit(): void {
    this.getAllBooks();
  }
  public getAllBooks = () => {
    let apiAddress: string = "api/book";
    this.repository.getData(apiAddress)
    .subscribe(res => {
      this.books = res as Book[];
    },
    (error) => {
      this.errorHandler.handleError(error);
      this.errorMessage = this.errorHandler.errorMessage;
    })
  }
}
