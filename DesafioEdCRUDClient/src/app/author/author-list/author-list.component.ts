import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../_services/repository.service';
import { AuthorList } from './../../_interfaces/author-list';
import { ErrorHandlerService } from '../../_services/error-handler.service';
import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css'],
})
export class AuthorListComponent implements OnInit {
  public authors;
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
    this.getAllAuthors();
  }
  public getAllAuthors = () => {
    let params = new HttpParams().set("pageNumber",this.currentPage.toString()).set("pageSize", this.pageSize.toString());
    this.repository.getData('api/author', params).subscribe(
      (res) => {
        const { authors, totalCount, currentPage, totalPages, pageSize  } = res as AuthorList;
        this.authors = authors;
        this.totalCount = totalCount;
        this.pageSize = currentPage;
        this.totalPages = totalPages;
        this.pageSize = pageSize;
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };

  handlePageChange(event){
    this.currentPage = event;
    this.getAllAuthors();
  }

  public redirectToUpdatePage = (id) => {
    const updateUrl: string = `/author/update/${id}`;
    this.router.navigate([updateUrl]);
  };

  public redirectToDeletePage = (id) => {
    const deleteUrl: string = `/author/delete/${id}`;
    this.router.navigate([deleteUrl]);
  };
}
