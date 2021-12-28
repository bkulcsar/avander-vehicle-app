import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import {
  MeasurementListComponent,
  MeasurementService,
  MeasurementListResolver,
} from './measurements/index';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav/nav-bar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent, NavBarComponent, MeasurementListComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FontAwesomeModule,
    HttpClientModule,
  ],
  providers: [MeasurementService, MeasurementListResolver],
  bootstrap: [AppComponent],
})
export class AppModule {}
