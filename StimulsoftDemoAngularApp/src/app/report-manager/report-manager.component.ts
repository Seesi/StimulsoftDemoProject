import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportDefinitionDto, ReportsServiceProxy } from "../../shared/service-proxies/service-proxies";
import { faPencilAlt, faBook, faAddressCard } from "@fortawesome/free-solid-svg-icons";
@Component({
  selector: 'app-report-manager',
  templateUrl: './report-manager.component.html',
  styleUrls: ['./report-manager.component.css']
})
export class ReportManagerComponent implements OnInit {

  reports: ReportDefinitionDto[];
  editIcon = faPencilAlt;
  viewIcon = faBook;
  emailIcon = faAddressCard;

  constructor(
    private router: Router,
    private reportService: ReportsServiceProxy,
    ) { }

  ngOnInit(): void {
    this.reportService.get().subscribe(data=> {
      this.reports = data;
    },(error)=>{
      console.log(error);
    })
  }
  
  editReport(id: number){
    this.router.navigateByUrl(`designer/${id}`);
  }

  viewReport(id: number){
    this.router.navigateByUrl(`viewer/${id}`);
  }

  emailReport(id: number){
    const email = "adoboahseesi@gmail.com";
    this.reportService.sendMail(id,email).subscribe(
      ()=>{
        alert(`Email Sent successfully to ${email}`);
      },
      (error)=>{
      console.log(error);
    });
  }
}
