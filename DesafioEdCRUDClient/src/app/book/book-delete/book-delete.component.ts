import { Component, OnInit } from '@angular/core';
import { ErrorHandlerService } from '../../_services/error-handler.service';
import { RepositoryService } from '../../_services/repository.service';
import { Book } from './../../_interfaces/book.model';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-delete',
  templateUrl: './book-delete.component.html',
  styleUrls: ['./book-delete.component.css'],
})
export class BookDeleteComponent implements OnInit {
  public errorMessage: string = '';
  public book: Book;

  constructor(
    private repository: RepositoryService,
    private errorHandler: ErrorHandlerService,
    private router: Router,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.getBookById();
  }

  private getBookById = () => {
    const bookId: string = this.activeRoute.snapshot.params['id'];
    const bookByIdUrl: string = `/books/${bookId}`;

    this.repository.getData(bookByIdUrl).subscribe(
      (res) => {
        this.book = res as Book;
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };

  public redirectToBookList = () => {
    this.router.navigate(['/book/list']);
  };

  public deleteBook = () => {
    const deleteUrl: string = `/books/${this.book.id}`;
    this.repository.delete(deleteUrl).subscribe(
      (res) => {
        $('#successModal').modal();
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };
}
