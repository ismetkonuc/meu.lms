import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { DxButtonModule, DxListModule, DxTileViewModule } from 'devextreme-angular';



@NgModule({
  declarations: [
    LoginComponent,
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,
    DxListModule,
    DxButtonModule,
    DxTileViewModule
  ]
})
export class AccountModule { }
