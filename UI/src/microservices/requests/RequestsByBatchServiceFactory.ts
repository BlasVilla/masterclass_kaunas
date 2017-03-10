import {Injectable} from "@angular/core";
import {Http} from '@angular/http';

import {RequestsByBatchReadOnlyService} from "./RequestsByBatchReadOnlyService";
import {RequestsByBatchService} from "./RequestsByBatchService";
import {IConfigurationRepository, ConfigurationRepository} from "../configuration/ConfigurationRepository";

export interface IRequestsByBatchServiceFactory {
  createReadOnlyService(batchId: string): RequestsByBatchReadOnlyService;
  createService(batchId: string): RequestsByBatchService;
}

@Injectable()
export class RequestsByBatchServiceFactory
  implements IRequestsByBatchServiceFactory {
  private readonly _http: Http;
  private readonly _configurationRepository: IConfigurationRepository;

  constructor(http: Http, configurationRepository: ConfigurationRepository){
    this._http = http;
    this._configurationRepository = configurationRepository;
  }

  public createReadOnlyService(batchId: string): RequestsByBatchReadOnlyService {
    return new RequestsByBatchReadOnlyService(batchId, this._http, this._configurationRepository);
  }

  public createService(batchId: string): RequestsByBatchService {
    let readOnlyService = this.createReadOnlyService(batchId);

    return new RequestsByBatchService(batchId, this._http, readOnlyService, this._configurationRepository);
  }
}
