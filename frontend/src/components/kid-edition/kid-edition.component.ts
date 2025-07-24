import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DriverUpdate } from '../../models/DriverUpdate';
import { KidService } from '../../services/kid.service';
import { KidListResponse } from '../../models/KidListResponse';

@Component({
  selector: 'kid-edition',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
  ],
  templateUrl: './kid-edition.component.html',
  styleUrls: ['./kid-edition.component.css'],
})
export class KidEditionComponent {
  kid?: KidListResponse;
  isEdition = true;
  buses: any[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private kidService: KidService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  documentNumberControl = new FormControl('');
  nameControl = new FormControl('');

  form = new FormGroup({
    documentNumber: this.documentNumberControl,
    name: this.nameControl,
  });

  ngOnInit() {
    const id = this.activatedRoute.snapshot.paramMap.get('id') || '';

    this.getKidData(id);
  }

  private getKidData(id: string) {
    if (id && id != '0') {
      this.kidService.getKidById(id).subscribe({
        next: (data) => {
          this.kid = data;

          this.form.patchValue({
            documentNumber: data.documentNumber,
            name: data.name,
          });
        },
        error: (err) => {
          console.error(err);
          this.kid = undefined;
        },
      });
    } else {
      this.isEdition = false;
      this.kid = {
        id: '',
        name: '',
        documentNumber: '',
        busRegistrationPlate: null,
      };
    }
  }

  onSave() {
    const formData = this.form.value;

    const data: DriverUpdate = {
      id: this.kid?.id ?? '',
      documentNumber: formData.documentNumber ?? '',
      name: formData.name ?? '',
    };

    if (this.isEdition) {
      this.kidService.updateKid(data).subscribe({
        next: (data) => {
          this.handleSuccessOnSave('actualizado');
        },
        error: (err) => {
          this.snackBar.open(
            'Error al intentar actualizar el Chico',
            'Cerrar',
            {
              duration: 3000,
            }
          ),
            console.error(err);
        },
      });
    } else {
      this.kidService.createKid(data).subscribe({
        next: (data) => this.handleSuccessOnSave('creado'),
        error: (err) => this.handleErrorOnSave('creado', err),
      });
    }
  }

  private handleSuccessOnSave(action: string) {
    this.snackBar.open(`Chico ${action} con éxito`, 'Cerrar', {
      duration: 3000,
    });

    this.router.navigate(['/kids']);
  }

  private handleErrorOnSave(action: string, err: any) {
    this.snackBar.open(`Error al intentar ${action} el Chico`, 'Cerrar', {
      duration: 3000,
    });

    console.error(err);
  }

  onCancel() {
    this.router.navigate(['/kids']);
  }
}
