export interface IBatch {
  readonly batchId: string;
  readonly description: string;
  readonly created: Date;
}

export class Batch implements IBatch {
  readonly batchId: string;
  readonly description: string;
  readonly created: Date;

  constructor(data: { batchId: string, description: string, created: any}) {
    this.batchId = data.batchId;
    this.description = data.description;
    this.created = data.created;
  }
}
