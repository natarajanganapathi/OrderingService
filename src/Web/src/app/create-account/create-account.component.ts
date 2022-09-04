import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AccountService } from '../service/account-service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {
  public mode: string;
  private id: number;
  public accountForm = this.formBuilder.group({ id: 0, accountName: '' });
  constructor(private activatedRoute: ActivatedRoute,
    private service: AccountService,
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
              this.accountForm.patchValue({
                id: res.id,
                accountName: res.accountName
              });
            });
        }
      });
  }
  onSubmit() {
    var api = (this.mode == 'edit')
      ? this.service.update(this.id, this.accountForm.value)
      : this.service.create(this.accountForm.value);

    api.subscribe(() => {
      this.accountForm.reset();
      this.router.navigate(['/account'])
        .then(() => {
          window.location.reload();
        });
    });
  }
}
