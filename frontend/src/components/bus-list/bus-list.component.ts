import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'bus-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './bus-list.component.html',
  styleUrls: ['./bus-list.component.css'],
})
export class BusListComponent {
  items: string[] = ['Elemento 1', 'Elemento 2', 'Elemento 3'];
}
