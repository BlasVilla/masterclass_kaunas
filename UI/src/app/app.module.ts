import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { MaterialModule } from '@angular/material';
import { UiRoutingModule } from './app.routing';

import 'hammerjs';

import { AppComponent } from './app.component';
import { SampleComponent } from './sample/sample.component';
import { BatchesComponent } from './batches/batches.component';
import { BatchComponent } from './batch/batch.component';
import { NewBatchComponent } from './new-batch/new-batch.component';

import {ConfigurationService} from "../microservices/configuration/ConfigurationService";
import {BatchesReadOnlyService} from "../microservices/requests/BatchesReadOnlyService";
import {BatchesService} from "../microservices/requests/BatchesService";
import {RequestsReadOnlyService} from "../microservices/requests/RequestsReadOnlyService";
import {RequestsByBatchServiceFactory} from "../microservices/requests/RequestsByBatchServiceFactory";
import {ConfigurationRepository} from "../microservices/configuration/ConfigurationRepository";
import {ResultServiceFactory} from "../microservices/results/ResultServiceFactory";
import { RequestComponent } from './request/request.component';

@NgModule({
  declarations: [
    AppComponent,
    SampleComponent,
    BatchesComponent,
    BatchComponent,
    NewBatchComponent,
    RequestComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MaterialModule,
    UiRoutingModule
  ],
  providers: [
    ConfigurationService,
    ConfigurationRepository,

    BatchesReadOnlyService,
    BatchesService,

    RequestsReadOnlyService,
    RequestsByBatchServiceFactory,

    ResultServiceFactory
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
