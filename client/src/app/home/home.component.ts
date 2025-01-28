import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RegisterComponent, RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  registerMode = false;
  http = inject(HttpClient);
  users: any;


  ngOnInit() {
      this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode
  }

  cancelRegisterMode(event: boolean){
    return this.registerMode = event;
  }

  getUsers(){
    this.http.get('http://localhost:5000/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log("Request completed.")
    })
  }
}
