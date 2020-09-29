import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SubjectForCreation } from './../../_interfaces/Subject-for-creation.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from 'src/app/_interfaces/Subject.model';

@Component({
  selector: 'app-subject-update',
  templateUrl: './subject-update.component.html',
  styleUrls: ['./subject-update.component.css']
})
export class SubjectUpdateComponent implements OnInit {
  public errorMessage: string = 'Erro ao atualizar Assunto.';

  public subjectForm: FormGroup;
  public subject: Subject;

 constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router,
  private activeRoute: ActivatedRoute) { }
  ngOnInit() {
    this.subjectForm = new FormGroup({
      description: new FormControl('', [Validators.required, Validators.maxLength(20)])
    });
    this.getSubjectById();
  }

  private getSubjectById = () => {
    let subjectId: string = this.activeRoute.snapshot.params['id'];

    let SubjectByIdUrl: string = `api/subject/${subjectId}`;
    this.repository.getData(SubjectByIdUrl)
      .subscribe(res => {
        this.subject = res as Subject;
        this.subjectForm.patchValue(this.subject);
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
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
  public updateSubject = (SubjectFormValue) => {
    if (this.subjectForm.valid) {
      this.executeSubjectUpdate(SubjectFormValue);
    }
  }
  private executeSubjectUpdate = (subjectFormValue) => {
    const subjectForm: SubjectForCreation = {
      description: subjectFormValue.description
    }
    let apiUrl = `api/subject/${this.subject.subjectId}`;
    this.repository.update(apiUrl, subjectForm)
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
