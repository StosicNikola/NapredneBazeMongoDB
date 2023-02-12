import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './modules/shared/shared.module';
import { CommonModule } from '@angular/common';
import { NotifierModule, NotifierOptions } from 'angular-notifier';
import { ModalModule } from 'ngx-bootstrap/modal';



const customNotifierOptions: NotifierOptions = {
  position: {
    horizontal: { position: 'right', distance: 12, },
    vertical: { position: 'top', distance: 25, }
  },
  theme: 'material',
  behaviour: { autoHide: 4500, onClick: 'hide', onMouseover: false, showDismissButton: true, stacking: 1 },
  animations: {
    enabled: true,
    show: { preset: 'slide', speed: 300, easing: 'ease'},
    hide: { preset: 'fade', speed: 300, easing: 'ease', offset: 50 },
    shift: { speed: 300, easing: 'ease' },
    overlap: 150
  }
};

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    NotifierModule.withConfig(customNotifierOptions),
    SharedModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
