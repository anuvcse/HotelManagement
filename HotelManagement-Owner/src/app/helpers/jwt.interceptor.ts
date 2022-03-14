import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

import { AuthenticationService } from '../services/authentication.service';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    currentUser:User;
    constructor(private authenticationService: AuthenticationService, private router:Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
       this.authenticationService.currentUser.subscribe(res=>this.currentUser=res);
        if (this.currentUser && this.currentUser.token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.currentUser.token}`
                }
            });
        }
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                this.authenticationService.logout();
            }
       
            const error = err.error.message || err.statusText;
            return throwError(error);
        }))
    
}
}