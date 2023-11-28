
Create DataBase Sistema_SurtidoraAlejandra

use Sistema_SurtidoraAlejandra 
go

----TABLA ROL----
Create Table Rol(
id_Rol int identity(1,1) primary key,
Descripcion nvarchar(50),
FechaRegistro datetime default getdate()
)
go

---TABLA PERMISO----
Create Table Permiso(
id_Permiso int identity(1,1) primary key,
id_rol int references Rol(id_Rol),
NombreMenu nvarchar(70),
FechaRegistro datetime default getdate()
)
go

-----TABLA PROVEEDOR----
Create Table Proveedor(
id_Proveedor int identity(1,1) primary key,
Documento nvarchar(50),
RazonSocial nvarchar(50),
Nombre nvarchar(50),
Correo nvarchar(20),
Telefono nvarchar(8),
Estado bit,
FechaRegistro datetime default getdate()
)
go

----TABLA CLIENTE-----
Create Table Cliente(
id_Cliente int identity(1,1) primary key,
Cedula nvarchar(15),
Nombre1 nvarchar(10),
Nombre2 nvarchar(10),
Apellido1 nvarchar(10),
Apellido2 nvarchar(10),
Telefono nvarchar(8),
Estado bit,
FechaRegistro datetime default getdate()
)
go

----TABLA USUARIO----
Create Table Usuario(
id_Usuario int identity(1,1) primary key,
Nombre1 nvarchar(10),
Nombre2 nvarchar(10),
Apellido1 nvarchar(10),
Apellido2 nvarchar(10),
usuario nvarchar(10),
Correo nvarchar(10),
Clave nvarchar(max),
id_rol int references Rol(id_Rol),
Estado bit,
FechaRegistro datetime default getdate()
)
go

----TABLA CATEGORIA----
Create Table Categoria(
id_Categoria int identity(1,1)primary key,
Nombre nvarchar(20),
Estado bit,
FechaRegistro datetime default getdate()
)
go

---TABLA MARCA---
Create Table Marca(
id_Marca int identity(1,1) primary key,
Nombre nvarchar(20),
id_categoria int references Categoria(id_Categoria),
Estado bit,
FechaRegistro datetime default getdate()
)

----TABLA PRODUCTO----
Create Table Producto(
id_Producto int identity(1,1)primary key,
Codigo nvarchar(20),
Nombre_Producto nvarchar(20),
Descripcion nvarchar(20),
PrecioCompra decimal(18,2) default 0,
PrecioVenta decimal(18,2) default 0,
id_categoria int references Categoria(id_Categoria),
id_marca int references Marca(id_Marca),
Stock int not null default 0,
FechaVencimiento date default '2000/01/01',
Estado bit,
FechaRegistro datetime default getdate()
)
go

----TABLA COMPRA---
Create Table Compra(
id_Compra int identity(1,1) primary key,
id_usuario int references Usuario(id_Usuario),
id_proveedor int references Proveedor(id_Proveedor),
TipoDocumento nvarchar(20),
NumeroDocumento nvarchar(20),
Sub_Total decimal(18,2),
FechaRegistro datetime default getdate()
)
go

---TABLA DETALLE DE COMPRA---
Create Table Detalle_Compra(
id_DetalleCompra int identity(1,1) primary key,
id_compra int references Compra(id_Compra),
id_producto int references Producto(id_Producto),
PrecioCompra decimal(18,2) default 0,
PrecioVenta decimal(18,2) default 0,
FechaVencimiento date default '2000/01/01',
Cantidad int not null,
Total decimal (18,2),
FechaRegistro datetime default getdate()
)
go

----TABLA VENTA----
Create Table Venta(
id_Venta int identity(1,1)primary key,
id_usuario int references Usuario(id_Usuario),
TipoDocumento nvarchar(20),
NumeroDocumento nvarchar(20),
id_cliente int references Cliente(id_Cliente),
MontoPago decimal(18,2),
MontoCambio decimal(18,2),
MontoTotal decimal(18,2),
FechaRegistro datetime default getdate()
)
go

---TABLA DETALLE DE VENTA----
Create Table Detalle_Venta(
id_DetalleVenta int identity(1,1) primary key,
id_venta int references Venta(id_Venta),
id_producto int references Producto(id_Producto),
PrecioVenta decimal(18,2),
Cantidad int not null,
Sub_Total decimal(18,2),
FechaRegistro datetime default getdate()
)
go

--TABLA DE DATOS DEL NEGOCIO--
Create Table Negocio(
id_Negocio int primary key,
Nombre varchar(80) not null,
RUC varchar(80)not null,
Direccion varchar(80)not null,
Logo varbinary(max) null
)
insert into Negocio (id_Negocio,Nombre,Ruc,Direccion)values(1,'Surtidora Alejandra','J0310000203288','Oficinas E.Chamorro 3 Cuadras al Norte')

select * From Negocio




