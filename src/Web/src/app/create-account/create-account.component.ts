import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
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
  public accountForm = this.formBuilder.group({ accountName: '' });
  constructor(private activatedRoute: ActivatedRoute, 
    private service: AccountService, 
    private router: Router,
    private formBuilder: FormBuilder,) { }
  ngOnInit() {
    this.activatedRoute.params
    .subscribe(params => {
      this.mode = params.mode;
      this.id = params.id;
      if(id){
        this.service.getById(this.id)
        .subscribe((data)=>{
          this.accountForm.patchValue({
            accountName: data.accountName
          });
        });
      }
    }); 
  }
  onSubmit() {
    var api = (this.mode == 'edit') 
      ? this.service.patch(this.accountForm.value)
      : this.service.post(this.accountForm.value);

    api.subscribe(()=> 
      this.accountForm.reset(); 
      this.router.navigate(['/account']);
    );
  }
}
