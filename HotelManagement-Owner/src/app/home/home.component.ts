import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OwnerService } from '../services/owner.service';
import { ColDef } from 'ag-grid-community';
import {NgbDateStruct, NgbCalendar} from '@ng-bootstrap/ng-bootstrap';
import { Status } from '../models/status';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  statusForm: FormGroup;
  submitted = false;
  returnUrl: string;
  error = '';
  list:Status[]
  

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private OwnerService: OwnerService
  ) {
      }
      columnDefs: ColDef[] = [
        { field: 'roomNo' },
        { field: 'roomType' },
        { field: 'status' }
    ];
  
  ngOnInit() {
    this.statusForm = this.formBuilder.group({
      date: ['', Validators.required]
  });
  
  }
  get f() { return this.statusForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.statusForm.invalid) {
        return;
    }
   
    let myDate =this.f.date.value.month+"-" +this.f.date.value.day+"-"+this.f.date.value.year;
    console.log(myDate);
    this.OwnerService.display(myDate).subscribe(res=>this.list=res)
  }
  AddRooms()
  {
    this.router.navigateByUrl("/rooms")
  }
}
