import { ImportError } from './../dtos/importError';
import {Component, Inject} from '@angular/core';
import {MatDialog, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-error-modal',
  templateUrl: './error-modal.component.html',
  styleUrls: ['./error-modal.component.css']
})
export class ErrorModalComponent {

  constructor(@Inject(MAT_DIALOG_DATA) public data: ImportError[]) { }

}
