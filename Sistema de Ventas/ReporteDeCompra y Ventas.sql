
use Sistema_SurtidoraAlejandra 
go

select 
convert(char(10),c.FechaRegistro,103)[FechaCreacion],c.TipoDocumento,c.NumeroDocumento,c.Sub_Total,
u.Nombre1[NombreUsuario],
pr.Documento[DocumentoProveedor],pr.RazonSocial,
p.Codigo[CodigoProducto],p.Nombre_Producto[NombreProducto],mc.Nombre[Marca],ca.Nombre[Categoria],p.FechaVencimiento[FechaVencimiento],dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.Total[Total]
from Compra c
inner join Usuario u on u.id_Usuario=c.id_usuario
inner join Proveedor pr on pr.id_Proveedor=c.id_proveedor
inner join Detalle_Compra dc on dc.id_compra=c.id_Compra
inner Join Producto p on p.id_Producto=dc.id_producto
inner join Categoria ca on ca.id_Categoria=p.id_categoria
inner join Marca mc on p.id_marca=mc.id_Marca
where convert(date,c.FechaRegistro) between '2023/10/12' and '2023/10/15'
and pr.id_Proveedor= iif(0=0, pr.id_Proveedor,3)
go

----Procedimiento para generar el reporte de Compra
create or alter procedure ReporteCompras(
@fechaCreacion varchar(10),
@fechaFin varchar(10),
@idproveedor int
)
as
	begin
	set dateformat dmy;
	select 
convert(char(10),c.FechaRegistro,103)[FechaCreacion],c.TipoDocumento,c.NumeroDocumento,c.Sub_Total,
u.Nombre1[NombreUsuario],
pr.Documento[DocumentoProveedor],pr.RazonSocial,
p.Codigo[CodigoProducto],p.Nombre_Producto[NombreProducto],mc.Nombre[Marca],ca.Nombre[Categoria],p.FechaVencimiento[FechaVencimiento],dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.Total[Total]
from Compra c
inner join Usuario u on u.id_Usuario=c.id_usuario
inner join Proveedor pr on pr.id_Proveedor=c.id_proveedor
inner join Detalle_Compra dc on dc.id_compra=c.id_Compra
inner Join Producto p on p.id_Producto=dc.id_producto
inner join Categoria ca on ca.id_Categoria=p.id_categoria
inner join Marca mc on p.id_marca=mc.id_Marca
where convert(date,c.FechaRegistro) between @fechaCreacion and @fechaFin
and pr.id_Proveedor= iif(@idproveedor=0,pr.id_Proveedor,@idproveedor)
end
go
---Procedimiento almacenado para generar el reporte de Venta---
create or alter procedure ReporteVentas(
@fechaCreacion varchar(10),
@fechaFin varchar(10),
@idcliente int
)
as
	begin
	set dateformat dmy;
	select 
convert(char(10),v.FechaRegistro,103)[FechaCreacion],v.TipoDocumento,v.NumeroDocumento,v.MontoTotal,
u.Nombre1[NombreUsuario],
cl.Cedula[Cedula],cl.Nombre1[NombreCliente],cl.Apellido1[ApellidoCliente],
p.Codigo[CodigoProducto],p.Nombre_Producto[NombreProducto],mc.Nombre[Marca],ca.Nombre[Categoria],p.FechaVencimiento[FechaVencimiento],dv.PrecioVenta,dv.Cantidad,dv.Sub_Total[Total]
from Venta v
inner join Usuario u on u.id_Usuario=v.id_usuario
inner join Cliente cl on cl.id_Cliente=v.id_cliente
inner join Detalle_Venta dv on dv.id_venta=v.id_Venta
inner Join Producto p on p.id_Producto=dv.id_producto
inner join Categoria ca on ca.id_Categoria=p.id_categoria
inner join Marca mc on p.id_marca=mc.id_Marca
where convert(date,v.FechaRegistro) between @fechaCreacion and @fechaFin
and cl.id_Cliente= iif(@idcliente=0,cl.id_Cliente,@idcliente)
end
