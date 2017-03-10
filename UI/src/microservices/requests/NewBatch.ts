export interface INewBatch {
  description: string;
}

export class NewBatch
  implements INewBatch {
  description: string;

  constructor(description?: string) {
    this.description = description;
  }
}
