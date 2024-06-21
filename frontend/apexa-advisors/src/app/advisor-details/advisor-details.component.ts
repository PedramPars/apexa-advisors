import { Component, OnInit } from '@angular/core';
import { AdvisorDetails } from '../models/advisor-details.model';
import { AdvisorService } from '../advisor.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AdvisorSave } from '../models/advisor-save.model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-advisor-details',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './advisor-details.component.html',
  styleUrl: './advisor-details.component.scss',
})
export class AdvisorDetailsComponent implements OnInit {
  advisorForm: FormGroup = this.fb.group({
    id: [''],
    name: ['', Validators.required],
    sin: ['', Validators.required],
    address: [''],
    phone: [''],
  });

  constructor(
    private fb: FormBuilder,
    private advisorService: AdvisorService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.advisorService.getAdvisor(id).subscribe((result) => {
        if (!result.isError) {
          this.advisorForm.patchValue(result.data);
        }
      });
    }
  }

  onSubmit(): void {
    if (this.advisorForm.invalid)
        return;

    const request = this.advisorForm.value.id
      ? this.advisorService.updateAdvisor(this.advisorForm.value)
      : this.advisorService.createAdvisor(this.advisorForm.value);
    request.subscribe((result) => {
      if (!result.isError) this.router.navigate(['/']);
    });
  }
}
