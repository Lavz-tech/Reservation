namespace API_Rdv.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int SalleId { get; set; }
        public int CreneauId { get; set; }
        public int UserId { get; set; }
        public DateTime DateReservation { get; set; } = DateTime.UtcNow;
    }
}
