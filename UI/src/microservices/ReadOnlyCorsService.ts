import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/mergeMap';

import {ReadOnlyServiceBase} from './ReadOnlyService';

export abstract class ReadOnlyCorsServiceBase<TOut>
    extends ReadOnlyServiceBase<TOut> {

    constructor(http: Http, baseUrl: string) {
        super(http, baseUrl);
    }

    public getAllItems(extra?: string): Observable<TOut[]> {
        return this.waitForConfiguration(hostUrl => this.getAllItemsInternal(hostUrl, extra));
    }

    public getItemById(id: string): Observable<TOut> {
        return this.waitForConfiguration(hostUrl => this.getItemByIdInternal(id, hostUrl));
    }

    public getItemsById(ids: string[]): Observable<TOut[]> {
        return this.waitForConfiguration(hostUrl => this.getItemsByIdInternal(ids, hostUrl));
    }

    private waitForConfiguration<T>(action: (hostUrl: string) => Observable<T>): Observable<T> {
        return this.getServerUrl().flatMap(serverUrl => action(serverUrl));
    }

    protected abstract getServerUrl(): Observable<string>;
}
