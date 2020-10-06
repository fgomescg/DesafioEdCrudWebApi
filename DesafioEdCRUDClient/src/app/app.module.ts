import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { RouterModule } from '@angular/router'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { InternalServerComponent } from './error-pages/internal-server/internal-server.component';
import { RelatorioModule } from './relatorio/relatorio.module';
import { AuthenticationService } from './_services';
import { JwtInterceptor, ErrorInterceptor, appInitializer  } from './_helpers';
import { LoginComponent } from './login';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    NotFoundComponent,
    InternalServerComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RelatorioModule,
    RouterModule.forRoot([
      { path: '404', component : NotFoundComponent},
      { path: '500', component: InternalServerComponent },
      { path: 'book', loadChildren: () => import('./book/book.module').then(b => b.BookModule) },
      { path: 'author', loadChildren: () => import('./author/author.module').then(a => a.AuthorModule) },
      { path: 'subject', loadChildren: () => import('./subject/subject.module').then(s => s.SubjectModule) },
      { path: 'relatorio', loadChildren: () => import('./relatorio/relatorio.module').then(r => r.RelatorioModule) },
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: '**', redirectTo: '/404', pathMatch: 'full'},
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
