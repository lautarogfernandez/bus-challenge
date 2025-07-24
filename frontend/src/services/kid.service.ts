import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { KidResponse } from '../models/KidResponse';
import { KidListResponse } from '../models/KidListResponse';

@Injectable({
  providedIn: 'root',
})
export class KidService {
  private apiUrl = 'https://localhost:7295/kid';

  constructor(private http: HttpClient) {}

  getKids(): Observable<KidListResponse[]> {
    return this.http.get<KidListResponse[]>(this.apiUrl);
  }

  getKidById(id: string): Observable<KidResponse> {
    return this.http.get<KidResponse>(`${this.apiUrl}/${id}`);
  }

  deleteKid(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
