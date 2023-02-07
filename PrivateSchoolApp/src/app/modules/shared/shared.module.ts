import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FileUploadModule } from 'ng2-file-upload';
import { RouterModule } from '@angular/router';
import { ApiService } from './services/api.service';
import { AuthService } from './services/auth.service';
import { appRoutingProviders } from '../../auth.guard';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ConfirmComponent } from './components/loading/confirm/confirm.component';

const imports = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
  HttpClientModule,
  FontAwesomeModule,
  NgSelectModule,
  PaginationModule,
  TabsModule,
  NgbModule,
  FileUploadModule,
  RouterModule
];

@NgModule({
  imports,
  exports: [
    imports,
    ConfirmComponent
  ],
  declarations: [
    ConfirmComponent
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<SharedModule> {
    return {
      ngModule: SharedModule,
      providers: [
        appRoutingProviders,
        ApiService,
        AuthService
      ]
    };
  }
}
