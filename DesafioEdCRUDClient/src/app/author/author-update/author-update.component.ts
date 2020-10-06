import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorForCreation } from './../../_interfaces/author-for-creation.model';
import { ErrorHandlerService } from '../../_services/error-handler.service';
import { RepositoryService } from '../../_services/repository.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Author } from 'src/app/_interfaces/author.model';

@Component({
  selector: 'app-author-update',
  templateUrl: './author-update.component.html',
  styleUrls: ['./author-update.component.css']
})
export class AuthorUpdateComponent implements OnInit {
  public errorMessage: string = 'Erro ao atualizar o Autor.';

  public authorForm: FormGroup;
  public author: Author;

 constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router,
  private activeRoute: ActivatedRoute) { }
  ngOnInit() {
    this.authorForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(40)])
    });
    this.getAuthorById();
  }

  private getAuthorById = () => {
    let authorId: string = this.activeRoute.snapshot.params['id'];

    let authorByIdUrl: string = `/authors/${authorId}`;
    this.repository.getData(authorByIdUrl)
      .subscribe(res => {
        this.author = res as Author;
        this.authorForm.patchValue(this.author);
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
  }

  public validateControl = (controlName: string) => {
    if (this.authorForm.controls[controlName].invalid && this.authorForm.controls[controlName].touched)
      return true;
    return false;
  }
  public hasError = (controlName: string, errorName: string) => {
    if (this.authorForm.controls[controlName].hasError(errorName))
      return true;
    return false;
  }
  public updateAuthor = (authorFormValue) => {
    if (this.authorForm.valid) {
      this.executeAuthorUpdate(authorFormValue);
    }
  }
  private executeAuthorUpdate = (authorFormValue) => {
    const author: AuthorForCreation = {
      name: authorFormValue.name
    }
    let apiUrl = `/authors/${this.author.authorId}`;
    this.repository.update(apiUrl, author)
      .subscribe(res => {
        $('#successModal').modal();
      },
      (error => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
    )
  }
  public redirectToAuthorList(){
    this.router.navigate(['/author/list']);
  }
}
