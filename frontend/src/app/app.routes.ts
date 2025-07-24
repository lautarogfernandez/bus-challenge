import { Routes } from '@angular/router';
import { BusListComponent } from '../components/bus-list/bus-list.component';
import { App } from './app';
import { MenuAppComponent } from '../components/menu-app/menu-app.component';
import { BusEditionComponent } from '../components/bus-edition/bus-edition.component';
import { DriverListComponent } from '../components/driver-list/driver-list.component';
import { DriverEditionComponent } from '../components/driver-edition/driver-edition.component';

export const routes: Routes = [
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
  { path: '**', component: MenuAppComponent },
];
