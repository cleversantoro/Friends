import { Injectable, Input } from '@angular/core'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch'
import { Contato, ContatoList } from "./amigos.model"
import { CONTATOS_API } from '../app.api'
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AmigosService {

  constructor(
    private http: HttpClient,
  ) { }

  GetContatos(): Observable<ContatoList[]> {
    return this.http.get<ContatoList[]>(`${CONTATOS_API}/Contatos/GetList`)
      .map(response => response)
  }

  GetCloseContatos(id: string): Observable<Contato[]> {
    return this.http.get<Contato[]>(`${CONTATOS_API}/Contatos/GetCloseContacts/${id}`)
      .map(response => response)
  }
}
