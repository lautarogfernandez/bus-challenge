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

  getAll(): Observable<BusListResponse[]> {
    return this.http.get<BusListResponse[]>(this.apiUrl);
  }

  getById(id: string): Observable<BusResponse> {
    return this.http.get<BusResponse>(`${this.apiUrl}/${id}`);
  }

  create(bus: BusResponse): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}`, bus);
  }

  update(bus: BusResponse): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${bus.id}`, bus);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
