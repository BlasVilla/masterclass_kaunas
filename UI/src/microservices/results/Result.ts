export interface IResult {
  readonly requestId: string;
  readonly method: string;
  readonly value: number;
  readonly created: Date;
}

export class Result
  implements IResult {
  readonly requestId: string;
  readonly method: string;
  readonly value: number;
  readonly created: Date;

  constructor(data: {requestId: string, method: string, value: number, created: any}) {
    this.requestId = data.requestId;
    this.method = data.method;
    this.value = data.value;
    this.created = new Date(data.created);
  }
}
