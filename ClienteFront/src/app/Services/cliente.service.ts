import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import {Observable} from 'rxjs';
import { Cliente } from '../Interface/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  private endpoint:string = environment.endPoint;
  private apiurl:string = this.endpoint + 'cliente/'

  constructor(private http:HttpClient) { }

  getList():Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.apiurl}lista`);
  }

  add(modelo:Cliente):Observable<Cliente>{
    return this.http.post<Cliente>(`${this.apiurl}guardar`, modelo);
  }

  update(idCliente:number, modelo:Cliente):Observable<Cliente>{
    return this.http.put<Cliente>(`${this.apiurl}actualizar/${idCliente}`, modelo);
  }

  delete(idCliente:number):Observable<void>{
    return this.http.delete<void>(`${this.apiurl}eliminar/${idCliente}`);
  }
}
