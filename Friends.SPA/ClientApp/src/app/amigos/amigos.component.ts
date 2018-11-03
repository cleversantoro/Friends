import { Component, Input } from '@angular/core';
import { Contato, ContatoList } from '../amigos/amigos.model'
import { AmigosService } from '../amigos/amigos.service'

@Component({
  selector: 'app-amigos',
  templateUrl: './amigos.component.html'
})
export class AmigosComponent {
  contatos: ContatoList[];
  closeContacts: Contato[] = [];
  optionselected: any;

  constructor(private amigosService: AmigosService, ) { }

  ngOnInit() {
    this.amigosService.GetContatos().subscribe(contatos => (this.contatos = contatos));
  }

  getOption(id) {
    this.optionselected = id;
  }

  getCloseContacts() {
    this.amigosService.GetCloseContatos(this.optionselected).subscribe(closecontatos => (this.closeContacts = closecontatos));
  }

}
