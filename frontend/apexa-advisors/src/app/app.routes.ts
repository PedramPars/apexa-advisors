import { Routes } from '@angular/router';
import { AdvisorListComponent } from './advisor-list/advisor-list.component';
import { AdvisorDetailsComponent } from './advisor-details/advisor-details.component';

export const routes: Routes = [
    { path: '', component: AdvisorListComponent },
    { path: 'advisor-details', component: AdvisorDetailsComponent },
    { path: 'advisor-details/:id', component: AdvisorDetailsComponent }
];
