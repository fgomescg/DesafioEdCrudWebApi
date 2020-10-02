import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BookForCreation } from './../../_interfaces/book-for-creation.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router } from '@angular/router';
import { AuthorList } from 'src/app/_interfaces/author-list';
import { SubjectList } from 'src/app/_interfaces/subject-list';


@Component({
  selector: 'app-book-create',
  templateUrl: './book-create.component.html',
  styleUrls: ['./book-create.component.css']
})
export class BookCreateComponent implements OnInit {
  public errorMessage: string = 'Erro ao cadastrar o livro.';

  public bookForm: FormGroup;
  public authors;
  public subjects;
  public bookValue = 0;
  public currentYear = new Date().getFullYear();

 constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }
  ngOnInit() {
    this.bookForm = new FormGroup({
      title: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      company: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      edition: new FormControl('', [Validators.required]),
      value: new FormControl(this.bookValue, [Validators.required]),
      publishYear: new FormControl('', [Validators.required, Validators.maxLength(4), Validators.min(1500), Validators.max(this.currentYear)]),
      bookAuthors: new FormControl(null),
      bookSubjects: new FormControl(null)
    });
    this.getAllAuthors();
    this.getAllSubjects();
  }

  public getAllAuthors = () => {
    this.repository.getData('api/author').subscribe(
      (res) => {
        const { authors, totalCount, currentPage, totalPages, pageSize  } = res as AuthorList;
        this.authors = authors;
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };

  public getAllSubjects = () => {
    this.repository.getData('api/subject').subscribe(
      (res) => {
        const { subjects, totalCount, currentPage, totalPages, pageSize  } = res as SubjectList;
        this.subjects = subjects;
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
      edition: Number(bookFormValue.edition),
      value:  Number(this.bookValue),
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
    if (ids) {
      return ids.map((id) => {
        return { "authorId": id }
      });
    }
    return [];
  }

  private transformToBookSubjectModel(ids) {
    if (ids) {
      return ids.map((id) => {
        return { "subjectId": id }
      });
    }
    return [];
  }
}
