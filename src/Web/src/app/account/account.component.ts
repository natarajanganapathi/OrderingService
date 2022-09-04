import { Component, OnInit } from '@angular/core';
import { AccountService } from '../service/account-service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  public accounts = [];
  constructor(private service: AccountService) { }

  ngOnInit() {
    this.service.getAll()
      .subscribe(response => {
        let data = response;
        this.accounts = data as any[];
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
