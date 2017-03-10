import {Observable} from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';

import {IBatch} from "../../microservices/requests/Batch";
import {BatchesService} from "../../microservices/requests/BatchesService";

@Component({
  templateUrl: './batches.component.html',
  styleUrls: ['./batches.component.css']
})
export class BatchesComponent
  implements OnInit {
  private readonly _service: BatchesService;

  public readonly batches$: Observable<IBatch[]>;

  private _showNewDialog;
  public get showNewDialog(): boolean {
    return this._showNewDialog;
  }

  constructor(service: BatchesService) {
    this._service = service;

    this.batches$ = this._service.getAllItems();
    this.hideDialog();
  }

  ngOnInit() {
  }

  public delete(batchId: string): void {
    this._service.deleteItem(batchId).subscribe();
  }

  public showDialog(): void {
    this._showNewDialog = true;
  }

  public hideDialog(): void {
    this._showNewDialog = false;
  }
}
