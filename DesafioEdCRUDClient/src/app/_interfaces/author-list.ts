import { Author } from './author.model';

export interface AuthorList {
  totalCount: number;
  totalPages: number;
  pageSize : number;
  currentPage: number;
  authors: Author;
}
