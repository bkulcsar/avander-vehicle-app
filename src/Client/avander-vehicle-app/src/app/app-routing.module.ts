import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  MeasurementListResolver,
  MeasurementListComponent,
} from './measurements/index';

const routes: Routes = [
  {
    path: '',
    component: MeasurementListComponent,
    resolve: { measurementsResponse: MeasurementListResolver },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
