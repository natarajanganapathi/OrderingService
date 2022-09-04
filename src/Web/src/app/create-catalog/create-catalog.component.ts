import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { CatalogService } from '../service/catalog-service';

@Component({
  selector: 'app-create-catalog',
  templateUrl: './create-catalog.component.html',
  styleUrls: ['./create-catalog.component.css']
})
export class CreateCatalogComponent implements OnInit {
  public mode: string;
  private id: number;
  public catalogForm = this.formBuilder.group({ id: 0, name: '', unitPrice: 0, discount: 0, stock: 0 });
  constructor(private activatedRoute: ActivatedRoute,
    private service: CatalogService,
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
              this.catalogForm.patchValue(res);
            });
        }
      });
  }
  onSubmit() {
    var api = (this.mode == 'edit')
      ? this.service.update(this.id, this.catalogForm.value)
      : this.service.create(this.catalogForm.value);

    api.subscribe(() => {
      this.catalogForm.reset();
      this.router.navigate(['/catalog'])
        .then(() => {
          window.location.reload();
        });
    });
  }
}
