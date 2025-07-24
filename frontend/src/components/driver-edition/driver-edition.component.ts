import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { DriverService } from '../../services/driver.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DriverUpdate } from '../../models/DriverUpdate';
import { DriverListResponse } from '../../models/DriverListResponse';
import { FormButtonsComponent } from '../form-buttons/form-buttons.component';

@Component({
  selector: 'driver-edition',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    FormButtonsComponent,
  ],
  templateUrl: './driver-edition.component.html',
  styleUrls: ['./driver-edition.component.css'],
})
export class DriverEditionComponent {
  driver?: DriverListResponse;
  isEdition = true;
  buses: any[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private driverService: DriverService,
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

    this.getDriverData(id);
  }

  private getDriverData(id: string) {
    if (id && id != '0') {
      this.driverService.getDriverById(id).subscribe({
        next: (data) => {
          this.driver = data;

          this.form.patchValue({
            documentNumber: data.documentNumber,
            name: data.name,
          });
        },
        error: (err) => {
          console.error(err);
          this.driver = undefined;
        },
      });
    } else {
      this.isEdition = false;
      this.driver = {
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
      id: this.driver?.id ?? '',
      documentNumber: formData.documentNumber ?? '',
      name: formData.name ?? '',
    };

    if (this.isEdition) {
      this.driverService.updateDriver(data).subscribe({
        next: (data) => {
          this.handleSuccessOnSave('actualizado');
        },
        error: (err) => {
          this.snackBar.open(
            'Error al intentar actualizar el Chofer',
            'Cerrar',
            {
              duration: 3000,
            }
          ),
            console.error(err);
        },
      });
    } else {
      this.driverService.createDriver(data).subscribe({
        next: (data) => this.handleSuccessOnSave('creado'),
        error: (err) => this.handleErrorOnSave('creado', err),
      });
    }
  }

  private handleSuccessOnSave(action: string) {
    this.snackBar.open(`Chofer ${action} con éxito`, 'Cerrar', {
      duration: 3000,
    });

    this.router.navigate(['/drivers']);
  }

  private handleErrorOnSave(action: string, err: any) {
    this.snackBar.open(`Error al intentar ${action} el Chofer`, 'Cerrar', {
      duration: 3000,
    });

    console.error(err);
  }

  onCancel() {
    this.router.navigate(['/drivers']);
  }
}
