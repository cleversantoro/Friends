import { Injectable, Input } from '@angular/core'
import { Http } from '@angular/http'

import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch'

import { FilmeSelecionado } from "../filmes.model"
import { Filme } from "./filme.model"

@Injectable()
export class FilmeService {
  filmes: FilmeSelecionado[] = []

  checked(filme: Filme) {
    this.filmes.push(new FilmeSelecionado(filme))
  }

  unchecked(filme: Filme) {
    var index = this.filmes.findIndex(function (o) { return o.Filme.id === filme.id; });
    this.filmes.splice(index, 1);
  }

  clear() {
    this.filmes = [];
  }

  qtde():number {
    return this.filmes.length;
  }
}
