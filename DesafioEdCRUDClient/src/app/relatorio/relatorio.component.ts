import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-relatorio',
  templateUrl: './relatorio.component.html',
  styleUrls: ['./relatorio.component.css']
})
export class RelatorioComponent implements OnInit {

  constructor(
  ) {}

  ngOnInit(): void {
  }

  openReport(download : boolean) {
    if(download) {
      window.open('http://localhost:5000/api/report/download');
     }
     window.open('http://localhost:5000/api/report');
  }

}
