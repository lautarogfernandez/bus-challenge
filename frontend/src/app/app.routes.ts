import { Routes } from '@angular/router';
import { BusListComponent } from '../components/bus-list/bus-list.component';
import { App } from './app';
import { MenuAppComponent } from '../components/menu-app/menu-app.component';

export const routes: Routes = [
  {
    path: 'buses',
    component: BusListComponent,
  },
  { path: '**', component: MenuAppComponent },
];
