namespace TEST_Institucion
{
    internal class Carrera
    {
        public string Nombre;
        public string Director;
        private string Comisiones;
        public Carrera(string nombre, string director, string comisiones)
        {
            Nombre = nombre;
            Director = director;
            Comisiones = comisiones;
        }
    }
}
