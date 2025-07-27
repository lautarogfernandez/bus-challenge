import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export abstract class BaseCrudService<TList, TType, TUpdate> {
  constructor(protected http: HttpClient, protected apiUrl: string) {}

  getAll(): Observable<TList[]> {
    return this.http.get<TList[]>(this.apiUrl);
  }

  getById(id: string): Observable<TType> {
    return this.http.get<TType>(`${this.apiUrl}/${id}`);
  }

  create(entity: TUpdate): Observable<void> {
    return this.http.post<void>(this.apiUrl, entity);
  }

  update(entity: TUpdate & { id: string }): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${entity.id}`, entity);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
