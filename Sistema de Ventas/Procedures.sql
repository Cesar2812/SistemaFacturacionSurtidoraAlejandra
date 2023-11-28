
use Sistema_SurtidoraAlejandra 
go

--Procedimientos Almacenados---
select * from Rol
delete from Rol
delete from Permiso
delete from Usuario

---Procedimiento de Ingreso de Proveedor---
Create or alter Procedure Ingresar_Proveedor(
@Documento nVarchar(30),
@RazonSocial nVarchar(30),
@Nombre nvarchar(30),
@Correo nvarchar(30),
@Telefono nvarchar(9),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as 
begin
	set @Resultado=0
	Declare @IDPERSONA INT
	if not exists(Select * from Proveedor where Documento=@Documento)
	begin
	insert into Proveedor(Documento,RazonSocial,Nombre,Correo,Telefono,Estado)values(@Documento,@RazonSocial,@Nombre,@Correo,@Telefono,@Estado)
	set @Resultado=SCOPE_IDENTITY()
	end
	else
	set @Mensaje='El Numero de Documento ya existe'
end
go


----Procedimiento Para Editar Proveedor---
Create or alter Procedure Editar_Proveedor(
@id_Proveedor int,
@Documento nVarchar(30),
@RazonSocial nVarchar(30),
@Nombre nvarchar(30),
@Correo nvarchar(30),
@Telefono nvarchar(9),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin 
	set @Resultado=1
	declare @IDPERSONA INT
	if not exists(Select * from Proveedor where Documento=@Documento and id_Proveedor != @id_Proveedor)
	begin 
	update Proveedor set
	Documento=@Documento,
	RazonSocial=@RazonSocial,
	Nombre=@Nombre,
	Correo=@Correo,
	Telefono=@Telefono,
	Estado=@Estado
	where id_Proveedor=@id_Proveedor
	end
	else
	begin
	set @Resultado=0
	set @Mensaje='El numero de Documento ya existe'
	end
end
go

---Procedimeinto Para Eliminar Proveedor--
Create Procedure Eliminar_Proveedor(
@id_Proveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
set @Resultado=1
if not exists(select * from Proveedor
inner join Compra on Proveedor.id_Proveedor=Compra.id_Proveedor
where Proveedor.id_Proveedor=@id_Proveedor)
begin
delete top(1) from Proveedor where id_Proveedor=@id_Proveedor
end
else
begin
set @Resultado=0
set @Mensaje='El Proveedor esta relacionado asociado a una compra'
end
end
go

--Inserciones para acessos--
--Los roles del sistema--
insert into Rol(Descripcion)values('Administrador')
insert into Rol(Descripcion)values('Bodeguero')
insert into Rol(Descripcion)values('Facturador')
select * from  Rol
delete from Rol
use Sistema_SurtidoraAlejandra 
go
select * from Permiso
---Permisos del sistema---
---Rol de Administrador---
insert into Permiso(id_rol,NombreMenu)values(10,'mantenedorToolStripMenuItem'),(10,'proveedoresToolStripMenuItem'),(10,'clientesToolStripMenuItem'),(10,'comprasToolStripMenuItem'),
(10,'ventasToolStripMenuItem'),(10,'reportesToolStripMenuItem'),(10,'usuariosToolStripMenuItem')
---Rol de Facturador---
insert into Permiso(id_rol,NombreMenu)values(12,'clientesToolStripMenuItem'),(12,'ventasToolStripMenuItem'),(12,'reportesToolStripMenuItem')
--Rol de Bodeguero--
insert into Permiso(id_rol,NombreMenu)values(11,'mantenedorToolStripMenuItem'),(11,'proveedoresToolStripMenuItem'),(11,'comprasToolStripMenuItem')
go


delete from Permiso
where id_rol=12
and NombreMenu='reportesToolStripMenuItem'

go
---Procedimiento para Ingresar Usuarios--
create or alter  procedure Insertar_Usuarios(
@Nombre1 nvarchar(max),
@Nombre2 nvarchar(max),
@Apellido1 nvarchar(max),
@Apellido2 nvarchar(max),
@Usuario nvarchar(max),
@Correo nvarchar(max),
@clave nvarchar(max),
@id_Rol int,
@Estado bit,
@idUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin
  set @idUsuarioResultado=0
  set @Mensaje=''
  if not exists(select * from Usuario where Correo=@Correo)
  begin
   insert into Usuario(Nombre1,Nombre2,Apellido1,Apellido2,usuario,Correo,Clave,id_rol,Estado)
   values(@Nombre1,@Nombre2,@Apellido1,@Apellido2,@Usuario,@Correo,@clave,@id_Rol,@Estado)

   set @idUsuarioResultado=SCOPE_IDENTITY()
    end
  else 
     set @Mensaje='No se puede repetir el mismo correo para mas de un Usuario'

end
go

--Procedmiento para Editar Usuario---
create or alter procedure Editar_Usuarios(
@idUsuario int,
@Nombre1 nvarchar(max),
@Nombre2 nvarchar(max),
@Apellido1 nvarchar(max),
@Apellido2 nvarchar(max),
@Usuario varchar(max),
@Correo nvarchar(max),
@Clave varchar(max),
@id_Rol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
  set @Respuesta=0
  set @Mensaje=''
  if not exists(select * from Usuario where @Correo=@Correo and id_Usuario!=@idUsuario)
  begin
      update Usuario set
	  Nombre1=@Nombre1,
	  Nombre2=@Nombre2,
	  Apellido1=@Apellido1,
	  Apellido2=@Apellido2,
	  usuario=@Usuario,
	  clave=@Clave,
	  id_Rol=@id_Rol,
	  Estado=@Estado
	  where id_Usuario=@idUsuario
   set @Respuesta=1
    end
  else 
     set @Mensaje='No se puede repetir el mismo Correo para mas de un Usuario'

end
go
--Procedmiento para Eliminar Usuario---
create procedure Eliminar_Usuarios(
@idUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
  set @Respuesta=0
  set @Mensaje=''
  declare @pasoreglas bit=1
   if exists(select * from Compra c 
     inner join Usuario u on u.id_Usuario=c.id_usuario
	 where u.id_Usuario=@idUsuario)
	 begin 
	 set @pasoreglas=0
	 set @Respuesta=0
	 set @Mensaje=@Mensaje+'No se puede eliminar el Usuario porque esta asociado a una compra\n'
	  end
	  if exists(Select * from Venta v
	  inner join Usuario u on u.id_Usuario=v.id_usuario
	  where u.id_Usuario=@idUsuario)
	  begin
	  set @pasoreglas=0
	  set @Respuesta=0
	  set @Mensaje=@Mensaje+'No se puede eliminar el Usuario porque esta asociado a una venta\n'
	  end
	  if(@pasoreglas=1)
  begin
      delete from Usuario where id_Usuario=@idUsuario
	  set @Respuesta=1
  
    end
end

select * from Usuario
select * from Rol
select * from Permiso

delete from Usuario
go

alter table Usuario
alter column Correo nvarchar(max);

alter table Producto
alter column Descripcion nvarchar(max)

select p.id_rol,p.NombreMenu from Permiso p
inner join Rol r on r.id_Rol=p.id_rol
inner join Usuario u on u.id_Rol=r.id_Rol
where u.id_Usuario=7
go
----Procedmiento almacenado para Clientes--

---Procedimiento para registar un cliente
create or alter procedure Registrar_Cliente
@Cedula nvarchar(15),
@Nombre1 nvarchar(max),
@Nombre2 nvarchar(max),
@Apellido1 nvarchar(max),
@Apellido2 nvarchar(max),
@Telefono nvarchar(8),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
as
begin
set @Resultado=0
declare @IDPERSONA int
if not exists(select * from Cliente Where Cedula=@Cedula)
begin
insert into Cliente(Cedula,Nombre1,Nombre2,Apellido1,Apellido2,Telefono,Estado)
values(@Cedula,@Nombre1,@Nombre2,@Apellido1,@Apellido2,@Telefono,@Estado)
set @Resultado=SCOPE_IDENTITY()
end
else 
set @Mensaje='No se Puede Repetir el Numero de Cedula de un Cliente'
end
go


---Procedmiento para editar un cliente----
Create or alter Procedure Editar_Cliente
@idCliente int,
@Cedula nvarchar(15),
@Nombre1 nvarchar(max),
@Nombre2 nvarchar(max),
@Apellido1 nvarchar(max),
@Apellido2 nvarchar(max),
@Telefono nvarchar(8),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
as
begin
set @Resultado=1
declare @IDPERSONA int
if not exists(select * from Cliente where Cedula=@Cedula and id_Cliente!=@idCliente)
begin
update Cliente set
Cedula=@Cedula,
Nombre1=@Nombre1,
Nombre2=@Nombre2,
Apellido1=@Apellido1,
Apellido2=@Apellido2,
Telefono=@Telefono,
Estado=@Estado
where id_Cliente=@idCliente
end
else
begin 
set @Resultado=0
set @Mensaje='El numero de Cedula ya existe'
end
end
go

---Procedmiento para eliminar un cliente----
Create Procedure Eliminar_Cliente(
@idCliente int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
set @Resultado=1
if not exists(select * from Cliente
inner join Venta on Cliente.id_Cliente=Venta.id_cliente
where Cliente.id_Cliente=@idCliente)
begin
delete top(1) from Cliente where id_Cliente=@idCliente
end
else
begin
set @Resultado=0
set @Mensaje='El Cliente esta relacionado asociado a una venta'
end
end
go

----Procedimientos almacenado para categorias--
alter table Categoria
alter column Nombre nvarchar(max)
go

Create or alter Procedure Ingresar_Categoria
@Nombre nvarchar(max),
@Estado bit,
@Resultado int output,
@Mensaje varchar(5005) output
as
begin
 set @Resultado=0
 if not exists(Select * from Categoria where Nombre=@Nombre)
 begin
	insert into Categoria(Nombre,Estado)values(@Nombre,@Estado)
	set @Resultado=SCOPE_IDENTITY()
	end
	else
	set @Mensaje='No se puede repetir el nombre de la categoria'
	end
go

Create or alter procedure Editar_Categoria
@idCategoria int,
@Nombre nvarchar(max),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
as
begin
set @Resultado=1
if not exists(Select * from Categoria where Nombre=@Nombre and id_Categoria!=@idCategoria)
update Categoria set
Nombre=@Nombre,
Estado=@Estado
where id_Categoria=@idCategoria
else
begin
set @Resultado=0
set @Mensaje='No se puede repetir el nombre de la categoria'
end
end
go

create Procedure Eliminar_Categoria
@idCategoria int,
@Resultado bit output,
@Mensaje varchar(500)output
as
begin
set
@Resultado=1
if not exists(Select * from Categoria 
inner join Producto on Categoria.id_Categoria=Producto.id_categoria
where Categoria.id_Categoria=@idCategoria)
begin
delete top(1) from Categoria where id_Categoria=@idCategoria
end
else
begin 
set @Resultado=0
set @Mensaje='La categoria esta relacianada a un producto'
end
end
go


select id_Categoria,Nombre,Estado from Categoria


select Marca.id_Marca,Marca.Nombre as Marca,Categoria.Nombre as Categoria,Marca.Estado
from Marca 
inner Join Categoria on Marca.id_categoria=Categoria.id_Categoria


select p.id_Marca,p.Nombre,c.Nombre,p.Estado from Marca p
inner join Categoria c on c.id_Categoria=p.id_categoria


select * from Marca
select * from Categoria

select id_Marca,Nombre,Estado
from Marca
select * from Usuario
select * from Categoria
go

--Procedimientos almacenados para Marca--
Create Procedure Ingresar_Marca
@Nombre nvarchar(20),
@idCateg int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
as
begin
set @Respuesta=0
  set @Mensaje=''
  if not exists(select * from Marca where Nombre=@Nombre)
  begin
   insert into Marca(Nombre,id_categoria,Estado)
   values(@Nombre,@idCateg,@Estado)

   set @Respuesta=SCOPE_IDENTITY()
    end
  else 
     set @Mensaje='No se puede repetir el mismo nombre para una marca'

end
go

Create or alter Procedure Editar_Marca
@idMarca int,
@Nombre nvarchar(max),
@idCateg int,
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
as
begin
set @Resultado=1
if not exists(Select * from Marca where Nombre=@Nombre and id_Marca!=@idMarca)
update Marca set
Nombre=@Nombre,
Estado=@Estado,
id_categoria=@idCateg
where id_Marca=@idMarca
else
begin
set @Resultado=0
set @Mensaje='No se puede repetir el nombre de la marca'
end
end
go

Create  or alter Procedure Eliminar_Marca
@idMarca int,
@Resultado bit output,
@Mensaje varchar(500)output
as
begin
set
@Resultado=1
if not exists(Select * from Marca 
inner join Producto on Marca.id_Marca=Producto.id_marca
where Marca.id_Marca=@idMarca)
begin
delete top(1) from Marca where id_Marca=@idMarca
end
else
begin 
set @Resultado=0
set @Mensaje='La Marca No se puede eliminar porque esta asociada a un producto'
end
end
go

alter table Producto
alter Column Descripcion nvarchar(max)
go
---Procedimeintos alamcenados para productos---
--------------------------------------------
select * from Producto
go
create or alter procedure Ingresar_Productos
@Codigo nvarchar(max),
@NombreProducto nvarchar(max),
@idCategoria int,
@idMarca int,
@Descripcion nvarchar(max),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500)output
as
begin
set @Resultado=0
if not exists(select * from Producto where Codigo=@Codigo)
begin
insert into Producto(Codigo,Nombre_Producto,id_categoria,id_marca,Descripcion,Estado)
values(@Codigo,@NombreProducto,@idCategoria,@idMarca,@Descripcion,@Estado)
set @Resultado=SCOPE_IDENTITY()
end
else
set @Mensaje='Ya existe un producto con el mismo Codigo'
end

go
-------------------------------------------
create or alter procedure Editar_Producto
@idProducto int,
@Codigo nvarchar(max),
@Nombre nvarchar(max),
@idCategoria int,
@idMarca int,
@Descripcion nvarchar(max),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
as
begin
set @Resultado=1
if not exists(Select * from Producto where Codigo=@Codigo and id_Producto!=@idProducto)
update Producto set
Codigo=@Codigo,
Nombre_Producto=@Nombre,
Descripcion=@Descripcion,
id_categoria=@idCategoria,
id_marca=@idMarca
where id_Producto=@idProducto
else
begin
set @Resultado=0
set @Mensaje='Ya existe un producto con el mismo codigo'
end
end
go

----------------------------------------------------
Create Procedure Eliminar_Producto
@idProducto int,
@Respuesta bit output,
@Mensaje varchar(500)output
as
begin
set @Respuesta=0
set @Mensaje=''
declare @pasoreglas bit=1
if exists (Select * from Detalle_Compra dc
inner join Producto p on p.id_Producto=dc.id_producto
where p.id_Producto=@idProducto)
begin
set @pasoreglas=0
set @Respuesta=0
set @Mensaje=@Mensaje+'No se puede eliminar el producto porque esta relacionado a una compra\n'
end
if  exists (Select * from Detalle_Venta dv
inner join Producto p on p.id_Producto=dv.id_producto
where p.id_Producto=@idProducto)
begin 
set @pasoreglas=0
set @Respuesta=0
set @Mensaje=@Mensaje+'No se puede eliminar el prodcuto porque esta asociado a una Venta\n'
end
if(@pasoreglas=1)
begin 
delete from Producto where id_Producto=@idProducto
set @Respuesta=1
end
end
---------------
go
create Procedure Listar_Productos
as
Select id_Producto,Codigo,Nombre_Producto,c.id_Categoria,c.Nombre as Categoria,m.id_Marca,m.Nombre as Marca,p.Descripcion,Stock,PrecioCompra,PrecioVenta,FechaVencimiento,p.Estado
from Producto p
inner join Categoria c on c.id_Categoria=p.id_categoria
inner join Marca m on m.id_Marca=p.id_marca

select * from Categoria
select * from Marca

insert into Producto(Codigo,Nombre_Producto,id_categoria,id_marca,Descripcion,Estado)
values('LCH-120','Leche',5,7,'En Bolsa 1/2 Litro',1)
select * from Producto
go

select * from Usuario

