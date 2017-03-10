import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import {Injectable} from "@angular/core";

import {IBatch} from "./Batch";
import {INewBatch} from "./NewBatch";
import {CorsServiceBase} from "../CorsService";
import {BatchesReadOnlyService} from "./BatchesReadOnlyService";
import {IConfigurationRepository, ConfigurationRepository} from "../configuration/ConfigurationRepository";

@Injectable()
export class BatchesService
  extends CorsServiceBase<INewBatch, IBatch> {
  private readonly _configurationRepository: IConfigurationRepository;

  constructor(http: Http,
              readOnlyService: BatchesReadOnlyService,
              configurationRepository: ConfigurationRepository) {
    super(http, '/api/batches/', readOnlyService);

    this._configurationRepository = configurationRepository;
  }

  protected getServerUrl(): Observable<string> {
    return this._configurationRepository.serviceUrls$.map(s => s.requestsUrl);
  }
}
