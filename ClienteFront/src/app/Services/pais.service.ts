import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import {Observable} from 'rxjs';
import { Pais } from '../Interface/pais';

@Injectable({
  providedIn: 'root'
})
export class PaisService {

  private endpoint:string = environment.endPoint;
  private apiurl:string = this.endpoint + 'pais/'

  constructor(private http:HttpClient) { }

  getList():Observable<Pais[]>{
    return this.http.get<Pais[]>(`${this.apiurl}lista`);
  }
}
