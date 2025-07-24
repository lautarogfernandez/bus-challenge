import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, of } from 'rxjs';
import { TableColumn } from '../../models/TableColumn';

@Component({
  selector: 'table-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css'],
})
export class TableListComponent {
  @Input() data: any[] = [];
  @Input() columns: TableColumn[] = [];
  @Input() entityName: string = '';
  @Input() editionUrl: string = '';
  @Input() onDeleteCallback: (id: string) => Observable<void> = () => of();
  @Input() getRowIdentifier: (row: any) => string = (row) => row?.id ?? '';

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  displayedColumns: string[] = [];

  ngOnInit(): void {
    this.displayedColumns = ['actions', ...this.columns.map((c) => c.name)];
  }

  onAdd() {
    this.router.navigate([this.editionUrl, 0]);
  }

  onEdit(row: { id: string }) {
    this.router.navigate([this.editionUrl, row.id]);
  }

  onDelete(row: any) {
    const identifier = this.getRowIdentifier(row);
    const message = `¿Desea eliminar el ${this.entityName} ${identifier}?`;
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: message },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.onDeleteCallback(row.id).subscribe({
          next: (_) => {
            this.data = this.data.filter((b) => b.id !== row.id);

            this.snackBar.open(
              `${this.entityName} eliminado con éxito`,
              'Cerrar',
              {
                duration: 3000,
              }
            );
          },
          error: (err) => {
            this.snackBar.open(
              `Error al intentar eliminar el ${this.entityName}`,
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
