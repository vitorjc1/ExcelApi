import { Product } from './../models/product';
import { Import } from './../models/import';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GlobalConstants } from '../common/global.constants';
import { ImportResponse } from '../dtos/importResponse';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ImportService {
  private readonly _rootUrl: string = GlobalConstants.baseApiUrl + '/import/';

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  showMessage(msg: string):void
  {
    this.snackBar.open(msg,'X',{
      duration: 2500,
      horizontalPosition: 'right',
      verticalPosition: 'top'
    })
  }

  getImports(): Observable<Import[]> {
    return this.http.get<Import[]>(this._rootUrl);
  }

  getImportById(id: string): Observable<Import> {
    return this.http.get<Import>(this._rootUrl + id);
  }

  uploadImport(file: File): Observable<ImportResponse> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post<ImportResponse>(this._rootUrl, formData);
  }
}
