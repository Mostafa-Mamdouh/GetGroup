import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from 'src/app/account/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(auth => {
        if (auth) {
          debugger
          if (route.data.roles && route.data.roles.indexOf(auth.role) === -1) {
            // role not authorised so redirect to home page
            this.router.navigate(['/account/login']);
            return false;
        }

        // authorised so return true
        return true;
        }
        this.router.navigate(['account/login'], {queryParams: {returnUrl: state.url}})
      })
    )
  }
}
