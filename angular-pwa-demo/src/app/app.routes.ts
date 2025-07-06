import { Routes } from '@angular/router';
import { UserListComponent } from './features/user/components/user-list/user-list';
import { UserFormComponent } from './features/user/components/user-form/user-form';
import { ProductListComponent } from './features/product/components/product-list/product-list';
import { ProductFormComponent } from './features/product/components/product-form/product-form';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/users',
    pathMatch: 'full'
  },
  {
    path: 'users',
    component: UserListComponent
  },
  {
    path: 'users/new',
    component: UserFormComponent
  },
  {
    path: 'users/edit/:id',
    component: UserFormComponent
  },
  {
    path: 'products',
    component: ProductListComponent
  },
  {
    path: 'products/new',
    component: ProductFormComponent
  },
  {
    path: 'products/edit/:id',
    component: ProductFormComponent
  },
  {
    path: '**',
    redirectTo: '/users'
  }
];
