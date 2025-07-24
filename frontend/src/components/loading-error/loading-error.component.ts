import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-loading-error',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './loading-error.component.html',
  styleUrls: ['./loading-error.component.css'],
})
export class LoadingErrorComponent {
  @Input() show: boolean = false;
}
