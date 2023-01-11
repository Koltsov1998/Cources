import { Component } from '@angular/core';
import { API } from '../api/api-client';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  courses: API.CourseDto[] | undefined;
  currencies: string[] = [];
  selectedCurrency: string | undefined;
  courseValue: API.GetCourseResponse | undefined;
  chartOptions = {
    plugins: {
      legend: {
        labels: {
          color: '#495057'
        }
      }
    },
    scales: {
      x: {
        ticks: {
          color: '#495057'
        },
        grid: {
          color: '#ebedef'
        }
      },
      y: {
        ticks: {
          color: '#495057'
        },
        grid: {
          color: '#ebedef'
        }
      }
    }
  };
  dateFrom: Date | undefined = new Date("2015-01-2");;

  chartData: any
  constructor(private apiClient: API.Client) {

  }

  ngOnInit() {
    this.apiClient
      .names()
      .subscribe(response => {
        this.currencies = response.map(name => name)
        this.selectedCurrency = response[0]
        this.refreshData();
      })
  }

  public refreshData() {
    if (this.selectedCurrency) {
      let dateFromUtc: Date | undefined

      if (this.dateFrom) {
        dateFromUtc = convertLocalDateToUTCIgnoringTimezone(this.dateFrom)
        this.apiClient
          .exactDate(this.selectedCurrency, dateFromUtc)
          .subscribe(response => this.courseValue = response)
      }

      this.apiClient
        .courses(dateFromUtc, undefined, this.selectedCurrency, undefined, undefined)
        .subscribe(response => {
          this.courses = response.courses;
          this.chartData = {
            labels: response.courses?.map(_ => _.date?.toLocaleDateString()),
            datasets: [
              {
                label: this.selectedCurrency,
                data: response.courses?.map(_ => _.value),
                fill: false,
                borderColor: '#42A5F5',
                tension: .4
              }
            ]
          };
        })
    }
  }
}

function convertLocalDateToUTCIgnoringTimezone(date: Date) {
  const timestamp = Date.UTC(
    date.getFullYear(),
    date.getMonth(),
    date.getDate(),
    date.getHours(),
    date.getMinutes(),
    date.getSeconds(),
    date.getMilliseconds(),
  );

  return new Date(timestamp);
}