use Sistema_SurtidoraAlejandra


---Procesos de abastecimiento de productos en a travez de las compras ---
--esta es una tabla temporal como un tipo de parametro para procedimiento almacenado----
 create type [dbo].[eDetalleCompr]as table(
 [id_Producto]int  null,
 [PrecioCompra]decimal(10,2) null,
 [PrecioVenta]decimal(10,2)null,
 [FechaVencimiento]date,
 [Cantidad]int null,
 [Total]decimal(10,2)null
 )
 go

 ---´Procediiento para resgstrar una compra y a la vez su detalle como lista de parametros
 --cabe recalacar que estamos usando la funcion transacition por medio de transaciones ----
 create or alter procedure RegistrarCompraa(
 @idusuario int,
 @idproveed int,
 @TipoDeDocumento nvarchar(max),
 @NumeroDocumento nvarchar(max),
 @MontoTotal decimal(10,2),
 @DetalleCompra [eDetalleCompr] readonly,
 @Resultado bit output,
 @Mensaje varchar(500) output
 )
 as
 begin
   begin try
		declare @id_Compra int=0
		set @Resultado=1
		set @Mensaje=''

		begin transaction registro
				insert into Compra(id_usuario,id_Proveedor,TipoDocumento,NumeroDocumento,Sub_Total)
				values(@idusuario,@idproveed,@TipoDeDocumento,@NumeroDocumento,@MontoTotal)

				set @id_Compra= SCOPE_IDENTITY()

				insert into Detalle_Compra(id_compra,id_producto,PrecioCompra,PrecioVenta,FechaVencimiento,Cantidad,Total)
				select @id_Compra,id_Producto,PrecioCompra,PrecioVenta,FechaVencimiento,Cantidad,Total from @DetalleCompra

				update p set p.Stock=p.Stock+dc.Cantidad,
				p.PrecioCompra=dc.PrecioCompra,
				p.PrecioVenta=dc.PrecioVenta,
				p.FechaVencimiento=dc.FechaVencimiento
				from Producto p
				inner join @DetalleCompra dc on dc.id_Producto=p.id_Producto
		commit transaction registro
   end try
   begin catch
   set @Resultado=0
   set @Mensaje=ERROR_MESSAGE() 
   rollback transaction registro
   end catch
 end
 go

 ----Para el correlativo de la Compra---
 select count(*)+1 from Compra
 -----Proceso para Obtener el detalle de la compra----
 select Nombre_Producto,Detalle_Compra.PrecioCompra,Cantidad,Total
 from Detalle_Compra
 inner join Producto on Detalle_Compra.id_producto=Producto.id_Producto
 where id_compra=2
