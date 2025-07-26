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
import { MatSnackBar } from '@angular/material/snack-bar';
import { DriverUpdate } from '../../models/DriverUpdate';
import { KidService } from '../../services/kid.service';
import { KidListResponse } from '../../models/KidListResponse';
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
  selector: 'kid-edition',
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
  templateUrl: './kid-edition.component.html',
  styleUrls: ['./kid-edition.component.css'],
})
export class KidEditionComponent {
  kid?: KidListResponse;
  isEdition = true;
  buses: any[] = [];
  loading = false;
  loadingError = false;
  title: string = '';
  entity: string = ENTITIES.KID;
  backUrl: string = '/kids';

  constructor(
    private activatedRoute: ActivatedRoute,
    private kidService: KidService,
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

    this.getKidData(id);

    this.title = getDetailPageTitle(this.entity, this.isEdition);
  }

  private getKidData(id: string) {
    if (id && id != '0') {
      this.loading = true;

      this.kidService.getById(id).subscribe({
        next: (data) => {
          this.kid = data;

          this.form.patchValue({
            documentNumber: data.documentNumber,
            name: data.name,
          });

          this.loading = false;
        },
        error: (err) => {
          this.kid = undefined;
          this.loading = false;
          this.loadingError = true;
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
      this.kidService.update(data).subscribe({
        next: (data) => {
          this.handleSuccessOnSave(ACTION_MESSAGES.UPDATED);
        },
        error: (err) => {
          this.handleErrorOnSave(ACTION_MESSAGES.UPDATED, err);
        },
      });
    } else {
      this.kidService.create(data).subscribe({
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
