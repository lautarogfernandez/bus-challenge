import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
import { MatSnackBar } from '@angular/material/snack-bar';
import { DriverUpdate } from '../../models/DriverUpdate';
import { DriverListResponse } from '../../models/DriverListResponse';
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
  selector: 'driver-edition',
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
  templateUrl: './driver-edition.component.html',
  styleUrls: ['./driver-edition.component.css'],
})
export class DriverEditionComponent {
  driver?: DriverListResponse;
  isEdition = true;
  buses: any[] = [];
  loading = false;
  loadingError = false;
  title: string = '';
  entity: string = ENTITIES.DRIVER;
  backUrl: string = '/drivers';

  constructor(
    private activatedRoute: ActivatedRoute,
    private driverService: DriverService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  form = new FormGroup({
    documentNumber: new FormControl('', [
      Validators.required,
      Validators.maxLength(8),
      Validators.pattern(/^[0-9]*$/),
    ]),
    name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
  });

  ngOnInit() {
    const id = this.activatedRoute.snapshot.paramMap.get('id') || '';

    this.getDriverData(id);

    this.title = getDetailPageTitle(this.entity, this.isEdition);
  }

  private getDriverData(id: string) {
    if (id && id != '0') {
      this.loading = true;

      this.driverService.getById(id).subscribe({
        next: (data) => {
          this.driver = data;

          this.form.patchValue({
            documentNumber: data.documentNumber,
            name: data.name,
          });

          this.loading = false;
        },
        error: (err) => {
          this.driver = undefined;
          this.loading = false;
          this.loadingError = true;
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
      this.driverService.update(data).subscribe({
        next: (data) => {
          this.handleSuccessOnSave(ACTION_MESSAGES.UPDATED);
        },
        error: (err) => {
          this.handleErrorOnSave(ACTION_MESSAGES.UPDATED, err);
        },
      });
    } else {
      this.driverService.create(data).subscribe({
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
}
