import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IdentityComponent } from './identity/identity.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { NotificationComponent } from './notifications/notification/notification.component';

@NgModule({
  declarations: [
    AppComponent,
    IdentityComponent,
    HeaderComponent,
    NotificationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
