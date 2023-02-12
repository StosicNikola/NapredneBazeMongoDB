import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthPrivateGuard, AuthPublicGuard } from './auth.guard';

const routes: Routes = [
  {
    path: 'private-school',
    loadChildren: () => import('./modules/public/public.module').then(m => m.PublicModule),
    // canLoad: [AuthPublicGuard]
  }, 
  {
    path: '',
    loadChildren: () => import('./modules/private/private.module').then(m => m.PrivateModule),
    // canLoad: [AuthPrivateGuard]
  },
   {
    path: '**',
    redirectTo: '/private-school/login'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
