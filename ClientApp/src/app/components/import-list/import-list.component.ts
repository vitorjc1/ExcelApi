import { ImportError } from './../../shared/dtos/importError';
import { ErrorModalComponent } from './../../shared/error-modal/error-modal.component';
import { ImportResponse } from './../../shared/dtos/importResponse';
import { ImportService } from './../../shared/services/import.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Import } from './../../shared/models/import';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ITS_JUST_ANGULAR } from '@angular/core/src/r3_symbols';

@Component({
  selector: 'app-import-list',
  templateUrl: './import-list.component.html',
  styleUrls: ['./import-list.component.css']
})
export class ImportListComponent implements OnInit, AfterViewInit {
  importResponse: ImportResponse;
  file: File;
  displayedColumns = ['id', 'date', 'amount', 'closestDate', 'total', 'action'];
  dataSource: any;
  paginator: any;

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
  }

  constructor(private importService: ImportService,
    public dialog: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.onLoadImports();
   }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.setPaginator();
    }, 100);
  }

  onLoadImports() {
    this.importService.getImports().subscribe(res => {
      this.dataSource = new MatTableDataSource<Import>(res);
    });
  }

  onFileChange(event) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.file = fileList.item(0);
      const extension: string = this.file.name.split('.').pop();
      if (extension !== 'xls' && extension !== 'xlsx') {
        this.importService.showMessage('Extensão de arquivo inválida');
        (<HTMLInputElement>document.getElementById('file-input')).value = null;
      }
    }
  }

  onSubmitFile() {
    if (this.file) {
      this.importService.uploadImport(this.file)
        .subscribe(res => {
          if (res.success) {
            this.router.navigate(['/importacao/' + res.importId]);
          }
          else {
            this.importService.showMessage('Erro inesperado, por favor tente novamente');
            (<HTMLInputElement>document.getElementById('file-input')).value = null;
          }
        }, reason => {
          (<HTMLInputElement>document.getElementById('file-input')).value = null;
          this.openDialog(reason.error.errors);
        });
    }
    else {
      this.importService.showMessage('Por favor, selecione uma planilha para ser exportada')
    }
  }

  openDialog(errors: ImportError[]) {
    this.dialog.open(ErrorModalComponent, {
      data: errors
    });
  }

  setPaginator() {
    this.dataSource.paginator = this.paginator;
  }
}