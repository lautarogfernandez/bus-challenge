import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { BusService } from '../../services/bus.service';
import { BusResponse } from '../../models/BusResponse';

@Component({
  selector: 'bus-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './bus-list.component.html',
  styleUrls: ['./bus-list.component.css'],
})
export class BusListComponent {
  constructor(private busService: BusService) {}

  ngOnInit(): void {
    this.busService.getBuses().subscribe({
      next: (data) => (this.buses = data),
      error: (err) => console.error(err),
    });
  }

  displayedColumns: string[] = [
    'actions',
    'registrationPlate',
    'children',
    'driver',
  ];

  buses: BusResponse[] = [];

  dataSource = [
    { id: 1, registrationPlate: 'AA123ZZ', children: 2, driver: 'Hector' },
    { id: 2, registrationPlate: 'BB355II', children: 1, driver: 'Jose' },
    { id: 3, registrationPlate: 'AA874MN', children: 0, driver: 'Juan' },
    { id: 4, registrationPlate: 'SD109PI', children: 7, driver: 'Quique' },
  ];

  onAdd() {
    console.log('add');
  }

  onEditar(row: any) {
    console.log('Editar', row);
  }

  onEliminar(row: any) {
    console.log('Eliminar', row);
  }
}
