import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from './../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RelatorioComponent } from './relatorio.component'


@NgModule({
  declarations: [RelatorioComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'relatorio', component: RelatorioComponent },
    ])
  ]

})

export class RelatorioModule { }
