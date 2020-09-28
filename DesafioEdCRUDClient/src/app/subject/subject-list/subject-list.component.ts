import { Component, OnInit } from '@angular/core';
import { RepositoryService } from './../../shared/services/repository.service';
import { Subject } from './../../_interfaces/subject.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-subject-list',
  templateUrl: './subject-list.component.html',
  styleUrls: ['./subject-list.component.css'],
})
export class SubjectListComponent implements OnInit {
  public subjects: Subject[];
  public errorMessage: string = '';

  constructor(
    private repository: RepositoryService,
    private errorHandler: ErrorHandlerService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getAllSubjects();
  }
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
  public redirectToUpdatePage = (id) => {
    const updateUrl: string = `/subject/update/${id}`;
    this.router.navigate([updateUrl]);
  };

  public redirectToDeletePage = (id) => {
    const deleteUrl: string = `/subject/delete/${id}`;
    this.router.navigate([deleteUrl]);
  };
}
