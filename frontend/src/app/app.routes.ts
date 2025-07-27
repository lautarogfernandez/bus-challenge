import { Routes } from '@angular/router';
import { BusListComponent } from '../components/bus-list/bus-list.component';
import { BusEditionComponent } from '../components/bus-edition/bus-edition.component';
import { DriverListComponent } from '../components/driver-list/driver-list.component';
import { DriverEditionComponent } from '../components/driver-edition/driver-edition.component';
import { KidListComponent } from '../components/kid-list/kid-list.component';
import { KidEditionComponent } from '../components/kid-edition/kid-edition.component';
import { HomeComponent } from '../components/home/home.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'buses',
    component: BusListComponent,
  },
  { path: 'buses/:id', component: BusEditionComponent },
  {
    path: 'drivers',
    component: DriverListComponent,
  },
  { path: 'drivers/:id', component: DriverEditionComponent },
  {
    path: 'kids',
    component: KidListComponent,
  },
  { path: 'kids/:id', component: KidEditionComponent },
  { path: '**', redirectTo: 'home' },
];
