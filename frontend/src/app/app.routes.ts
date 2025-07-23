import { Routes } from '@angular/router';
import { BusListComponent } from '../components/bus-list/bus-list.component';
import { App } from './app';
import { MenuAppComponent } from '../components/menu-app/menu-app.component';
import { BusEditionComponent } from '../components/bus-edition/bus-edition.component';

export const routes: Routes = [
  {
    path: 'buses',
    component: BusListComponent,
  },
  { path: 'buses/:id', component: BusEditionComponent },
  { path: '**', component: MenuAppComponent },
];
