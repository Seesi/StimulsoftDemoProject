import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as moment from 'moment';
import { Observable } from 'rxjs';
import { ReportDefinitionDto, ReportsServiceProxy } from 'src/shared/service-proxies/service-proxies';
declare var Stimulsoft: any;
declare var StiOptions: any;

@Component({
  selector: 'app-report-designer',
  templateUrl: './report-designer.component.html',
  styleUrls: ['./report-designer.component.css']
})
export class ReportDesignerComponent implements OnInit {

   //#region Properties
   reportId: number | undefined = 0;
   categoryId: number | undefined = 0;
   options: any;
   designer: any;   

   url = "https://manduureportserver.azurewebsites.net/"; 
   //#endregion

   //#region Constructor
   constructor(
       private router: Router,
       private route: ActivatedRoute,
       private reportService: ReportsServiceProxy      
   ) {}
   //#endregion Constructor

   //#region Methods
   ngOnInit() {
       const id = this.route.snapshot.paramMap.get("id");
       this.reportId = id ? parseInt(id) : undefined;      
       this.setupReportDesigner(this.reportId);
   }

   setupReportDesigner = (id: number) => {
       // Sti Server Setting
       StiOptions.WebServer.url = this.url;
       StiOptions.WebServer.timeout = 300;    
       
       // Designer Options set up
       this.options = new Stimulsoft.Designer.StiDesignerOptions();
       this.options.appearance.fullScreenMode = false;
       this.options.appearance.backgroundColor = false;
       this.options.toolbar.showFileMenuExit = true;
       this.designer = new Stimulsoft.Designer.StiDesigner(
           this.options,
           "StiDesigner",
           false
       );

       // Report set up
       this.designer.report = new Stimulsoft.Report.StiReport();
       this.designer.onSaveReport = this.onSaveReport;
       this.designer.onSaveAsReport = this.onSaveAsReport;
       this.designer.onExit = this.onExitDesigner;

       console.log(this.reportId, "my id");
       if (id === 0 || id == undefined) {
           this.designer.renderHtml("designer");
       } else {
           this.reportService.getReport(id).subscribe(
               (reportDefinitionJsonString) => {
                   this.designer.report = new Stimulsoft.Report.StiReport();
                   this.designer.report.load(
                       reportDefinitionJsonString.definition
                   );
               },
               (error) => {
                   console.log(
                       `Error occured while loading report: ${error.message}`
                   );
               },
               () => {
                   this.designer.renderHtml("designer");
               }
           );
       }
   };

   onSaveReport = (event) => {
       const jsonStr = event.report.saveToJsonString();
       const def: ReportDefinitionDto = new ReportDefinitionDto();
       def.id = this.reportId;
       def.definition = jsonStr;
       def.name = event.fileName;
       def.reportIdent = event.report._reportGuid;              
       this.saveReportDefinition(def).subscribe(
           (id) => {      
               alert("Saved Successfully");
               this.navigateToManagePage();
           },
           (err) => {}
       );
   };

   onSaveAsReport = (event) => {
       event.preventDefault = true;
       const jsonStr = event.report.saveToJsonString();
       const def: ReportDefinitionDto = new ReportDefinitionDto();
       def.definition = jsonStr;
       def.name = event.fileName;
       def.reportIdent = event.report._reportGuid;              
       this.saveReportDefinition(def).subscribe(() => {           
           alert("Saved Successfully");
           this.navigateToManagePage();
       });
   };

   onExitDesigner = (event) => {
       this.navigateToManagePage();
   };

   private navigateToManagePage() {
       this.router.navigateByUrl("/home");
   }

   private saveReportDefinition(def: ReportDefinitionDto): Observable<void> {            
      return this.reportService.post(def);
   }  
   //#endregion Methods

}
