import { Component, OnInit } from '@angular/core';
import {InputTextModule,PasswordModule,ButtonModule} from 'primeng/primeng';


@Component({
  selector: 'app-login-component',
  templateUrl: './login-component.component.html',
  styleUrls: ['./login-component.component.css']
})
export class LoginComponentComponent implements OnInit {
  text: string;

  constructor() { }

  ngOnInit() {
  }

}
