import {Http, Headers} from '@angular/http';

export class HttpServiceBase {
    protected readonly http: Http;
    protected readonly getJsonHeaders: Headers;
    protected readonly sendJsonHeaders: Headers;
    protected readonly generalHeaders: Headers;

    protected readonly baseUrl: string;

    constructor(http: Http, baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;

        this.getJsonHeaders = new Headers({'Accept': 'application/json'});

        this.sendJsonHeaders = new Headers({'Accept' : 'application/json', 'Content-Type': 'application/json'});

        this.generalHeaders = new Headers();
    }

    protected logError(error: string): void {
        console.error('[ERROR]: ' + error);
    }
}
