import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { DriverListResponse } from '../../models/DriverListResponse';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DriverService } from '../../services/driver.service';

@Component({
  selector: 'driver-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './driver-list.component.html',
  styleUrls: ['./driver-list.component.css'],
})
export class DriverListComponent {
  constructor(
    private driverService: DriverService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  drivers: DriverListResponse[] = [];

  displayedColumns: string[] = [
    'actions',
    'documentNumber',
    'name',
    'busRegistrationPlate',
  ];

  ngOnInit(): void {
    this.driverService.getDrivers().subscribe({
      next: (data) => (this.drivers = data),
      error: (err) => console.error(err),
    });
  }

  onAdd() {
    this.router.navigate(['/drivers', 0]);
  }

  onEdit(row: DriverListResponse) {
    this.router.navigate(['/drivers', row.id]);
  }

  onDelete(row: DriverListResponse) {
    const message = `¿Desea eliminar el Chofer ${row.documentNumber}?`;
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: message },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.driverService.deleteDriver(row.id).subscribe({
          next: (data) => {
            this.drivers = this.drivers.filter((b) => b.id !== row.id);

            this.snackBar.open('Chofer eliminado con éxito', 'Cerrar', {
              duration: 3000,
            });
          },
          error: (err) => {
            this.snackBar.open(
              'Error al intentar eliminar el Chofer',
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
