import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReportManagerComponent } from './report-manager/report-manager.component';
import { ReportDesignerComponent } from './report-designer/report-designer.component';
import { ReportViewerComponent } from './report-viewer/report-viewer.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HttpClientModule } from '@angular/common/http';
import { ReportsServiceProxy, ServiceProxy } from 'src/shared/service-proxies/service-proxies';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [
    AppComponent,
    ReportManagerComponent,
    ReportDesignerComponent,
    ReportViewerComponent,
    NavBarComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FontAwesomeModule
  ],
  providers: [
    ServiceProxy,
    ReportsServiceProxy
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
