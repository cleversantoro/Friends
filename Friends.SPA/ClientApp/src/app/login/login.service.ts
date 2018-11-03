import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { CONTATOS_API } from '../app.api';

@Injectable()
export class LoginService {
  constructor(
    private http: HttpClient
  ) { }

  login(username: string, password: string) {
    return this.http.post<any>(`${CONTATOS_API}/users/authenticate`, { username, password })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
        }
        return user;
      }));
  }

  logout() {
    localStorage.removeItem('currentUser');
  }
}
