import { Component, OnInit, Inject } from '@angular/core';

import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import {MatSnackBar} from '@angular/material/snack-bar';

import { Pais } from 'src/app/Interface/pais';
import { Cliente } from 'src/app/Interface/cliente';
import { PaisService } from 'src/app/Services/pais.service';
import { ClienteService } from 'src/app/Services/cliente.service';



@Component({
  selector: 'app-dialog-add-edit',
  templateUrl: './dialog-add-edit.component.html',
  styleUrls: ['./dialog-add-edit.component.css']
})
export class DialogAddEditComponent implements OnInit {
  formCliente:FormGroup;
  tituloAccion:string = "Nuevo";
  botonAccion:string = "Guardar";
  listaPais: Pais[]=[];

  constructor(
    private dialogoReferencia: MatDialogRef<DialogAddEditComponent>,
    private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private _paisServicio: PaisService,
    private _clienteServicio: ClienteService,
    @Inject (MAT_DIALOG_DATA) public dataCliente: Cliente
  ){
    this.formCliente = this.fb.group({
      nombre: ['', Validators.required],
      telefono: ['', Validators.required],
      email: ['', Validators.required],
      codigoPais: ['', Validators.required]
    })

    this._paisServicio.getList().subscribe({
      next:(data) => {
        this.listaPais = data;
      }, error:(e) => {

      }

    })
  }

  mostrarAlerta(msg: string, accion: string) {
    this._snackBar.open(msg, accion, {
      horizontalPosition: "end",
      verticalPosition: "top",
      duration: 3000
    });
  }

  addEditCliente(){
    // console.log(this.formCliente)
     //console.log(this.formCliente.value)

    const modelo: Cliente={
      idCliente: 0,
      nombre: this.formCliente.value.nombre,
      telefono: this.formCliente.value.telefono,
      email: this.formCliente.value.email,
      codigoPais: this.formCliente.value.codigoPais
    }

    if(this.dataCliente == null){
      this._clienteServicio.add(modelo).subscribe({
        next:(data) =>{
          this.mostrarAlerta("Cliente fue creado","Listo");
          this.dialogoReferencia.close("Creado");
        },error:(e)=> {
          this.mostrarAlerta("No se pudo crear","Error");
        }
      })
    }else{
      this._clienteServicio.update(this.dataCliente.idCliente,modelo).subscribe({
        next:(data) =>{
          this.mostrarAlerta("Cliente fue editado","Listo");
          this.dialogoReferencia.close("Editado");
        },error:(e)=> {
          this.mostrarAlerta("No se pudo editar","Error");
        }
      })
    }

    //console.log(modelo);

    
  }

  ngOnInit(): void {
    if(this.dataCliente){
      this.formCliente.patchValue({
        nombre: this.dataCliente.nombre,
        telefono: this.dataCliente.telefono,
        email: this.dataCliente.email,
        codigoPais: this.dataCliente.codigoPais

      })

      this.tituloAccion = "Editar";
      this.botonAccion = "Actualizar";

    }
  }

}
