import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ReportDesignerComponent } from './report-designer/report-designer.component';
import { ReportManagerComponent } from './report-manager/report-manager.component';
import { ReportViewerComponent } from './report-viewer/report-viewer.component';

const routes: Routes = [
  { path: 'home', component: ReportManagerComponent },
  { path: '',   redirectTo: '/home', pathMatch: 'full' },
  { path: 'designer/:id', component: ReportDesignerComponent },
  { path: 'designer', component: ReportDesignerComponent },
  { path: 'viewer/:id', component: ReportViewerComponent },
  { path: 'viewer', component: ReportViewerComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
