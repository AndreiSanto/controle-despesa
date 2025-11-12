import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, from } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { environment } from '../../environment/environment';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private apiUrl = `${environment.apiUrl}`;
    constructor(private authService: AuthService, private http: HttpClient) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let token = this.authService.getAccessToken();
        const idUsuario = this.authService.getUsuarioId();

        // adiciona o Authorization header
        if (token) {
            req = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`,
                     ...(idUsuario ? { 'codUsuario': idUsuario } : {})

                }
            });
        }

        return next.handle(req).pipe(
            catchError((error: HttpErrorResponse) => {
                // se deu 401, tenta renovar token
                if (error.status === 401) {
                    const refreshToken = this.authService.getRefreshToken();
                    if (refreshToken) {
                        return this.http.post<any>(`${this.apiUrl}/login/refresh`, { refreshToken })
                            .pipe(
                                switchMap(res => {
                                    localStorage.setItem('access_token', res.token);
                                    localStorage.setItem('refresh_token', res.refreshToken);

                                    const cloneReq = req.clone({
                                        setHeaders: { Authorization: `Bearer ${res.token}` }
                                    });
                                    return next.handle(cloneReq);
                                }),
                                catchError(err => {
                                    // Se não conseguir renovar, desloga
                                    this.authService.logout();
                                    return throwError(() => err);
                                })
                            );

                    }
                }
                return throwError(() => error);
            })
        );
    }
}
