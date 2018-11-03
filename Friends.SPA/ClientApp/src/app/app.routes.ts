import { Routes } from '@angular/router'

import { HomeComponent } from './home/home.component'
import { AmigosComponent } from './amigos/amigos.component'
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './login/login.component';

export const ROUTES: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },// pathMatch: 'full' },
  { path: 'amigos', component: AmigosComponent },
  { path: 'login', component: LoginComponent },
  { path: 'about', loadChildren: './about/about.module#AboutModule' },
  { path: '**', redirectTo: '' }
]
