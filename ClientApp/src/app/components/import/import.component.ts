import { Product } from './../../shared/models/product';
import { Import } from './../../shared/models/import';
import { ImportService } from './../../shared/services/import.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.css']
})
export class ImportComponent implements OnInit, AfterViewInit {
  import: Import;
  dataSource: any;
  paginator: any;

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
  }

  displayedColumns = ['#', 'date', 'name', 'amount', 'unitPrice', 'total']

  constructor(private route: ActivatedRoute,
    private importService: ImportService) { }

  ngOnInit(): void { 
    let importId = this.route.snapshot.paramMap.get('id');
    this.onLoadImport(importId);
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.setPaginator();
    }, 100);
  }

  onLoadImport(importId) {
    this.importService.getImportById(importId).subscribe(res => {
      this.import = res;
      this.dataSource = new MatTableDataSource<Product>(res.products);
      this.setPaginator();

    });
  }

  setPaginator() {
    this.dataSource.paginator = this.paginator;
  }
}
