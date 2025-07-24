import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { KidListResponse } from '../models/KidListResponse';
import { KidUpdate } from '../models/KidUpdate';

@Injectable({
  providedIn: 'root',
})
export class KidService {
  private apiUrl = 'https://localhost:7295/kid';

  constructor(private http: HttpClient) {}

  getKids(): Observable<KidListResponse[]> {
    return this.http.get<KidListResponse[]>(this.apiUrl);
  }

  getKidById(id: string): Observable<KidListResponse> {
    return this.http.get<KidListResponse>(`${this.apiUrl}/${id}`);
  }

  createKid(kid: KidUpdate): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}`, kid);
  }

  updateKid(kid: KidUpdate): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${kid.id}`, kid);
  }

  deleteKid(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
