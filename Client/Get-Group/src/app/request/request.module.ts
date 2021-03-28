import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestComponent } from './request.component';
import { SharedModule } from '../shared/shared.module';
import { RequestRoutingModule } from './request-routing.module';



@NgModule({
  declarations: [RequestComponent],
  imports: [
    CommonModule,
    SharedModule,
    RequestRoutingModule
  ]
})
export class RequestModule { }
