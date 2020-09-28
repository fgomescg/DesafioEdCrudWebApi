import { Component, OnInit } from '@angular/core';
import { RepositoryService } from './../../shared/services/repository.service';
import { Author } from './../../_interfaces/author.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css'],
})
export class AuthorListComponent implements OnInit {
  public authors: Author[];
  public errorMessage: string = '';

  constructor(
    private repository: RepositoryService,
    private errorHandler: ErrorHandlerService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getAllAuthors();
  }
  public getAllAuthors = () => {
    let apiAddress: string = 'api/author';
    this.repository.getData(apiAddress).subscribe(
      (res) => {
        this.authors = res as Author[];
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };
  public redirectToUpdatePage = (id) => {
    const updateUrl: string = `/author/update/${id}`;
    this.router.navigate([updateUrl]);
  };

  public redirectToDeletePage = (id) => {
    const deleteUrl: string = `/author/delete/${id}`;
    this.router.navigate([deleteUrl]);
  };
}
