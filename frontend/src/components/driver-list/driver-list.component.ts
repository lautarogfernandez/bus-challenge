import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DriverListResponse } from '../../models/DriverListResponse';
import { DriverService } from '../../services/driver.service';
import { TableColumn } from '../../models/TableColumn';
import { Observable, tap } from 'rxjs';
import { TableListComponent } from '../table-list/table-list.component';

@Component({
  selector: 'driver-list',
  standalone: true,
  imports: [CommonModule, TableListComponent],
  templateUrl: './driver-list.component.html',
  styleUrls: ['./driver-list.component.css'],
})
export class DriverListComponent {
  drivers: DriverListResponse[] = [];
  columns: TableColumn[] = [
    {
      name: 'documentNumber',
      label: 'Documento',
    },
    {
      name: 'name',
      label: 'Nombre',
    },
    {
      name: 'busRegistrationPlate',
      label: 'Patente del Micro',
    },
  ];
  loading: boolean = false;
  loadingError: boolean = false;

  constructor(private driverService: DriverService) {}

  ngOnInit(): void {
    this.loading = true;

    this.driverService.getAll().subscribe({
      next: (data) => {
        this.drivers = data;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.loadingError = true;
      },
    });
  }

  getRowIdentifier(row: DriverListResponse): string {
    return row.documentNumber;
  }

  onDeleteCallback = (id: string): Observable<void> => {
    return this.driverService.delete(id).pipe(
      tap(() => {
        this.drivers = this.drivers.filter((x) => x.id !== id);
      })
    );
  };
}
