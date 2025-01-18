namespace MKT.EventoLead.Domain.Entities
{
    public class Lead
    {
        public int Id { get; set; }

        public string JsonLead { get; set; } = string.Empty;

        public int IdCampanha { get; set; }
    }
}
