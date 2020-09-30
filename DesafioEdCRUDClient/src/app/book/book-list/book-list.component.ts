import { Component, OnInit } from '@angular/core';
import { RepositoryService } from './../../shared/services/repository.service';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { Router } from '@angular/router';
import { BookList } from 'src/app/_interfaces/book-list';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
})
export class BookListComponent implements OnInit {
  public bookList: BookList;
  public errorMessage: string = '';
  public totalCount : Number;
  public totalPages: Number;
  public currentPage : Number = 1;
  public pageSize : Number = 10;

  constructor(
    private repository: RepositoryService,
    private errorHandler: ErrorHandlerService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getBooks();
  }
  public getBooks = () => {
    let apiAddress: string = 'api/book';

    let params = new HttpParams().set("pageNumber",this.currentPage.toString()).set("pageSize", this.pageSize.toString());

    this.repository.getData(apiAddress, params).subscribe(
      (res) => {
        this.bookList = res as BookList;
        this.totalCount = this.bookList.totalCount;
        this.pageSize = this.bookList.currentPage;
        this.totalPages = this.bookList.totalPages;
        this.pageSize = this.bookList.pageSize;
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };

  handlePageChange(event){
    this.currentPage = event;
    this.getBooks();
  }

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
