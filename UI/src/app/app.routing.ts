import {Routes, RouterModule} from '@angular/router';
import {NgModule} from '@angular/core';

import {SampleComponent} from './sample/sample.component';
import {BatchComponent} from './batch/batch.component';
import {BatchesComponent} from "./batches/batches.component";

const routes: Routes = [
  { path: '', redirectTo: '/batches', pathMatch: 'full' },
  { path: 'sample', component: SampleComponent },
  { path: 'batches', component: BatchesComponent },
  { path: 'batches/:batchId', component: BatchComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class UiRoutingModule { }
