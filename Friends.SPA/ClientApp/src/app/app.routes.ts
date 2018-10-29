import { Routes } from '@angular/router'

import { HomeComponent } from './home/home.component'
//import { FilmesComponent } from './filmes/filmes.component'

export const ROUTES: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  //{ path: 'filmes', component: FilmesComponent },
  { path: 'about', loadChildren: './about/about.module#AboutModule' },
]
