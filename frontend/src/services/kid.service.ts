import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { KidListResponse } from '../models/KidListResponse';
import { KidUpdate } from '../models/KidUpdate';
import { environment } from '../environments/environment';
import { BaseCrudService } from './base-crud.service';
import { API_ROUTES } from '../utils/textConstants';

@Injectable({ providedIn: 'root' })
export class KidService extends BaseCrudService<
  KidListResponse,
  KidListResponse,
  KidUpdate
> {
  constructor(http: HttpClient) {
    super(http, `${environment.backendUrl}${API_ROUTES.KID}`);
  }
}
