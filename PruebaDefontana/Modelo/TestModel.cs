namespace PruebaDefontana.Modelo
{
    public class QuestModel
    {
        public object CantidadDias { get; set; }
        public object Question1 { get; set; }
        public object Question2 { get; set; }
        public object Question3 { get; set; }
        public object Question4 { get; set; }
        public object Question5 { get; set; }
        public object Question6 { get; set; }
    }
    public class ResModel
    {
        public string Res1 { get; set; }
        public string Res2 { get; set; }
        public string Res3 { get; set; }
        public string Res4 { get; set; }
        public string Res5 { get; set; }
        public string Res6 { get; set; }
    }
    public class ListSQL
    {
        public int Total { get; set; }
        public int TotalLinea { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreProducto { get; set; }
        public string IdProducto { get; set; }
        public string IdLocal { get; set; }
    }
}
