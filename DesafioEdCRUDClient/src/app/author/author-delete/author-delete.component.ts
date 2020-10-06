import { Component, OnInit } from '@angular/core';
import { ErrorHandlerService } from '../../_services/error-handler.service';
import { RepositoryService } from '../../_services/repository.service';
import { Author } from './../../_interfaces/author.model';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-author-delete',
  templateUrl: './author-delete.component.html',
  styleUrls: ['./author-delete.component.css'],
})
export class AuthorDeleteComponent implements OnInit {
  public errorMessage: string = 'Erro ao deletar Autor.';
  public author: Author;

  constructor(
    private repository: RepositoryService,
    private errorHandler: ErrorHandlerService,
    private router: Router,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.getAuthorById();
  }

  private getAuthorById = () => {
    const authorId: string = this.activeRoute.snapshot.params['id'];
    const authorByIdUrl: string = `/authors/${authorId}`;

    this.repository.getData(authorByIdUrl).subscribe(
      (res) => {
        this.author = res as Author;
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };

  public redirectToAuthorList = () => {
    this.router.navigate(['/author/list']);
  };

  public deleteAuthor = () => {
    const deleteUrl: string = `/authors/${this.author.authorId}`;
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
