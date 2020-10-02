import { Subject } from './subject.model';

export interface SubjectList {
  totalCount: number;
  totalPages: number;
  pageSize : number;
  currentPage: number;
  subjects: Subject;
}
