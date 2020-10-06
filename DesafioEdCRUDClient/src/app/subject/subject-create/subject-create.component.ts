import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SubjectForCreation } from './../../_interfaces/subject-for-creation.model';
import { ErrorHandlerService } from '../../_services/error-handler.service';
import { RepositoryService } from '../../_services/repository.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-subject-create',
  templateUrl: './subject-create.component.html',
  styleUrls: ['./subject-create.component.css']
})
export class SubjectCreateComponent implements OnInit {
  public errorMessage: string = 'Erro ao cadastrar o Autor.';

  public subjectForm: FormGroup;

 constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }
  ngOnInit() {
    this.subjectForm = new FormGroup({
      description: new FormControl('', [Validators.required, Validators.maxLength(20)])
    });
  }
  public validateControl = (controlName: string) => {
    if (this.subjectForm.controls[controlName].invalid && this.subjectForm.controls[controlName].touched)
      return true;
    return false;
  }
  public hasError = (controlName: string, errorName: string) => {
    if (this.subjectForm.controls[controlName].hasError(errorName))
      return true;
    return false;
  }
  public createSubject = (subjectFormValue) => {
    if (this.subjectForm.valid) {
      this.executeSubjectCreation(subjectFormValue);
    }
  }
  private executeSubjectCreation = (subjectFormValue) => {
    const subject: SubjectForCreation = {
      description: subjectFormValue.description
    }
    this.repository.create('/subjects', subject)
      .subscribe(res => {
        $('#successModal').modal();
      },
      (error => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
    )
  }
  public redirectToSubjectList(){
    this.router.navigate(['/subject/list']);
  }
}
