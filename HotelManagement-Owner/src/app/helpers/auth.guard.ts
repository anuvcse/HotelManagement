import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import {AuthenticationService} from '../services/authentication.service'

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
   currentUser:User; 
  constructor(private router: Router,private authenticationService: AuthenticationService) { }
  canActivate( next: ActivatedRouteSnapshot, state: RouterStateSnapshot):boolean {
    this.authenticationService.currentUser.subscribe(res=>this.currentUser=res);
    if (this.currentUser) {
        return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
 

}
