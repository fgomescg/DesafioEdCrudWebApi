import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from './../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

import { SubjectListComponent } from './subject-list/subject-list.component';
import { SubjectCreateComponent } from './subject-create/subject-create.component';
import { SubjectUpdateComponent } from './subject-update/subject-update.component';
import { SubjectDeleteComponent } from './subject-delete/subject-delete.component';


@NgModule({
  declarations: [SubjectListComponent, SubjectCreateComponent, SubjectUpdateComponent, SubjectDeleteComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'list', component: SubjectListComponent },
      { path: 'details/:id', component: SubjectListComponent },
      { path: 'create', component: SubjectCreateComponent },
      { path: 'update/:id', component: SubjectUpdateComponent },
      { path: 'delete/:id', component: SubjectDeleteComponent }
    ])
  ]

})
export class SubjectModule { }
