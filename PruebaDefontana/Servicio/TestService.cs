using PruebaDefontana.Interfaz;
using PruebaDefontana.Modelo;
using System.Data.SqlClient;

namespace PruebaDefontana.Servicio
{
    public class TestService : ITest
    {
        public async Task<object> consultaDetalleDeVentas (int days)
        {
            try 
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "lab-defontana.caporvnn6sbh.us-east-1.rds.amazonaws.com";
                builder.InitialCatalog = "Prueba";
                builder.UserID = "ReadOnly";
                builder.Password = "d*3PSf2MmRX9vJtA5sgwSphCVQ26*T53uU";

                using (SqlConnection sqlConnection = new SqlConnection(builder.ConnectionString))
                {
                    await sqlConnection.OpenAsync();

                    QuestModel Quest = new QuestModel();
                    ResModel Rest = new ResModel();
                    List<ListSQL> list = new List<ListSQL>();

                    string sql =
                        "SELECT " +
                        " Venta.ID_Venta," +
                        " Venta.Fecha," +
                        " Venta.Total," +
                        " VentaDetalle.TotalLinea," +
                        " Venta.ID_Local," +
                        " Local.Nombre," +
                        " VentaDetalle.ID_Producto," +
                        " Producto.Nombre," +
                        " Marca.ID_Marca," +
                        " Marca.Nombre" +
                        " FROM Venta" +
                        " INNER JOIN VentaDetalle" +
                        " ON Venta.ID_Venta = VentaDetalle.ID_Venta" +
                        " INNER JOIN Producto" +
                        " ON Producto.ID_Producto = VentaDetalle.ID_Producto" +
                        " INNER JOIN Marca" +
                        " ON Marca.ID_Marca = Producto.ID_Marca" +
                        " INNER JOIN Local" +
                        " ON Venta.ID_Local = Local.ID_Local" +
                        " WHERE CAST(Venta.Fecha as date) between cast( dateadd (day,-" + days + ",getdate()) as date)" +
                        " and  cast (getdate() as date)";

                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader sqldr = await cmd.ExecuteReaderAsync();

                    if(sqldr.HasRows)
                    {
                        while(await sqldr.ReadAsync())
                        {
                            ListSQL db = new ListSQL();
                            //El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).
                            db.TotalLinea = Convert.ToInt32(sqldr["TotalLinea"]);
                            db.Total = Convert.ToInt32(sqldr["Total"]);
                            db.Fecha = (DateTime)sqldr["Fecha"];
                            db.IdProducto = sqldr["ID_Producto"].ToString();
                            db.NombreProducto = sqldr["Nombre"].ToString();
                            db.IdLocal = sqldr["ID_Local"].ToString();
                            list.Add(db);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    //Cantidad de dias solicitados
                    Quest.CantidadDias = days +" Días";
                    //El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).
                    var total30 = list.Sum(x => x.Total);
                    var totalLinea = list.Sum(x => x.TotalLinea);
                    Rest.Res1 = "Monto total ventas: "+"$"+total30.ToString() +" - "+"Cantidad total de ventas: "+ totalLinea.ToString();
                    //El día y hora en que se realizó la venta con el monto más alto(y cuál es aquel monto).

                    var query = from x in list
                                where x.Total == list.Max(x => x.Total)
                                select x;

                    var ventaMasAlta = "";
                    var Fecha = "";

                    foreach (var x in query ) 
                    {
                         ventaMasAlta = x.Total.ToString();
                         Fecha = x.Fecha.ToString();
                    }

                    Rest.Res2 = "El día y hora en que se realizó la venta fue: "+ Fecha+ " Con el monto más alto: " + ventaMasAlta;

                    //Indicar cuál es el producto con mayor monto total de ventas

                    var query2 = from x in list
                                 where x.TotalLinea == list.Max(x => x.TotalLinea)
                                 select x;

                    var NombreProducto = "";
                    var TotalLinea = "";
                    var IdProducto = "";
                    foreach (var x in query2)
                    {
                        NombreProducto = x.NombreProducto.ToString();
                        TotalLinea = x.TotalLinea.ToString();
                        IdProducto = x.IdProducto.ToString();
                    }

                    Rest.Res3 = "Producto con mayor monto total de ventas: " + "ID:" +IdProducto +" "+NombreProducto+" "+ TotalLinea;

                    //Indicar el local con mayor monto de ventas

                    var query3 = from x in list
                                 where x.Total == list.Max(x => x.Total) 
                                 select x;

                    var NombreMarca = "";
                    var Total = "";
                    var IdLocal = "";
                    foreach (var x in query3)
                    {
                        NombreMarca = x.NombreProducto.ToString();
                        Total = x.Total.ToString();
                        IdLocal = x.IdProducto.ToString();
                    }

                    Rest.Res4 = "Local con mayor monto de ventas: " + "ID:" + IdLocal + " " + NombreMarca + " " + Total;

                    //¿Cuál es la marca con mayor margen de ganancias?

                    var query4 = from x in list
                                 where x.Total == list.Max(x => x.Total)
                                 select x;

                    var NombreMarca1 = "";
                    var IdLocal1 = "";
                    foreach (var x in query4)
                    {
                        NombreMarca1 = x.NombreProducto.ToString();
                        IdLocal1 = x.IdProducto.ToString();
                    }

                    Rest.Res5 = "Marca con mayor margen de ganancias: " + "ID:"+ IdLocal1 + " " + NombreMarca1;

                    //¿Cómo obtendrías cuál es el producto que más se vende en cada local?

                    var query5 = from x in list
                                 group x by x.IdProducto into g
                                 orderby g.Count() descending
                                 select new
                                 {
                                     IdProducto = g.Key
                                 };

                    var IDPRoducto = "";
                    var NombreProducto1 = "";

                    foreach (var x in query4)
                    {
                        NombreProducto1 = x.NombreProducto.ToString();
                        IDPRoducto = x.IdProducto.ToString();
                    }

                    Rest.Res6 = "Producto que más se vende: " + "ID:" + IDPRoducto + " " + NombreProducto1;

                    Quest.Question1 = Rest.Res1;
                    Quest.Question2 = Rest.Res2;
                    Quest.Question3 = Rest.Res3;
                    Quest.Question4 = Rest.Res4;
                    Quest.Question5 = Rest.Res5;
                    Quest.Question6 = Rest.Res6;
                    await sqldr.CloseAsync();
                    return Quest;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
