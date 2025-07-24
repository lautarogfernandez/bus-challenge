import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { KidService } from '../../services/kid.service';
import { KidListResponse } from '../../models/KidListResponse';
import { TableListComponent } from '../table-list/table-list.component';
import { TableColumn } from '../../models/TableColumn';
import { Observable } from 'rxjs';

@Component({
  selector: 'kid-list',
  standalone: true,
  imports: [CommonModule, TableListComponent],
  templateUrl: './kid-list.component.html',
  styleUrls: ['./kid-list.component.css'],
})
export class KidListComponent {
  constructor(private kidService: KidService) {}

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

  ngOnInit(): void {
    this.kidService.getKids().subscribe({
      next: (data) => (this.kids = data),
      error: (err) => console.error(err),
    });
  }

  getRowIdentifier(row: KidListResponse): string {
    return row.documentNumber;
  }

  onDeleteCallback = (id: string): Observable<void> => {
    return this.kidService.deleteKid(id);
  };
}
