import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Advisor } from '../models/advisor.model';
import { AdvisorService } from '../advisor.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-advisor-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './advisor-list.component.html',
  styleUrl: './advisor-list.component.scss',
})
export class AdvisorListComponent {
  advisors: Advisor[] = [];

  constructor(private advisorService: AdvisorService, private router: Router) {}

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    this.advisorService
      .getAdvisors()
      .subscribe(
        (result) => (this.advisors = !result.isError ? result.data : [])
      );
  }

  createAdvisor(): void {
    this.router.navigate(['/advisor-details']);
  }

  editAdvisor(id: string): void {
    this.router.navigate(['/advisor-details', id]);
  }

  deleteAdvisor(id: string): void {
    this.advisorService.deleteAdvisor(id).subscribe((result) => {
      if (!result.isError) {
        this.getData();
      }
    });
  }
}
