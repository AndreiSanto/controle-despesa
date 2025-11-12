import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../environment/environment';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

interface AuthResponse {
  token: string;
  refreshToken: string;
}

interface JwtPayload {
  sid: string;       // ClaimTypes.Sid -> identificador
  nameid: string;    // ClaimTypes.NameIdentifier -> idUsuario
  exp: number;       // Expiração
  iat: number;       // Criação
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private httpClient: HttpClient, private router: Router) {}

  // Faz login e armazena tokens no localStorage
  login(email: string, password: string): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(`${this.apiUrl}/login`, { email, password })
      .pipe(
        tap(res => {
          localStorage.setItem('access_token', res.token);
          localStorage.setItem('refresh_token', res.refreshToken);
        })
      );
  }

  // Obtém o access token do localStorage
  getAccessToken(): string | null {
    return localStorage.getItem('access_token');
  }

  // Obtém o refresh token do localStorage
  getRefreshToken(): string | null {
    return localStorage.getItem('refresh_token');
  }

  // Decodifica o JWT para pegar os claims
  getDecodedToken(): JwtPayload | null {
    const token = this.getAccessToken();
    if (!token) return null;

    try {
      return jwtDecode<JwtPayload>(token);
    } catch (error) {
      console.error('Erro ao decodificar token:', error);
      return null;
    }
  }

  // Retorna o ID do usuário (claim nameid)
  getUsuarioId(): string | null {
    const decoded = this.getDecodedToken();
    return decoded ? decoded.nameid : null;
  }

  // Retorna o identificador (claim sid)
  getIdentificador(): string | null {
    const decoded = this.getDecodedToken();
    return decoded ? decoded.sid : null;
  }

  // Logout: limpa tokens e redireciona para tela de login
  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    this.router.navigate(['/login']);
  }
}
