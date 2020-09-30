import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Book } from './../../_interfaces/book.model';
import { Author } from 'src/app/_interfaces/author.model';
import { Subject } from 'src/app/_interfaces/subject.model';

@Component({
  selector: 'app-book-update',
  templateUrl: './book-update.component.html',
  styleUrls: ['./book-update.component.css']
})
export class BookUpdateComponent implements OnInit {

  public errorMessage: string = '';
  public book: Book;
  public authors: Author[];
  public subjects: Subject[];
  public bookForm: FormGroup;
  public currentYear = new Date().getFullYear();

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router,
    private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    this.bookForm = new FormGroup({
      title: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      company: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      edition: new FormControl('', [Validators.required]),
      value: new FormControl('', [Validators.required]),
      publishYear: new FormControl('', [Validators.required, Validators.maxLength(4)]),
      bookAuthors: new FormControl(null),
      bookSubjects: new FormControl(null)
    });
    this.getBookById();
    this.getAllAuthors();
    this.getAllSubjects();
  }

  private getBookById = () => {
    let bookId: string = this.activeRoute.snapshot.params['id'];

    let bookIdByIdUrl: string = `api/book/${bookId}`;
    this.repository.getData(bookIdByIdUrl)
      .subscribe(res => {
        this.book = res as Book;
        this.bookForm.patchValue(this.book);
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
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

  public validateControl = (controlName: string) => {
    if (this.bookForm.controls[controlName].invalid && this.bookForm.controls[controlName].touched)
      return true;
    return false;
  }
  public hasError = (controlName: string, errorName: string)  => {
    if (this.bookForm.controls[controlName].hasError(errorName))
      return true;
    return false;
  }
  public redirectToBookList = () => {
    this.router.navigate(['/book/list']);
  }

  public updateBook = (bookFormValue) => {
    if (this.bookForm.valid) {
      this.executeBookUpdate(bookFormValue);
    }
  }
  private executeBookUpdate = (bookFormValue) => {

    this.book.title = bookFormValue.title;
    this.book.company = bookFormValue.company;
    this.book.publishYear = bookFormValue.publishYear;
    this.book.value = Number(bookFormValue.value);
    this.book.edition = Number(bookFormValue.edition);
    this.book.bookAuthors =  this.transformToBookAuthorModel(bookFormValue.bookAuthors);
    this.book.bookSubjects= this.transformToBookSubjectModel(bookFormValue.bookSubjects);

    let apiUrl = `api/book/${this.book.id}`;
    this.repository.update(apiUrl, this.book)
      .subscribe(res => {
        $('#successModal').modal();
      },
      (error => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
    )
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
