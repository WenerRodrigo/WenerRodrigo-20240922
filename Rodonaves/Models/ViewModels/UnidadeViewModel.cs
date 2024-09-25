namespace Rodonaves.Models.ViewModels
{
    public class UnidadeViewModel
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Status { get; set; }
        public List<Colaboradores>? Colaboradores { get; set; }
    }
}
