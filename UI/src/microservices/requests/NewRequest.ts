export interface INewRequest {
  index: number;
  x: number;
}

export class NewRequest
  implements INewRequest {
  index: number;
  x: number;

  constructor(index?: number, x?: number) {
    this.index = index;
    this.x = x;
  }
}
