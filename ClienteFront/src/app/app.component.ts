import {AfterViewInit, Component, ViewChild, OnInit, importProvidersFrom} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatDialog, MAT_DIALOG_DATA, MatDialogModule} from '@angular/material/dialog';

import { Cliente } from './Interface/cliente';
import { ClienteService } from './Services/cliente.service';
import { DialogAddEditComponent } from './Dialogs/dialog-add-edit/dialog-add-edit.component';
import { DialogoDeleteComponent } from './Dialogs/dialogo-delete/dialogo-delete.component';
import {MatSnackBar} from '@angular/material/snack-bar';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit, OnInit {
  displayedColumns: string[] = ['Nombre', 'Telefono', 'Email', 'NombrePais', 'Acciones'];
  dataSource = new MatTableDataSource<Cliente>();
  constructor(
    private _clienteService:ClienteService,
    public dialog: MatDialog,
    private _snackBar: MatSnackBar
  ){
    
  }

  ngOnInit(): void {
    this.mostrarClientes();
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  mostrarClientes(){
    this._clienteService.getList().subscribe({
      next:(dataResponse) => {
        this.dataSource.data = dataResponse;
      },error:(e) => {}
    })
  }

  dialogoNuevoCliente() {
    this.dialog.open(DialogAddEditComponent,{
      disableClose:true,
      width: "350px"
    }).afterClosed().subscribe(resultado =>{
      if(resultado === "Creado"){
        this.mostrarClientes();
      }
    })
  }

  dialogoEditarCliente(dataCliente: Cliente) {
    this.dialog.open(DialogAddEditComponent,{
      disableClose:true,
      width: "350px",
      data: dataCliente
    }).afterClosed().subscribe(resultado =>{
      if(resultado === "Editado"){
        this.mostrarClientes();
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

  dialogoEliminarCliente(dataCliente:Cliente){
    this.dialog.open(DialogoDeleteComponent,{
      disableClose:true,
      data: dataCliente
    }).afterClosed().subscribe(resultado =>{
      if(resultado === "Eliminar"){
        this._clienteService.delete(dataCliente.idCliente).subscribe({
          next:(data)=>{
            this.mostrarAlerta("Cliente fue eliminado","Listo");
            this.mostrarClientes();
          },error:(e)=>{
            console.log(e);
          }
        })
      }
    })
  }
}

