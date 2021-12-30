import { Component, OnInit } from '@angular/core';
import { NgModule, enableProdMode, ChangeDetectionStrategy,} from '@angular/core';
import {HttpClient, HttpClientModule, HttpHeaders, HttpParams,} from '@angular/common/http';
import CustomStore from 'devextreme/data/custom_store';
import { formatDate } from 'devextreme/localization';
import { environment } from 'src/environments/environment';
import { MyCoursesService } from 'src/app/shared/services/my-courses.service';

if (!/localhost/.test(document.location.host)) {
  enableProdMode();
}

const URL = 'https://js.devexpress.com/Demos/Mvc/api/DataGridWebApi';
const baseUrl = environment.apiUrl + 'Assignments/assignScore';
const baseUrlPut = environment.apiUrl + 'Assignments/update';

@Component({
  selector: 'app-task-assignments',
  templateUrl: './task-assignments.component.html',
  styleUrls: ['./task-assignments.component.css']
})
export class TaskAssignmentsComponent implements OnInit {

  dataSource: any;

  customersData: any;

  shippersData: any;

  refreshModes: string[];

  refreshMode: string;

  requests: string[] = [];

  taskId:number = 0;

  constructor(private http: HttpClient, private myCoursesService:MyCoursesService) {

    this.refreshMode = 'reshape';
    this.refreshModes = ['full', 'reshape', 'repaint'];

    this.myCoursesService.task.subscribe( async (task) => {
      this.taskId = task;
      this.prepareToRequest();
    })
  }

  prepareToRequest(){
    const id = this.taskId;
    this.dataSource = new CustomStore({
      key: 'id',
      load: () => this.sendRequest(`${baseUrl}/${id}`),
      update: (key, values) => this.sendRequest(`${baseUrlPut}`, 'PUT', {
        key,
        values: JSON.stringify(values),
      }),
    });
  }

  sendRequest(url: string, method = 'GET', data: any = {}): any {
    this.logRequest(method, url, data);
    const httpParams = new HttpParams({ fromObject: data });
    const headers = this.myCoursesService.getHeader();
    const httpOptions = { withCredentials: true, body: httpParams, headers: headers };
    let result = this.http.get(url, httpOptions);

    switch (method) {
      case 'GET':    
        result = this.http.get(url, httpOptions);
        break;
      case 'PUT':
        result = this.http.put(url, httpParams, httpOptions);
        break;
    }

    return result
      .toPromise()
      .then((data: any) => (method === 'GET' ? data.data : data))
      .catch((e) => {
        throw e && e.error && e.error.Message;
      });
  }

  logRequest(method: string, url: string, data: any): void {
    const args = Object.keys(data || {}).map((key) => `${key}=${data[key]}`).join(' ');

    const time = formatDate(new Date(), 'HH:mm:ss');

    this.requests.unshift([time, method, url.slice(URL.length), args].join(' '));
  }

  clearRequests() {
    this.requests = [];
  }

  ngOnInit(): void {
  }

}
