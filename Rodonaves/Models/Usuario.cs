namespace Rodonaves.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Status { get; set; }

    }
}
