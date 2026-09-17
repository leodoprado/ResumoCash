import { Routes } from '@angular/router';

import { Landing } from '../app/pages/landing/landing';
import { Dashboard } from './pages/dashboard/dashboard';
import { Categories } from './pages/categories/categories';
import { Transactions } from './pages/transactions/transactions';
import { Profile } from './pages/profile/profile';

export const routes: Routes = [
  {
    path: '',
    component: Landing,
    title: 'ResumoCash'
  },
  {
    path: 'dashboard',
    component: Dashboard,
    title: 'Dashboard | ResumoCash'
  },
  {
    path: 'categories',
    component: Categories,
    title: 'Categorias | ResumoCash'
  },
  {
    path: 'transactions',
    component: Transactions,
    title: 'Transações | ResumoCash'
  },
  {
    path: 'profile',
    component: Profile,
    title: 'Perfil | ResumoCash'
  },
  {
    path: '**',
    redirectTo: ''
  }
];