import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Status } from '../models/status';

@Injectable({
  providedIn: 'root'
})
export class OwnerService {
  private baseURL:string;
  constructor(private _http: HttpClient) { 
    this.baseURL="https://localhost:5001/api/owner"
  }
  display(date):Observable<Status[]>
  {   
    console.log(date);
    return this._http.get<Status[]>(this.baseURL+"/DisplayRooms/"+date)
  }
  AddRooms(roomType):any
  {
    let room={roomno:0,roomType:roomType};
    return this._http.post(this.baseURL+"/AddRooms",room);
  }
}
