import { ImportListComponent } from './components/import-list/import-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ImportComponent } from './components/import/import.component';

const routes: Routes = [
  { 
    path: '',
    component: ImportListComponent
  },
  {
    path: 'importacao/:id',
    component: ImportComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
