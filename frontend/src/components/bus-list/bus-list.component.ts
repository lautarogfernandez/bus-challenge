import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BusService } from '../../services/bus.service';
import { BusListResponse } from '../../models/BusListResponse';
import { TableColumn } from '../../models/TableColumn';
import { Observable } from 'rxjs';
import { TableListComponent } from '../table-list/table-list.component';

@Component({
  selector: 'bus-list',
  standalone: true,
  imports: [CommonModule, TableListComponent],
  templateUrl: './bus-list.component.html',
  styleUrls: ['./bus-list.component.css'],
})
export class BusListComponent {
  buses: BusListResponse[] = [];
  columns: TableColumn[] = [
    {
      name: 'registrationPlate',
      label: 'Patente',
    },
    {
      name: 'kids',
      label: 'Chicos',
    },
    {
      name: 'driverDocumentNumber',
      label: 'Chofer',
    },
  ];
  loading: boolean = false;
  loadingError: boolean = false;

  constructor(private busService: BusService) {}

  ngOnInit(): void {
    this.loading = true;

    this.busService.getAll().subscribe({
      next: (data) => {
        this.buses = data;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.loadingError = true;
      },
    });
  }

  getRowIdentifier(row: BusListResponse): string {
    return row.registrationPlate;
  }

  onDeleteCallback = (id: string): Observable<void> => {
    return this.busService.delete(id);
  };
}
