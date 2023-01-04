import { Component } from '@angular/core';
import { API } from '../api/api-client';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  courses: API.CourseDto[] | undefined;

  constructor(private apiClient: API.Client) {

  }

  ngOnInit() {
    this.apiClient
      .courses(undefined, undefined, "AED", undefined, undefined)
      .subscribe(response => this.courses = response.courses)
  }
}
