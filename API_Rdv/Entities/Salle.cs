namespace API_Rdv.Entities
{
    public class Salle
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public int Capacite { get; set; }
        public List<Creneau> Creneaux { get; set; } = new();
    }
}
