namespace API_Rdv.Models
{
    public class SalleModel
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public int Capacite { get; set; }
        public List<CreneauModel> Creneaux { get; set; } = new();
    }

    public class CreneauModel
    {
        public int Id { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public bool EstReserve { get; set; }
    }
}
