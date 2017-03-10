import { Component, OnInit } from '@angular/core';
import 'rxjs/add/operator/delay';
import 'rxjs/add/operator/take';
import 'rxjs/add/operator/retry';
import 'rxjs/add/operator/retryWhen';

import {IRequest} from "../../microservices/requests/Request";
import {ResultServiceFactory} from "../../microservices/results/ResultServiceFactory";
import {IResult} from "../../microservices/results/Result";

@Component({
  selector: 'request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css'],
  inputs:['request']
})
export class RequestComponent
  implements OnInit {
  private readonly _resultServiceFactory: ResultServiceFactory;

  private _request: IRequest;
  public get request(): IRequest {
    return this._request;
  }
  public set request(value: IRequest) {
    this._request = value;
    this.onRequestSet();
  }

  private _result: IResult;
  public get result(): IResult {
    return this._result;
  }

  private _duration: number;
  public get duration(): number {
    return this._duration;
  }

  constructor(resultServiceFactory: ResultServiceFactory) {
    this._resultServiceFactory = resultServiceFactory;
  }

  ngOnInit() {
  }

  private onRequestSet(): void {
    if(this._request) {
      this._resultServiceFactory.create(this._request.requestId)
        .getItem()
        //.do(request => console.log('(before) Request:', JSON.stringify(request)))
        .retryWhen(errors => errors.delay(5000).take(5))
        //.do(request => console.log('(after) Request:', JSON.stringify(request)))
        .subscribe(result => this.setResult(result), error => console.log(`error found '${error}'`));
    }
  }

  private setResult(result: IResult): void {
    this._result = result;
    this._duration = this._result.created.getTime() - this._request.created.getTime();
  }
}
