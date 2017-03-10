import {Http} from '@angular/http';
import {Injectable} from "@angular/core";

import {ResultService} from "./ResultService";
import {ConfigurationRepository, IConfigurationRepository} from "../configuration/ConfigurationRepository";

export interface IResultServiceFactory {
  create(requestId: string): ResultService;
}

@Injectable()
export class ResultServiceFactory
  implements IResultServiceFactory {
  private readonly _http: Http;
  private readonly _configurationRepository: IConfigurationRepository;

  constructor(http: Http, configurationRepository: ConfigurationRepository) {
    this._http = http;
    this._configurationRepository = configurationRepository;
  }

  public create(requestId: string): ResultService {
    return new ResultService(requestId, this._http, this._configurationRepository);
  }
}
