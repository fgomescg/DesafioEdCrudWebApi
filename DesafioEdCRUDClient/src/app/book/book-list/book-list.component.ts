import { Component, OnInit } from '@angular/core';
import { RepositoryService } from './../../shared/services/repository.service';
import { Book } from './../../_interfaces/book.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
})
export class BookListComponent implements OnInit {
  public books: Book[];
  public errorMessage: string = '';

  public TotalCount : Number = 100;
  public PageSize : Number = 10;
  public CurrentPage : Number = 1;
  public TotalPages: Number = 10;
  public HasNext : boolean = true;
  public HasPrevious : boolean = true;

  constructor(
    private repository: RepositoryService,
    private errorHandler: ErrorHandlerService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getAllBooks();
  }
  public getAllBooks = () => {
    let apiAddress: string = 'api/book';
    this.repository.getData(apiAddress).subscribe(
      (res) => {
        this.books = res as Book[];
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };
  public getBookDetails = (id) => {
    const detailsUrl: string = `/book/details/${id}`;
    this.router.navigate([detailsUrl]);
  }

  public redirectToUpdatePage = (id) => {
    const updateUrl: string = `/book/update/${id}`;
    this.router.navigate([updateUrl]);
  };

  public redirectToDeletePage = (id) => {
    const deleteUrl: string = `/book/delete/${id}`;
    this.router.navigate([deleteUrl]);
  };
}
