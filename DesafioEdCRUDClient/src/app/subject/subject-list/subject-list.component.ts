import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../_services/repository.service';
import { SubjectList } from './../../_interfaces/subject-list';
import { ErrorHandlerService } from '../../_services/error-handler.service';
import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-subject-list',
  templateUrl: './subject-list.component.html',
  styleUrls: ['./subject-list.component.css'],
})
export class SubjectListComponent implements OnInit {
  public subjects;
  public errorMessage: string = '';
  public totalCount : Number;
  public totalPages: Number;
  public currentPage : Number = 1;
  public pageSize : Number = 10;

  constructor(
    private repository: RepositoryService,
    private errorHandler: ErrorHandlerService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getAllSubjects();
  }
  public getAllSubjects = () => {
    let params = new HttpParams().set("pageNumber",this.currentPage.toString()).set("pageSize", this.pageSize.toString());
    this.repository.getData('/subjects', params).subscribe(
      (res) => {
        const { subjects, totalCount, currentPage, totalPages, pageSize  } = res as SubjectList;
        this.subjects = subjects;
        this.totalCount = totalCount;
        this.pageSize = currentPage;
        this.totalPages = totalPages;
        this.pageSize = pageSize;
      },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      }
    );
  };

  handlePageChange(event){
    this.currentPage = event;
    this.getAllSubjects();
  }

  public redirectToUpdatePage = (id) => {
    const updateUrl: string = `/subject/update/${id}`;
    this.router.navigate([updateUrl]);
  };

  public redirectToDeletePage = (id) => {
    const deleteUrl: string = `/subject/delete/${id}`;
    this.router.navigate([deleteUrl]);
  };
}
