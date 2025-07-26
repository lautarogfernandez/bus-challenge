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
import {
  getDetailPageTitle,
  handleErrorOnSave,
  handleSuccessOnSave,
} from '../../utils/utils';
import { ACTION_MESSAGES, ENTITIES } from '../../utils/textConstants';

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
  entity: string = ENTITIES.BUS;
  backUrl: string = '/buses';

  constructor(
    private activatedRoute: ActivatedRoute,
    private busService: BusService,
    private driverService: DriverService,
    private kidService: KidService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  form = new FormGroup({
    driverId: new FormControl('', [Validators.required]),
    registrationPlate: new FormControl('', [
      Validators.required,
      Validators.maxLength(10),
    ]),
    kidIds: new FormControl([] as string[]),
  });

  ngOnInit() {
    this.getDrivers();
    this.getKids();

    const id = this.activatedRoute.snapshot.paramMap.get('id') || '';

    this.getBusData(id);

    this.title = getDetailPageTitle(this.entity, this.isEdition);
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
          this.handleSuccessOnSave(ACTION_MESSAGES.UPDATED);
        },
        error: (err) => {
          this.handleErrorOnSave(ACTION_MESSAGES.UPDATED, err);
        },
      });
    } else {
      this.busService.create(data).subscribe({
        next: (data) => this.handleSuccessOnSave(ACTION_MESSAGES.CREATED),
        error: (err) => this.handleErrorOnSave(ACTION_MESSAGES.CREATED, err),
      });
    }
  }

  private handleSuccessOnSave(action: string) {
    handleSuccessOnSave(
      this.snackBar,
      this.entity,
      action,
      this.router,
      this.backUrl
    );
  }

  private handleErrorOnSave(action: string, err: any) {
    handleErrorOnSave(this.snackBar, this.entity, action, err);
  }

  onCancel() {
    this.router.navigate([this.backUrl]);
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
    this.kidService.getAll().subscribe({
      next: (data) => {
        this.kids = data;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
