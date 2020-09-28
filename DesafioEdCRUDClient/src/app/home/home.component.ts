import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public homeText: string;

  constructor() { }

  ngOnInit(){
    this.homeText = "CADASTRO DE LIVROS";
  }

  public executeSelectedChange = (event) => {
    console.log(event);
  }

}
