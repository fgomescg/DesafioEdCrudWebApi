import { Book } from './book.model';

export interface BookList {
  totalCount: number;
  totalPages: number;
  pageSize : number;
  currentPage: number;
  books: Book;
}
