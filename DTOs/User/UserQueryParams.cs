namespace SkillSwapAPI.Helpers
{
    public class UserQueryParams
    {
        public string? SearchTerm { get; set; }  // Término de búsqueda (por nombre o correo)
        public string OrderBy { get; set; } = "name";  // Campo para ordenar (por nombre, correo)
        public bool AscendingOrder { get; set; } = true;  // Indica si es ascendente o descendente
        public int PageNumber { get; set; } = 1;  // Número de página para paginación
        public int PageSize { get; set; } = 10;  // Tamaño de página para paginación
    }
}
