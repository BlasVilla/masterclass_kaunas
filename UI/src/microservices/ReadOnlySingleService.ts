import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import {HttpServiceBase} from './HttpService';

export interface IReadOnlySingleService<TOut> {
    getItem(): Observable<TOut>;
}

export class ReadOnlySingleServiceBase<TOut>
    extends HttpServiceBase
    implements IReadOnlySingleService<TOut> {

    constructor(http: Http, baseUrl: string) {
        super(http, baseUrl);
    }

    public getItem(): Observable<TOut> {
        return this.getItemInternal("");
    }

    protected getItemInternal(hostUrl: string): Observable<TOut> {
        return this.http.get(hostUrl + this.baseUrl, { headers: this.getJsonHeaders })
            .map(r => this.mapItem(r.json()));
    }

    protected mapItem(data: any): TOut {
        return <TOut>data;
    }
}