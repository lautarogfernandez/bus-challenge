import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusListResponse } from '../models/BusListResponse';
import { BusResponse } from '../models/BusResponse';

@Injectable({
  providedIn: 'root',
})
export class BusService {
  private apiUrl = 'https://localhost:7295/bus';

  constructor(private http: HttpClient) {}

  getBuses(): Observable<BusListResponse[]> {
    return this.http.get<BusListResponse[]>(this.apiUrl);
  }

  getBusById(id: number): Observable<BusResponse> {
    return this.http.get<BusResponse>(`${this.apiUrl}/${id}`);
  }
}
