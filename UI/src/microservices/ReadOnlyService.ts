import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/reduce';
import 'rxjs/add/observable/from';
import 'rxjs/add/operator/mergeMap';

import {HttpServiceBase} from './HttpService';

export interface IReadOnlyService<TOut> {
    getAllItems(extra?: string): Observable<TOut[]>;
    getItemById(id: string): Observable<TOut>;
    getItemsById(ids: string[]): Observable<TOut[]>;
}

export class ReadOnlyServiceBase<TOut>
    extends HttpServiceBase
    implements IReadOnlyService<TOut> {

    constructor(http: Http, baseUrl: string) {
        super(http, baseUrl);
    }

    /* Public methods */

    public getAllItems(extra?: string): Observable<TOut[]> {
        return this.getAllItemsInternal('', extra);
    }

    public getItemById(id: string): Observable<TOut> {
        return this.getItemByIdInternal(id, '');
    }

    public getItemsById(ids: string[]): Observable<TOut[]> {
        return this.getItemsByIdInternal(ids, '');
    }

    /* Protected methods */

    protected getAllItemsInternal(hostUrl: string, extra?: string): Observable<TOut[]> {
      let url: string = hostUrl + this.baseUrl + (extra || "");

      return this.http.get(url, { headers: this.getJsonHeaders })
          .map(r => this.mapItems(r.json()));
    }

    protected getItemByIdInternal(id: string, hostUrl: string): Observable<TOut> {
        return this.http.get(hostUrl + this.baseUrl + id, { headers: this.getJsonHeaders })
            .map(r => this.mapItem(r.json()));
    }

    protected getItemsByIdInternal(ids: string[], hostUrl: string): Observable<TOut[]> {
        return Observable.from(ids)
            .flatMap(id => this.getItemByIdInternal(id, hostUrl))
            .reduce((array, item) =>  [...array, item], []);
    }

    protected mapItems(data: any[]): TOut[] {
        return data.map(this.mapItem);
    }

    protected mapItem(data: any): TOut {
        return <TOut>data;
    }
}
