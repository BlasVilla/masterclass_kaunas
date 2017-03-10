import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import {Injectable} from "@angular/core";

import {ReadOnlyCorsServiceBase} from "../ReadOnlyCorsService";
import {IRequest, Request} from "./Request";
import {IConfigurationRepository, ConfigurationRepository} from "../configuration/ConfigurationRepository";

@Injectable()
export class RequestsReadOnlyService
  extends ReadOnlyCorsServiceBase<IRequest> {
  private readonly _configurationRepository: IConfigurationRepository;

  constructor(http: Http, configurationRepository: ConfigurationRepository) {
    super(http, '/api/requests/');
    this._configurationRepository = configurationRepository;
  }

  protected getServerUrl(): Observable<string> {
    return this._configurationRepository.serviceUrls$.map(s => s.requestsUrl);
  }

  protected mapItem(data: any): IRequest {
    return new Request(data);
  }
}
