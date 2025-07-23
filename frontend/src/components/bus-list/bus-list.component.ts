import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { BusService } from '../../services/bus.service';
import { BusListResponse } from '../../models/BusListResponse';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'bus-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './bus-list.component.html',
  styleUrls: ['./bus-list.component.css'],
})
export class BusListComponent {
  constructor(
    private busService: BusService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.busService.getBuses().subscribe({
      next: (data) => (this.buses = data),
      error: (err) => console.error(err),
    });
  }

  displayedColumns: string[] = [
    'actions',
    'registrationPlate',
    'kids',
    'driverDocumentNumber',
  ];

  buses: BusListResponse[] = [];

  onAdd() {
    this.router.navigate(['/bus', 0]);
  }

  onEdit(row: BusListResponse) {
    this.router.navigate(['/bus', row.id]);
  }

  onDelete(row: BusListResponse) {
    const message = '';
    `¿Desea eliminar el Micro ${row.registrationPlate}?`;
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: message },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.busService.deleteBus(row.id).subscribe({
          next: (data) => {
            this.buses = this.buses.filter((b) => b.id !== row.id);

            this.snackBar.open('Micro eliminado con éxito', 'Cerrar', {
              duration: 3000,
            });
          },
          error: (err) => {
            this.snackBar.open(
              'Error al intentar eliminar el Micro',
              'Cerrar',
              {
                duration: 3000,
              }
            ),
              console.error(err);
          },
        });
      }
    });
  }
}
