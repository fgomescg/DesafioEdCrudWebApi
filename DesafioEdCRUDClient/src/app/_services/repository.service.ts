import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EnvironmentUrlService } from './environment-url.service';
@Injectable({
  providedIn: 'root'
})
export class RepositoryService {
  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }
  public getData = (route: string, params?: HttpParams) => {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress), { params});
  }

  public create = (route: string, body) => {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }

  public update = (route: string, body) => {
    return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }

  public delete = (route: string) => {
    return this.http.delete(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/api/v1${route}`;
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({'Access-Control-Allow-Origin': '*' })
    }
  }
}
