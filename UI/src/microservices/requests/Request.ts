export interface IRequest {
  readonly requestId: string;
  readonly index: number;
  readonly x: number;
  readonly created: Date;
}

export class Request
  implements IRequest {
  readonly requestId: string;
  readonly index: number;
  readonly x: number;
  readonly created: Date;

  constructor(data: {requestId: string, index: number, x: number, created: any}) {
    this.requestId = data.requestId;
    this.index = data.index;
    this.x = data.x;
    this.created = new Date(data.created);
  }
}
