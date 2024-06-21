import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { AdvisorDetails } from './models/advisor-details.model';
import { Advisor } from './models/advisor.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Result } from './models/result.model';
import { AdvisorSave } from './models/advisor-save.model';

@Injectable({
  providedIn: 'root',
})
export class AdvisorService {
  private readonly baseAddress = `${environment.baseAddress}/advisors`

  constructor(private client: HttpClient) {

  }
  getAdvisors(): Observable<Result<Advisor[]>> {
    return this.client.get<Result<Advisor[]>>(`${this.baseAddress}`)
  }

  getAdvisor(id: string): Observable<Result<AdvisorDetails>> {
    return this.client.get<Result<AdvisorDetails>>(`${this.baseAddress}/${id}`)
  }

  createAdvisor(advisor: AdvisorSave): Observable<Result<any>> {
    return this.client.post<Result<any>>(`${this.baseAddress}`, advisor)
  }

  updateAdvisor(advisor: AdvisorSave): Observable<Result<any>> {
    return this.client.put<Result<any>>(`${this.baseAddress}`, advisor)
  }

  deleteAdvisor(id: string): Observable<Result<any>> {
    return this.client.delete<Result<any>>(`${this.baseAddress}/${id}`)
  }
}
