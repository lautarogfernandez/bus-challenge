import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { KidService } from '../../services/kid.service';
import { KidListResponse } from '../../models/KidListResponse';
import { TableListComponent } from '../table-list/table-list.component';
import { TableColumn } from '../../models/TableColumn';
import { Observable, tap } from 'rxjs';

@Component({
  selector: 'kid-list',
  standalone: true,
  imports: [CommonModule, TableListComponent],
  templateUrl: './kid-list.component.html',
  styleUrls: ['./kid-list.component.css'],
})
export class KidListComponent {
  kids: KidListResponse[] = [];
  columns: TableColumn[] = [
    {
      name: 'name',
      label: 'Nombre',
    },
    {
      name: 'documentNumber',
      label: 'Documento',
    },
    {
      name: 'busRegistrationPlate',
      label: 'Patente del Micro',
    },
  ];
  loading: boolean = false;
  loadingError: boolean = false;

  constructor(private kidService: KidService) {}

  ngOnInit(): void {
    this.loading = true;

    this.kidService.getKids().subscribe({
      next: (data) => {
        this.kids = data;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.loadingError = true;
      },
    });
  }

  getRowIdentifier(row: KidListResponse): string {
    return row.documentNumber;
  }

  onDeleteCallback = (id: string): Observable<void> => {
    return this.kidService.deleteKid(id).pipe(
      tap(() => {
        this.kids = this.kids.filter((x) => x.id !== id);
      })
    );
  };
}
