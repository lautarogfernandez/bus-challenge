import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DriverListResponse } from '../models/DriverListResponse';
import { DriverUpdate } from '../models/DriverUpdate';
import { BaseCrudService } from './base-crud.service';
import { environment } from '../environments/environment';
import { API_ROUTES } from '../utils/textConstants';

@Injectable({ providedIn: 'root' })
export class DriverService extends BaseCrudService<
  DriverListResponse,
  DriverListResponse,
  DriverUpdate
> {
  constructor(http: HttpClient) {
    super(http, `${environment.backendUrl}${API_ROUTES.DRIVER}`);
  }
}
