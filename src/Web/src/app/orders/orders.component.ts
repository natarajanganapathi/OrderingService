import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../service/orders-service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  public orders = [];
  constructor(private service: OrdersService) { }

  ngOnInit() {
    this.service.getAll()
      .subscribe(response => {
        this.orders = response as any[];
      });
  }

  delete(id: number) {
    this.service.delete(id)
      .subscribe(response => {
        console.log(`Recored deleted. Id= ${id} `);
        window.location.reload();
      });
  }
}
