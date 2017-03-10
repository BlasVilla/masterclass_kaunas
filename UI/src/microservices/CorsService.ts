import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/mergeMap';

import {IReadOnlyService} from './ReadOnlyService';
import {ServiceBase} from './Service';

export abstract class CorsServiceBase<TNew, TOut>
  extends ServiceBase<TNew, TOut> {
  constructor(http: Http, baseUrl: string, readOnlyService: IReadOnlyService<TOut>) {
    super(http, baseUrl, readOnlyService);
  }

  public postItem(item: TNew): Observable<Response> {
    return this.waitForConfiguration(hostUrl => this.postItemInternal(item, hostUrl));
  }

  public putItem<TIn>(id: string, item: TIn): Observable<Response> {
    return this.waitForConfiguration(hostUrl => this.putItemInternal<TIn>(id, item, hostUrl));
  }

  public deleteItem(id: string): Observable<Response> {
    return this.waitForConfiguration(hostUrl => this.deleteItemInternal(id, hostUrl));
  }

  private waitForConfiguration<T>(action: (hostUrl: string) => Observable<T>): Observable<T> {
    return this.getServerUrl().flatMap(serverUrl => action(serverUrl));
  }

  protected abstract getServerUrl(): Observable<string>;
}
