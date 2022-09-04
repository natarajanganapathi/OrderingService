import { Component, OnInit } from '@angular/core';
import { SummaryService } from '../service/summary-service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public summaries = [];
  constructor(private service: SummaryService) { }

  ngOnInit() {
    this.service.get()
      .subscribe(response => {
        this.summaries = response as any;
      })
  }
}
