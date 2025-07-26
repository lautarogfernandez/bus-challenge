import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { BusListResponse } from '../models/BusListResponse';
import { BusResponse } from '../models/BusResponse';
import { BaseCrudService } from './base-crud.service';

@Injectable({ providedIn: 'root' })
export class BusService extends BaseCrudService<
  BusListResponse,
  BusResponse,
  BusResponse
> {
  constructor(http: HttpClient) {
    super(http, `${environment.backendUrl}bus`);
  }
}
