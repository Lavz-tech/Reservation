namespace API_Rdv.Entities
{
    public class Creneau
    {
        public int Id { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public bool EstReserve { get; set; } = false;
        public int SalleId { get; set; }
    }
}
