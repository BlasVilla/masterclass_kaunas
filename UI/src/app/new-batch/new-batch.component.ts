import {Component, OnInit, EventEmitter} from '@angular/core';
import {Observable} from 'rxjs/Observable';

import {BatchesService} from "../../microservices/requests/BatchesService";
import {INewBatch, NewBatch} from "../../microservices/requests/NewBatch";
import {RequestsByBatchServiceFactory} from "../../microservices/requests/RequestsByBatchServiceFactory";
import {NewRequest} from "../../microservices/requests/NewRequest";

@Component({
  selector: 'new-batch',
  templateUrl: './new-batch.component.html',
  styleUrls: ['./new-batch.component.css'],
  outputs:["onCreated", "onCanceled"]
})
export class NewBatchComponent
  implements OnInit {
  public readonly onCreated: EventEmitter<any>;
  public readonly onCanceled: EventEmitter<any>;

  private readonly _batchesService: BatchesService;
  private readonly _requestsServiceFactory: RequestsByBatchServiceFactory;

  private _newBatch: INewBatch;
  public get newBatch(): INewBatch {
    return this._newBatch;
  }

  public batchSize: number;

  constructor(batchesService: BatchesService, requestsServiceFactory: RequestsByBatchServiceFactory) {
    this._batchesService = batchesService;
    this._requestsServiceFactory = requestsServiceFactory;

    this.onCreated = new EventEmitter<any>();
    this.onCanceled = new EventEmitter<any>();

    this.initialize();
  }

  ngOnInit() {
  }

  public create(): void {
    this._batchesService.postItem(this.newBatch)
      .map(response => response.json())
      .map(newBatchResponse => newBatchResponse.batchId)
      .do(batchId => console.log(`New Batch. ID: ${batchId}`))
      .map(batchId => this._requestsServiceFactory.createService(batchId))
      .flatMap(service => Observable.from(this.generateIndices(this.batchSize))
        .map(index => new NewRequest(index, this.generateX(index)))
        .flatMap(newRequest => service.postItem(newRequest)))
      .subscribe(_ => {
        this.initialize();
        this.onCreated.emit();
      });
  }

  public cancel(): void {
    this.onCanceled.emit();
  }

  private generateIndices(count: number): number[] {
    let result: number[] = [];

    for(let i=0; i<count;i++) {
      result.push(i);
    }

    return result;
  }

  private generateX(index: number): number{
    return index;
  }

  private initialize(): void {
    this._newBatch = new NewBatch();
    this.batchSize = null;
  }
}
