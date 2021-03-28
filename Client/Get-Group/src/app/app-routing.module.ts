import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from './core/guards/auth.guard';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { Role } from './shared/models/role';

const routes: Routes = [
  { path: '', component: AppComponent, data: { breadcrumb: 'Home' } },
  { path: 'server-error', component: ServerErrorComponent, data: { breadcrumb: 'Server Error' } },
  { path: 'not-found', component: NotFoundComponent, data: { breadcrumb: 'Not found' } },
  {
    path: 'admin', loadChildren: () => import('./admin/admin.module').then(mod => mod.AdminModule),
     canActivate: [AuthGuard],
     data: { breadcrumb: 'Admin' ,roles: [Role.Admin]}
  },
  {
    path: 'request', loadChildren: () => import('./request/request.module').then(mod => mod.RequestModule),
     canActivate: [AuthGuard],
     data: { breadcrumb: 'Request' ,roles: [Role.User] }
  },
  {
    path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule),
    data: { breadcrumb: {skip: true} }
  },
   { path: '**', redirectTo: 'not-found', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }