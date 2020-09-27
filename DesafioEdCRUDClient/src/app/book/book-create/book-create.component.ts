import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BookForCreation } from './../../_interfaces/book-for-creation.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book-create',
  templateUrl: './book-create.component.html',
  styleUrls: ['./book-create.component.css']
})
export class BookCreateComponent implements OnInit {
  public errorMessage: string = '';

  public bookForm: FormGroup;

 constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }
  ngOnInit() {
    this.bookForm = new FormGroup({
      title: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      company: new FormControl('', [Validators.required, Validators.maxLength(40)]),
      value: new FormControl('', [Validators.required]),
      publishYear: new FormControl('', [Validators.required, Validators.maxLength(4)])
    });
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
  public executeDatePicker = (event) => {
    this.bookForm.patchValue({ 'dateOfBirth': event });
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
      value: bookFormValue.value,
      publishYear: bookFormValue.publishYear
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
}
