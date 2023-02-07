import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ConfirmEmailInfoComponent } from './components/confirm-email-info/confirm.email.info.component';
import { PublicWrapperComponent } from './components/public-wrapper/public-wrapper.component';
import { LoginModalComponent } from './components/login-modal/login-modal.component';
import { LoginComponent } from './components/login/login.component';
import { ConfirmComponent } from './components/confirm/confirm.component';
import { CommentsComponent } from './components/comments/comments.component';
import { SharedModule } from '../shared/shared.module';


const routes: Routes = [{
  path: '',
  component: PublicWrapperComponent,
  children: [
    { path: 'login', component: LoginComponent, pathMatch: 'full' },
    { path: 'register', component: RegisterComponent, pathMatch: 'full' },
    { path: 'confirm-email-info', component: ConfirmEmailInfoComponent, pathMatch: 'full' },
    { path: '**', redirectTo: 'home' },
  ]
}];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
  ],
  declarations: [
    LoginComponent,
    ConfirmEmailInfoComponent,
    RegisterComponent,
    PublicWrapperComponent,
    ConfirmComponent,
    CommentsComponent,
    LoginModalComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class PublicModule {}
