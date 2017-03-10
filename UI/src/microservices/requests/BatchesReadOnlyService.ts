import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import {Injectable} from "@angular/core";

import {IBatch, Batch} from "./Batch";
import {ReadOnlyCorsServiceBase} from "../ReadOnlyCorsService";
import {ConfigurationRepository, IConfigurationRepository} from "../configuration/ConfigurationRepository";

@Injectable()
export class BatchesReadOnlyService
  extends ReadOnlyCorsServiceBase<IBatch> {
  private readonly _configurationRepository: IConfigurationRepository;

  constructor(http: Http, configurationRepository: ConfigurationRepository) {
    super(http, '/api/batches/');
    this._configurationRepository = configurationRepository;
  }

  protected getServerUrl(): Observable<string> {
    return this._configurationRepository.serviceUrls$.map(s => s.requestsUrl);
  }

  protected mapItem(data: any): IBatch {
    return new Batch(data);
  }
}
