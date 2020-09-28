import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BookForCreation } from './../../_interfaces/book-for-creation.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router } from '@angular/router';
import { Author } from 'src/app/_interfaces/author.model';
import { Subject } from 'src/app/_interfaces/subject.model';

@Component({
  selector: 'app-book-create',
  templateUrl: './book-create.component.html',
  styleUrls: ['./book-create.component.css']
})
export class BookCreateComponent implements OnInit {
  public errorMessage: string = 'Erro ao cadastrar o livro.';

  public bookForm: FormGroup;
  public authors: Author[];
  public subjects: Subject[];
  public bookValue = 0;
  public currentYear = new Date().getFullYear();

 constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }
  ngOnInit() {
    this.bookForm = new FormGroup({
      title: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      company: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      value: new FormControl(0, [Validators.required]),
      edition: new FormControl('', [Validators.required]),
      publishYear: new FormControl('', [Validators.required, Validators.maxLength(4), Validators.max(this.currentYear), Validators.min(1500)]),
      bookAuthors: new FormControl(null),
      bookSubjects: new FormControl(null)
    });
    this.getAllAuthors();
    this.getAllSubjects();
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

  public getAllSubjects = () => {
    let apiAddress: string = 'api/subject';
    this.repository.getData(apiAddress).subscribe(
      (res) => {
        this.subjects = res as Subject[];
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };

  updateBRLAmount(event){
    this.bookValue = event.target.value;
  }

  public validateControl = (controlName: string) => {
    if (this.bookForm.controls[controlName].invalid && this.bookForm.controls[controlName].touched)
      return true;
    return false;
  }
  public hasError = (controlName: string, errorName: string) => {
    if (this.bookForm.controls[controlName].hasError(errorName))
      return true;
    return false;
  }
  public createBook = (bookFormValue) => {
    if (this.bookForm.valid) {
      this.executeBookCreation(bookFormValue);
    }
  }
  private executeBookCreation = (bookFormValue) => {
    const book: BookForCreation = {
      title: bookFormValue.title,
      company: bookFormValue.company,
      value:  Number(this.bookValue),
      edition: Number(bookFormValue.edition),
      publishYear: bookFormValue.publishYear,
      bookAuthors:  this.transformToBookAuthorModel(bookFormValue.bookAuthors),
      bookSubjects: this.transformToBookSubjectModel(bookFormValue.bookSubjects)
    }
    const apiUrl = 'api/book';
    this.repository.create(apiUrl, book)
      .subscribe(res => {
        $('#successModal').modal();
      },
      (error => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
    )
  }
  public redirectToBookList(){
    this.router.navigate(['/book/list']);
  }

  private transformToBookAuthorModel(ids) {
    return ids.map((id) => {
      return { "authorId": id }
    });
  }

  private transformToBookSubjectModel(ids) {
    return ids.map((id) => {
      return { "subjectId": id }
    });
  }
}
