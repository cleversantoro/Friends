import { Component, Input } from '@angular/core';
import { Filme } from './filme/filme.model'
import { FilmesService } from '../filmes/filmes.service'
import { FilmeService } from './filme/filme.service'
import swal from 'sweetalert2'

import "rxjs/add/operator/switchMap";
import "rxjs/add/operator/do";
import "rxjs/add/operator/debounceTime";
import "rxjs/add/operator/distinctUntilChanged";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/from";

import { FilmeSelecionado } from './filmes.model';
import { Router } from '@angular/router'

@Component({
  selector: 'app-filmes',
  templateUrl: './filmes.component.html'
})
export class FilmesComponent {
  filmes: Filme[];
  @Input() filmesSelecionados: FilmeSelecionado[];
  @Input() qtdSelecionada: number = 0;

  constructor(
    private filmesService: FilmesService,
    private filmeservice: FilmeService,
    private router: Router
  ) { }

  ngOnInit() {
    this.filmesService.filmescopa().subscribe(filmes => (this.filmes = filmes));
    this.filmeservice.clear();
  }

  gerarCampeonato() {
    if (this.qtdSelecionada < 8) {
      swal({
        title: 'Alerta!',
        text: 'Selecione seus 8 Filmes.',
        type: 'info',
        confirmButtonText: 'OK'
      });
      return false;
    }

    this.filmesSelecionados = this.filmeservice.filmes;
    let filmes: Filme[] = [];
    for (var i = 0; i < this.filmesSelecionados.length; i++) {
      filmes.push(this.filmesSelecionados[i].Filme)
    }
    this.filmesService.gerarCampeonato(filmes).subscribe(filmes => (this.filmes = filmes));
  }

  recebeQtde(qtde) {
    this.qtdSelecionada = qtde;
  }

}
