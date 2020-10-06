import { Component, OnInit } from '@angular/core';
import { ErrorHandlerService } from '../../_services/error-handler.service';
import { RepositoryService } from '../../_services/repository.service';
import { Subject } from './../../_interfaces/subject.model';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-subject-delete',
  templateUrl: './subject-delete.component.html',
  styleUrls: ['./subject-delete.component.css'],
})
export class SubjectDeleteComponent implements OnInit {
  public errorMessage: string = 'Erro ao deletar Assunto.';
  public subject: Subject;

  constructor(
    private repository: RepositoryService,
    private errorHandler: ErrorHandlerService,
    private router: Router,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.getSubjectById();
  }

  private getSubjectById = () => {
    const subjectId: string = this.activeRoute.snapshot.params['id'];
    const subjectByIdUrl: string = `api/subject/${subjectId}`;

    this.repository.getData(subjectByIdUrl).subscribe(
      (res) => {
        this.subject = res as Subject;
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };

  public redirectToSubjectList = () => {
    this.router.navigate(['/subject/list']);
  };

  public deleteSubject = () => {
    const deleteUrl: string = `api/subject/${this.subject.subjectId}`;
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
