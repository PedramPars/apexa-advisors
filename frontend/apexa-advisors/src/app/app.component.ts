import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AppToastService } from './toast.service';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, NgbToastModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'apexa-advisors';
  constructor(public toastService: AppToastService) {

  }
}
