USE Prueba

--El total de ventas de los �ltimos 30 d�as (monto total y cantidad total de ventas).

SELECT  
Venta.Fecha,
Venta.Total,
VentaDetalle.TotalLinea
FROM Venta
INNER JOIN VentaDetalle
ON Venta.ID_Venta = VentaDetalle.ID_Venta
WHERE CAST(Venta.Fecha as date) between cast( dateadd (day,-30,getdate()) as date)   
                               and  cast (getdate() as date)

--El d�a y hora en que se realiz� la venta con el monto m�s alto (y cu�l es aquel monto).

SELECT 
Venta.Fecha,
Venta.Total,
VentaDetalle.TotalLinea
FROM Venta
INNER JOIN VentaDetalle
ON Venta.ID_Venta = VentaDetalle.ID_Venta
WHERE CAST(Venta.Fecha as date) between cast( dateadd (day,-30,getdate()) as date)   
                               and  cast (getdate() as date)
order by Total desc

--Indicar cu�l es el producto con mayor monto total de ventas.

SELECT  
Venta.ID_Venta,
Venta.Fecha,
Venta.Total,
VentaDetalle.TotalLinea,
VentaDetalle.ID_Producto,
Producto.Nombre
FROM Venta
INNER JOIN VentaDetalle
ON Venta.ID_Venta = VentaDetalle.ID_Venta
INNER JOIN Producto
ON Producto.ID_Producto = VentaDetalle.ID_Producto
WHERE CAST(Venta.Fecha as date) between cast( dateadd (day,-30,getdate()) as date)   
                               and  cast (getdate() as date)
order by TotalLinea desc


--Indicar el local con mayor monto de ventas.

SELECT  
Venta.ID_Venta,
Venta.Fecha,
Venta.Total,
VentaDetalle.TotalLinea,
Venta.ID_Local,
Local.Nombre
FROM Venta
INNER JOIN VentaDetalle
ON Venta.ID_Venta = VentaDetalle.ID_Venta
INNER JOIN Producto
ON Producto.ID_Producto = VentaDetalle.ID_Producto
INNER JOIN Local
ON Venta.ID_Local = Local.ID_Local
WHERE CAST(Venta.Fecha as date) between cast( dateadd (day,-30,getdate()) as date)   
                               and  cast (getdate() as date)
order by Total desc

--�Cu�l es la marca con mayor margen de ganancias?

SELECT  
Venta.ID_Venta,
Venta.Fecha,
Venta.Total,
VentaDetalle.TotalLinea,
Marca.ID_Marca,
Marca.Nombre
FROM Venta
INNER JOIN VentaDetalle
ON Venta.ID_Venta = VentaDetalle.ID_Venta
INNER JOIN Producto
ON Producto.ID_Producto = VentaDetalle.ID_Producto
INNER JOIN Marca
ON Marca.ID_Marca = Producto.ID_Marca
INNER JOIN Local
ON Venta.ID_Local = Local.ID_Local
WHERE CAST(Venta.Fecha as date) between cast( dateadd (day,-30,getdate()) as date)   
                               and  cast (getdate() as date)
order by Total desc

--�C�mo obtendr�as cu�l es el producto que m�s se vende en cada local?

SELECT  
Venta.ID_Venta,
Venta.Fecha,
Venta.Total,
VentaDetalle.TotalLinea,
Venta.ID_Local,
Local.Nombre,
VentaDetalle.ID_Producto,
Producto.Nombre,
Marca.ID_Marca,
Marca.Nombre
FROM Venta
INNER JOIN VentaDetalle
ON Venta.ID_Venta = VentaDetalle.ID_Venta
INNER JOIN Producto
ON Producto.ID_Producto = VentaDetalle.ID_Producto
INNER JOIN Marca
ON Marca.ID_Marca = Producto.ID_Marca
INNER JOIN Local
ON Venta.ID_Local = Local.ID_Local
WHERE CAST(Venta.Fecha as date) between cast( dateadd (day,-30,getdate()) as date)   
                               and  cast (getdate() as date)
order by Total desc