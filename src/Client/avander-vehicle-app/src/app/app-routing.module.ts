import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  MeasurementListResolver,
  MeasurementListComponent,
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
