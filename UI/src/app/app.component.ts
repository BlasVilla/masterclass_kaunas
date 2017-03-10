import { Component } from '@angular/core';

export interface IRootViewData {
  name: string;
  description: string;
  icon: string;
  route: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public readonly views: IRootViewData[];

  constructor(){
    this.views = [
      {
        name: 'Batches',
        description: 'List of batches',
        icon: 'view_headline',
        route: 'batches'
      },
      {
        name: 'Sample',
        description: 'Sample Material',
        icon: 'terrain',
        route: 'sample'
      },
    ];
  }
}
