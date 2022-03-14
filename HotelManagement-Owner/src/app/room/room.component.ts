import { Component, OnInit } from '@angular/core';

import { OwnerService } from '../services/owner.service';


@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})

export class RoomComponent implements OnInit {
  status:Number;
  roomType:string;
  submitted = false;
  constructor( private OwnerService: OwnerService) {
    this.status=0;
   }

  ngOnInit() {
  
  }
  onSubmit()
  {
    this.roomType=(<HTMLSelectElement>document.getElementById('roomType')).value;
    console.log(this.roomType);
   this.OwnerService.AddRooms(this.roomType).subscribe(res=>
  {
    console.log(res);
    this.status=res;
  });
  }
  
}
