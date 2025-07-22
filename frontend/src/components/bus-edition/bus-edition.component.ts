import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BusService } from '../../services/bus.service';
import { ActivatedRoute } from '@angular/router';
import { BusResponse } from '../../models/BusResponse';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'bus-edition',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
  ],
  templateUrl: './bus-edition.component.html',
  styleUrls: ['./bus-edition.component.css'],
})
export class BusEditionComponent {
  bus?: BusResponse;
  isEdition = true;
  drivers: any[] = [];

  children: any[] = [
    { id: 1111, name: 'Juancito' },
    { id: 1112, name: 'Agustin' },
    { id: 1113, name: 'Felipe' },
    { id: 1114, name: 'Juana' },
    { id: 1115, name: 'María' },
    { id: 1116, name: 'Federico' },
    { id: 1117, name: 'Natalia' },
    { id: 1118, name: 'Santiago' },
    { id: 1119, name: 'Bianca' },
  ];

  constructor(private route: ActivatedRoute, private busService: BusService) {}

  driverControl = new FormControl(0);
  registrationPlateControl = new FormControl('');
  childrenControl = new FormControl([] as number[]);

  form = new FormGroup({
    driverId: this.driverControl,
    registrationPlate: this.registrationPlateControl,
    childrenIds: this.childrenControl,
  });

  ngOnInit() {
    this.getDrivers();

    const id = this.route.snapshot.paramMap.get('id') || '';
    this.getBusData(+id);
  }

  getBusData(id: number) {
    if (id) {
      this.busService.getBusById(+id).subscribe({
        next: (data) => {
          this.bus = data;

          this.form.patchValue({
            driverId: data.driverId,
            registrationPlate: data.registrationPlate,
            childrenIds: data.childrenIds,
          });
        },
        error: (err) => {
          console.error(err);
          this.bus = undefined;
        },
      });
    } else {
      this.isEdition = false;
      this.bus = { id: 0, childrenIds: [], driverId: 0, registrationPlate: '' };
    }
  }

  save() {
    console.log('on Save', this.bus);
  }

  getDrivers() {
    this.drivers = [
      { id: 100, name: 'Héctor' },
      { id: 101, name: 'José' },
      { id: 102, name: 'Juan' },
    ];
  }
}
