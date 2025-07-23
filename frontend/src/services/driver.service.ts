import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DriverListResponse } from '../models/DriverListResponse';
import { DriverResponse } from '../models/DriverResponse';

@Injectable({
  providedIn: 'root',
})
export class DriverService {
  private apiUrl = 'https://localhost:7295/driver';

  constructor(private http: HttpClient) {}

  getDrivers(): Observable<DriverListResponse[]> {
    return this.http.get<DriverListResponse[]>(this.apiUrl);
  }

  getDriverById(id: string): Observable<DriverResponse> {
    return this.http.get<DriverResponse>(`${this.apiUrl}/${id}`);
  }

  deleteDriver(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
