import { Component, OnInit, Inject } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

import { Cliente } from 'src/app/Interface/cliente';

@Component({
  selector: 'app-dialogo-delete',
  templateUrl: './dialogo-delete.component.html',
  styleUrls: ['./dialogo-delete.component.css']
})
export class DialogoDeleteComponent implements OnInit{
  constructor(
    private dialogoReferencia: MatDialogRef<DialogoDeleteComponent>,
    @Inject (MAT_DIALOG_DATA) public dataCliente: Cliente
  ){
    
  }

  ngOnInit(): void {
    
  }

  confirmar_Eliminar(){
    if(this.dataCliente){
      this.dialogoReferencia.close("Eliminar")
    }
  }

}