----Proceso para obtener compra
 Select c.id_Compra,
 u.Nombre1,
 pr.Documento,pr.RazonSocial,
 c.TipoDocumento,c.NumeroDocumento,c.Sub_Total,convert(char(10),c.FechaRegistro,103)[FechaCreacion]
 from Compra c
 inner join Usuario u on u.id_Usuario=c.id_usuario
 inner join Proveedor pr on pr.id_Proveedor=c.id_Proveedor
 where c.NumeroDocumento='0002'

 ---Proceso para obtner venta de venta---
 select v.id_Venta,
 u.Nombre1,c.Cedula,c.Nombre1 as NombreCliente,
 v.TipoDocumento,v.NumeroDocumento,
 v.MontoPago,v.MontoCambio,
 v.MontoTotal,convert(char(10),v.FechaRegistro,103)[FechaCreacion]
 from Venta v
 inner join Usuario u on u.id_Usuario=v.id_usuario
 inner join Cliente c on c.id_Cliente=v.id_cliente
 where NumeroDocumento='0001'
 ----proceso para obtener el detalle de venta---
 Select p.Nombre_Producto,dv.PrecioVenta,dv.Cantidad,dv.Sub_Total
 from Detalle_Venta dv
 inner Join Producto p on p.id_Producto=dv.id_producto
 where dv.id_venta=1





 ----Proceso de Ventas con tabla temporal todo esto para registrar la venta---


 --create or alter procedure RegistrarVenta(
 --@idusuario int,
 --@TipoDeDocumento nvarchar(max),
 --@NumeroDocumento nvarchar(max),
 --@idcliente int,
 --@MontoPago decimal(10,2),
 --@Montocambio decimal(10,2),
 --@MontoTotal decimal(10,2),
 --@DetalleVenta [eDetalleVenta] readonly,
 --@Resultado bit output,
 --@Mensaje varchar(500) output
 --)
 --as
 --begin
 --  begin try
	--	declare @idVenta int=0
	--	set @Resultado=1
	--	set @Mensaje=''

	--	begin transaction registro
	--			insert into Venta(id_usuario,TipoDocumento,NumeroDocumento,id_cliente,MontoPago,MontoPago,MontoTotal)
	--			values(@idusuario,@TipoDeDocumento,@NumeroDocumento,@idcliente,@MontoPago,@MontoPago,@MontoTotal)

	--			set @idVenta= SCOPE_IDENTITY()

	--			insert into Detalle_Venta(id_venta,id_producto,PrecioVenta,Cantidad,Sub_Total)
	--			select @idVenta,id_producto,PrecioVenta,Cantidad,Sub_Total from @DetalleVenta

	--	commit transaction registro
 --  end try
 --  begin catch
 --  set @Resultado=0
 --  set @Mensaje=ERROR_MESSAGE() 
 --  rollback transaction registro
 --  end catch
 --end
 --go

 
  create type [dbo].[eDetalleVenta]as table(
 [id_producto]int  null,
 [PrecioVenta]decimal(10,2)null,
 [Cantidad]int null,
 [Sub_Total]decimal(10,2)null
 )
 go

 CREATE OR ALTER PROCEDURE RegistrarVenta(
    @idusuario INT,
    @TipoDeDocumento NVARCHAR(MAX),
    @NumeroDocumento NVARCHAR(MAX),
    @idcliente INT,
    @MontoPago DECIMAL(10, 2),
    @Montocambio DECIMAL(10, 2),
    @MontoTotal DECIMAL(10, 2),
    @DetalleVenta [eDetalleVenta] READONLY,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        DECLARE @idVenta INT = 0
        SET @Resultado = 1
        SET @Mensaje = ''

        -- Validar la fecha de vencimiento
        IF not EXISTS (
                SELECT 1
                FROM @DetalleVenta dv
                JOIN Producto p ON dv.id_producto = p.id_Producto
                WHERE p.FechaVencimiento < GETDATE()
            )
         BEGIN TRANSACTION registro
        INSERT INTO Venta (id_usuario, TipoDocumento, NumeroDocumento, id_cliente, MontoPago, Montocambio, MontoTotal)
        VALUES (@idusuario, @TipoDeDocumento, @NumeroDocumento, @idcliente, @MontoPago, @Montocambio, @MontoTotal)

            SET @idVenta = SCOPE_IDENTITY()

			INSERT INTO Detalle_Venta (id_venta, id_producto, PrecioVenta, Cantidad, Sub_Total)
          SELECT @idVenta, id_producto, PrecioVenta, Cantidad, Sub_Total FROM @DetalleVenta

        COMMIT TRANSACTION registro
      END TRY
      BEGIN CATCH
        SET @Resultado = 0
         SET @Mensaje = 'No se puede realizar la venta. La fecha de vencimiento de al menos un producto es mayor a la fecha actual.'
        ROLLBACK TRANSACTION registro
    END CATCH
END

go

CREATE or alter VIEW VistaVentaDetalle AS
SELECT
	 C.NumeroDocumento AS NumeroDocumento,
	 C.TipoDocumento AS TipoDeDocumento,
	 p.Cedula as Cedula,
     P.Nombre1 AS NombreCliente,
     U.Nombre1 AS NombreDelFacturador,
	 PC.Nombre_Producto AS NombreProducto,
	 DC.PrecioVenta as Precio,
	 DC.Cantidad as Cantidad,
     C.MontoTotal AS MontoTotal,
	 C.MontoPago as MontoPago,
	 C.MontoCambio as Cambio
FROM
    Venta C
JOIN
    Usuario U ON C.id_usuario = U.id_Usuario
JOIN
    Cliente P ON C.id_cliente = P.id_Cliente
JOIN
    Detalle_Venta DC ON C.id_Venta = DC.id_venta
JOIN
    Producto PC ON DC.id_producto = PC.id_Producto
WHERE
    C.NumeroDocumento =NumeroDocumento
go

select * from VistaVentaDetalle
where NumeroDocumento='0001'