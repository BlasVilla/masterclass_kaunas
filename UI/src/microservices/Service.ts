import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';

import {IReadOnlyService} from './ReadOnlyService';
import {HttpServiceBase} from './HttpService';

export interface IService<TNew, TOut>
  extends IReadOnlyService<TOut> {
  postItem(item: TNew): Observable<Response>;
  putItem<TIn>(id: string, item: TIn): Observable<Response>;
  deleteItem(id: string): Observable<Response>;
}

export abstract class ServiceBase<TNew, TOut>
  extends HttpServiceBase
  implements IService<TNew, TOut> {
  private readonly _readOnlyService: IReadOnlyService<TOut>;

  constructor(http: Http, baseUrl: string, readOnlyService: IReadOnlyService<TOut>){
    super(http, baseUrl);

    this._readOnlyService = readOnlyService;
  }

  public postItem(item: TNew): Observable<Response> {
    return this.postItemInternal(item, "");
  }

  public putItem<TIn>(id: string, item: TIn): Observable<Response> {
    return this.putItemInternal<TIn>(id, item, "");
  }

  public deleteItem(id: string): Observable<Response> {
    return this.deleteItemInternal(id, "");
  }

  protected postItemInternal(item: TNew, hostUrl: string): Observable<Response> {
    let data: string = JSON.stringify(item);

    return this.http.post(hostUrl + this.baseUrl, data, { headers: this.sendJsonHeaders });
  }

  protected putItemInternal<TIn>(id: string, item: TIn, hostUrl: string): Observable<Response> {
    let data: string = JSON.stringify(item);

    return this.http.put(hostUrl + this.baseUrl + id, data, { headers: this.sendJsonHeaders });
  }

  protected deleteItemInternal(id: string, hostUrl: string): Observable<Response> {
    return this.http.delete(hostUrl + this.baseUrl + id, { headers: this.generalHeaders });
  }

  // Read-only methods:

  public getAllItems(extra?: string): Observable<TOut[]> {
    return this._readOnlyService.getAllItems(extra);
  }

  public getItemById(id: string): Observable<TOut> {
    return this._readOnlyService.getItemById(id);
  }

  public getItemsById(ids: string[]): Observable<TOut[]> {
    return this._readOnlyService.getItemsById(ids);
  }
}
