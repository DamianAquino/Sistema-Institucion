namespace API_Institucion.Interfaces
{
    public class MateriaDto
    {
        public string Nombre;
        public int CarreraId;

        public MateriaDto(string nombre, int carreraId)
        {
            Nombre = nombre;
            CarreraId = carreraId;
        }
    }
}
