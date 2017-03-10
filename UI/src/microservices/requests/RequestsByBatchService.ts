import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';

import {CorsServiceBase} from "../CorsService";
import {IRequest} from "./Request";
import {INewRequest} from "./NewRequest";
import {RequestsByBatchReadOnlyService} from "./RequestsByBatchReadOnlyService";
import {IConfigurationRepository, ConfigurationRepository} from "../configuration/ConfigurationRepository";

export class RequestsByBatchService
  extends CorsServiceBase<INewRequest, IRequest> {
  private readonly _configurationRepository: IConfigurationRepository;

  constructor(batchId: string, http: Http, readOnlyService: RequestsByBatchReadOnlyService, configurationRepository: ConfigurationRepository) {
    super(http, `/api/batches/${batchId}/requests/`, readOnlyService);

    this._configurationRepository = configurationRepository;
  }

  protected getServerUrl(): Observable<string> {
    return this._configurationRepository.serviceUrls$.map(s => s.requestsUrl);
  }
}
