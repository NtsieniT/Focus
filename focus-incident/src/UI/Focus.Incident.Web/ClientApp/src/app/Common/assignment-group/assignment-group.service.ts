import { Injectable, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { environment } from '../../../environments/environment';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { toODataString, State } from '@progress/kendo-data-query';

@Injectable({
  providedIn: 'root'
})
export class AssignmentGroupService {

  private BASE_URL = `${environment.serviceUrl}/odata/AssignmentGroup`;

  constructor(private http: HttpClient) {
  }

  public read(state: State): Observable<GridDataResult> {
    const queryStr = `${toODataString(state)}&$count=true`;

    return this.http
      .get(`${this.BASE_URL}()?${queryStr}`)
      .map((response: any) => (<GridDataResult>{
        data: response.value,
        total: parseInt(response['@odata.count'], 10)
      }));
  }

  public readTop20(state: State): Observable<GridDataResult> {
    const queryStr = `$count=true&$orderby=Counter%20desc&$top=20`;

    return this.http
      .get(`${this.BASE_URL}()?${queryStr}`)
      .map((response: any) => (<GridDataResult>{
        data: response.value,
        total: parseInt(response['@odata.count'], 10)
      }));
  }
}
