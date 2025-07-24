import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DriverListResponse } from '../../models/DriverListResponse';
import { DriverService } from '../../services/driver.service';
import { TableColumn } from '../../models/TableColumn';
import { Observable } from 'rxjs';
import { TableListComponent } from '../table-list/table-list.component';

@Component({
  selector: 'driver-list',
  standalone: true,
  imports: [CommonModule, TableListComponent],
  templateUrl: './driver-list.component.html',
  styleUrls: ['./driver-list.component.css'],
})
export class DriverListComponent {
  constructor(private driverService: DriverService) {}

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

  ngOnInit(): void {
    this.driverService.getDrivers().subscribe({
      next: (data) => (this.drivers = data),
      error: (err) => console.error(err),
    });
  }

  getRowIdentifier(row: DriverListResponse): string {
    return row.documentNumber;
  }

  onDeleteCallback = (id: string): Observable<void> => {
    return this.driverService.deleteDriver(id);
  };
}
