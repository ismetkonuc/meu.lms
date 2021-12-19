import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot()
  ],
  exports: [
    ReactiveFormsModule,
    BsDropdownModule
  ]
})
export class SharedModule { }
