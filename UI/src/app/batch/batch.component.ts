import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import {Router, ActivatedRoute, Params} from '@angular/router';

import {IBatch} from "../../microservices/requests/Batch";
import {IRequest} from "../../microservices/requests/Request";
import {RequestsByBatchReadOnlyService} from "../../microservices/requests/RequestsByBatchReadOnlyService";
import {BatchesReadOnlyService} from "../../microservices/requests/BatchesReadOnlyService";
import {RequestsByBatchServiceFactory} from "../../microservices/requests/RequestsByBatchServiceFactory";

@Component({
  templateUrl: './batch.component.html',
  styleUrls: ['./batch.component.css']
})
export class BatchComponent
  implements OnInit {
  private readonly _route: ActivatedRoute;
  private readonly _router: Router;
  private readonly _batchesService: BatchesReadOnlyService;
  private readonly _requestsByBatchServiceFactory: RequestsByBatchServiceFactory;

  private _batchId;
  public get batchId(): string {
    return this._batchId;
  }

  private _batch: IBatch;
  public get batch():IBatch {
    return this._batch;
  }

  private _requests$: Observable<IRequest[]>;
  public get requests$():Observable<IRequest[]> {
    return this._requests$;
  }

  constructor(batchesService: BatchesReadOnlyService,
              requestsByBatchServiceFactory: RequestsByBatchServiceFactory,
              route: ActivatedRoute,
              router: Router) {
    this._batchesService = batchesService;
    this._requestsByBatchServiceFactory = requestsByBatchServiceFactory;
    this._route = route;
    this._router = router;
  }

  ngOnInit() {
    this._route.params.forEach((params: Params) => {
      this.setBatchId(params['batchId']);
    });
  }

  private setBatchId(batchId: string): void {
    if(batchId) {
      this._batchId = batchId;

      this._batchesService.getItemById(this._batchId).subscribe(batch => this._batch = batch);

      this._requests$ = this._requestsByBatchServiceFactory
        .createReadOnlyService(this._batchId).getAllItems()
        //.do(requests => console.log('Before', requests))
        .map(requests => requests.sort((a, b) => a.index - b.index));
        //.do(requests => console.log('After', requests));
    }
  }
}
