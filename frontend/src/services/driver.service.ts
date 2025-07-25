import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DriverListResponse } from '../models/DriverListResponse';
import { DriverUpdate } from '../models/DriverUpdate';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DriverService {
  private apiUrl = `${environment.backendUrl}driver`;

  constructor(private http: HttpClient) {}

  getDrivers(): Observable<DriverListResponse[]> {
    return this.http.get<DriverListResponse[]>(this.apiUrl);
  }

  getDriverById(id: string): Observable<DriverListResponse> {
    return this.http.get<DriverListResponse>(`${this.apiUrl}/${id}`);
  }

  createDriver(driver: DriverUpdate): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}`, driver);
  }

  updateDriver(driver: DriverUpdate): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${driver.id}`, driver);
  }

  deleteDriver(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
