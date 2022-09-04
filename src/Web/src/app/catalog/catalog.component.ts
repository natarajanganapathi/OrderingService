import { Component, OnInit } from '@angular/core';
import { CatalogService } from '../service/catalog-service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  public catalogs = [];
  constructor(private service: CatalogService) { }

  ngOnInit() {
    this.service.getAll()
      .subscribe(response => {
        this.catalogs = response as any[];
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
