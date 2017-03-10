import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/publishReplay';
import {ConfigurationService} from "./ConfigurationService";
import {IServiceUrls} from "./ServiceUrls";

export interface IConfigurationRepository {
  serviceUrls$: Observable<IServiceUrls>;
}

@Injectable()
export class ConfigurationRepository
  implements IConfigurationRepository {
  public readonly serviceUrls$: Observable<IServiceUrls>;

  constructor(configurationService: ConfigurationService) {
    this.serviceUrls$ = configurationService.getItem().publishReplay(1).refCount();
  }
}
