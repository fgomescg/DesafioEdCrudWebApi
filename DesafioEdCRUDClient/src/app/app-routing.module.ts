import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'book', loadChildren: () => import('./book/book.module').then(b => b.BookModule) },
    { path: 'author', loadChildren: () => import('./author/author.module').then(a => a.AuthorModule) },
    { path: 'subject', loadChildren: () => import('./subject/subject.module').then(s => s.SubjectModule) },
    { path: 'relatorio', loadChildren: () => import('./relatorio/relatorio.module').then(r => r.RelatorioModule) },
    { path: '**', redirectTo: '/404', pathMatch: 'full'},
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
