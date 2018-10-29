import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Filme } from './filme.model'
import { FilmeSelecionado } from '../filmes.model'
import { FilmeService } from './filme.service'
import { FilmesService } from '../filmes.service';
import swal from 'sweetalert2'

@Component({
  selector: 'app-filme',
  templateUrl: './filme.component.html'
})
export class FilmeComponent implements OnInit {
  @Input() filme: Filme
  @Output() qtde = new EventEmitter();
  qtdSelecionada: number = 0;

  constructor(
    private filmeService: FilmeService,
    private filmesService: FilmesService
  ) { }

  ngOnInit() { }

  filmes(): any {
    return this.filmeService.filmes;
  }

  quantidade(): number {
    return this.filmeService.qtde();
  }

  changeCheck(e: any, filme: Filme) {
    this.qtdSelecionada = this.quantidade();

    if (e.target.checked) {
      if (this.qtdSelecionada >= 8) {
        swal({
          title: 'Alerta!',
          text: 'Você já escolheu seus filmes. Selecione "GERAR MEU CAMPEONATO".',
          type: 'info',
          confirmButtonText: 'OK'
        });        
        e.target.checked = false;
        return false;
      }

      this.filmeService.checked(filme);
      this.qtdSelecionada = this.quantidade()
      this.qtde.emit(this.qtdSelecionada);
      //console.log(this.filmeService.filmes);
      //console.log(this.qtdSelecionada);
    }
    else {
      this.filmeService.unchecked(filme);
      this.qtdSelecionada = this.quantidade()
      this.qtde.emit(this.qtdSelecionada);
      //console.log(this.filmeService.filmes);
      //console.log(this.qtdSelecionada);
    }
  }

  checkDisable(filme: Filme) {
    //if (this.filmesSelecionados.find(f => f == filme))
    //return false;
    //console.log(this.qtdSelecionada);
    //console.log(this.filmesSelecionados);
    //return this.qtdSelecionada >= 8;
    
  }
}
