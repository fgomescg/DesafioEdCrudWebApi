import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorForCreation } from './../../_interfaces/author-for-creation.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-author-create',
  templateUrl: './author-create.component.html',
  styleUrls: ['./author-create.component.css']
})
export class AuthorCreateComponent implements OnInit {
  public errorMessage: string = 'Erro ao cadastrar o Autor.';

  public authorForm: FormGroup;

 constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }
  ngOnInit() {
    this.authorForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(40)])
    });
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
  public createAuthor = (authorFormValue) => {
    if (this.authorForm.valid) {
      this.executeAuthorCreation(authorFormValue);
    }
  }
  private executeAuthorCreation = (authorFormValue) => {
    const author: AuthorForCreation = {
      name: authorFormValue.name
    }
    const apiUrl = 'api/author';
    this.repository.create(apiUrl, author)
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
