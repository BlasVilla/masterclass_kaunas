import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';

import {ReadOnlyCorsSingleServiceBase} from "../ReadOnlyCorsSingleService";
import {IConfigurationRepository, ConfigurationRepository} from "../configuration/ConfigurationRepository";
import {IResult, Result} from "./Result";

export class ResultService
  extends ReadOnlyCorsSingleServiceBase<IResult> {
  private readonly _configurationRepository: IConfigurationRepository;

  constructor(requestId: string, http: Http, configurationRepository: ConfigurationRepository) {
    super(http, `/api/requests/${requestId}/result/`);
    this._configurationRepository = configurationRepository;
  }

  protected getServerUrl(): Observable<string> {
    return this._configurationRepository.serviceUrls$.map(s => s.resultsUrl);
  }

  protected mapItem(data: any): IResult {
    return new Result(data);
  }
}
