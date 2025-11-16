public class Institucion
{
    public string Nombre { get; private set; }
    public string CUIT { get; private set; }
    public string Direccion { get; private set; }
    private readonly List<string> Carreras;

    public Institucion(string cuit, string nombre, string direccion, List<string> carreras)
    {
        CUIT = cuit;
        Nombre = nombre;
        Direccion = direccion;
        Carreras = carreras;
    }   
}
