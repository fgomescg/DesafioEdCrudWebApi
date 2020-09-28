import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Book } from './../../_interfaces/book.model';

@Component({
  selector: 'app-book-update',
  templateUrl: './book-update.component.html',
  styleUrls: ['./book-update.component.css']
})
export class BookUpdateComponent implements OnInit {

  public errorMessage: string = '';
  public book: Book;
  public bookForm: FormGroup;

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router,
    private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    this.bookForm = new FormGroup({
      title: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      company: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      publishYear: new FormControl('', [Validators.required, Validators.maxLength(4)]),
      value: new FormControl('', [Validators.required])
    });
    this.getOwnerById();
  }

  private getOwnerById = () => {
    let ownerId: string = this.activeRoute.snapshot.params['id'];

    let ownerByIdUrl: string = `api/book/${ownerId}`;
    this.repository.getData(ownerByIdUrl)
      .subscribe(res => {
        this.book = res as Book;
        this.bookForm.patchValue(this.book);

      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
  }

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
}
