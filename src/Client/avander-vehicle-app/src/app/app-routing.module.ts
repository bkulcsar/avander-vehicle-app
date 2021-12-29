import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  MeasurementListResolver,
  MeasurementListComponent,
  MeasurementUploadComponent,
  ShopListResolver,
} from './measurements/index';

const routes: Routes = [
  {
    path: '',
    component: MeasurementListComponent,
    resolve: {
      measurementsResponse: MeasurementListResolver,
      shops: ShopListResolver,
    },
  },
  {
    path: 'upload',
    component: MeasurementUploadComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
