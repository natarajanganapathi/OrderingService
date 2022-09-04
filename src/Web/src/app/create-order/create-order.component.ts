import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { OrdersService } from '../service/orders-service';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {
  public mode: string;
  private id: number;
  public orderForm = this.formBuilder.group({ id: 0, accountId: 0, catalogId: 0, quantity: 0 });
  constructor(private activatedRoute: ActivatedRoute,
    private service: OrdersService,
    private router: Router,
    private formBuilder: FormBuilder,) { }
  ngOnInit() {
    this.activatedRoute.params
      .subscribe(params => {
        this.mode = params.mode;
        this.id = parseInt(params.id);
        if (this.id) {
          this.service.getById(this.id)
            .subscribe((res: any) => {
              this.orderForm.patchValue(res);
            });
        }
      });
  }
  onSubmit() {
    var api = (this.mode == 'edit')
      ? this.service.update(this.id, this.orderForm.value)
      : this.service.create(this.orderForm.value);

    api.subscribe(() => {
      this.orderForm.reset();
      this.router.navigate(['/orders'])
        .then(() => {
          window.location.reload();
        });
    });
  }
}
