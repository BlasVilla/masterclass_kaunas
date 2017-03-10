import {Injectable} from '@angular/core';
import {Http} from '@angular/http';

import {IServiceUrls} from './ServiceUrls';
import {ReadOnlySingleServiceBase, IReadOnlySingleService} from '../ReadOnlySingleService';

@Injectable()
export class ConfigurationService
  extends ReadOnlySingleServiceBase<IServiceUrls>
  implements IReadOnlySingleService<IServiceUrls> {

  constructor(http: Http) {
    super(http, 'services.json');
  }
}
