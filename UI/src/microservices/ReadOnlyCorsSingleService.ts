import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/mergeMap';

import {ReadOnlySingleServiceBase} from './ReadOnlySingleService';

export abstract class ReadOnlyCorsSingleServiceBase<TOut>
    extends ReadOnlySingleServiceBase<TOut> {

    constructor(http: Http, baseUrl: string) {
        super(http, baseUrl);
    }
    
    public getItem(): Observable<TOut> {
        return this.waitForConfiguration(hostUrl => this.getItemInternal(hostUrl));
    }

    private waitForConfiguration<T>(action: (hostUrl: string) => Observable<T>): Observable<T> {
        return this.getServerUrl().flatMap(serverUrl => action(serverUrl));
    }

    protected abstract getServerUrl(): Observable<string>;
}
