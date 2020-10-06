import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html'
})
export class ReportComponent implements OnInit {

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
