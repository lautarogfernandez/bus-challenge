import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusResponse } from '../models/BusResponse';

@Injectable({
  providedIn: 'root',
})
export class BusService {
  private apiUrl = 'https://localhost:5079/bus';

  constructor(private http: HttpClient) {}

  getBuses(): Observable<BusResponse[]> {
    return this.http.get<BusResponse[]>(this.apiUrl);
  }
}
