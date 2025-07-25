import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BusService } from '../../services/bus.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BusResponse } from '../../models/BusResponse';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { DriverService } from '../../services/driver.service';
import { KidService } from '../../services/kid.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormButtonsComponent } from '../form-buttons/form-buttons.component';
import { ProgressBarComponent } from '../progress-bar/progress-bar.component';
import { LoadingErrorComponent } from '../loading-error/loading-error.component';

@Component({
  selector: 'bus-edition',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    FormButtonsComponent,
    ProgressBarComponent,
    LoadingErrorComponent,
  ],
  templateUrl: './bus-edition.component.html',
  styleUrls: ['./bus-edition.component.css'],
})
export class BusEditionComponent {
  bus?: BusResponse;
  isEdition = true;
  drivers: any[] = [];
  kids: any[] = [];
  loading = false;
  loadingError = false;
  title: string = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    private busService: BusService,
    private driverService: DriverService,
    private kidService: KidService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  driverControl = new FormControl('', [Validators.required]);
  registrationPlateControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(10),
  ]);
  kidsControl = new FormControl([] as string[]);

  form = new FormGroup({
    driverId: this.driverControl,
    registrationPlate: this.registrationPlateControl,
    kidIds: this.kidsControl,
  });

  ngOnInit() {
    this.getDrivers();
    this.getKids();

    const id = this.activatedRoute.snapshot.paramMap.get('id') || '';

    this.getBusData(id);

    this.title = `${this.isEdition ? 'Edición' : 'Creación'} de Micro`;
  }

  private getBusData(id: string) {
    if (id && id != '0') {
      this.loading = true;

      this.busService.getById(id).subscribe({
        next: (data) => {
          this.bus = data;

          this.form.patchValue({
            driverId: data.driverId,
            registrationPlate: data.registrationPlate,
            kidIds: data.kidIds,
          });

          this.loading = false;
        },
        error: (err) => {
          this.bus = undefined;
          this.loading = false;
          this.loadingError = true;
        },
      });
    } else {
      this.isEdition = false;
      this.bus = { id: '', registrationPlate: '', driverId: '', kidIds: [] };
    }
  }

  onSave() {
    const formData = this.form.value;

    const data: BusResponse = {
      id: this.bus?.id ?? '',
      driverId: formData.driverId ?? '',
      registrationPlate: formData.registrationPlate ?? '',
      kidIds: formData.kidIds ?? [],
    };

    if (this.isEdition) {
      this.busService.update(data).subscribe({
        next: (data) => {
          this.handleSuccessOnSave('actualizado');
        },
        error: (err) => {
          this.snackBar.open(
            'Error al intentar actualizar el Micro',
            'Cerrar',
            {
              duration: 3000,
            }
          ),
            console.error(err);
        },
      });
    } else {
      this.busService.create(data).subscribe({
        next: (data) => this.handleSuccessOnSave('creado'),
        error: (err) => this.handleErrorOnSave('creado', err),
      });
    }
  }

  private handleSuccessOnSave(action: string) {
    this.snackBar.open(`Micro ${action} con éxito`, 'Cerrar', {
      duration: 3000,
    });

    this.router.navigate(['/buses']);
  }

  private handleErrorOnSave(action: string, err: any) {
    this.snackBar.open(`Error al intentar ${action} el Micro`, 'Cerrar', {
      duration: 3000,
    });

    console.error(err);
  }

  onCancel() {
    this.router.navigate(['/buses']);
  }

  private getDrivers() {
    this.driverService.getAll().subscribe({
      next: (data) => {
        this.drivers = data;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  private getKids() {
    this.kidService.getKids().subscribe({
      next: (data) => {
        this.kids = data;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
