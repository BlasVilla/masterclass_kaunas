import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';

import {ReadOnlyCorsServiceBase} from "../ReadOnlyCorsService";
import {IRequest, Request} from "./Request";
import {IConfigurationRepository, ConfigurationRepository} from "../configuration/ConfigurationRepository";

export class RequestsByBatchReadOnlyService
  extends ReadOnlyCorsServiceBase<IRequest> {
  private readonly _configurationRepository: IConfigurationRepository;

  constructor(batchId: string, http: Http, configurationRepository: ConfigurationRepository) {
    super(http, `/api/batches/${batchId}/requests/`);
    this._configurationRepository = configurationRepository;
  }

  protected getServerUrl(): Observable<string> {
    return this._configurationRepository.serviceUrls$.map(s => s.requestsUrl);
  }

  protected mapItem(data: any): IRequest {
    return new Request(data);
  }
}
