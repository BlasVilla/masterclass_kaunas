export interface INewResult {
  method: string;
  value: number;
}

export class NewResult
  implements INewResult {
  method: string;
  value: number;

  constructor(method?: string, value?: number) {
    this.method = method;
    this.value = value;
  }
}
